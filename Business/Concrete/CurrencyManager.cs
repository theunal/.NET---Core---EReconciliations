using Business.Abstract;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;

namespace Business.Concrete
{
    public class CurrencyManager : ICurrencyService<Currency>
    {
        private readonly ICurrencyDal currencyDal;
        public CurrencyManager(ICurrencyDal currencyDal)
        {
            this.currencyDal = currencyDal;
        }

        
        public IResult Add(Currency entity)
        {
            throw new NotImplementedException();
        }

        public IResult Delete(Currency entity)
        {
            throw new NotImplementedException();
        }

        public IDataResult<Currency> Get(int id)
        {
            throw new NotImplementedException();
        }

        public IDataResult<List<Currency>> GetAll()
        {
            throw new NotImplementedException();
        }

        public IResult Update(Currency entity)
        {
            throw new NotImplementedException();
        }
    }
}
