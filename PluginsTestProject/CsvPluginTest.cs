using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace PluginsTestProject
{
    using System.Runtime.Remoting.Messaging;

    using CodingTest.Common.Model;

    using CsxParserPlugin;

    using XmlParserPlugin;

    [TestClass]
    public class CsvPluginTest
    {

        [TestMethod]    
        public void ParserIsNotNull()
        {
            var parser = new CsvParserPlugin<DataSample>().Parser;
            Assert.IsNotNull(parser);
        }

        [TestMethod]
        public void ParserParse()
        {
            var parser = new CsvParserPlugin<DataSample>().Parser;
            var data = parser.Process("..\\..\\Files\\datasample.csv").Result;

            Assert.IsNotNull(data);
            CollectionAssert.AllItemsAreNotNull(data);
            CollectionAssert.DoesNotContain(data, new DataSample());
        }
    }
}
