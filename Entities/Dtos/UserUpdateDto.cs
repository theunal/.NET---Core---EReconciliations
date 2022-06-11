using Core.Entities.Abstract;

namespace Entities.Dtos
{
    public class UserUpdateDto : IDto
    {
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public bool IsActive { get; set; }
    }
}
