namespace CodingTest.Core.Services.Interfaces
{
    using System.Collections.Generic;

    using CodingTest.Common.Plugins;

    public interface IPluginManager<TSomePlugin>
        where TSomePlugin : IPlugin
    {
        void ScanFolder(string folder);

        List<TSomePlugin> Plugins { get; }
    }
}