namespace Valiasr.NewDomain
{
    using System;
    using System.Collections.Generic;

    public class Account
    {
        #region Properties

        /// Mojodi Hesab
        public double Balance { get; set; }

        public string Description { get; set; }

        public Guid Id { get; set; }

        //Vokalaye Hesab
        public ICollection<Correspondent> Correspondent { get; set; }

        #endregion

        #region Public Methods

        public bool Bardasht(Customer customer, double amount)
        {
            if (this.Correspondent.Contains(customer) &&
                customer.Balegh && 
                customer.HagheBardasht && 
                amount <= customer.Portion * this.Balance)
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