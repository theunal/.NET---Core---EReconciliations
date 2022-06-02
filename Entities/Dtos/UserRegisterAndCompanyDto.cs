using Entities.Concrete;

namespace Entities.Dtos
{
    public class UserRegisterAndCompanyDto
    {
        public UserRegisterDto UserRegisterDto { get; set; }
        public Company Company { get; set; }
    }
}
