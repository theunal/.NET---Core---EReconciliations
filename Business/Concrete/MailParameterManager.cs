using Business.Abstract;
using Business.BusinessAcpects;
using Business.Const;
using Core.Aspects.Performance;
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



        [PerformanceAspect(3)]
        [SecuredOperation("admin")]
        public IResult Update(MailParameter mailParameter)
        {
            var result = Get(mailParameter.CompanyId).Data;
            if (result is null) 
            {
                mailParameterDal.Add(mailParameter);
            }
            else // mail kayıtlı ise update ediyorum
            {
                result.SMTP = mailParameter.SMTP;
                result.Port = mailParameter.Port;
                result.SSL = mailParameter.SSL;
                result.Email = mailParameter.Email;
                if (mailParameter.Password != "") result.Password = mailParameter.Password;
                mailParameterDal.Update(result);
            }
            return new SuccessResult(Messages.MailParametersUpdated);
        }
    }
}
