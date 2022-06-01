using Business.Abstract;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;

namespace Business.Concrete
{
    public class BaBsReconciliationManager : BaBsReconciliationService<BaBsReconciliation>
    {
        private readonly IBaBsReconciliationDal baBsReconciliationDal;
        public BaBsReconciliationManager(IBaBsReconciliationDal baBsReconciliationDal)
        {
            this.baBsReconciliationDal = baBsReconciliationDal;
        }

        
        public IResult Add(BaBsReconciliation entity)
        {
            throw new NotImplementedException();
        }

        public IResult Delete(BaBsReconciliation entity)
        {
            throw new NotImplementedException();
        }

        public IDataResult<BaBsReconciliation> Get(int id)
        {
            throw new NotImplementedException();
        }

        public IDataResult<List<BaBsReconciliation>> GetAll()
        {
            throw new NotImplementedException();
        }

        public IResult Update(BaBsReconciliation entity)
        {
            throw new NotImplementedException();
        }
    }
}
