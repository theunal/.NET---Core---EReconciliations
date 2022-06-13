using Core.DataAccess;
using Entities.Concrete;

namespace DataAccess.Abstract
{
    public interface ICompanyDal : IEntityRepository<Company>
    {
        void AddUserCompany(int userId, int companyId);

        List<Company> GetAllCompanyAdminUserId(int adminUserId);
        
    }
}
