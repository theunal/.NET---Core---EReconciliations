using Core.Utilities.Results;
using Entities.Concrete;
using Entities.Dtos;

namespace Business.Abstract
{
    public interface IAccountReconciliationService
    {
        IDataResult<List<AccountReconciliation>> GetAll(int companyId);
        IDataResult<AccountReconciliation> GetById(int id);

        
        IResult Add(AccountReconciliation entity);
        IResult Update(AccountReconciliation entity);
        IResult Delete(AccountReconciliation entity);


        
        IResult AddByExcel(AccountReconciliationDto dto);

        
    }
}
