using Business.Abstract;
using Business.Const;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;

namespace Business.Concrete
{
    public class MailParameterManager : IMailParameterService
    {
        private readonly IMailParameterDal mailParameterDal;
        public MailParameterManager(IMailParameterDal mailParameterDal)
        {
            this.mailParameterDal = mailParameterDal;
        }

        
        


        public IDataResult<MailParameter> Get(int companyId)
        {
            return new SuccessDataResult<MailParameter>
                (mailParameterDal.Get(x => x.CompanyId == companyId));
        }


       // [SecuredOperation("Admin,admin")]
        public IResult Update(MailParameter mailParameter)
        {
            var result = Get(mailParameter.CompanyId);
            
            if (result.Data == null) 
            {
                mailParameterDal.Add(mailParameter);
            }
            else // mail kayıtlı ise update ediyorum
            {
                result.Data.SMTP = mailParameter.SMTP;
                result.Data.Port = mailParameter.Port;
                result.Data.SSL = mailParameter.SSL;
                result.Data.Email = mailParameter.Email;
                result.Data.Password = mailParameter.Password;
                mailParameterDal.Update(result.Data);
            }

            return new SuccessResult(Messages.MailUpdated);
        }
     



        
    }
}
