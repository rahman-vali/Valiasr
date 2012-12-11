namespace Valiasr.Domain
{
    using System;

    using Valiasr.Domain.SystemJari;

    public class CustomerHesab
    {
        public Guid Id { get; set; }
        public Role Role { get; set; }
        public Customer Customer { get; set; }
        public Account Account { get; set; }

    }
}
