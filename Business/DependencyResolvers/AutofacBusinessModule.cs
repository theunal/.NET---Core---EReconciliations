using Autofac;
using Autofac.Extras.DynamicProxy;
using Business.Abstract;
using Business.Concrete;
using Castle.DynamicProxy;
using Core.Utilities.Interceptors;
using Core.Utilities.Security.JWT;
using DataAccess.Abstract;
using DataAccess.Concrete;

namespace Business.DependencyResolvers
{
    public class AutofacBusinessModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<AccountReconciliationDetailManager>().As<IAccountReconciliationDetailService>().SingleInstance();
            builder.RegisterType<AccountReconciliationDetailDal>().As<IAccountReconciliationDetailDal>().SingleInstance();

            builder.RegisterType<AccountReconciliationManager>().As<IAccountReconciliationService>().SingleInstance();
            builder.RegisterType<AccountReconciliationDal>().As<IAccountReconciliationDal>().SingleInstance();

            builder.RegisterType<BaBsReconciliationDetailManager>().As<IBaBsReconciliationDetailService>().SingleInstance();
            builder.RegisterType<BaBsReconciliationDetailDal>().As<IBaBsReconciliationDetailDal>().SingleInstance();

            builder.RegisterType<BaBsReconciliationManager>().As<IBaBsReconciliationService>().SingleInstance();
            builder.RegisterType<BaBsReconciliationDal>().As<IBaBsReconciliationDal>().SingleInstance();

            builder.RegisterType<CompanyManager>().As<ICompanyService>().SingleInstance();
            builder.RegisterType<CompanyDal>().As<ICompanyDal>().SingleInstance();
                        
            builder.RegisterType<CurrentAccountManager>().As<ICurrentAccountService>().SingleInstance();
            builder.RegisterType<CurrentAccountDal>().As<ICurrentAccountDal>().SingleInstance();

            builder.RegisterType<CurrencyManager>().As<ICurrencyService>().SingleInstance();
            builder.RegisterType<CurrencyDal>().As<ICurrencyDal>().SingleInstance();

            builder.RegisterType<MailParameterManager>().As<IMailParameterService>().SingleInstance();
            builder.RegisterType<MailParameterDal>().As<IMailParameterDal>().SingleInstance();

            builder.RegisterType<OperationClaimManager>().As<IOperationClaimService>().SingleInstance();
            builder.RegisterType<OperationClaimDal>().As<IOperationClaimDal>().SingleInstance();

            builder.RegisterType<UserCompanyManager>().As<IUserCompanyService>().SingleInstance();
            builder.RegisterType<UserCompanyDal>().As<IUserCompanyDal>().SingleInstance();
            
            builder.RegisterType<UserManager>().As<IUserService>().SingleInstance();
            builder.RegisterType<UserDal>().As<IUserDal>().SingleInstance();

            builder.RegisterType<UserOperationClaimManager>().As<IUserOperationClaimService>().SingleInstance();
            builder.RegisterType<UserOperationClaimDal>().As<IUserOperationClaimDal>().SingleInstance();

            builder.RegisterType<AuthManager>().As<IAuthService>().SingleInstance();
            
            builder.RegisterType<JwtHelper>().As<ITokenHelper>().SingleInstance();

            builder.RegisterType<MailManager>().As<IMailService>();
            builder.RegisterType<MailDal>().As<IMailDal>();

            builder.RegisterType<MailTemplateManager>().As<IMailTemplateService>();
            builder.RegisterType<MailTemplateDal>().As<IMailTemplateDal>();

            builder.RegisterType<ForgotPasswordManager>().As<IForgotPasswordService>();
            builder.RegisterType<ForgotPasswordDal>().As<IForgotPasswordDal>();

            builder.RegisterType<UserRelationshipManager>().As<IUserRelationshipService>();
            builder.RegisterType<UserRelationshipDal>().As<IUserRelationshipDal>();

            builder.RegisterType<UserThemeManager>().As<IUserThemeService>();
            builder.RegisterType<UserThemeDal>().As<IUserThemeDal>();

            
            // autofac config
            var assembly = System.Reflection.Assembly.GetExecutingAssembly();

            builder.RegisterAssemblyTypes(assembly).AsImplementedInterfaces()
                .EnableInterfaceInterceptors(new ProxyGenerationOptions()
            {
                Selector = new AspectInterceptorSelector()
            }).SingleInstance();

        }
    }
}
