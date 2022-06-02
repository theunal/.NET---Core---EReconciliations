using Core.Entities.Concrete;

namespace Entities.Dtos
{
    public class UserCompanyDto : User
    {
        public int CompanyId { get; set; }
    }
}
