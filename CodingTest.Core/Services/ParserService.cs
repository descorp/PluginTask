using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingTest.Core.Services
{
    using System.IO;

    using CodingTest.Common.Model;
    using CodingTest.Common.Plugins;

    using MvvmCross.Plugins.Messenger;
    using MvvmCross.Plugins.Messenger.ThreadRunners;

    public class ParserService : IParserService<DataSample>
    {
        private readonly IPluginManager<IParserPlugin<DataSample>> plugins;

        private readonly IMvxMessenger hub;

        private MvxSubscriptionToken mvxSubscriptionToken;

        public ParserService(IPluginManager<IParserPlugin<DataSample>> plugins)
        {
            this.plugins = plugins;
            this.hub = hub;
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
