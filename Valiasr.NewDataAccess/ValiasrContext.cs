namespace Valiasr.DataAccess
{
    using System.Data.Entity;

    using Valiasr.DataAccess.Mapping;
    using Valiasr.NewDomain;

    public class ValiasrContext : DbContext
    {
        #region Constructors and Destructors

        public ValiasrContext(string conn)
            : base(conn)
        {
            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<ValiasrContext>());
        }

        #endregion

        #region Properties

        public DbSet<Account> Accounts { get; set; }

        public DbSet<Customer> Customers { get; set; }

        #endregion

        #region Methods

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new PersonMap());
//            modelBuilder.Configurations.Add(new CustomerMap());
            modelBuilder.Configurations.Add(new AccountMap());
            base.OnModelCreating(modelBuilder);
        }

        #endregion
    }
}