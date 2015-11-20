using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XmlParser
{
    using CodingTest.Common.Model;
    using CodingTest.Common.Parser;
    using CodingTest.Common.Plugins;

    public class XmlParserPlugin : IParserPlugin
    {
        private string name;

        private IPluginHost host;

        private string fileFormat;

        private IParser<DataSample> parser;

        #region Implementation of IPlugin

        public string Name
        {
            get
            {
                return this.name;
            }
            set
            {
                this.name = value;
            }
        }

        public IPluginHost Host
        {
            get
            {
                return this.host;
            }
            set
            {
                this.host = value;
            }
        }

        #endregion

        #region Implementation of IParserPlugin

        public string FileFormat => ".xml";

        public IParser<DataSample> Parser
        {
            get
            {
                return this.parser;
            }
        }

        #endregion
    }
}
