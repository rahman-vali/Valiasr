namespace Valiasr.DataAccess
{
    using System.Linq;

    using Valiasr.Domain;

    public class PersonDAO
    {
        public Correspondent GetPerson(string melliIdentity)
        {
            var context = new ValiasrContext("Valiasr.ce");
            return (from p in context.Correspondents where p.MelliIdentity == melliIdentity select p).FirstOrDefault();           
        }
        public void AddPerson(Correspondent person)
        {
            var context = new ValiasrContext("Valiasr.ce");
            Correspondent correspondent = person;
            context.Correspondents.Add(correspondent);
            context.SaveChanges();

        }
    }
}
