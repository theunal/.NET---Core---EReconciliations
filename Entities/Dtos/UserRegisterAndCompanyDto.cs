using Entities.Concrete;
using Core.Entities.Abstract;

namespace Entities.Dtos
{
    public class UserRegisterAndCompanyDto : IDto
    {
        public UserRegisterDto UserRegisterDto { get; set; }
        public Company Company { get; set; }
    }
}
