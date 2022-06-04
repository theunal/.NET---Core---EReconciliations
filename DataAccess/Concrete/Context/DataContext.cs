using Core.Entities.Concrete;
using Entities.Concrete;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Concrete.Context
{
    public class DataContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"server=(localdb)\MSSQLLocalDB; database=EReconciliations; integrated security=true");
            base.OnConfiguring(optionsBuilder);
        }


        public DbSet<AccountReconciliation>? AccountReconciliations { get; set; }
        public DbSet<AccountReconciliationDetail>? AccountReconciliationDetails { get; set; }
        public DbSet<BaBsReconciliation>? BaBsReconciliations { get; set; }
        public DbSet<BaBsReconciliationDetail>? BaBsReconciliationDetails { get; set; }
        public DbSet<Company>? Companies { get; set; }
        public DbSet<Currency>? Currencies { get; set; }
        public DbSet<CurrencyAccount>? CurrencyAccounts { get; set; }
        public DbSet<MailParameter>? MailParameters { get; set; }


        public DbSet<OperationClaim>? OperationClaims { get; set; }
        public DbSet<User>? Users { get; set; }
        public DbSet<UserCompany>? UserCompanies { get; set; }
        public DbSet<UserOperationClaim>? UserOperationClaims { get; set; }


        public DbSet<MailTemplate>? MailTemplates { get; set; }

    }
}
