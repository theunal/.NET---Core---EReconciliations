using Core.Entities.Abstract;

namespace Entities.Dtos.Excel
{
    public class AccountReconciliationExcelDto : IDto
    {
        public string FilePath { get; set; }
        public int CompanyId { get; set; }
    }
}
