using Core.Utilities.Results;
using Entities.Concrete;

namespace Business.Abstract
{
    public interface IAccountReconciliationService
    {
        IDataResult<List<AccountReconciliation>> GetAll();
        IDataResult<AccountReconciliation> Get(int id);


        IResult Add(AccountReconciliation entity);
        IResult Update(AccountReconciliation entity);
        IResult Delete(AccountReconciliation entity);
    }
}
