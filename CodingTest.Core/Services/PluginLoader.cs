using System.Collections.Generic;
using System.Linq;

namespace CodingTest.Core.Services
{
    using Common.Plugins;
    using Interfaces;

    using MvvmCross.Plugins.File;

    public class PluginLoaderService<TPlugin> : IPluginManager<TPlugin>
     where TPlugin : class, IPlugin
    {
        private readonly IAssemblyExtractor<TPlugin> extractor;

        private readonly IMvxFileStore store;

        public PluginLoaderService(IAssemblyExtractor<TPlugin> extractor, IMvxFileStore store)
        {
            this.extractor = extractor;
            this.store = store;
            Plugins = new List<TPlugin>();
        }

        #region Implementation of IPluginManager

        public void ScanFolder(string path)
        {
            this.store.EnsureFolderExists(path);

            var dllFileNames = this.store.GetFilesIn(path).Where(f => f.EndsWith(".dll"));

            foreach (var item in dllFileNames.SelectMany( n => this.extractor.Extract(n)).Where(item => item != null))
            {
                this.Plugins.Add(item);
            }
        }

        public List<TPlugin> Plugins { get; }

        #endregion
    }
}
