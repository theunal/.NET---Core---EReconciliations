using Core.Utilities.Results;
using Entities.Concrete;

namespace Business.Abstract
{
    public interface IMailTemplateService
    {
        IResult Add(MailTemplate mailTemplate);
        IResult Update(MailTemplate mailTemplate);
        IResult Delete(MailTemplate mailTemplate);
        
        IDataResult<MailTemplate> Get(int id);
        IDataResult<List<MailTemplate>> GetAll(int companyId);


    }
}
