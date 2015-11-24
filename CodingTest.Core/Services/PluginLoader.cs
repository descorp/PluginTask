using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingTest.Core.Services
{
    using System.Reflection;

    using Cirrious.CrossCore;

    using CodingTest.Common.Plugins;

    using MvvmCross.Plugins.File;

    public class PluginLoaderService<TPlugin> : IPluginManager<TPlugin>
     where TPlugin : class, IPlugin
    {
        private readonly IMvxFileStore store;

        public PluginLoaderService(IMvxFileStore store)
        {
            this.store = store;
            Plugins = new List<TPlugin>();
        }

        #region Implementation of IPluginManager

        public void ScanFolder(string path)
        {
            this.store.EnsureFolderExists(path);

            var dllFileNames = this.store.GetFilesIn(path).Where(f => f.EndsWith(".dll"));

            var pluginType = typeof(TPlugin);
            Type[] dataType = new Type[] { };
            if (pluginType.GetGenericArguments() != null)
            {
                dataType = pluginType.GetGenericArguments();
                pluginType = pluginType.GetGenericTypeDefinition();
            }

            foreach (var fileName in dllFileNames)
            {
                var assembly = Assembly.Load( new AssemblyName(fileName));
                if (assembly == null)
                {
                    continue;
                }

                var types = assembly.GetTypes();
                foreach (var type in types)
                {
                    var interfaces = type.GetInterfaces();
                    if (interfaces == null || !interfaces.Contains(typeof(IPlugin)))
                    {
                        continue;
                    }

                    var plugin = dataType != null ? type.MakeGenericType(dataType) : type;
                    var item = Activator.CreateInstance(plugin);

                    this.Plugins.Add(item as TPlugin);
                }
            }
        }

        public List<TPlugin> Plugins { get; }

        #endregion
    }
}
