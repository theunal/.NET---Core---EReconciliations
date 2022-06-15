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
            var result = from reconciliation in context.AccountReconciliations.Where(p => p.CompanyId == companyId)
                         join company in context.Companies 
                         on reconciliation.CompanyId equals company.Id
                         
                         join account in context.CurrentAccounts 
                         on reconciliation.CurrentAccountId equals account.Id

                         join currency in context.Currencies
                         on reconciliation.CurrencyId equals currency.Id

                         select new AccountReconciliationDto
                         {
                             Id = reconciliation.Id,
                             
                             CompanyId = companyId,
                             CurrentAccountId = account.Id,
                             AccountIdentityNumber = account.IdentityNumber,
                             AccountName = account.Name,
                             AccountTaxDepartment = account.TaxDepartment,
                             AccountTaxIdNumber = account.TaxIdNumber,
                             CompanyIdentityNumber = company.IdentityNumber,
                             AccountEmail = account.Email,
                             AccountCode = account.Code,

                             
                             CompanyName = company.Name,
                             CompanyTaxDepartment = company.TaxDepartment,
                             CompanyTaxIdNumber = company.TaxIdNumber,
                             
                             CurrencyCredit = reconciliation.CurrencyCredit,
                             CurrencyDebit = reconciliation.CurrencyDebit,
                             CurrencyId = reconciliation.CurrencyId,
                             EmailReadDate = reconciliation.EmailReadDate,
                             EndingDate = reconciliation.EndingDate,
                             Guid = reconciliation.Guid,
                             IsEmailRead = reconciliation.IsEmailRead,
                             IsResultSucceed = reconciliation.IsResultSucceed,
                             IsSendEmail = reconciliation.IsSendEmail,
                             ResultDate = reconciliation.ResultDate,
                             ResultNote = reconciliation.ResultNote,
                             SendEmailDate = reconciliation.SendEmailDate,
                             StartingDate = reconciliation.StartingDate,

                             CurrencyCode = currency.Name,
                             
                         };
            
            return result.ToList();
        }

        public AccountReconciliationDto GetById(int id)
        {
            using var context = new DataContext();
            var result = from reconciliation in context.AccountReconciliations.Where(p => p.Id == id)
                         join company in context.Companies
                         on reconciliation.CompanyId equals company.Id

                         join account in context.CurrentAccounts
                         on reconciliation.CurrentAccountId equals account.Id

                         join currency in context.Currencies
                         on reconciliation.CurrencyId equals currency.Id

                         select new AccountReconciliationDto
                         {
                             Id = reconciliation.Id,

                             CompanyId = reconciliation.CompanyId,
                             CurrentAccountId = account.Id,
                             AccountIdentityNumber = account.IdentityNumber,
                             AccountName = account.Name,
                             AccountTaxDepartment = account.TaxDepartment,
                             AccountTaxIdNumber = account.TaxIdNumber,
                             CompanyIdentityNumber = company.IdentityNumber,
                             AccountEmail = account.Email,
                             AccountCode = account.Code,


                             CompanyName = company.Name,
                             CompanyTaxDepartment = company.TaxDepartment,
                             CompanyTaxIdNumber = company.TaxIdNumber,

                             CurrencyCredit = reconciliation.CurrencyCredit,
                             CurrencyDebit = reconciliation.CurrencyDebit,
                             CurrencyId = reconciliation.CurrencyId,
                             EmailReadDate = reconciliation.EmailReadDate,
                             EndingDate = reconciliation.EndingDate,
                             Guid = reconciliation.Guid,
                             IsEmailRead = reconciliation.IsEmailRead,
                             IsResultSucceed = reconciliation.IsResultSucceed,
                             IsSendEmail = reconciliation.IsSendEmail,
                             ResultDate = reconciliation.ResultDate,
                             ResultNote = reconciliation.ResultNote,
                             SendEmailDate = reconciliation.SendEmailDate,
                             StartingDate = reconciliation.StartingDate,

                             CurrencyCode = currency.Name,

                         };

            return result.FirstOrDefault();
        }

    }
}
