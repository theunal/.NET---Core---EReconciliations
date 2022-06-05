using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Business.Const;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Transaction;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.Dtos;
using ExcelDataReader;
using System.Text;

namespace Business.Concrete
{
    public class AccountReconciliationManager : IAccountReconciliationService
    {
        private readonly IAccountReconciliationDal accountReconciliationDal;
        private readonly ICurrencyAccountService currencyAccountService;
        public AccountReconciliationManager(IAccountReconciliationDal accountReconciliationDal, ICurrencyAccountService currencyAccountService)
        {
            this.accountReconciliationDal = accountReconciliationDal;
            this.currencyAccountService = currencyAccountService;
        }




        [CacheAspect(30)]
        public IDataResult<List<AccountReconciliation>> GetAll(int companyId)
        {
            return new SuccessDataResult<List<AccountReconciliation>>
                (accountReconciliationDal.GetAll(x => x.CompanyId == companyId),
                Messages.AccountReconciliationsHasBeenBrought);
        }
        
        [CacheAspect(30)]
        public IDataResult<AccountReconciliation> GetById(int id)
        {
            return new SuccessDataResult<AccountReconciliation>
                (accountReconciliationDal.Get(x => x.Id == id), Messages.AccountReconciliationHasBeenBrought);
        }


      //  [SecuredOperation("ar.add")]
        [CacheRemoveAspect("IAccountReconciliationService.Get")]
        public IResult Add(AccountReconciliation entity)
        {
            accountReconciliationDal.Add(entity);
            return new SuccessResult(Messages.AccountReconciliationAdded);
        }
        
        [CacheRemoveAspect("IAccountReconciliationService.Get")]        
        public IResult Delete(AccountReconciliation entity)
        {
            accountReconciliationDal.Delete(entity);
            return new SuccessResult(Messages.AccountReconciliationDeleted);
        }
        
        [CacheRemoveAspect("IAccountReconciliationService.Get")]
        public IResult Update(AccountReconciliation entity)
        {
            accountReconciliationDal.Update(entity);
            return new SuccessResult(Messages.AccountReconciliationUpdated);
        }




        [CacheRemoveAspect("IAccountReconciliationService.Get")]
        public IResult AddByExcel(AccountReconciliationDto dto)
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

                        DateTime startingDate = Convert.ToDateTime(reader.GetValue(1));
                        DateTime endingDate = Convert.ToDateTime(reader.GetValue(2));
                        int currencyId = Convert.ToInt32(reader.GetValue(3));
                        decimal debit = Convert.ToDecimal(reader.GetValue(4));
                        decimal credit = Convert.ToDecimal(reader.GetValue(5));


                        if (code != "Cari Kodu") // ilk satırı okumaması için böyle yaptım
                        {
                            var currencyAccountId = currencyAccountService.GetByCompanyIdAndCode(code, dto.CompanyId).Data.Id;
                            var x = currencyAccountId;
                            AccountReconciliation accountReconciliation = new AccountReconciliation
                            {
                                CompanyId = dto.CompanyId,
                                CurrencyAccountId = currencyAccountId,
                                CurrencyCredit = credit,
                                CurrencyDebit = debit,
                                CurrencyId = currencyId, // currency id TL USD
                                StartingDate = startingDate,
                                EndingDate = endingDate,
                                IsSendEmail = true
                            };
                            
                            accountReconciliationDal.Add(accountReconciliation);
                        }
                    }
                }
            }
            File.Delete(dto.filePath);
            return new SuccessResult(Messages.AccountReconciliationsAdded);
        }

    }


}

