namespace CodingTest.Core.Services
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Reflection;

    using CodingTest.Common.Plugins;

    using MvvmCross.Plugins.File;

    public class PluginLoaderService<TSomePlugin> : IPluginManager<TSomePlugin>
        where TSomePlugin : IPlugin
    {
        private readonly string path;

        public PluginLoaderService(string path)
        {
            this.path = path;
        }

        private readonly IMvxFileStore file;

        public PluginLoaderService(IMvxFileStore file)
        {
            this.file = file;
        }

        #region Implementation of IPluginManager

        public void Start()
        {
            if (!Directory.Exists(this.path))
            {
                throw new FileNotFoundException($"No such folder {this.path}");
            }

            var dllFileNames = Directory.GetFiles(this.path, "*.dll");           
                            
            ICollection<Assembly> assemblies = new List<Assembly>(dllFileNames.Count());
            foreach (var assembly in dllFileNames.Select(dllFile => Assembly.Load(AssemblyName.GetAssemblyName(dllFile))))
            {
                assemblies.Add(assembly);
            }

            var pluginType = typeof(TSomePlugin);

            ICollection<Type> pluginTypes = new List<Type>();
            foreach (var assembly in assemblies)
            {
                if (assembly == null)
                {
                    continue;
                }

                var types = assembly.GetTypes();
                foreach (var type in types)
                {
                    if (type.IsInterface || type.IsAbstract)
                    {
                        continue;
                    }
                    else
                    {
                        if (type.GetInterface(pluginType.FullName) != null)
                        {
                            pluginTypes.Add(type);
                        }
                    }
                }
            }
        }

        public List<TSomePlugin> Plugins { get; } = new List<TSomePlugin>();

        #endregion
    }
}
