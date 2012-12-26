namespace Valiasr.DataAccess.Mapping
{
    using System.Data.Entity.ModelConfiguration;

    using Valiasr.Domain;

    public class GeneralAccountMap : EntityTypeConfiguration<GeneralAccount>
    {
        public GeneralAccountMap()
        {
            this.HasKey(k => k.Id);
            this.Property(k => k.Id).HasColumnName("KolId");
            this.Property(k => k.Description).HasMaxLength(150).HasColumnName("Kol_Des");
            this.Property(k => k.Category).HasColumnName("KodKind");
            this.HasMany(k => k.IndexAccounts).WithRequired(m => m.GeneralAccount);
        }
    }

    public class IndexAccountMap : EntityTypeConfiguration<IndexAccount>
    {
        public IndexAccountMap()
        {
            this.HasKey(m => m.Id);
            this.Property(m => m.Id).HasColumnName("MoinId");
            this.Property(m => m.Description).HasMaxLength(150).HasColumnName("Moin_Des");
            this.HasRequired(k => k.GeneralAccount).WithMany(m => m.IndexAccounts).Map(m => m.MapKey("KolId"));
        }
    }

    public class AccountMap : EntityTypeConfiguration<Account>
    {
        public AccountMap()
        {
            this.HasKey(a => a.Id);
            this.Property(a => a.Id)
                .HasColumnName("AccountId");
            this.Property(a => a.Description).HasMaxLength(210);
            this.Property(a => a.Balance);
            this.HasMany(a => a.Lawyers).WithMany();
            this.HasMany(a => a.Customers).WithMany();
            this.HasRequired(m => m.IndexAccount).WithMany(a => a.Accounts).Map(m => m.MapKey("MoinId"));
        }
    }
}
