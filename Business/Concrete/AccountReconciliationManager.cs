using Business.Abstract;
using Business.Const;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;

namespace Business.Concrete
{
    public class AccountReconciliationManager : IAccountReconciliationService
    {
        private readonly IAccountReconciliationDal accountReconciliationDal;
        public AccountReconciliationManager(IAccountReconciliationDal accountReconciliationDal)
        {
            this.accountReconciliationDal = accountReconciliationDal;
        }

        
        public IResult Add(AccountReconciliation entity)
        {
            accountReconciliationDal.Add(entity);
            return new SuccessResult(Messages.AccountReconciliationAdded);
        }

        public IResult Delete(AccountReconciliation entity)
        {
            accountReconciliationDal.Delete(entity);
            return new SuccessResult(Messages.AccountReconciliationDeleted);
        }
        public IResult Update(AccountReconciliation entity)
        {
            accountReconciliationDal.Update(entity);
            return new SuccessResult(Messages.AccountReconciliationUpdated);
        }

        public IDataResult<AccountReconciliation> GetById(int id)
        {
            return new SuccessDataResult<AccountReconciliation>(accountReconciliationDal.Get(x => x.Id == id));
        }

        public IDataResult<List<AccountReconciliation>> GetAll(int companyId)
        {
            return new SuccessDataResult<List<AccountReconciliation>>
                (accountReconciliationDal.GetAll(x => x.CompanyId == companyId));
        }

     
    }
}
