using Entities.Concrete;
using FluentValidation;

namespace Business.ValidationRules
{
    public class CurrencyAccountValidator : AbstractValidator<CurrencyAccount>
    {
        public CurrencyAccountValidator()
        {
            RuleFor(c => c.CompanyId).Must(companyIdnullOrEmpty).WithMessage("Şirket seçmediniz.");

            RuleFor(c => c.Name).NotEmpty().WithMessage("Firma adı boş geçilemez.");
            RuleFor(c => c.Name).NotNull().WithMessage("Firma adı boş geçilemez.");
            RuleFor(c => c.Name).MinimumLength(4).WithMessage("Firma adı en az 4 karakter olmalıdır.");

            RuleFor(c => c.Address).NotEmpty().WithMessage("Adres alanı boş geçilemez.");
            RuleFor(c => c.Address).NotNull().WithMessage("Adres alanı boş geçilemez.");
            RuleFor(c => c.Address).MinimumLength(10).WithMessage("Adres alanı en az 10 karakter olmalıdır.");

        }

        private bool companyIdnullOrEmpty(int arg)
        {
            if (arg <= 0 || arg == null) return false;
            return true;
        }
    }
}

