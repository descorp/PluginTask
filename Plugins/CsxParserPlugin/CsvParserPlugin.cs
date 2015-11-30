using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CodingTest.Common.Parser;
using CodingTest.Common.Plugins;

namespace CsxParserPlugin
{
    public class CsvParserPlugin<TData> : IParserPlugin<TData>
    {
        public CsvParserPlugin()
        {
            Parser = new CsvParser();
        }

        #region IParserPlugin<TData> Members

        public string Name { get; set; } = "CsvParserPlugin";

        #endregion

        #region Nested type: CsvParser

        internal class CsvParser : IParser<TData>
        {
            public CsvParser()
            {
                Status = "ok";
            }


            #region Implementation of IParser<TData>

            public string Status { get; }

            public async Task<List<TData>> Process(string filename)
            {
                var data = new List<TData>();
                var fs = File.ReadAllLines(filename).ToList();

                var fields = fs[0].Split(new[] {" ", "   "}, StringSplitOptions.RemoveEmptyEntries);

                foreach (var line in fs.Skip(fs.IndexOf("Content:") + 2))
                {
                    var values = line.Split(';');
                    var item = Activator.CreateInstance<TData>();

                    for (var i = 0; i < fields.Length; i++)
                    {
                        typeof (TData).GetField(fields[i]).SetValue(item, values[i]);
                    }

                    data.Add(item);
                }

                return data;
            }

            #endregion
        }

        #endregion

        #region Implementation of IParserPlugin

        public string FileFormat => ".csv";

        public IParser<TData> Parser { get; }

        #endregion
    }
}