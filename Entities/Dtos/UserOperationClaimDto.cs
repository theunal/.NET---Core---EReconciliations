using Core.Entities.Abstract;



namespace Entities.Dtos
{
    public class UserOperationClaimDto : IDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int CompanyId { get; set; }
        public int OperationClaimId { get; set; }
        public string OperationName { get; set; }
        public string OperationDescription { get; set; }
                
    }
}
