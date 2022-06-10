using Core.DataAccess;
using DataAccess.Abstract;
using DataAccess.Concrete.Context;
using Entities.Concrete;
using Entities.Dtos;

namespace DataAccess.Concrete
{
    public class BaBsReconciliationDal : EfEntityRepositoryBase<BaBsReconciliation, DataContext>, IBaBsReconciliationDal
    {
        public List<BaBsReconciliationDto> GetAllDto(int companyId)
        {
            using var context = new DataContext();

            var result = from recoinciliation in context.BaBsReconciliations.Where(p => p.CompanyId == companyId)
                         join company in context.Companies
                         on recoinciliation.CompanyId equals company.Id

                         join account in context.CurrentAccounts
                         on recoinciliation.CurrencyAccountId equals account.Id

                         select new BaBsReconciliationDto
                         {
                             CompanyId = companyId,
                             AccountEmail = account.Email,

                             CurrencyAccountId = account.Id,
                             AccountIdentityNumber = account.IdentityNumber,
                             AccountName = account.Name,
                             AccountTaxDepartment = account.TaxDepartment,
                             AccountTaxIdNumber = account.TaxIdNumber,
                             
                             CompanyIdentityNumber = company.IdentityNumber,
                             CompanyName = company.Name,
                             CompanyTaxDepartment = company.TaxDepartment,
                             CompanyTaxIdNumber = company.TaxIdNumber,
                             
                             Total = recoinciliation.Total,
                             EmailReadDate = recoinciliation.EmailReadDate,
                             Guid = recoinciliation.Guid,
                             Id = recoinciliation.Id,
                             IsEmailRead = recoinciliation.IsEmailRead,
                             IsResultSucceed = recoinciliation.IsResultSucceed,
                             IsSendEmail = recoinciliation.IsSendEmail,
                             ResultDate = recoinciliation.ResultDate,
                             ResultNote = recoinciliation.ResultNote,
                             SendEmailDate = recoinciliation.SendEmailDate,
                             Mounth = recoinciliation.Mounth,
                             Type = recoinciliation.Type,
                             Quantity = recoinciliation.Quantity,
                             Year = recoinciliation.Year,

                             CurrencyCode = "TL",
                         };
            
            return result.ToList();
        }

    }
}
