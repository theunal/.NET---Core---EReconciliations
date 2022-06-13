using Core.DataAccess;
using Core.Entities.Concrete;
using DataAccess.Abstract;
using DataAccess.Concrete.Context;
using Entities.Concrete;

namespace DataAccess.Concrete
{
    public class CompanyDal : EfEntityRepositoryBase<Company, DataContext>, ICompanyDal
    {
        public void AddUserCompany(int userId, int companyId)
        {
            using var context = new DataContext();
            UserCompany userCompany = new UserCompany
            {
                UserId = userId,
                CompanyId = companyId,
                AddedAt = DateTime.Now,
                IsActive = true
            };

            context.Add(userCompany);
            context.SaveChanges();
        }

        public List<Company> GetAllCompanyAdminUserId(int adminUserId)
        {
            using var context = new DataContext();
            var result = from userCompany in context.UserCompanies.Where(u => u.UserId == adminUserId)
                         join company in context.Companies
                         on userCompany.CompanyId equals company.Id

                         select new Company
                         {
                             Id = company.Id,
                             Name = company.Name,
                             TaxDepartment = company.TaxDepartment,
                             TaxIdNumber = company.TaxIdNumber,
                             IdentityNumber = company.IdentityNumber,
                             AddedAt = company.AddedAt,
                             Address = company.Address,
                             IsActive = company.IsActive
                         };
            return result.OrderBy(c => c.AddedAt).ToList();
        }
    }
}
