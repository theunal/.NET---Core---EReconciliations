using Core.Utilities.Results;
using Entities.Concrete;
using Entities.Dtos;
using Entities.Dtos.Excel;

namespace Business.Abstract
{
    public interface IBaBsReconciliationService
    {
        IDataResult<List<BaBsReconciliation>> GetAll(int companyId);
        IDataResult<List<BaBsReconciliationDto>> GetAllDto(int companyId);
        IDataResult<BaBsReconciliation> GetById(int id);
        IDataResult<BaBsReconciliation> GetByCode(string code);

        IResult Add(BaBsReconciliation entity); 
        IResult Update(BaBsReconciliation entity);
        IResult Delete(BaBsReconciliation entity);


        IResult AddByExcel(BaBsReconciliationExcelDto dto);
        IResult SendReconciliationMail(BaBsReconciliationDto dto);
    }
}
