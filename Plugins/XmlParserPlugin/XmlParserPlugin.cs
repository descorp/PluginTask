using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using CodingTest.Common.Parser;
using CodingTest.Common.Plugins;

namespace XmlParserPlugin
{
    public class XmlParserPlugin<TData> : IParserPlugin<TData>
    {
        #region IParserPlugin<TData> Members

        public string Name { get; set; }

        #endregion

        #region Nested type: XmlParser

        internal class XmlParser : IParser<TData>
        {
            public XmlParser()
            {
                Status = "ok";
            }

            #region Implementation of IParser<TData>

            public string Status { get; }

            public async Task<List<TData>> Process(string filepath)
            {
                var serializer = new XmlSerializer(typeof(List<TData>));

                // A FileStream is needed to read the XML document.
                var fs = new FileStream(filepath, FileMode.Open);
                var reader = XmlReader.Create(fs);

                // Declare an object variable of the type to be deserialized.
                var data = (List<TData>)serializer.Deserialize(reader);

                fs.Close();

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