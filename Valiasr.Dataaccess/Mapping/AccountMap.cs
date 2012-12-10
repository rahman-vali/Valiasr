using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Valiasr.Domain;

namespace Valiasr.DataAccess.Mapping
{
    public class AccountMap : EntityTypeConfiguration<Account>
    {
        public AccountMap()
        {
            this.HasKey(a => a.Id);
            this.Property(a => a.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity)
                .HasColumnName("AccountId");
            this.Property(a => a.Hesab_Des).HasMaxLength(210);
            this.HasOptional(a => a.CustomerHesabs).WithMany();
        }
    }
}
