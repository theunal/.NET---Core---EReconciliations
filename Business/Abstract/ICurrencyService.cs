using Core.Utilities.Results;
using Entities.Concrete;

namespace Business.Abstract
{
    public interface ICurrencyService
    {
        IDataResult<List<Currency>> GetAll();
        IDataResult<Currency> Get(int id);


        IResult Add(Currency entity);
        IResult Update(Currency entity);
        IResult Delete(Currency entity);
    }
}
