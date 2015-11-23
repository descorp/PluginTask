namespace XmlParser
{
    using System.IO;
    using System.Xml;
    using System.Xml.Serialization;

    using CodingTest.Common.Parser;
    using CodingTest.Common.Plugins;

    public class XmlParserPlugin<TData> : IParserPlugin<TData>
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

            public TData Process(string filepath)
            {
                var serializer = new XmlSerializer(typeof(TData));

                // A FileStream is needed to read the XML document.
                var fs = new FileStream(filepath, FileMode.Open);
                var reader = XmlReader.Create(fs);

                // Declare an object variable of the type to be deserialized.
                var data = (TData)serializer.Deserialize(reader);

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