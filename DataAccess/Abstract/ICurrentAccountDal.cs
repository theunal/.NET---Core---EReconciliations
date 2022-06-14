using Core.DataAccess;
using Entities.Concrete;

namespace DataAccess.Abstract
{
    public interface ICurrentAccountDal : IEntityRepository<CurrentAccount>
    {
       bool ReconciliationCheck(int currentAccountId);
    }
}
