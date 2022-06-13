using Core.Entities.Abstract;
using Entities.Concrete;

namespace Entities.Dtos
{
    public class AdminCompaniesDto : Company, IDto
    {
        public bool IsTrue { get; set; }
    }
}
