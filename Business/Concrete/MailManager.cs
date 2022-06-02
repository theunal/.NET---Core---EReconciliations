using Business.Abstract;
using Business.Const;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Dtos;

namespace Business.Concrete
{
    public class MailManager : IMailService
    {
        private readonly IMailDal mailDal;
        public MailManager(IMailDal mailDal)
        {
            this.mailDal = mailDal;
        }

        
        public IResult SendMail(SendMailDto sendMailDto)
        {
            mailDal.SendMail(sendMailDto);
            return new SuccessResult(Messages.MailSentSuccessfully);
        }

        
    }
}
