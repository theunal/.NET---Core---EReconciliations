using FluentValidation;
using Entities.Dtos;

namespace Business.ValidationRules
{
    public class UserRegisterAndCompanyValidator : AbstractValidator<UserRegisterAndCompanyDto>
    {
        public UserRegisterAndCompanyValidator()
        {
            RuleFor(x => x.UserRegisterDto.Name).NotEmpty().WithMessage("Ad soyad alanı boş olamaz.");
            RuleFor(x => x.UserRegisterDto.Name).NotNull().WithMessage("Ad soyad alanı boş olamaz.");
            RuleFor(x => x.UserRegisterDto.Name).MinimumLength(3).WithMessage("Ad soyad alanı en az 3 karakter olmalıdır.");

            RuleFor(x => x.UserRegisterDto.Email).NotEmpty().WithMessage("E-posta alanı boş olamaz.");
            RuleFor(x => x.UserRegisterDto.Email).NotNull().WithMessage("E-posta alanı boş olamaz.");
            RuleFor(x => x.UserRegisterDto.Email).EmailAddress().WithMessage("Geçerli bir e-posta adresi giriniz.");

            RuleFor(x => x.UserRegisterDto.Password).NotEmpty().WithMessage("Şifre alanı boş olamaz.");
            RuleFor(x => x.UserRegisterDto.Password).NotNull().WithMessage("Şifre alanı boş olamaz.");
            RuleFor(x => x.UserRegisterDto.Password).Length(6, 20).WithMessage("Şifre 6-20 karakter arasında olmalıdır.");



            RuleFor(x => x.Company.Name).NotEmpty().WithMessage("Şirket adı boş olamaz.");
            RuleFor(x => x.Company.Name).NotNull().WithMessage("Şirket adı boş olamaz.");
            RuleFor(x => x.Company.Name).MinimumLength(3).WithMessage("Şirket adı en az 3 karakter olmalıdır.");

            RuleFor(x => x.Company.Address).NotEmpty().WithMessage("Şirket adı boş olamaz.");
            RuleFor(x => x.Company.Address).NotNull().WithMessage("Şirket adı boş olamaz.");
            RuleFor(x => x.Company.Address).MinimumLength(3).WithMessage("Şirket adı en az 3 karakter olmalıdır.");

            RuleFor(x => x.Company.Address).NotEmpty().WithMessage("Şirket adres alanı boş olamaz.");
            RuleFor(x => x.Company.Address).NotNull().WithMessage("Şirket adres alanı boş olamaz.");
            RuleFor(x => x.Company.Address).MinimumLength(10).WithMessage("Şirket adresi en az 10 karakter olmalıdır.");

            RuleFor(x => x.Company.TaxDepartment).NotEmpty().WithMessage("Vergi dairesi alanı boş olamaz.");
            RuleFor(x => x.Company.TaxDepartment).NotNull().WithMessage("Vergi dairesi alanı boş olamaz.");
            RuleFor(x => x.Company.TaxDepartment).MinimumLength(3).WithMessage("Vergi dairesi alanı en az 3 karakter olmalıdır.");

            RuleFor(x => x.Company.TaxIdNumber).Must(Sifirveya10).WithMessage("Vergi numarası alanı en az 11 karakter olmalıdır.");

            RuleFor(x => x.Company.IdentityNumber).Must(Sifirveya11).WithMessage("Kimlik numarası alanı en az 10 karakter olmalıdır.");

        }
        private bool Sifirveya10(string arg)
        {
            if (arg.Length == 0 || arg.Length == 10) return true;
            return false;
        }
        private bool Sifirveya11(string arg)
        {
            if (arg.Length == 0 || arg.Length == 11) return true;
            return false;
        }
  
    }
}
