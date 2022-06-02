using Core.Utilities.Results;
using Entities.Concrete;

namespace Business.Abstract
{
    public interface ICompanyService
    {
        IDataResult<List<Company>> GetAll();
        IDataResult<Company> Get(Company company);


        IResult Add(Company entity);
        IResult Update(Company entity);
        IResult Delete(Company entity);

        IResult AddUserCompany(int userId, int companyId);
    }
}
