namespace CodingTest.Core.Services
{
    using System.Collections.Generic;

    using CodingTest.Common.Plugins;

    public interface IPluginManager<TSomePlugin>
    {
        void Start();

        List<TSomePlugin> Plugins { get; }
    }
}