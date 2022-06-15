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
    public class AccountReconciliationManager : IAccountReconciliationService
    {
        private readonly IAccountReconciliationDal accountReconciliationDal;
        private readonly ICurrentAccountService currentAccountService;
        private readonly IMailService mailService;
        private readonly IMailTemplateService mailTemplateService;
        private readonly IMailParameterService mailParameterService;
        public AccountReconciliationManager(IAccountReconciliationDal accountReconciliationDal,
            ICurrentAccountService currentAccountService,
            IMailService mailService, IMailTemplateService mailTemplateService,
            IMailParameterService mailParameterService)
        {
            this.accountReconciliationDal = accountReconciliationDal;
            this.currentAccountService = currentAccountService;
            this.mailService = mailService;
            this.mailTemplateService = mailTemplateService;
            this.mailParameterService = mailParameterService;
        }



        [PerformanceAspect(3)]
        [SecuredOperation("admin,accountReconciliation.getall")]
        [CacheAspect(30)]
        public IDataResult<List<AccountReconciliation>> GetAll(int companyId)
        {
            return new SuccessDataResult<List<AccountReconciliation>>
                (accountReconciliationDal.GetAll(x => x.CompanyId == companyId),
                Messages.AccountReconciliationsHasBeenBrought);
        }

        [PerformanceAspect(3)]
        [SecuredOperation("admin,accountReconciliation.getall")]
        //[CacheAspect(30)]
        public IDataResult<List<AccountReconciliationDto>> GetAllDto(int companyId)
        {
            return new SuccessDataResult<List<AccountReconciliationDto>>
           (accountReconciliationDal.GetAllDto(companyId),
           Messages.AccountReconciliationsHasBeenBrought);
        }




        [PerformanceAspect(3)]
        [SecuredOperation("admin,accountReconciliation.get")]
        //[CacheAspect(30)]
        public IDataResult<AccountReconciliationDto> GetById(int id)
        {
            return new SuccessDataResult<AccountReconciliationDto>
                (accountReconciliationDal.GetById(id), Messages.AccountReconciliationHasBeenBrought);
        }

        [PerformanceAspect(3)]
        public IDataResult<AccountReconciliation> GetByCode(string code)
        {
            return new SuccessDataResult<AccountReconciliation>
                (accountReconciliationDal.Get(x => x.Guid == code), Messages.AccountReconciliationHasBeenBrought);
        }




        //[PerformanceAspect(3)]
        //[SecuredOperation("admin,accountReconciliation.add")]
        //[CacheRemoveAspect("IAccountReconciliationService.Get")]
        public IResult Add(AccountReconciliation entity)
        {
            entity.Guid = Guid.NewGuid().ToString();
            accountReconciliationDal.Add(entity);
            return new SuccessResult(Messages.AccountReconciliationAdded);
        }

        [PerformanceAspect(3)]
        [SecuredOperation("admin,accountReconciliation.delete")]
        [CacheRemoveAspect("IAccountReconciliationService.Get")]
        public IResult Delete(int id)
        {
            var result = accountReconciliationDal.Get(x => x.Id == id);
            accountReconciliationDal.Delete(result);
            return new SuccessResult(Messages.AccountReconciliationDeleted);
        }

        [PerformanceAspect(3)]
        [SecuredOperation("admin,accountReconciliation.update")]
        [CacheRemoveAspect("IAccountReconciliationService.Get")]
        public IResult Update(AccountReconciliation entity, string accountEmail, string code)
        {
            var result = currentAccountService.GetByCompanyIdAndCode(code, entity.CompanyId).Data;
            result.Email = accountEmail;
            currentAccountService.Update(result);

            accountReconciliationDal.Update(entity);
            return new SuccessResult(Messages.AccountReconciliationUpdated);
        }








      //  [PerformanceAspect(3)]
       // [SecuredOperation("admin")]
        [CacheRemoveAspect("IAccountReconciliationService.Get")]
        public IResult AddByExcel(AccountReconciliationExcelDto dto)
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

                        DateTime startingDate = Convert.ToDateTime(reader.GetValue(1));
                        DateTime endingDate = Convert.ToDateTime(reader.GetValue(2));
                        int currencyId = Convert.ToInt32(reader.GetValue(3));
                        decimal debit = Convert.ToDecimal(reader.GetValue(4));
                        decimal credit = Convert.ToDecimal(reader.GetValue(5));



                        if (code != "Cari Kodu") // ilk satırı okumaması ociçin böyle yaptım
                        {
                            var currentAccountId = currentAccountService.GetByCompanyIdAndCode(code, dto.CompanyId).Data.Id;


                            AccountReconciliation accountReconciliation = new AccountReconciliation()
                            {
                                CompanyId = dto.CompanyId,
                                CurrentAccountId = currentAccountId,
                                CurrencyCredit = credit,
                                CurrencyDebit = debit,
                                CurrencyId = currencyId, // currency id TL USD
                                StartingDate = startingDate,
                                EndingDate = endingDate,
                                IsSendEmail = true,
                                Guid = Guid.NewGuid().ToString()
                            };

                            accountReconciliationDal.Add(accountReconciliation);
                        }
                    }
                }
            }
            File.Delete(dto.FilePath);
            return new SuccessResult(Messages.AccountReconciliationsAdded);
        }


        [PerformanceAspect(3)]
        [SecuredOperation("admin")]
        public IResult SendReconciliationMail(AccountReconciliationDto dto)
        {
            string body = $"Şirket Adımız: {dto.CompanyName} <br /> " +
                $"Şirket Vergi Dairesi: {dto.CompanyTaxDepartment} <br />" +
                $"Şirket Vergi Numarası: {dto.CompanyTaxIdNumber} - {dto.CompanyIdentityNumber} <br /><hr>" +
                $"Sizin Şirket: {dto.AccountName} <br />" +
                $"Sizin Şirket Vergi Dairesi: {dto.AccountTaxDepartment} <br />" +
                $"Sizin Şirket Vergi Numarası: {dto.AccountTaxIdNumber} - {dto.AccountIdentityNumber} <br /><hr>" +
                $"Borç: {dto.CurrencyDebit} {dto.CurrencyCode} <br />" +
                $"Alacak: {dto.CurrencyCredit} {dto.CurrencyCode} <br />";

            string link = "https://localhost:7154/api/AccountReconciliations/getByCode?code=" + dto.Guid;
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

