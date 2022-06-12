using Core.Entities.Abstract;

namespace Entities.Dtos
{
    public class DeleteRelationshipBetweenUserAndCompanyDto : IDto
    {
        public int UserId { get; set; }
        public int CompanyId { get; set; }
        public int OperationClaimId { get; set; } // frontend kısmında sıfırdan dto
                                                  // oluşturmamak için bunu da ekledim aslında gerek yok
    }
}
