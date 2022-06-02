using Business.Abstract;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;

namespace Business.Concrete
{
    public class AccountReconciliationDetailManager : IAccountReconciliationDetailService<AccountReconciliationDetail>
    {
        private readonly IAccountReconciliationDetailDal accountReconciliationDetailDal;
        public AccountReconciliationDetailManager(IAccountReconciliationDetailDal accountReconciliationDetailDal)
        {
            this.accountReconciliationDetailDal = accountReconciliationDetailDal;
        }

        
        public IResult Add(AccountReconciliationDetail entity)
        {
            throw new NotImplementedException();
        }

        public IResult Delete(AccountReconciliationDetail entity)
        {
            throw new NotImplementedException();
        }

        public IDataResult<AccountReconciliationDetail> Get(int id)
        {
            throw new NotImplementedException();
        }

        public IDataResult<List<AccountReconciliationDetail>> GetAll()
        {
            throw new NotImplementedException();
        }

        public IResult Update(AccountReconciliationDetail entity)
        {
            throw new NotImplementedException();
        }
    }
}
