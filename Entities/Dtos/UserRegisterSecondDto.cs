using Core.Entities.Abstract;

namespace Entities.Dtos
{
    public class UserRegisterSecondDto : UserRegisterDto, IDto
    {
        public int CompanyId { get; set; }
        
    }
}
