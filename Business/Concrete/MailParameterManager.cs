using Business.Abstract;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;

namespace Business.Concrete
{
    public class MailParameterManager : MailParameterService<MailParameter>
    {
        private readonly IMailParameterDal mailParameterDal;
        public MailParameterManager(IMailParameterDal mailParameterDal)
        {
            this.mailParameterDal = mailParameterDal;
        }

        
        public IResult Add(MailParameter entity)
        {
            throw new NotImplementedException();
        }

        public IResult Delete(MailParameter entity)
        {
            throw new NotImplementedException();
        }

        public IDataResult<MailParameter> Get(int id)
        {
            throw new NotImplementedException();
        }

        public IDataResult<List<MailParameter>> GetAll()
        {
            throw new NotImplementedException();
        }

        public IResult Update(MailParameter entity)
        {
            throw new NotImplementedException();
        }
    }
}
