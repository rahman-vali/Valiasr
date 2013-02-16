namespace Valiasr.DataAccess.Mapping
{
    using System.Data.Entity.ModelConfiguration;

    using Valiasr.Domain.Model;

    public class PersonMap : EntityTypeConfiguration<Person>
    {
        public PersonMap()
        {
            this.ToTable("Persons");
            this.HasKey(p => p.Id);
            this.Property(p => p.Id);
            Property(p => p.CustomerId).IsRequired();
            Property(p => p.ShobehCode).IsRequired();
            Property(p => p.Firstname).IsRequired().HasMaxLength(120);
            Property(p => p.Lastname).HasMaxLength(120);
            Property(p => p.FatherName).HasMaxLength(120);
            Property(p => p.NationalIdentity).HasMaxLength(30);
            Property(p => p.CretyId).HasMaxLength(60);
            Property(p => p.CretySerial).HasMaxLength(20);
            Property(p => p.Sadereh).HasMaxLength(90);
            Property(p => p.JobName).HasMaxLength(90);
            Property(p => p.HeadNationalIdentity).HasMaxLength(30);
            Property(p => p.ContactInfo.HomeAddress).HasMaxLength(250).HasColumnName("HomeAddress");
            Property(p => p.ContactInfo.WorkAddress).HasMaxLength(250).HasColumnName("WorkAddress");
            Property(p => p.ContactInfo.HomeTelno).HasColumnName("HomeTelno").HasMaxLength(45);
            Property(p => p.ContactInfo.OfficeTelNo).HasColumnName("OfficeTelno").HasMaxLength(45);
            Property(p => p.ContactInfo.Mobile).HasMaxLength(15).HasColumnName("Mobile");
            Property(p => p.ContactInfo.PostalIdentity).HasMaxLength(15).HasColumnName("PostalIdentity");
            Property(p => p.ContactInfo.PostalIdentity).HasMaxLength(20);        
        }

        /// <summary>
        /// this is an example of nested class
        /// In kelas ro faghat baraye inke bedoonid nested calss chie jabejash kardam avordam dakhele class personmap
        /// </summary>
        public class ContactInfoMap : ComplexTypeConfiguration<ContactInfo>
        {
        }

    }

    public class CustomerMap : EntityTypeConfiguration<Customer>
    {
        public CustomerMap()
        {
            this.Property(c => c.No).IsRequired().HasColumnName("CustomerNo");
            this.HasRequired(c => c.Person);
        }
    }

    public class LawyerMap : EntityTypeConfiguration<Lawyer>
    {
        public LawyerMap()
        {
            this.Property(v => v.StartDate).IsRequired().HasColumnName("StartDate");
            this.HasRequired(l => l.Person);
        }
    }
}
