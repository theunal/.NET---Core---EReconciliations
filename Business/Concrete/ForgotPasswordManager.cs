using Business.Abstract;
using Business.Const;
using Core.Entities.Concrete;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;

namespace Business.Concrete
{
    public class ForgotPasswordManager : IForgotPasswordService
    {
        private readonly IForgotPasswordDal forgotPasswordDal;
        public ForgotPasswordManager(IForgotPasswordDal forgotPasswordDal)
        {
            this.forgotPasswordDal = forgotPasswordDal;
        }
        public IDataResult<ForgotPassword> Add(User user)
        {
            ForgotPassword forgotPassword = new ForgotPassword
            {
                UserId = user.Id,
                Value = Guid.NewGuid().ToString(),
                SendDate = DateTime.Now,
                IsActive = true
            };
            forgotPasswordDal.Add(forgotPassword);
            return new SuccessDataResult<ForgotPassword>(forgotPassword);
        }

        public IDataResult<List<ForgotPassword>> GetAllById(int userId)
        {
            var result = forgotPasswordDal.GetAll(x => x.UserId == userId && x.IsActive == true);
            return new SuccessDataResult<List<ForgotPassword>>(result);
        }

        public IDataResult<ForgotPassword> GetForgotPasswordByValue(string value)
        {
            var forgotPassword = forgotPasswordDal.Get(p => p.Value == value && p.IsActive == true);
            return new SuccessDataResult<ForgotPassword>(forgotPassword);
        }

        public void Update(ForgotPassword forgotPassword)
        {
            forgotPasswordDal.Update(forgotPassword);
        }
    }
}
