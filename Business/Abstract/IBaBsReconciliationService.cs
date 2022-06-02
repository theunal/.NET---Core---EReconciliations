using Core.Utilities.Results;
using Entities.Concrete;

namespace Business.Abstract
{
    public interface IBaBsReconciliationService
    {
        IDataResult<List<BaBsReconciliation>> GetAll();
        IDataResult<BaBsReconciliation> Get(int id);


        IResult Add(BaBsReconciliation entity);
        IResult Update(BaBsReconciliation entity);
        IResult Delete(BaBsReconciliation entity);
    }
}
