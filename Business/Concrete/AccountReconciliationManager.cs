using Business.Abstract;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;

namespace Business.Concrete
{
    public class AccountReconciliationManager : IAccountReconciliationService<AccountReconciliation>
    {
        private readonly IAccountReconciliationDal accountReconciliationDal;
        public AccountReconciliationManager(IAccountReconciliationDal accountReconciliationDal)
        {
            this.accountReconciliationDal = accountReconciliationDal;
        }

        
        public IResult Add(AccountReconciliation entity)
        {
            throw new NotImplementedException();
        }

        public IResult Delete(AccountReconciliation entity)
        {
            throw new NotImplementedException();
        }

        public IDataResult<AccountReconciliation> Get(int id)
        {
            throw new NotImplementedException();
        }

        public IDataResult<List<AccountReconciliation>> GetAll()
        {
            throw new NotImplementedException();
        }

        public IResult Update(AccountReconciliation entity)
        {
            throw new NotImplementedException();
        }
    }
}
