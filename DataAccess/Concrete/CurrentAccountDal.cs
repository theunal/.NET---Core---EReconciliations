using Core.DataAccess;
using DataAccess.Abstract;
using DataAccess.Concrete.Context;
using Entities.Concrete;

namespace DataAccess.Concrete
{
    public class CurrentAccountDal : EfEntityRepositoryBase<CurrentAccount, DataContext>, ICurrentAccountDal
    {
        public bool ReconciliationCheck(int currentAccountId)
        {
            using var context = new DataContext();
            var accountReconciliations = context.AccountReconciliations.Where(a => a.CurrentAccountId == currentAccountId);
            var babsReconciliations = context.BaBsReconciliations.Where(b => b.CurrentAccountId == currentAccountId);

            if (accountReconciliations.Any() || babsReconciliations.Any())
            {
                return true;
            }
            return false;
        }
    }
}
