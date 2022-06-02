using Core.Entities.Abstract;
using Core.Entities.Concrete;

namespace Entities.Dtos
{
    public class UserCompanyDto : User, IDto
    {
        public int CompanyId { get; set; }
    }
}
