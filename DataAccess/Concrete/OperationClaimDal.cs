using Core.DataAccess;
using Core.Entities.Concrete;
using DataAccess.Abstract;
using DataAccess.Concrete.Context;

namespace DataAccess.Concrete
{
    public class OperationClaimDal : EfEntityRepositoryBase<OperationClaim, DataContext>, IOperationClaimDal
    {
    }
}
