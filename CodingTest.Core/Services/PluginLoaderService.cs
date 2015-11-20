namespace CodingTest.Core.Services
{
    using System.Collections.Generic;

    using CodingTest.Common.Plugins;

    public class PluginLoaderService : IPluginManager
    {
        private List<IParserPlugin> plugins;

        #region Implementation of IPluginManager

        public void Start()       
        {
            string[] dllFileNames = null;
            if (Directory.Exists(path))
            {
                dllFileNames = Directory.GetFiles(path, "*.dll");
            }
        }

        public List<IParserPlugin> Plugins
        {
            get
            {
                return this.plugins;
            }
        }

        #endregion
    }

    public interface IPluginManager
    {
        void Start();

        List<IParserPlugin> Plugins { get; }
    }
}
