using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using CodingTest.Common.Parser;
using CodingTest.Common.Plugins;

namespace XmlParserPlugin
{
    using System;
    using System.ComponentModel;
    using System.Linq;
    using System.Net;
    using System.Reflection;

    public class XmlParserPlugin<TData> : IParserPlugin<TData>
        where TData : class
    {
        public XmlParserPlugin()
        {
            this.Parser = new XmlParser();
        }

        #region IParserPlugin<TData> Members

        public string Name { get; set; } = "XmlParserPlugin";

        #endregion

        #region Nested type: XmlParser

        internal class XmlParser : IParser<TData>
        {
            private Type type;

            private Dictionary<string, PropertyInfo> propertyInfos;

            private Dictionary<string, FieldInfo> fieldInfos;

            public XmlParser()
            {
                this.Status = "ok";
                this.type = typeof(TData);
                this.propertyInfos = this.type.GetProperties().ToDictionary(p => p.Name.ToLower(), p => p);
                this.fieldInfos = this.type.GetFields().Where(f => f.IsPublic).ToDictionary(f => f.Name.ToLower(), f => f);
            }

            #region Implementation of IParser<TData>

            public string Status { get; }

            public async Task<List<TData>> Process(string filename)
            {
                var data = new List<TData>();


                var reader = new XmlTextReader(filename);
                TData currentItem = null;

                while (reader.Read())
                {
                    switch (reader.NodeType)
                    {
                        case XmlNodeType.Element:
                            if (reader.Name == "value")
                            {
                                if (currentItem != null)
                                {
                                    data.Add(this.DeepCopy(currentItem));
                                }

                                currentItem = Activator.CreateInstance(this.type) as TData;

                                if (reader.HasAttributes)
                                {
                                    while (reader.MoveToNextAttribute())
                                    {
                                        this.SetAttribute(currentItem, reader.Name, reader.Value);
                                    }
                                }
                            }
                            break;
                        case XmlNodeType.Text:
                        case XmlNodeType.EntityReference:
                        case XmlNodeType.Attribute:
                            if (currentItem != null)
                            {
                                this.SetAttribute(currentItem, reader.Name, reader.Value);
                            }
                            break;
                        case XmlNodeType.EndElement:
                        case XmlNodeType.EndEntity:
                            if (reader.Name == "values")
                            {
                                return data;
                            }

                            break;
                    }

                }

                return data;

                #endregion
            }

            private void SetAttribute(TData currentItem, string param, string rawValue)
            {
                if (this.propertyInfos.ContainsKey(param.ToLower()))
                {
                    var propertyInfo = this.propertyInfos[param];
                    var tryParse = propertyInfo.PropertyType.GetMethods().FirstOrDefault(m => m.Name == "Parse");

                    if (tryParse == null)
                    {
                        return;
                    }

                    try
                    {
                        var value = tryParse.Invoke(null, new[] { rawValue });
                        propertyInfo.SetValue(currentItem, value);
                    }
                    catch
                    {
                        // ignored
                    }

                    return;
                }

                if (this.fieldInfos.ContainsKey(param.ToLower()))
                {
                    var fieldInfo = this.fieldInfos[param];
                    var tryParse = fieldInfo.FieldType.GetMethod("Parse");

                    if (tryParse == null)
                    {
                        return;
                    }

                    try
                    {
                        var value = tryParse.Invoke(null, new[] { rawValue });
                        fieldInfo.SetValue(currentItem, value);
                    }
                    catch
                    {
                        // ignored
                    }
                }
            }

            private TData DeepCopy(TData item)
            {
                var temp = Activator.CreateInstance(this.type) as TData;

                foreach (var info in this.propertyInfos.Values)
                {
                    var value = info.GetValue(item);
                    if (value != null)
                    {
                        info.SetValue(temp, value);
                    }
                }

                foreach (var info in this.fieldInfos.Values)
                {
                    var value = info.GetValue(item);
                    if (value != null)
                    {
                        info.SetValue(temp, value);
                    }
                }

                return temp;
            }
        }

        #endregion

        #region Implementation of IParserPlugin

        public string FileFormat => ".xml";

        public IParser<TData> Parser { get; }

        #endregion
    }
}