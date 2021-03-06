using Business.Abstract;
using Business.BusinessAcpects;
using Business.Const;
using Business.ValidationRules;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Transaction;
using Core.Aspects.Performance;
using Core.Aspects.Validation;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.Dtos.Excel;
using ExcelDataReader;
using System.Text;

namespace Business.Concrete
{
    public class CurrentAccountManager : ICurrentAccountService
    {
        private readonly ICurrentAccountDal currencyAccountDal;
        private readonly ICompanyService companyService;
        public CurrentAccountManager(ICurrentAccountDal currencyAccountDal, ICompanyService companyService)
        {
            this.currencyAccountDal = currencyAccountDal;
            this.companyService = companyService;
        }



        //[PerformanceAspect(3)]
        //[SecuredOperation("admin")]
        //[CacheAspect(30)]
        public IDataResult<List<CurrentAccount>> GetAll(int companyId)
        {
            var result = companyService.GetById(companyId);
            if (result.Success)
            {
                var currencyAccounts = currencyAccountDal.GetAll(c => c.CompanyId == result.Data.Id).ToList();
                return new SuccessDataResult<List<CurrentAccount>>
                    (currencyAccounts, Messages.CurrencyAccountsHasBeenBrought);
            }
            return new ErrorDataResult<List<CurrentAccount>>(result.Message);
        }


        // [PerformanceAspect(3)]
        //[SecuredOperation("admin")]
        //[CacheAspect(30)]
        public IDataResult<CurrentAccount> GetById(int id)
        {
            var result = currencyAccountDal.Get(c => c.Id == id);
            if (result is not null)
            {
                return new SuccessDataResult<CurrentAccount>(result, Messages.CurrencyAccountHasBeenBrought);
            }
            return new ErrorDataResult<CurrentAccount>(Messages.CurrencyAccountNotFound);
        }


        // [PerformanceAspect(3)]
        // [SecuredOperation("admin")]
        //[CacheAspect(30)]
        public IDataResult<CurrentAccount> GetByCompanyIdAndCode(string code, int companyId)
        {
            var result = currencyAccountDal.Get(c => c.CompanyId == companyId && c.Code == code);
            return new SuccessDataResult<CurrentAccount>(result, Messages.CurrencyAccountHasBeenBrought);
            //return new SuccessDataResult<CurrencyAccount>(currencyAccountDal.Get(p => p.CompanyId == companyId));
        }


        [PerformanceAspect(3)]
        [SecuredOperation("admin,accountReconciliation.update")]
        [CacheAspect(30)]
        public IDataResult<CurrentAccount> GetByCode(string code)
        {
            var result = currencyAccountDal.Get(c => c.Code == code);
            if (result is not null)
            {
                return new SuccessDataResult<CurrentAccount>(result, Messages.CurrencyAccountHasBeenBrought);
            }
            return new ErrorDataResult<CurrentAccount>(Messages.CurrencyAccountNotFound);
        }


        [PerformanceAspect(3)]
        [SecuredOperation("admin")]
        [CacheAspect(30)]
        public IDataResult<CurrentAccount> GetByCompanyId(int companyId)
        {
            var result = currencyAccountDal.Get(c => c.CompanyId == companyId);
            if (result is not null)
            {
                return new SuccessDataResult<CurrentAccount>(result, Messages.CurrencyAccountHasBeenBrought);
            }
            return new ErrorDataResult<CurrentAccount>(Messages.CurrencyAccountNotFound);
        }









        //[PerformanceAspect(3)]
        //[SecuredOperation("admin")]
        [CacheRemoveAspect("ICurrencyAccountService.Get")]
        [ValidationAspect(typeof(CurrencyAccountValidator))]
        public IResult Add(CurrentAccount entity)
        {
            var result = companyService.GetById(entity.CompanyId);
            if (result.Success)
            {
                currencyAccountDal.Add(entity);
                return new SuccessResult(Messages.CurrencyAccountAdded);
            }
            return result;
        }


        //[PerformanceAspect(3)]
        //[SecuredOperation("admin")]
        [CacheRemoveAspect("ICurrencyAccountService.Get")]
        public IResult Delete(int id)
        {
            var result = GetById(id);
            if (result.Success)
            {
                if (currencyAccountDal.ReconciliationCheck(id))
                    return new ErrorResult(Messages.CurrentAccountHasAccountReconciliation);

                currencyAccountDal.Delete(result.Data);
                return new SuccessResult(Messages.CurrencyAccountDeleted);
            }
            return GetById(id);
        }


        //[PerformanceAspect(3)]
        // [SecuredOperation("admin")]
        [CacheRemoveAspect("ICurrencyAccountService.Get")]
        [ValidationAspect(typeof(CurrencyAccountValidator))]
        public IResult Update(CurrentAccount entity)
        {
            var result = GetById(entity.Id);
            if (result.Success)
            {
                result.Data.Code = entity.Code;
                result.Data.Name = entity.Name;
                result.Data.Address = entity.Address;
                result.Data.TaxDepartment = entity.TaxDepartment;
                result.Data.TaxIdNumber = entity.TaxIdNumber;
                result.Data.IdentityNumber = entity.IdentityNumber;
                result.Data.Email = entity.Email;
                result.Data.Authorized = entity.Authorized;
                result.Data.AddedAt = entity.AddedAt;
                result.Data.IsActive = entity.IsActive;


                currencyAccountDal.Update(result.Data);
                return new SuccessResult(Messages.CurrencyAccountUpdated);
            }
            return result;
        }










        [PerformanceAspect(3)]
        [SecuredOperation("admin,currentAccount.add")]
        [CacheRemoveAspect("ICurrencyAccountService.Get")]
        [ValidationAspect(typeof(CurrencyAccountValidator))]
        [TransactionScopeAspect]
        public IResult AddByExcel(CurrencyAccountExcelDto dto)
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            using (var stream = File.Open(dto.FilePath, FileMode.Open, FileAccess.Read))
            {
                using (var reader = ExcelReaderFactory.CreateReader(stream))
                {
                    while (reader.Read())
                    {
                        string code = reader.GetValue(0).ToString();
                        string name = reader.GetString(1);
                        string adres = reader.GetString(2);
                        string taxDepartment = reader.GetString(3);
                        //string taxIdNumber = reader.GetString(4);
                        //string identityNumber = reader.GetString(5);
                        string email = reader.GetString(6);
                        string authorized = reader.GetString(7);

                        if (code != "Cari Kodu") // ilk satırı okumaması için böyle yaptım
                        {
                            var x = new Random().Next(1, 100000000);
                            CurrentAccount currencyAccount = new CurrentAccount
                            {

                                Name = name,
                                Address = adres,
                                TaxDepartment = taxDepartment,
                                TaxIdNumber = new Random().Next(1, 100000000).ToString() + new Random().Next(1, 100).ToString(),
                                IdentityNumber = new Random().Next(1, 100000000).ToString() + new Random().Next(1, 1000).ToString(),
                                Email = email,
                                Authorized = authorized,
                                AddedAt = DateTime.Now,
                                Code = code,
                                CompanyId = dto.CompanyId,
                                IsActive = true
                            };
                            currencyAccountDal.Add(currencyAccount);
                        }
                    }
                }
            }
            return new SuccessResult(Messages.CurrencyAccountsAdded);
        }

        public IDataResult<CurrentAccount> GetByEmail(string email)
        {
            var result = currencyAccountDal.Get(c => c.Email == email);
            return new SuccessDataResult<CurrentAccount>(result);
        }
    }
}