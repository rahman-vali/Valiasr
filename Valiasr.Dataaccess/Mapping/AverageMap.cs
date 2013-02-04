namespace Valiasr.DataAccess.Mapping
{
    using System.Data.Entity.ModelConfiguration;

    using Valiasr.Domain.Model;

    public class AverageMap : EntityTypeConfiguration<Average>
    {
        public AverageMap()
        {
            this.HasKey(a => a.Id);
        }
    }

}
