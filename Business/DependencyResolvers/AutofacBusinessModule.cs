using Autofac;
using Business.Abstract;
using Business.Concrete;
using Core.Entities.Concrete;
using DataAccess.Abstract;
using DataAccess.Concrete;
using Entities.Concrete;

namespace Business.DependencyResolvers
{
    public class AutofacBusinessModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<AccountReconciliationDetailManager>().As<AccountReconciliationDetailService<AccountReconciliationDetail>>().SingleInstance();
            builder.RegisterType<AccountReconciliationDetailDal>().As<IAccountReconciliationDetailDal>().SingleInstance();

            builder.RegisterType<AccountReconciliationManager>().As<AccountReconciliationService<AccountReconciliation>>().SingleInstance();
            builder.RegisterType<AccountReconciliationDal>().As<IAccountReconciliationDal>().SingleInstance();

            builder.RegisterType<BaBsReconciliationDetailManager>().As<BaBsReconciliationDetailService<BaBsReconciliationDetail>>().SingleInstance();
            builder.RegisterType<BaBsReconciliationDetailDal>().As<IBaBsReconciliationDetailDal>().SingleInstance();

            builder.RegisterType<BaBsReconciliationManager>().As<BaBsReconciliationService<BaBsReconciliation>>().SingleInstance();
            builder.RegisterType<BaBsReconciliationDal>().As<IBaBsReconciliationDal>().SingleInstance();

            builder.RegisterType<CompanyManager>().As<CompanyService<Company>>().SingleInstance();
            builder.RegisterType<CompanyDal>().As<ICompanyDal>().SingleInstance();

            builder.RegisterType<CurrencyAccountManager>().As<CurrencyAccountService<CurrencyAccount>>().SingleInstance();
            builder.RegisterType<CurrencyAccountDal>().As<ICurrencyAccountDal>().SingleInstance();

            builder.RegisterType<CurrencyManager>().As<CurrencyService<Currency>>().SingleInstance();
            builder.RegisterType<CurrencyDal>().As<ICurrencyDal>().SingleInstance();

            builder.RegisterType<MailParameterManager>().As<MailParameterService<MailParameter>>().SingleInstance();
            builder.RegisterType<MailParameterDal>().As<IMailParameterDal>().SingleInstance();

            builder.RegisterType<OperationClaimManager>().As<OperationClaimService<OperationClaim>>().SingleInstance();
            builder.RegisterType<OperationClaimDal>().As<IOperationClaimDal>().SingleInstance();

            builder.RegisterType<UserCompanyManager>().As<UserCompanyService<UserCompany>>().SingleInstance();
            builder.RegisterType<UserCompanyDal>().As<IUserCompanyDal>().SingleInstance();

            builder.RegisterType<UserManager>().As<UserService>().SingleInstance();
            builder.RegisterType<UserDal>().As<IUserDal>().SingleInstance();

            builder.RegisterType<UserOperationClaimManager>().As<UserOperationClaimService<UserOperationClaim>>().SingleInstance();
            builder.RegisterType<UserOperationClaimDal>().As<IUserOperationClaimDal>().SingleInstance();
        }
    }
}
