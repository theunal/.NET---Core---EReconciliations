using Business.Abstract;
using Business.BusinessAcpects;
using Business.Const;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Performance;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.Dtos;
using Entities.Dtos.Excel;
using ExcelDataReader;
using System.Text;

namespace Business.Concrete
{
    public class BaBsReconciliationManager : IBaBsReconciliationService
    {
        private readonly IBaBsReconciliationDal baBsReconciliationDal;
        private readonly ICurrencyAccountService currencyAccountService;
        private readonly IMailService mailService;
        private readonly IMailTemplateService mailTemplateService;
        private readonly IMailParameterService mailParameterService;

        public BaBsReconciliationManager(IBaBsReconciliationDal baBsReconciliationDal,
            ICurrencyAccountService currencyAccountService, IMailService mailService, 
            IMailTemplateService mailTemplateService, IMailParameterService mailParameterService)
        {
            this.baBsReconciliationDal = baBsReconciliationDal;
            this.currencyAccountService = currencyAccountService;
            this.mailService = mailService;
            this.mailTemplateService = mailTemplateService;
            this.mailParameterService = mailParameterService;
        }






        
        [PerformanceAspect(3)]
        [SecuredOperation("admin")]
        [CacheAspect(30)]
        public IDataResult<List<BaBsReconciliation>> GetAll(int companyId)
        {
            return new SuccessDataResult<List<BaBsReconciliation>>
                (baBsReconciliationDal.GetAll(x => x.CompanyId == companyId),
                Messages.BaBsReconciliationsHasBeenBrought);
        }


        [PerformanceAspect(3)]
        [SecuredOperation("admin")]
        [CacheAspect(30)]
        public IDataResult<BaBsReconciliation> GetById(int id)
        {
            var result = baBsReconciliationDal.Get(x => x.Id == id);
            if (result is not null)
            {
                return new SuccessDataResult<BaBsReconciliation>
                (baBsReconciliationDal.Get(x => x.Id == id), Messages.BaBsReconciliationHasBeenBrought);
            }
            return new ErrorDataResult<BaBsReconciliation>(Messages.BaBsReconciliationNotFound);
        }




        //[PerformanceAspect(3)]
        //[SecuredOperation("admin")]
        //[CacheRemoveAspect("IBaBsReconciliationService.Get")]
        public IResult Add(BaBsReconciliation entity)
        {
            entity.Guid = new Guid().ToString();
            baBsReconciliationDal.Add(entity);
            return new SuccessResult(Messages.BaBsReconciliationAdded);
        }


        [PerformanceAspect(3)]
        [SecuredOperation("admin")]
        [CacheRemoveAspect("IBaBsReconciliationService.Get")]
        public IResult Delete(BaBsReconciliation entity)
        {
            baBsReconciliationDal.Delete(entity);
            return new SuccessResult(Messages.BaBsReconciliationDeleted);
        }


        [PerformanceAspect(3)]
        [SecuredOperation("admin")]
        [CacheRemoveAspect("IBaBsReconciliationService.Get")]
        public IResult Update(BaBsReconciliation entity)
        {
            baBsReconciliationDal.Update(entity);
            return new SuccessResult(Messages.BaBsReconciliationUpdated);
        }






        [PerformanceAspect(3)]
        //[SecuredOperation("admin")]
        [CacheRemoveAspect("IBaBsReconciliationService.Get")]
        public IResult AddByExcel(BaBsReconciliationExcelDto dto)
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            using (var stream = File.Open(dto.FilePath, FileMode.Open, FileAccess.Read))
            {
                using (var reader = ExcelReaderFactory.CreateReader(stream))
                {
                    while (reader.Read())
                    {

                        String code = reader.GetValue(0) != null ? reader.GetValue(0).ToString() : null;

                        if (code is null) break;
                        if (code == "Cari Kodu") continue;

                        string type = reader.GetString(1);
                        int mounth = Convert.ToInt32(reader.GetValue(2));
                        int year = Convert.ToInt32(reader.GetValue(3));
                        int quantity = Convert.ToInt32(reader.GetValue(4));
                        decimal total = Convert.ToDecimal(reader.GetValue(5));

                        if (code != "Cari Kodu") // ilk satırı okumaması için böyle yaptım
                        {
                            var currencyAccountId = currencyAccountService.GetByCompanyIdAndCode(code, dto.CompanyId).Data.Id;
                            var x = currencyAccountId;
                            BaBsReconciliation baBsReconciliation = new BaBsReconciliation
                            {
                                CompanyId = dto.CompanyId,
                                CurrencyAccountId = currencyAccountId,
                                Type = type,
                                Mounth = mounth,
                                Year = year,
                                Quantity = quantity,
                                Total = total,
                                IsSendEmail = true,
                                Guid = Guid.NewGuid().ToString()
                            };

                            baBsReconciliationDal.Add(baBsReconciliation);
                        }
                    }
                }
            }
            File.Delete(dto.FilePath);
            return new SuccessResult(Messages.BaBsReconciliationsAdded);
        }

        public IDataResult<List<BaBsReconciliationDto>> GetAllDto(int companyId)
        {
            return new SuccessDataResult<List<BaBsReconciliationDto>>
              (baBsReconciliationDal.GetAllDto(companyId),
                                    Messages.AccountReconciliationsHasBeenBrought);
        }

        public IDataResult<BaBsReconciliation> GetByCode(string code)
        {
            return new SuccessDataResult<BaBsReconciliation>
            (baBsReconciliationDal.Get(x => x.Guid == code), Messages.AccountReconciliationHasBeenBrought);
        }

        public IResult SendReconciliationMail(BaBsReconciliationDto dto)
        {
            string body = $"Şirket Adımız: {dto.CompanyName} <br /> " +
                $"Şirket Vergi Dairesi: {dto.CompanyTaxDepartment} <br />" +
                $"Şirket Vergi Numarası: {dto.CompanyTaxIdNumber} - {dto.CompanyIdentityNumber} <br /><hr>" +
                $"Sizin Şirket: {dto.AccountName} <br />" +
                $"Sizin Şirket Vergi Dairesi: {dto.AccountTaxDepartment} <br />" +
                $"Sizin Şirket Vergi Numarası: {dto.AccountTaxIdNumber} - {dto.AccountIdentityNumber} <br /><hr>" +
                $"Ay / Yıl: {dto.Mounth} / {dto.Year} <br />" +
                $"Adet: {dto.Quantity} <br />" +
                $"Tutar: {dto.Total} {dto.CurrencyCode} <br />";

            string link = "https://localhost:7154/api/BaBsReconciliations/getByCode?code=" + dto.Guid;
            string linkDescription = "Mutabakatı Cevaplamak için Tıklayın";

            var mailTemplate = mailTemplateService.GetByTemplateName("string", 9028);

            string templateBody = mailTemplate.Data.Value;
            templateBody = templateBody.Replace("{{title}}", "Mutabakat Maili");
            templateBody = templateBody.Replace("{{message}}", body);
            templateBody = templateBody.Replace("{{link}}", link);
            templateBody = templateBody.Replace("{{linkDescription}}", linkDescription);


            var mailPamareter = mailParameterService.Get(9028);
            Entities.Dtos.SendMailDto sendMailDto = new Entities.Dtos.SendMailDto()
            {
                MailParameter = mailPamareter.Data,
                Email = dto.AccountEmail,
                Subject = "Mutabakat Maili",
                Body = templateBody
            };
            mailService.SendMail(sendMailDto);
            return new SuccessResult(Messages.MailSentSuccessfully);
        }
    }
}
