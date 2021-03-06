using Core.Utilities.Results;
using Entities.Concrete;
using Entities.Dtos;
using Entities.Dtos.Excel;

namespace Business.Abstract
{
    public interface IBaBsReconciliationDetailService
    {

        IDataResult<List<BaBsReconciliationDetail>> GetAll(int babsReconciliationId);
        IDataResult<BaBsReconciliationDetail> GetById(int id);


        IResult Add(BaBsReconciliationDetail entity);
        IResult Update(BaBsReconciliationDetail entity);
        IResult Delete(BaBsReconciliationDetail entity);



        IResult AddByExcel(BaBsReconciliationDetailExcelDto dto);
    }
}
