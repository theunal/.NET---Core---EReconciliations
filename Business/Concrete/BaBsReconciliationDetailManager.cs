using Business.Abstract;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;

namespace Business.Concrete
{
    public class BaBsReconciliationDetailManager : BaBsReconciliationDetailService<BaBsReconciliationDetail>
    {
        private readonly IBaBsReconciliationDetailDal baBsReconciliationDetailDal;
        public BaBsReconciliationDetailManager(IBaBsReconciliationDetailDal baBsReconciliationDetailDal)
        {
            this.baBsReconciliationDetailDal = baBsReconciliationDetailDal;
        }

        
        public IResult Add(BaBsReconciliationDetail entity)
        {
            throw new NotImplementedException();
        }

        public IResult Delete(BaBsReconciliationDetail entity)
        {
            throw new NotImplementedException();
        }

        public IDataResult<BaBsReconciliationDetail> Get(int id)
        {
            throw new NotImplementedException();
        }

        public IDataResult<List<BaBsReconciliationDetail>> GetAll()
        {
            throw new NotImplementedException();
        }

        public IResult Update(BaBsReconciliationDetail entity)
        {
            throw new NotImplementedException();
        }
    }
}
