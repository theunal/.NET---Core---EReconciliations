using Core.DataAccess;
using DataAccess.Abstract;
using DataAccess.Concrete.Context;
using Entities.Concrete;


namespace DataAccess.Concrete
{
    public class BaBsReconciliationDal : EfEntityRepositoryBase<BaBsReconciliation, DataContext>, IBaBsReconciliationDal
    {
    
    }
}
