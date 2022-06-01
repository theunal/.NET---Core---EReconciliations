using Core.DataAccess;
using DataAccess.Abstract;
using DataAccess.Concrete.Context;
using Entities.Concrete;

namespace DataAccess.Concrete
{
    public class MailParameterDal : EfEntityRepositoryBase<MailParameter, DataContext>, IMailParameterDal
    {
    }
}
