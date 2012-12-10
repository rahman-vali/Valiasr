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
    class CustomerMap:EntityTypeConfiguration<Customer>
    {
        public CustomerMap()
        {
            //Primary Key
            this.HasKey(p => p.Id);
            this.Property(p => p.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity)
                .HasColumnName("PersonId");

            //prperty
            this.Property(p => p.Firstname).IsRequired().HasMaxLength(30).HasColumnName("Firstname");
            this.Property(p => p.Lastname).IsRequired().HasMaxLength(30).HasColumnName("Lastname");
            this.Property(p => p.ContactInfo.Address).HasMaxLength(40).HasColumnName("Peson_Address");
            this.Property(p => p.ContactInfo.Tellno).HasColumnName("Person_Tellno");
            this.HasOptional(o => o.CustomerHesabs).WithMany();
        }
    }
}
