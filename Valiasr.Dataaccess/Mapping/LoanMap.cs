namespace Valiasr.DataAccess.Mapping
{
    using System.Data.Entity.ModelConfiguration;

    using Valiasr.Domain.Model;

    class LoanRequestMap:EntityTypeConfiguration<LoanRequest>
    {
        public LoanRequestMap ()
        {
            HasKey(p => p.Id);            
            this.Property(lr => lr.Id).HasColumnName("LoanRequestId");            
            Property(lr => lr.Description).HasMaxLength(90);
            Property(lr => lr.DurationType).HasMaxLength(8);
            HasRequired(lr => lr.LoanRequestOkyAsistant).WithRequiredPrincipal();
            ToTable("LoanRequests");

        }
    }

    class LoanRequestOkyAssistantMap:EntityTypeConfiguration<LoanRequestOkyAssistant>
    {
        public LoanRequestOkyAssistantMap()
        {
            HasKey(lrao => lrao.Id);
            ToTable("LoanRequests");
            Property(lroa => lroa.OkyDurationType).HasMaxLength(8);
            Property(lroa => lroa.OkyAss).HasMaxLength(45);
        }
        
    }

    class RequestAccountAvetMap : EntityTypeConfiguration<RequestAccountAve>
    {
        public RequestAccountAvetMap()
        {
            HasKey(ra => ra.Id);
            ToTable("RequestAccountAve");
            HasRequired(ra => ra.LoanRequest).WithMany( lr => lr.RequestAccountAves).WillCascadeOnDelete(false);
            HasOptional(ra => ra.AverageM);
        }

    }


    class LoanMap:EntityTypeConfiguration<Loan>
    {
        public LoanMap()
        {
            ToTable("Loans");
            Property(l => l.DurationType).HasMaxLength(8);
            Property(l => l.LoanNo).HasMaxLength(30);
            Property(l => l.StepDef).HasMaxLength(150);
            Property(l => l.Assurance).HasMaxLength(90);
            HasRequired(l => l.LoanRequest);
        }
    }
}
