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
    }
}
