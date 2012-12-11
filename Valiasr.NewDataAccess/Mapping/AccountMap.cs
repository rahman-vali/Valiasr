using System.Data.Entity.ModelConfiguration;

namespace Valiasr.DataAccess.Mapping
{
    using Valiasr.NewDomain;

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

            this.HasMany(a => a.Correspondent).WithMany();
        }
    }
}
