using System.Data.Entity.ModelConfiguration;

namespace Valiasr.DataAccess.Mapping
{
    using Valiasr.NewDomain;


    class PersonMap : EntityTypeConfiguration<Person>
    {
        public PersonMap()
        {
            //Primary Key
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

//    class CustomerMap: EntityTypeConfiguration<Customer>
//    {
//        public CustomerMap()
//        {
//            this.HasMany(p => p.Accounts).WithMany();
//        }
//    }
}
