using Core.Entities.Abstract;

namespace Entities.Dtos
{
    public class UserOperationClaimUpdateDto : IDto
    {
        public int UserId { get; set; }
        public int OperationClaimId { get; set; }
        public int CompanyId { get; set; }
        public bool IsActive { get; set; }
    }
}
