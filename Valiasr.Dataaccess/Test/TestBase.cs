namespace Valiasr.DataAccess.Test
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Reflection;

    using Xunit;

    public class TestBase
    {

        /// <summary>
        /// File database ra delete mikonad va az aan motmaen mishavad
        /// </summary>
        [Fact]
        public void Setup()
        {
            var dllPath = (new Uri(Assembly.GetExecutingAssembly().GetName().CodeBase)).LocalPath;
            var fullFilePath = dllPath.Remove(dllPath.LastIndexOf("\\Valiasr.DataAccess")) + "\\Valiasr.DataAccess\\App_Data\\Valiasr.Ce.sdf";
            File.Delete(fullFilePath);

            Assert.False(File.Exists(fullFilePath));
        }

        public TestBase()
        {
            this.Setup();
        }

        public IEnumerable<char> GenerateRandomString(int len)
        {
            int i = 0;
            while (i < len)
            {
                var rand = (new Random()).Next(122 - 97);
                yield return Convert.ToChar(rand + 97);
                i++;
            }
        }
    }
}
