namespace CodingTest.MainUI.Service

{
    using System;
    using System.Collections.Generic;
    using System.Reflection;

    using CodingTest.Common.Plugins;
    using CodingTest.Core.Services.Interfaces;

    //IParserPlugin<TData>

    public class AssemblyExtractor<TPlugin> : IAssemblyExtractor<TPlugin>
        where TPlugin : class, IPlugin
    {
        private readonly Type[] datatype;

        private readonly Type pluginType;

        public AssemblyExtractor()
        {
            var type = typeof(TPlugin);

            if (!type.IsGenericType)
            {
                this.pluginType = type;
                return;
            }

            this.datatype = type.GetGenericArguments();
            this.pluginType = type.GetGenericTypeDefinition();
        }

        #region Implementation of IPluginManager

        public IEnumerable<TPlugin> Extract(string fileName)
        {
            var plugins = new List<TPlugin>();

            var assembly = Assembly.Load(AssemblyName.GetAssemblyName(fileName));
            if (assembly == null)
            {
                return null;
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
                    if (type.GetInterface(this.pluginType.Name) == null)
                    {
                        continue;
                    }

                    var plugin = this.datatype != null ? type.MakeGenericType(this.datatype) : type;
                    var item = Activator.CreateInstance(plugin);

                    plugins.Add(item as TPlugin);
                }
            }

            return plugins;
        }

        #endregion
    }
}
