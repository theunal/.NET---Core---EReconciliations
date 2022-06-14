using Business.Abstract;
using Business.Const;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;

namespace Business.Concrete
{
    public class UserThemeManager : IUserThemeService
    {
        private readonly IUserThemeDal userThemeDal;
        public UserThemeManager(IUserThemeDal userThemeDal)
        {
            this.userThemeDal = userThemeDal;
        }



        public IDataResult<UserTheme> CreateDefaultUserTheme(int userId)
        {
            UserTheme newUserTheme = new UserTheme
            {
                UserId = userId,
                SidebarButtonColor = "primary", // 6-7 tane farklı renk
                SidebarMode = "dark", // white dark transparent
                Mode = "" // "" ya da dark-version
            };
            Update(newUserTheme);
            return GetUserThemeByUserId(userId);
        }

        
        public IDataResult<UserTheme> GetUserThemeByUserId(int userId)
        {
            var userTheme = userThemeDal.Get(u => u.UserId == userId);
            if (userTheme is not null)
            {
                return new SuccessDataResult<UserTheme>(userTheme);
            }
            return new ErrorDataResult<UserTheme>();
        }
        
        public IResult Update(UserTheme entity)
        {
            var userTheme = GetUserThemeByUserId(entity.UserId);
            if (!userTheme.Success)
            {
                userThemeDal.Add(entity);
                return new SuccessResult(Messages.UserThemeAdded);
            } 
            userThemeDal.Update(entity);
            return new SuccessResult(Messages.UserThemeUpdated);
        }
    }
}
