using Core.DataAccess;
using Core.Entities.Concrete;
using DataAccess.Abstract;
using DataAccess.Concrete.Context;
using Entities.Dtos;

namespace DataAccess.Concrete
{
    public class UserDal : EfEntityRepositoryBase<User, DataContext>, IUserDal
    {
        public List<OperationClaim> GetClaims(User user, int companyId)
        {
            using (var context = new DataContext())
            {
                var result = from operationClaim in context.OperationClaims
                             join userOperationClaim in context.UserOperationClaims
                             on operationClaim.Id equals userOperationClaim.OperationClaimId
                             where userOperationClaim.CompanyId == companyId && userOperationClaim.UserId == user.Id
                             select new OperationClaim
                             {
                                 Id = operationClaim.Id,
                                 Name = operationClaim.Name,
                             };

                return result.ToList();
            }
        }

        public List<UsersByCompanyDto> GetUsersByCompanyId(int companyId)
        {
            using var context = new DataContext();
            var result = from userCompany in context.UserCompanies.Where(u => u.CompanyId == companyId && u.IsActive == true)

                         join user in context.Users
                         on userCompany.UserId equals user.Id

                         join company in context.Companies
                         on userCompany.CompanyId equals company.Id

                         select new UsersByCompanyDto
                         {
                             Id = userCompany.Id,
                             UserId = userCompany.UserId,

                             CompanyId = companyId,
                             CompanyName = company.Name,

                             Name = user.Name,
                             Email = user.Email,
                             MailConfirm = user.MailConfirm,
                             UserAddedAt = user.AddedAt,
                             UserIsActive = user.IsActive,
                             UserMailValue = user.MailConfirmValue,
                         };

            return result.OrderBy(r => r.UserAddedAt).ToList();
        }

        public List<AdminCompaniesDto> GetAdminCompanies(int adminUserId, int userUserId)
        {
            using var context = new DataContext();
            var result = from userCompany in context.UserCompanies.Where(u => u.UserId == adminUserId)
                         join company in context.Companies
                         on userCompany.CompanyId equals company.Id

                         select new AdminCompaniesDto
                         {
                             Id = company.Id,
                             AddedAt = company.AddedAt,
                             Address = company.Address,
                             IdentityNumber = company.IdentityNumber,
                             IsActive = company.IsActive,
                             Name = company.Name,
                             TaxDepartment = company.TaxDepartment,
                             TaxIdNumber = company.TaxIdNumber,
                             IsTrue = context.UserCompanies.
                             Where(u => u.UserId == userUserId && u.CompanyId == company.Id)
                             .Count() > 0 ? true : false
                         };

            return result.OrderBy(u => u.Name).ToList();
        }
    }
}
