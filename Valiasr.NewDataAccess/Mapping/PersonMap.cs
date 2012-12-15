﻿namespace Valiasr.NewDataAccess.Mapping
{
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.ModelConfiguration;

    using Valiasr.NewDomain;

    public class PersonMap : EntityTypeConfiguration<Person>
    {
        public PersonMap()
        {
            //Primary Key
            this.ToTable("Persons");
            this.HasKey(p => p.Id);
            this.Property(p => p.Id)
                //It's not supported in Sql Server CE
                //.HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity)
                .HasColumnName("PersonId");

            //prperty
            this.Property(p => p.Firstname).IsRequired().HasMaxLength(30).HasColumnName("Firstname");
            this.Property(p => p.Lastname).IsRequired().HasMaxLength(30).HasColumnName("Lastname");
            this.Property(p => p.ContactInfo.Address).HasMaxLength(40).HasColumnName("Peson_Address");
            this.Property(p => p.ContactInfo.Tellno).HasColumnName("Person_Tellno"); 
        }
    }
    public class CustomerMap : EntityTypeConfiguration<Customer>
    {
        public CustomerMap()
        {
            this.ToTable("Customers");
            this.Property(c => c.No).IsRequired().HasColumnName("CustomerNo");
        }
    }

    public class VakilMap : EntityTypeConfiguration<Vakil>
    {
        public VakilMap()
        {
            this.ToTable("Vakils");
            this.Property(v => v.StartDate).IsRequired().HasColumnName("StartDate");
        }
    }

}
