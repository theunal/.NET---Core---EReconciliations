using Autofac;
using Business.Abstract;
using Business.Concrete;
using Core.Entities.Concrete;
using Core.Utilities.Security.JWT;
using DataAccess.Abstract;
using DataAccess.Concrete;
using Entities.Concrete;

namespace Business.DependencyResolvers
{
    public class AutofacBusinessModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<AccountReconciliationDetailManager>().As<IAccountReconciliationDetailService<AccountReconciliationDetail>>().SingleInstance();
            builder.RegisterType<AccountReconciliationDetailDal>().As<IAccountReconciliationDetailDal>().SingleInstance();

            builder.RegisterType<AccountReconciliationManager>().As<IAccountReconciliationService<AccountReconciliation>>().SingleInstance();
            builder.RegisterType<AccountReconciliationDal>().As<IAccountReconciliationDal>().SingleInstance();

            builder.RegisterType<BaBsReconciliationDetailManager>().As<IBaBsReconciliationDetailService<BaBsReconciliationDetail>>().SingleInstance();
            builder.RegisterType<BaBsReconciliationDetailDal>().As<IBaBsReconciliationDetailDal>().SingleInstance();

            builder.RegisterType<BaBsReconciliationManager>().As<IBaBsReconciliationService<BaBsReconciliation>>().SingleInstance();
            builder.RegisterType<BaBsReconciliationDal>().As<IBaBsReconciliationDal>().SingleInstance();

            builder.RegisterType<CompanyManager>().As<ICompanyService<Company>>().SingleInstance();
            builder.RegisterType<CompanyDal>().As<ICompanyDal>().SingleInstance();

            builder.RegisterType<CurrencyAccountManager>().As<ICurrencyAccountService<CurrencyAccount>>().SingleInstance();
            builder.RegisterType<CurrencyAccountDal>().As<ICurrencyAccountDal>().SingleInstance();

            builder.RegisterType<CurrencyManager>().As<ICurrencyService<Currency>>().SingleInstance();
            builder.RegisterType<CurrencyDal>().As<ICurrencyDal>().SingleInstance();

            builder.RegisterType<MailParameterManager>().As<IMailParameterService<MailParameter>>().SingleInstance();
            builder.RegisterType<MailParameterDal>().As<IMailParameterDal>().SingleInstance();

            builder.RegisterType<OperationClaimManager>().As<IOperationClaimService<OperationClaim>>().SingleInstance();
            builder.RegisterType<OperationClaimDal>().As<IOperationClaimDal>().SingleInstance();

            builder.RegisterType<UserCompanyManager>().As<IUserCompanyService<UserCompany>>().SingleInstance();
            builder.RegisterType<UserCompanyDal>().As<IUserCompanyDal>().SingleInstance();
            
            builder.RegisterType<UserManager>().As<IUserService<User>>().SingleInstance();
            builder.RegisterType<UserDal>().As<IUserDal>().SingleInstance();

            builder.RegisterType<UserOperationClaimManager>().As<IUserOperationClaimService<UserOperationClaim>>().SingleInstance();
            builder.RegisterType<UserOperationClaimDal>().As<IUserOperationClaimDal>().SingleInstance();

            builder.RegisterType<AuthManager>().As<IAuthService>().SingleInstance();
            
            builder.RegisterType<JwtHelper>().As<ITokenHelper>().SingleInstance();
        }
    }
}
