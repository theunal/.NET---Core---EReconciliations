using Core.Utilities.Results;
using Entities.Concrete;
using Entities.Dtos.Excel;

namespace Business.Abstract
{
    public interface ICurrentAccountService
    {
        IDataResult<List<CurrentAccount>> GetAll(int companyId); // şirketin cari lerini getir
        IDataResult<CurrentAccount> GetById(int id);
        IDataResult<CurrentAccount> GetByCode(string code);
        IDataResult<CurrentAccount> GetByCompanyId(int companyId);
        IDataResult<CurrentAccount> GetByCompanyIdAndCode(string code, int companyId);

        IResult Add(CurrentAccount entity);
        IResult Update(CurrentAccount entity);
        IResult Delete(int id);


        IResult AddByExcel(CurrencyAccountExcelDto dto);
    }
}