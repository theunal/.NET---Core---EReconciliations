using Business.Abstract;
using Business.BusinessAcpects;
using Business.Const;
using Core.Aspects.Performance;
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

        public IDataResult<MailTemplate> GetByTemplateName(string type, int companyId)
        {
            return new SuccessDataResult<MailTemplate>
              (mailTemplateDal.Get(m => m.Type == type && m.CompanyId == companyId));
        }


        [PerformanceAspect(3)]
        [SecuredOperation("admin")]
        public IDataResult<List<MailTemplate>> GetAll(int companyId)
        {
            return new SuccessDataResult<List<MailTemplate>>
                (mailTemplateDal.GetAll(m => m.CompanyId == companyId));
        }






        [PerformanceAspect(3)]
        [SecuredOperation("admin")]
        public IResult Add(MailTemplate mailTemplate)
        {
            mailTemplateDal.Add(mailTemplate);
            return new SuccessResult(Messages.MailTemplateAdded);
        }


        [PerformanceAspect(3)]
        [SecuredOperation("admin")]
        public IResult Delete(MailTemplate mailTemplate)
        {
            mailTemplateDal.Delete(mailTemplate);
            return new SuccessResult(Messages.MailTemplateDeleted);
        }


        [PerformanceAspect(3)]
        [SecuredOperation("admin")]
        public IResult Update(MailTemplate mailTemplate)
        {
            mailTemplateDal.Update(mailTemplate);
            return new SuccessResult(Messages.MailTemplateUpdated);
        }

        
    }
}
