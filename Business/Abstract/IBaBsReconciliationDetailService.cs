using Core.Utilities.Results;
using Entities.Concrete;


namespace Business.Abstract
{
    public interface IBaBsReconciliationDetailService
    {
        IDataResult<List<BaBsReconciliationDetail>> GetAll();
        IDataResult<BaBsReconciliationDetail> Get(int id);


        IResult Add(BaBsReconciliationDetail entity);
        IResult Update(BaBsReconciliationDetail entity);
        IResult Delete(BaBsReconciliationDetail entity);
    }
}
