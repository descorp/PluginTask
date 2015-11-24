using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CodingTest.Common.Parser;
using CodingTest.Common.Plugins;

namespace TextParserPlugin
{
    public class TextParserPlugin<TData> : IParserPlugin<TData>
    {
        public TextParserPlugin()
        {
            Parser = new TextParser();
        }

        #region IParserPlugin<TData> Members

        public string Name { get; set; }

        #endregion

        #region Nested type: CsvParser

        internal class TextParser : IParser<TData>
        {
            #region Implementation of IParser<TData>

            public string Status { get; }

            public async Task<List<TData>> Process(string filepath)
            {
                var data = new List<TData>();
                var fs = File.ReadAllLines(filepath).ToList();

                var fields = fs[0].Split(new[] { " ", "   " }, StringSplitOptions.RemoveEmptyEntries);

                foreach (var line in fs.Skip(fs.IndexOf("Content:") + 2))
                {
                    var values = line.Split(';');
                    var item = Activator.CreateInstance<TData>();

                    for (var i = 0; i < fields.Length; i++)
                    {
                        typeof(TData).GetField(fields[i]).SetValue(item, values[i]);
                    }

                    data.Add(item);
                }

                return data;
            }

            #endregion
        }

        #endregion

        #region Implementation of IParserPlugin

        public string FileFormat => ".txt";

        public IParser<TData> Parser { get; }

        #endregion
    }
}