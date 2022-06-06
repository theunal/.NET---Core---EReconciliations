using Core.Entities.Abstract;



namespace Entities.Dtos.Excel
{
    public class CurrencyAccountExcelDto : IDto
    {
        public string FilePath { get; set; }
        public int CompanyId { get; set; }

    }
}

