using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Valiasr.Domain;
using Valiasr.Domain.SystemJari;

namespace Valiasr.DataAccess.Mapping
{
    public class CustomerHesabMap:EntityTypeConfiguration<CustomerHesab>
    {
        public CustomerHesabMap()
        {
            this.HasRequired(o => o.Account);
            this.HasRequired(o => o.Customer);
            this.HasKey(o => o.Id);
            this.Property(o => o.Id).HasColumnName("CustomerHesabId");
                //It's not supported in Sql Server CE
                //.HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity)
            this.Property(o => o.Role);
        }
    }
}
