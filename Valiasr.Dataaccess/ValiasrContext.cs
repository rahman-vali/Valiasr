namespace Valiasr.DataAccess
{
    using System.Data.Entity;

    using Valiasr.DataAccess.Mapping;
    using Valiasr.Domain.Model;

    public class ValiasrContext : DbContext
    {
        #region Constructors and Destructors

        public ValiasrContext(string conn)
            : base(conn)
        {
            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<ValiasrContext>());
        }

        public ValiasrContext()
        {
            // TODO: Complete member initialization
        }

        #endregion

        #region Properties

        public DbSet<GeneralAccount> GeneralAccounts { get; set; }

        public DbSet<IndexAccount> IndexAccounts { get; set; }

        public DbSet<BankAccount> BankAccounts { get; set; }

        public DbSet<Person> Persons { get; set; }

        public DbSet<LoanRequest> LoanRequests { get; set; }

        public DbSet<Loan> Loans { get; set; }

        public DbSet<Average> Averages { get; set; }

        #endregion

        #region Methods

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new PersonMap());
            modelBuilder.Configurations.Add(new CustomerMap());
            modelBuilder.Configurations.Add(new LawyerMap());
            modelBuilder.Configurations.Add(new GeneralAccountMap());
            modelBuilder.Configurations.Add(new IndexAccountMap());
            modelBuilder.Configurations.Add(new PersonMap.ContactInfoMap());
            modelBuilder.Configurations.Add(new BankAccountMap());
            modelBuilder.Configurations.Add(new AccountMap());
            modelBuilder.Configurations.Add(new LoanRequestMap());
            modelBuilder.Configurations.Add(new LoanRequestOkyAssistantMap());
            modelBuilder.Configurations.Add(new RequestAccountAvetMap());
            modelBuilder.Configurations.Add(new LoanMap());
            modelBuilder.Configurations.Add(new AverageMap());
            modelBuilder.Entity<Average>()
                        .Map<NormAverage>(a => a.Requires("AverageType").HasValue(0))
                        .Map<AverageWithMin>(a => a.Requires("AverageType").HasValue(1))
                        .Map<AverageWithHoliday>(a => a.Requires("AverageType").HasValue(2)).ToTable("Average");
            base.OnModelCreating(modelBuilder);
        }

        #endregion
    }
}