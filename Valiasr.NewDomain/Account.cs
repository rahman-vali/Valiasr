namespace Valiasr.NewDomain
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;

    public class Account
    {
        public Account()
        {
            Id = Guid.NewGuid();
            this.Correspondents = new Collection<Correspondent>();
        }

        #region Properties

        //Shomare Hesab
        public string No { get; set; }

        /// Mojodi Hesab
        public double Balance { get; set; }

        public string Description { get; set; }

        public Guid Id { get; set; }

        //Vokalaye Hesab
        public ICollection<Correspondent> Correspondents { get; set; }

        #endregion

        #region Public Methods

        public bool Bardasht(string customerNo, double amount)
        {
            var customer = this.Correspondents.OfType<Customer>().FirstOrDefault(o => o.No == customerNo);
            if (customer != null &&
                customer.Balegh &&
                customer.HagheBardasht &&
                amount <= customer.Portion/100 * this.Balance)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        #endregion
    }


}