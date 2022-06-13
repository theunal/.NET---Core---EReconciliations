using Core.Entities.Abstract;

namespace Entities.Dtos
{
    public class ChangeCompanyDto : IDto
    {
        public int UserId { get; set; }
        public int CompanyId { get; set; }
    }
}
