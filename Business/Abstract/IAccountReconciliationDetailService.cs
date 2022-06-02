using Core.Utilities.Results;
using Entities.Concrete;

namespace Business.Abstract
{
    public interface IAccountReconciliationDetailService
    {
        IDataResult<List<AccountReconciliationDetail>> GetAll();
        IDataResult<AccountReconciliationDetail> Get(int id);

        
        IResult Add(AccountReconciliationDetail entity);
        IResult Update(AccountReconciliationDetail entity);
        IResult Delete(AccountReconciliationDetail entity);
    }
}
