using Core.Utilities.Results;
using Core.Entities.Concrete;

namespace Business.Abstract
{
    public interface IUserCompanyService
    {
        IDataResult<List<UserCompany>> GetAll();
        IDataResult<UserCompany> GetById(int id);


        IResult Add(UserCompany entity);
        IResult Update(UserCompany entity);
        IResult Delete(UserCompany entity);


        IResult DeleteByUserIdAndCompanyId(int userId, int companyId);
    }
}
