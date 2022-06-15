using Core.Utilities.Results;
using Entities.Concrete;
using Entities.Dtos;
using Entities.Dtos.Excel;

namespace Business.Abstract
{
    public interface IAccountReconciliationService
    {
        IDataResult<List<AccountReconciliation>> GetAll(int companyId);
        IDataResult<List<AccountReconciliationDto>> GetAllDto(int companyId);
        IDataResult<AccountReconciliationDto> GetById(int id);
        IDataResult<AccountReconciliation> GetByCode(string code);

        IResult Add(AccountReconciliation entity);
        IResult Update(AccountReconciliation entity, string accountEmail, string code);
        IResult Delete(int id);


        
        IResult AddByExcel(AccountReconciliationExcelDto dto);

        IResult SendReconciliationMail(AccountReconciliationDto entity);
    }
}
