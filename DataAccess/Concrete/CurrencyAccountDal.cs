using Core.DataAccess;
using DataAccess.Abstract;
using DataAccess.Concrete.Context;
using Entities.Concrete;



namespace DataAccess.Concrete
{
    public class CurrencyAccountDal : EfEntityRepositoryBase<CurrencyAccount, DataContext>, ICurrencyAccountDal
    {
    }
}
