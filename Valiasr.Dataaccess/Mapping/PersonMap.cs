namespace Valiasr.DataAccess.Mapping
{
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.ModelConfiguration;

    using Valiasr.Domain;

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
            Property(p => p.CustomerId).IsRequired();
            Property(p => p.ShobehCode).IsRequired();
            Property(p => p.Firstname).IsRequired().HasMaxLength(120);
            Property(p => p.Lastname).HasMaxLength(120);
            Property(p => p.FatherName).HasMaxLength(120);
            Property(p => p.MelliIdentity).HasMaxLength(30);
            Property(p => p.CretyId).HasMaxLength(60);
            Property(p => p.CretySerial).HasMaxLength(20);
            Property(p => p.Sadereh).HasMaxLength(90);
            Property(p => p.JobName).HasMaxLength(90);
            Property(p => p.HeadMelliIdentity).HasMaxLength(30);
            Property(p => p.ContactInfo.HomeAddress).HasMaxLength(250).HasColumnName("HomeAddress");
            Property(p => p.ContactInfo.WorkAddress).HasMaxLength(250).HasColumnName("WorkAddress");
            Property(p => p.ContactInfo.HomeTelno).HasColumnName("HomeTelno").HasMaxLength(45);
            Property(p => p.ContactInfo.OfficeTelNo).HasColumnName("OfficeTelno").HasMaxLength(45);
            Property(p => p.ContactInfo.Mobile).HasMaxLength(15).HasColumnName("Mobile");
            Property(p => p.ContactInfo.PostIdentity).HasMaxLength(20);
            Property(p => p.IndivOrOrgan).HasMaxLength(1);
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
