using Core.Entities.Abstract;
namespace Entities.Dtos
{
    public class AccountReconciliationExcelDto : IDto
    {
        public string filePath { get; set; }
        public int CompanyId { get; set; }
    }
}
