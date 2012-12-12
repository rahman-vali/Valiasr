namespace Valiasr.NewDomain
{
    using System;
    using System.Runtime.Serialization;

    public class Customer : Correspondent
    {
        public string No { get; set; }

        public bool HagheBardasht { get; set; }

        public float Portion { get; set; }       
    }

    public class Vakil : Correspondent
    {
        public DateTime? EndDate { get; set; }

        public DateTime StartDate { get; set; }
    }

    //    public class Zamen : Customer
    //    {
    //        public ICollection<Vam> Vams { get; set; }
    //    }
}