using Business.Abstract;
using Business.Const;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.Dtos;
using ExcelDataReader;
using System.Text;

namespace Business.Concrete
{
    public class BaBsReconciliationManager : IBaBsReconciliationService
    {
        private readonly IBaBsReconciliationDal baBsReconciliationDal;
        private readonly ICurrencyAccountService currencyAccountService;
        public BaBsReconciliationManager(IBaBsReconciliationDal baBsReconciliationDal,
            ICurrencyAccountService currencyAccountService)
        {
            this.baBsReconciliationDal = baBsReconciliationDal;
            this.currencyAccountService = currencyAccountService;
        }



        public IDataResult<List<BaBsReconciliation>> GetAll(int companyId)
        {
            return new SuccessDataResult<List<BaBsReconciliation>>
                (baBsReconciliationDal.GetAll(x => x.CompanyId == companyId),
                Messages.BaBsReconciliationsHasBeenBrought);
        }
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




        public IResult Add(BaBsReconciliation entity)
        {
            baBsReconciliationDal.Add(entity);
            return new SuccessResult(Messages.BaBsReconciliationAdded);
        }
        
        public IResult Delete(BaBsReconciliation entity)
        {
            baBsReconciliationDal.Delete(entity);
            return new SuccessResult(Messages.BaBsReconciliationDeleted);
        }
        
        public IResult Update(BaBsReconciliation entity)
        {
            baBsReconciliationDal.Update(entity);
            return new SuccessResult(Messages.BaBsReconciliationUpdated);
        }





        public IResult AddByExcel(BaBsReconciliationDto dto)
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            using (var stream = File.Open(dto.filePath, FileMode.Open, FileAccess.Read))
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
                                IsSendEmail = true
                            };

                            baBsReconciliationDal.Add(baBsReconciliation);
                        }
                    }
                }
            }
            File.Delete(dto.filePath);
            return new SuccessResult(Messages.BaBsReconciliationsAdded);
        }


    }
}
