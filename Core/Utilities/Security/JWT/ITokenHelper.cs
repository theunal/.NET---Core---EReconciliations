using Core.Entities.Concrete;

namespace Core.Utilities.Security.JWT
{
    // token burda olusturuluyor
    public interface ITokenHelper
    {
        // user ve onun operation claimleri için token oluşturuluyor
        AccessToken CreateToken(User user, List<OperationClaim> operationClaims, int companyId);
    }
}
