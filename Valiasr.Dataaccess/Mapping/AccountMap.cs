namespace Valiasr.DataAccess.Mapping
{
    using System.Data.Entity.ModelConfiguration;

    using Valiasr.Domain.Model;

    public class GeneralAccountMap : EntityTypeConfiguration<GeneralAccount>
    {
        public GeneralAccountMap()
        {
            this.HasKey(k => k.Id);
            this.Property(ga => ga.Id).HasColumnName("KolId");           
            this.Property(ga => ga.Description).HasMaxLength(80).HasColumnName("Kol_Des");
            this.Property(ga => ga.Category).HasColumnName("KodKind");
            this.HasMany(ga => ga.IndexAccounts).WithRequired(m => m.GeneralAccount);
        }
    }

    public class IndexAccountMap : EntityTypeConfiguration<IndexAccount>
    {
        public IndexAccountMap()
        {
            this.HasKey(ia => ia.Id);
            this.Property(ia => ia.Id).HasColumnName("MoinId");
            Property(ia => ia.Code).HasMaxLength(20);
            this.Property(ia => ia.Description).HasMaxLength(80).HasColumnName("Moin_Des");
        }
    }

    public class AccountMap : EntityTypeConfiguration<Account>
    {
        public AccountMap()
        {
            this.HasKey(a => a.Id);
            this.Property(a => a.Id)
                .HasColumnName("AccountId");
            this.Property(a => a.Code).HasMaxLength(30);
            this.Property(a => a.IndexAccountCode).HasMaxLength(20);
            this.Property(a => a.No).HasMaxLength(20);
            this.Property(a => a.Description).HasMaxLength(150);
            this.Property(a => a.Balance);
            this.HasMany(a => a.Lawyers).WithMany(l => l.Accounts);
            this.HasMany(a => a.Customers).WithMany(c => c.Accounts);
            this.HasRequired(m => m.IndexAccount).WithMany(a => a.Accounts);
    //        HasMany(a => a.LoanRequests).WithRequired(lr => lr.Account);
        }
    }
}
