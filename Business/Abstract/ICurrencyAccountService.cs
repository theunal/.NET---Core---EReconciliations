using Core.Utilities.Results;
using Entities.Concrete;
using Entities.Dtos;

namespace Business.Abstract
{
    public interface ICurrencyAccountService
    {
        IDataResult<List<CurrencyAccount>> GetAll(int companyId); // şirketin cari lerini getir
        IDataResult<CurrencyAccount> GetById(int id);
        IDataResult<CurrencyAccount> GetByCode(string code);
        IDataResult<CurrencyAccount> GetByCompanyId(int companyId);
        IDataResult<CurrencyAccount> GetByCompanyIdAndCode(string code, int companyId);

        IResult Add(CurrencyAccount entity);
        IResult Update(CurrencyAccount entity);
        IResult Delete(int id);


        IResult AddByExcel(CurrencyAccountExcelDto dto);
    }
}
