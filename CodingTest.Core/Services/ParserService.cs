using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodingTest.Core.Services
{
    using CodingTest.Common.Model;
    using CodingTest.Common.Plugins;
    using CodingTest.Core.Services.Interfaces;

    using MvvmCross.Plugins.Messenger;

    public class ParserService : IParserService<DataSample>
    {
        private readonly IPluginManager<IParserPlugin<DataSample>> plugins;

        private MvxSubscriptionToken mvxSubscriptionToken;

        public ParserService(IPluginManager<IParserPlugin<DataSample>> plugins)
        {
            this.plugins = plugins;
            plugins.ScanFolder("Plugins");
        }

        #region Implementation of IParserService<DataSample>

        public async Task<IEnumerable<DataSample>> Parse(string filepath)
        {
            List<DataSample> data = null;
            foreach (var plugin in this.plugins.Plugins.Where(p => filepath.EndsWith(p.FileFormat)))
            {
                data = await plugin.Parser.Process(filepath);
            }

            return data;
        }

        #endregion
    }
}
