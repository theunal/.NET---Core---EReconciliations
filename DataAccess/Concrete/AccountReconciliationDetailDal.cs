using Core.DataAccess;
using DataAccess.Abstract;
using DataAccess.Concrete.Context;
using Entities.Concrete;

namespace DataAccess.Concrete
{
    public class AccountReconciliationDetailDal : EfEntityRepositoryBase<AccountReconciliationDetail, DataContext>, IAccountReconciliationDetailDal
    {
     
    }
}
