using Entities.Dtos;
using FluentValidation;

namespace Business.ValidationRules
{
    public class UserLoginValidator : AbstractValidator<UserLoginDto>
    {
        public UserLoginValidator()
        {
            RuleFor(x => x.Email).NotEmpty().WithMessage("Email boş geçilemez.");
            RuleFor(x => x.Email).EmailAddress().WithMessage("Geçerli bir email adresi giriniz.");
            
            RuleFor(x => x.Password).NotEmpty().WithMessage("Şifre boş geçilemez.");
            RuleFor(x => x.Password).Length(8, 20).WithMessage("Şifre 8-20 karakter arasında olmalıdır.");
        }
    }
}
