using Core.Entities.Abstract;

namespace Entities.Dtos
{
    public class UserRegisterDto : IDto
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

    }
}
