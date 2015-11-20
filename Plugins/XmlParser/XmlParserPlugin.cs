using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XmlParser
{
    using System.ComponentModel.DataAnnotations;
    using System.IO;
    using System.Xml;
    using System.Xml.Linq;
    using System.Xml.Serialization;

    using CodingTest.Common.Model;
    using CodingTest.Common.Parser;
    using CodingTest.Common.Plugins;

    public class XmlParserPlugin<TData> : IParserPlugin<TData>
    {
        #region Implementation of IPlugin

        public string Name { get; set; }

        #endregion

        #region Implementation of IParserPlugin

        public string FileFormat => ".xml";

        public IParser<TData> Parser { get; }

        #endregion

        internal class XmlParser : IParser<TData>
        {
            #region Implementation of IParser<TData>

            public string Status { get; }

            public List<TData> Process(string filepath)
            {

                XmlSerializer serializer = new XmlSerializer(typeof(TData));

                // A FileStream is needed to read the XML document.
                var fs = FileStream(filename, FileMode.Open);
                XmlReader reader = XmlReader.Create(fs);

                // Declare an object variable of the type to be deserialized.
                var data = (TData)serializer.Deserialize(reader);

                fs.Close();

                return data;
            }

            #endregion
        }
    }
}
