using Core.Entities.Abstract;
namespace Entities.Dtos
{
    public class AccountReconciliationDto : IDto
    {
        public string filePath { get; set; }
        public int CompanyId { get; set; }
    }
}
