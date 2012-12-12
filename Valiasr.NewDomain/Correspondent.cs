namespace Valiasr.NewDomain
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;

    public class Correspondent : Person
    {
        public Correspondent()
        {
            this.Accounts = new Collection<Account>();
        }
        public ICollection<Account> Accounts { get; set; }

        protected bool Equals(Correspondent other)
        {
            return this.Id.Equals(other.Id) && string.Equals(this.Firstname, other.Firstname) && string.Equals(this.Lastname, other.Lastname);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = this.Id.GetHashCode();
                hashCode = (hashCode * 397) ^ (this.Firstname != null ? this.Firstname.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (this.Lastname != null ? this.Lastname.GetHashCode() : 0);
                return hashCode;
            }
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }
            if (ReferenceEquals(this, obj))
            {
                return true;
            }
            if (obj.GetType() != this.GetType())
            {
                return false;
            }
            return Equals((Correspondent)obj);
        }

    }
}