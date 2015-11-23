namespace XmlParser
{
    using System;
    using System.Collections;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Xml;
    using System.Xml.Serialization;

    using CodingTest.Common.Parser;
    using CodingTest.Common.Plugins;

    public class CsvParserPlugin<TData> : IParserPlugin<TData>
    {
        #region IParserPlugin<TData> Members

        #region Implementation of IPlugin

        public string Name { get; set; }

        #endregion

        #endregion

        #region Nested type: XmlParser

        internal class XmlParser : IParser<TData>
        {
            #region Implementation of IParser<TData>

            public string Status { get; }

            public Task<TData> Process(string filepath)
            {
                // A FileStream is needed to read the XML document.
                var fs = File.ReadAllLines(filepath);

                var fields = fs[0].Split(new[] {" ", "   "}, StringSplitOptions.RemoveEmptyEntries);

                    foreach (var line in fs.Skip(1))
                    {
                        var item = Activator.CreateInstance<TData>();

                        foreach (var field in fields)
                        {
                            typeof(TData).GetField(field)
                        }
                    }

                return data;
            }

            #endregion
        }

        #endregion

        #region Implementation of IParserPlugin

        public string FileFormat => ".xml";

        public IParser<TData> Parser { get; }

        #endregion


    }
}