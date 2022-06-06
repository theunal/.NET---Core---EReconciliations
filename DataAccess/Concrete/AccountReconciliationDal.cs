using Core.DataAccess;
using DataAccess.Abstract;
using DataAccess.Concrete.Context;
using Entities.Concrete;
using Entities.Dtos;

namespace DataAccess.Concrete
{
    public class AccountReconciliationDal : EfEntityRepositoryBase<AccountReconciliation, DataContext>, IAccountReconciliationDal
    {
        public List<AccountReconciliationDto> GetAllDto(int companyId)
        {
            using var context = new DataContext();
            var result = from recoinciliation in context.AccountReconciliations.Where(p => p.CompanyId == companyId)
                         join company in context.Companies 
                         on recoinciliation.CompanyId equals company.Id
                         
                         join account in context.CurrencyAccounts 
                         on recoinciliation.CurrencyAccountId equals account.Id

                         join currency in context.Currencies
                         on recoinciliation.CurrencyId equals currency.Id

                         select new AccountReconciliationDto
                         {
                             Id = recoinciliation.Id,
                             
                             CompanyId = companyId,
                             CurrencyAccountId = account.Id,
                             AccountIdentityNumber = account.IdentityNumber,
                             AccountName = account.Name,
                             AccountTaxDepartment = account.TaxDepartment,
                             AccountTaxIdNumber = account.TaxIdNumber,
                             CompanyIdentityNumber = company.IdentityNumber,
                             AccountEmail = account.Email,


                             CompanyName = company.Name,
                             CompanyTaxDepartment = company.TaxDepartment,
                             CompanyTaxIdNumber = company.TaxIdNumber,
                             
                             CurrencyCredit = recoinciliation.CurrencyCredit,
                             CurrencyDebit = recoinciliation.CurrencyDebit,
                             CurrencyId = recoinciliation.CurrencyId,
                             EmailReadDate = recoinciliation.EmailReadDate,
                             EndingDate = recoinciliation.EndingDate,
                             Guid = recoinciliation.Guid,
                             IsEmailRead = recoinciliation.IsEmailRead,
                             IsResultSucceed = recoinciliation.IsResultSucceed,
                             IsSendEmail = recoinciliation.IsSendEmail,
                             ResultDate = recoinciliation.ResultDate,
                             ResultNote = recoinciliation.ResultNote,
                             SendEmailDate = recoinciliation.SendEmailDate,
                             StartingDate = recoinciliation.StartingDate,

                             CurrencyCode = currency.Code,
                             
                         };
            
            return result.ToList();
        }
        
    }
}
