using Business.Abstract;
using Business.BusinessAcpects;
using Business.Const;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Performance;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.Dtos;
using ExcelDataReader;
using System.Text;

namespace Business.Concrete
{
    public class AccountReconciliationDetailManager : IAccountReconciliationDetailService
    {
        private readonly IAccountReconciliationDetailDal accountReconciliationDetailDal;
        private readonly IAccountReconciliationService accountReconciliationService;
        public AccountReconciliationDetailManager(IAccountReconciliationDetailDal accountReconciliationDetailDal,
            IAccountReconciliationService accountReconciliationService)
        {
            this.accountReconciliationDetailDal = accountReconciliationDetailDal;
            this.accountReconciliationService = accountReconciliationService;
        }
        


        
        [PerformanceAspect(3)]
        [SecuredOperation("admin")]
        [CacheAspect(30)]
        public IDataResult<List<AccountReconciliationDetail>> GetAll(int accountReconciliationId)
        {
            return new SuccessDataResult<List<AccountReconciliationDetail>>
                (accountReconciliationDetailDal.GetAll(x => x.AccountReconciliationId == accountReconciliationId),
                Messages.AccountReconciliationDetailsHasBeenBrought);
        }
        
        [PerformanceAspect(3)]
        [SecuredOperation("admin")]
        [CacheAspect(30)]
        public IDataResult<AccountReconciliationDetail> GetById(int id)
        {
            return new SuccessDataResult<AccountReconciliationDetail>
                    (accountReconciliationDetailDal.Get(x => x.Id == id), Messages.AccountReconciliationDetailHasBeenBrought);
        }




        [PerformanceAspect(3)]
        [SecuredOperation("admin")]
        [CacheRemoveAspect("IAccountReconciliationDetailService.Get")]
        public IResult Add(AccountReconciliationDetail entity)
        {
            var result = accountReconciliationService.GetById(entity.AccountReconciliationId);
            entity.CurrencyId = result.Data.CurrencyId;
            entity.CurrencyCredit = result.Data.CurrencyCredit;
            entity.CurrencyDebit = result.Data.CurrencyDebit;

            accountReconciliationDetailDal.Add(entity);
            return new SuccessResult(Messages.AccountReconciliationDetailAdded);
        }

        [PerformanceAspect(3)]
        [SecuredOperation("admin")]
        [CacheRemoveAspect("IAccountReconciliationDetailService.Get")]
        public IResult Delete(AccountReconciliationDetail entity)
        {
            accountReconciliationDetailDal.Delete(entity);
            return new SuccessResult(Messages.AccountReconciliationDetailDeleted);
        }
        
        [PerformanceAspect(3)]
        [SecuredOperation("admin")]
        [CacheRemoveAspect("IAccountReconciliationDetailService.Get")]
        public IResult Update(AccountReconciliationDetail entity)
        {
            accountReconciliationDetailDal.Update(entity);
            return new SuccessResult(Messages.AccountReconciliationDetailUpdated);
        }



        [PerformanceAspect(3)]
      //  [SecuredOperation("admin")]
        [CacheRemoveAspect("IAccountReconciliationDetailService.Get")]
        public IResult AddByExcel(AccountReconciliationDetailDto dto)
        {

            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            using (var stream = File.Open(dto.filePath, FileMode.Open, FileAccess.Read))
            {
                using (var reader = ExcelReaderFactory.CreateReader(stream))
                {
                    while (reader.Read())
                    {

                        var date = reader.GetValue(0) != null ? reader.GetValue(0).ToString() : null;
                        // String description = reader.GetValue(1) != null ? reader.GetValue(0).ToString() : null;

                        if (date is null) break;
                        if (date == "Tarih") continue;

                        // DateTime date = Convert.ToDateTime(reader.GetValue(0));
                        string description = reader.GetString(1);
                        int currencyId = Convert.ToInt32(reader.GetValue(2));
                        decimal debit = Convert.ToDecimal(reader.GetValue(3));
                        decimal credit = Convert.ToDecimal(reader.GetValue(4));


                        if (date != "Tarih") // ilk satırı okumaması için böyle yaptım
                        {
                            
                            AccountReconciliationDetail accountReconciliation = new AccountReconciliationDetail
                            {
                                AccountReconciliationId = dto.AccountReconciliationId,
                                Date = Convert.ToDateTime(date),
                                CurrencyId = currencyId,
                                CurrencyDebit = debit,
                                CurrencyCredit = credit,
                                Description = description
                            };

                            accountReconciliationDetailDal.Add(accountReconciliation);
                        }
                    }
                }
            }
            File.Delete(dto.filePath);
            return new SuccessResult(Messages.AccountReconciliationDetailsAdded);
        }

    }
}
