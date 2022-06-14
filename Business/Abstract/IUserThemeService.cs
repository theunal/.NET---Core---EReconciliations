using Core.Utilities.Results;
using Entities.Concrete;

namespace Business.Abstract
{
    public interface IUserThemeService
    {
        IResult Update(UserTheme userTheme);
        IDataResult<UserTheme> GetUserThemeByUserId(int userId);
        IDataResult<UserTheme> CreateDefaultUserTheme(int userId);
    }
}
