using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace PluginsTestProject
{
    using System.Runtime.Remoting.Messaging;

    using CodingTest.Common.Model;

    using TextParserPlugin;

    using XmlParserPlugin;

    [TestClass]
    public class TextPluginTest
    {

        [TestMethod]
        public void ParserIsNotNull()
        {
            var parser = new TextParserPlugin<DataSample>().Parser;
            Assert.IsNotNull(parser);
        }

        [TestMethod]
        public void ParserParse()
        {
            var parser = new TextParserPlugin<DataSample>().Parser;
            var data = parser.Process("..\\..\\Files\\datasample.txt").Result;

            Assert.IsNotNull(data);
            CollectionAssert.AllItemsAreNotNull(data);
            CollectionAssert.DoesNotContain(data, new DataSample());
        }
    }
}
