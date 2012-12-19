namespace Valiasr.DataAccess
{
    using System.Linq;

    using Valiasr.Domain;

    public class PersonDAO
    {
        public Person GetPerson(string melliIdentity)
        {
            var context = new ValiasrContext("Valiasr.ce");
            return (from p in context.Correspondents where p.MelliIdentity == melliIdentity select p).FirstOrDefault();           
        }
    }
}
