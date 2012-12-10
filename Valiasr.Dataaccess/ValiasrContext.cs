using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Configuration;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Data.Entity.Infrastructure;
using Valiasr.Domain;

namespace Valiasr.DataAccess
{
    public class ValiasrContext:DbContext
    {
        public ValiasrContext(string conn):base(conn)
        {
            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<ValiasrContext>());
        }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<CustomerHesab> CustomerHesabs { get; set; }
    }
}
