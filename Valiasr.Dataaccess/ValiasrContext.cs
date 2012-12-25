namespace Valiasr.DataAccess
{
    using System.Data.Entity;

    using Valiasr.DataAccess.Mapping;
    using Valiasr.Domain;
    

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

        public DbSet<Kol> Kols { get; set; }

        public DbSet<Moin> Moins { get; set; }

        public DbSet<Account> Accounts { get; set; }

        public DbSet<Correspondent> Correspondents { get; set; }

        #endregion

        #region Methods

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new PersonMap());
            modelBuilder.Configurations.Add(new CustomerMap());
            modelBuilder.Configurations.Add(new VakilMap());
            modelBuilder.Configurations.Add(new KolMap());
            modelBuilder.Configurations.Add(new MoinMap());
            modelBuilder.Configurations.Add(new ContactInfoMap());
            modelBuilder.Configurations.Add(new AccountMap());
            modelBuilder.Configurations.Add(new CorrespondentMap());

          //  modelBuilder.Entity<Person>().Map<Correspondent>(c => c.Requires("PersonTye").HasValue(1));
            //modelBuilder.Entity<Person>().Map<Correspondent>(c => c.Requires("Diccriminator").HasValue("1"));
            //In khat ezafi ast chera, tozihat aan dar file CorrespondentMap.cs amade ast
            //modelBuilder.Configurations.Add(new CorrespondentMap());
            base.OnModelCreating(modelBuilder);
        }

        #endregion
    }
}