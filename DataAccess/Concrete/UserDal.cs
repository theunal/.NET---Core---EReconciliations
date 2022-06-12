using Core.DataAccess;
using Core.Entities.Concrete;
using DataAccess.Abstract;
using DataAccess.Concrete.Context;
using Entities.Dtos;
using Microsoft.EntityFrameworkCore;

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
            
            //using (var context = new DataContext())
            //{
            //    var result = from operationClaim in context.OperationClaims
            //                 join userOperationClaim in context.UserOperationClaims
            //                 on operationClaim.Id equals userOperationClaim.OperationClaimId
            //                 where userOperationClaim.UserId == user.Id && userOperationClaim.CompanyId == companyId
            //                 select operationClaim; burada hata var evet hocam farrkettim

            //    return result.ToList();
            //}
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
    }
}
