using Core.Utilities.Results;
using Entities.Concrete;
using Entities.Dtos;

namespace Business.Abstract
{
    public interface IBaBsReconciliationService
    {
        IDataResult<List<BaBsReconciliation>> GetAll(int companyId);
        IDataResult<BaBsReconciliation> GetById(int id);


        IResult Add(BaBsReconciliation entity); 
        IResult Update(BaBsReconciliation entity);
        IResult Delete(BaBsReconciliation entity);


        IResult AddByExcel(BaBsReconciliationDto dto);
    }
}
