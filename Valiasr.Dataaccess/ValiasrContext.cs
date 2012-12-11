﻿namespace Valiasr.DataAccess
{
    using System.Data.Entity;

    using Valiasr.DataAccess.Mapping;
    using Valiasr.Domain;
    using Valiasr.Domain.SystemJari;

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

        public DbSet<CustomerHesab> CustomerHesabs { get; set; }

        public DbSet<Customer> Customers { get; set; }

        #endregion

        #region Methods

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new CustomerMap());
            modelBuilder.Configurations.Add(new CustomerHesabMap());
            modelBuilder.Configurations.Add(new AccountMap());
            base.OnModelCreating(modelBuilder);
        }

        #endregion
    }
}