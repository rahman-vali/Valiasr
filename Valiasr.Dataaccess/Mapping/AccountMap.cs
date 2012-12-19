namespace Valiasr.DataAccess.Mapping
{
    using System.Data.Entity.ModelConfiguration;

    using Valiasr.Domain;



    public class KolMap : EntityTypeConfiguration<Kol>
    {
        public KolMap()
        {
            this.HasKey(k => k.Id);
            this.Property(k => k.Id).HasColumnName("Kol_Code");
                //It's not supported in Sql Server CE
                //.HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity)

            this.Property(k => k.Description).HasMaxLength(150).HasColumnName("Kol_Des");
            this.Property(k => k.Kind).HasColumnName("KodKind");
            this.HasMany(k => k.Moins).WithRequired(m => m.Kol);
        }
    }

    public class MoinMap : EntityTypeConfiguration<Moin>
    {
        public MoinMap()
        {
            this.HasKey(m => m.Id);
            this.Property(m => m.Id).HasColumnName("Moin_Code");
                //It's not supported in Sql Server CE
                //.HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity)
               
            this.Property(m => m.Description).HasMaxLength(150).HasColumnName("Moin_Des");
            this.HasMany(m => m.Accounts).WithRequired(a => a.Moin);
        }
    }


    public class AccountMap : EntityTypeConfiguration<Account>
    {
        public AccountMap()
        {
            this.HasKey(a => a.Id);
            this.Property(a => a.Id)
                //It's not supported in Sql Server CE
                //.HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity)
                .HasColumnName("AccountId");
            this.Property(a => a.Description).HasMaxLength(210);
            this.Property(a => a.Balance);
            this.HasMany(a => a.Correspondents).WithMany(c => c.Accounts);
        }
    }
}
