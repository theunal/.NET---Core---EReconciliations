using Core.Entities.Concrete;
using Core.Utilities.Results;
using Entities.Concrete;


namespace Business.Abstract
{
    public interface IForgotPasswordService
    {
        IDataResult<ForgotPassword> Add(User user);
        IDataResult<List<ForgotPassword>> GetAllById(int userId);
        IDataResult<ForgotPassword> GetForgotPasswordByValue(string value);

        void Update(ForgotPassword forgotPassword);
    }
}
