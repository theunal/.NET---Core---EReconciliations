using Core.Utilities.Results;
using Entities.Concrete;
using Entities.Dtos.Excel;

namespace Business.Abstract
{
    public interface IAccountReconciliationDetailService
    {

        IDataResult<List<AccountReconciliationDetail>> GetAll(int accountReconciliationId);
        IDataResult<AccountReconciliationDetail> GetById(int id);


        
        IResult Add(AccountReconciliationDetail entity);
        IResult Update(AccountReconciliationDetail entity);
        IResult Delete(AccountReconciliationDetail entity);



        
        IResult AddByExcel(AccountReconciliationDetailExcelDto dto);
    }
}
