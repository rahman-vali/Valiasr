namespace Valiasr.Domain
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;

    public class Kol
    {
        public Kol() 
        {
            this.Moins = new Collection<Moin>();
        }
        public int Id{get;set;}

        public int Kol_Code { get; set; }
        public string  Description{get;set;}
        public int Kind {get;set;}
        public int Last_Date{get;set;}

        public virtual ICollection<Moin> Moins{get;set;}
    }

    public class Moin
    {
        public Moin()
        {
          //  this.Kol = new Kol();
            this.Accounts = new Collection<Account>();
        }
        public int Id { get; set; }

        public int Kol_Code { get; set; }
        public string Moin_Code { get; set; }
        public int Moin_InKol_Code { get; set; }
        public string Description {get;set;}
        public bool Hesab_Have {get;set;}
        public short YearEnd_Kind {get;set;}
        public int Last_Date{get;set;}

        public virtual Kol Kol {get;set;}
        public virtual ICollection<Account> Accounts{get;set;}
    }
    public class Account
    {
        public Account()
        {
            Id = Guid.NewGuid();
            //this.Moin = new Moin();
            this.Persons = new Collection<Person>();
        }

        #region Properties

        //Shomare Hesab
        public string Moin_Code { get; set; }
        public string Hesab_No { get; set; }
        public string No { get; set; }

        /// Mojodi Hesab
        public double Balance { get; set; }

        public string Description { get; set; }

        public Guid Id { get; set; }

        public virtual Moin Moin{get;set;}
        //Vokalaye Hesab
        public Collection<Person> Persons { get; set; }

        #endregion

        #region Public Methods

        public bool Bardasht(string customerNo, double amount)
        {
            var customer = this.Persons.OfType<Customer>().FirstOrDefault(o => o.No == customerNo);            
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