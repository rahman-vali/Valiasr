using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Valiasr.Domain;

namespace Valiasr.DataAccess
{
    [TestClass]
    public class DbTest

    {
        [TestMethod]
        public void dbTest()
        {
            var context = new ValiasrContext("Valiasr");
            context.Persons.Add(new Person(){Firstname = "ali",Lastname = "ahmadi",ContactInfo = {Address = "babol",Tellno = 12435}});
            context.SaveChanges();
            Assert.IsTrue(context.Database.Exists());
        }
    }
}
