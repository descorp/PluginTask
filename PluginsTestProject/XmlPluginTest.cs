using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace PluginsTestProject
{
    using System.Runtime.Remoting.Messaging;

    using CodingTest.Common.Model;

    using XmlParserPlugin;

    [TestClass]
    public class XmlPluginTest
    {

        [TestMethod]
        public void ParserIsNotNull()
        {
            var parser = new XmlParserPlugin<DataSample>().Parser;
            Assert.IsNotNull(parser);
        }

        [TestMethod]
        public void ParserParse()
        {
            var parser = new XmlParserPlugin<DataSample>().Parser;
            var data = parser.Process("..\\..\\Files\\datasample.xml").Result;

            Assert.IsNotNull(data);
            CollectionAssert.AllItemsAreNotNull(data);
            CollectionAssert.DoesNotContain(data, new DataSample());
        }
    }
}
