using Business.Abstract;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;

namespace Business.Concrete
{
    public class CurrencyAccountManager : CurrencyAccountService<CurrencyAccount>
    {
        private readonly ICurrencyAccountDal currencyAccountDal;
        public CurrencyAccountManager(ICurrencyAccountDal currencyAccountDal)
        {
            this.currencyAccountDal = currencyAccountDal;
        }

        
        public IResult Add(CurrencyAccount entity)
        {
            throw new NotImplementedException();
        }

        public IResult Delete(CurrencyAccount entity)
        {
            throw new NotImplementedException();
        }

        public IDataResult<CurrencyAccount> Get(int id)
        {
            throw new NotImplementedException();
        }

        public IDataResult<List<CurrencyAccount>> GetAll()
        {
            throw new NotImplementedException();
        }

        public IResult Update(CurrencyAccount entity)
        {
            throw new NotImplementedException();
        }
    }
}
