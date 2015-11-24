namespace CodingTest.Core.Services
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Reflection;

    using CodingTest.Common.Plugins;

    using MvvmCross.Plugins.File;

    //IParserPlugin<TData>

    public class PluginLoaderService<TPlugin> : IPluginManager<TPlugin>
        where TPlugin : class, IPlugin
    {
        private readonly string path;

        public PluginLoaderService(string path)
        {
            this.path = path;
            Plugins = new List<TPlugin>();
        }

        #region Implementation of IPluginManager

        public void Start()
        {
            if (!Directory.Exists(this.path))
            {
                Directory.CreateDirectory(path);
            }

            var dllFileNames = Directory.GetFiles(this.path, "*.dll");

            var pluginType = typeof(TPlugin);
            Type[] dataType = new Type[] { };
            if (pluginType.IsGenericType)
            {
                dataType = pluginType.GetGenericArguments();
                pluginType = pluginType.GetGenericTypeDefinition();
            }

            foreach (var fileName in dllFileNames)
            {
                var assembly = Assembly.Load(AssemblyName.GetAssemblyName(fileName));
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
                        if (type.GetInterface(pluginType.Name) != null)
                        {
                            var plugin = dataType != null ? type.MakeGenericType(dataType) : type;
                            var item = Activator.CreateInstance(plugin);

                            Plugins.Add(item as TPlugin);
                        }
                    }
                }
            }
        }

        public List<TPlugin> Plugins { get; }

        #endregion
    }
}
