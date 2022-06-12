using Core.DataAccess;
using DataAccess.Abstract;
using DataAccess.Concrete.Context;
using Entities.Concrete;
using Entities.Dtos;

namespace DataAccess.Concrete
{
    public class UserRelationshipDal : EfEntityRepositoryBase<UserRelationship, DataContext>, IUserRelationshipDal
    {
        public List<UserRelationshipDto> GetAllDto(int adminUserId)
        {
            using var context = new DataContext();
            var result = from userRelationship in context.UserRelationships.Where(u => u.AdminUserId == adminUserId)
                         join adminUser in context.Users
                         on userRelationship.AdminUserId equals adminUser.Id

                         join userUser in context.Users
                         on userRelationship.UserUserId equals userUser.Id

                         select new UserRelationshipDto
                         {
                             Id = userRelationship.Id,
                             
                             UserUserId = userUser.Id,
                             UserAddedAt = userUser.AddedAt,
                             UserEmail = userUser.Email,
                             UserIsActive = userUser.IsActive,
                             UserUserName = userUser.Name,
                             UserMailValue = userUser.MailConfirmValue,
                             UserMailConfirm = userUser.MailConfirm,

                             AdminUserId = adminUserId,
                             AdminAddedAt = adminUser.AddedAt,
                             AdminEmail = adminUser.Email,
                             AdminIsActive = adminUser.IsActive,
                             AdminUserName = adminUser.Name,

                             // company listesi
                             Companies = (from userCompany in context.UserCompanies.Where(u => u.UserId == userUser.Id)
                                          join user in context.Users
                                          on userCompany.UserId equals user.Id

                                          join company in context.Companies
                                          on userCompany.CompanyId equals company.Id

                                          select new Company
                                          {
                                              Id = company.Id,
                                              Name = company.Name,
                                              TaxDepartment = company.TaxDepartment,
                                              TaxIdNumber = company.TaxIdNumber,
                                              IdentityNumber = company.IdentityNumber,
                                              Address = company.Address,
                                              AddedAt = company.AddedAt,
                                              IsActive = company.IsActive
                                          }).ToList()
                         };
            
            return result.OrderBy(u => u.UserAddedAt).ToList();
        }

        public UserRelationshipDto GetByUserUserId(int userUserId)
        {
            using var context = new DataContext();
            var result = from userRelationship in context.UserRelationships.Where(u => u.UserUserId == userUserId)
                         join adminUser in context.Users
                         on userRelationship.AdminUserId equals adminUser.Id

                         join userUser in context.Users
                         on userRelationship.UserUserId equals userUser.Id

                         select new UserRelationshipDto
                         {
                             Id = userRelationship.Id,

                             UserUserId = userUser.Id,
                             UserAddedAt = userUser.AddedAt,
                             UserEmail = userUser.Email,
                             UserIsActive = userUser.IsActive,
                             UserUserName = userUser.Name,
                             UserMailValue = userUser.MailConfirmValue,
                             UserMailConfirm = userUser.MailConfirm,

                             AdminUserId = adminUser.Id,
                             AdminAddedAt = adminUser.AddedAt,
                             AdminEmail = adminUser.Email,
                             AdminIsActive = adminUser.IsActive,
                             AdminUserName = adminUser.Name,

                             // company listesi
                             Companies = (from userCompany in context.UserCompanies.Where(u => u.UserId == userUser.Id)
                                          join user in context.Users
                                          on userCompany.UserId equals user.Id

                                          join company in context.Companies
                                          on userCompany.CompanyId equals company.Id

                                          select new Company
                                          {
                                              Id = company.Id,
                                              Name = company.Name,
                                              TaxDepartment = company.TaxDepartment,
                                              TaxIdNumber = company.TaxIdNumber,
                                              IdentityNumber = company.IdentityNumber,
                                              Address = company.Address,
                                              AddedAt = company.AddedAt,
                                              IsActive = company.IsActive
                                          }).ToList()
                         };
            return result.FirstOrDefault();
        }
    }
}
