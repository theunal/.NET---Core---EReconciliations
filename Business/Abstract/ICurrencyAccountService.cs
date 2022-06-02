using Core.Utilities.Results;
using Entities.Concrete;

namespace Business.Abstract
{
    public interface ICurrencyAccountService
    {
        IDataResult<List<CurrencyAccount>> GetAll();
        IDataResult<CurrencyAccount> Get(int id);


        IResult Add(CurrencyAccount entity);
        IResult Update(CurrencyAccount entity);
        IResult Delete(CurrencyAccount entity);
    }
}
