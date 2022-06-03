using Entities.Dtos;
using FluentValidation;

namespace Business.ValidationRules
{
    public class UserRegisterSecondValidator : AbstractValidator<UserRegisterSecondDto>
    {
        public UserRegisterSecondValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Ad soyad alanı boş olamaz.");
            RuleFor(x => x.Name).NotNull().WithMessage("Ad soyad alanı boş olamaz.");
            RuleFor(x => x.Name).MinimumLength(3).WithMessage("Ad soyad alanı en az 3 karakter olmalıdır.");

            RuleFor(x => x.Email).NotEmpty().WithMessage("E-posta alanı boş olamaz.");
            RuleFor(x => x.Email).NotNull().WithMessage("E-posta alanı boş olamaz.");
            RuleFor(x => x.Email).EmailAddress().WithMessage("Geçerli bir e-posta adresi giriniz.");

            RuleFor(x => x.Password).NotEmpty().WithMessage("Şifre alanı boş olamaz.");
            RuleFor(x => x.Password).NotNull().WithMessage("Şifre alanı boş olamaz.");
            RuleFor(x => x.Password).Length(6, 20).WithMessage("Şifre 6-20 karakter arasında olmalıdır.");


            RuleFor(x => x.CompanyId).NotEmpty().WithMessage("Şirket seçilmedi.");
            RuleFor(x => x.CompanyId).NotNull().WithMessage("Şirket seçilmedi.");

        }
    }
}
