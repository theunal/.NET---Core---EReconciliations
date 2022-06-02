using Business.Abstract;
using Business.Const;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;

namespace Business.Concrete
{
    public class MailTemplateManager : IMailTemplateService
    {
        private readonly IMailTemplateDal mailTemplateDal;
        public MailTemplateManager(IMailTemplateDal mailTemplateDal)
        {
            this.mailTemplateDal = mailTemplateDal;
        }


        public IDataResult<MailTemplate> Get(int id)
        {
            return new SuccessDataResult<MailTemplate>
                (mailTemplateDal.Get(m => m.Id == id));
        }

        public IDataResult<MailTemplate> GetByTemplateName(string name, int companyId)
        {
            return new SuccessDataResult<MailTemplate>
              (mailTemplateDal.Get(m => m.Type == name && m.CompanyId == companyId));
        }

        public IDataResult<List<MailTemplate>> GetAll(int companyId)
        {
            return new SuccessDataResult<List<MailTemplate>>
                (mailTemplateDal.GetAll(m => m.CompanyId == companyId));
        }

        



        
        public IResult Add(MailTemplate mailTemplate)
        {
            mailTemplateDal.Add(mailTemplate);
            return new SuccessResult(Messages.MailTemplateAdded);
        }

        public IResult Delete(MailTemplate mailTemplate)
        {
            mailTemplateDal.Delete(mailTemplate);
            return new SuccessResult(Messages.MailTemplateDeleted);
        }

        public IResult Update(MailTemplate mailTemplate)
        {
            mailTemplateDal.Update(mailTemplate);
            return new SuccessResult(Messages.MailTemplateUpdated);
        }

        
    }
}
