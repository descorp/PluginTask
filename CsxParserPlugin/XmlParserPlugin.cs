namespace XmlParser
{
    using System.IO;
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
                var serializer = new XmlSerializer(typeof(TData));

                // A FileStream is needed to read the XML document.
                var fs = File.ReadAllLines(filepath);



                // Declare an object variable of the type to be deserialized.
                var data = (TData)serializer.Deserialize(reader);


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