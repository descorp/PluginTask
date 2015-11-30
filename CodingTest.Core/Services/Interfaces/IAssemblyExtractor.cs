namespace CodingTest.Core.Services.Interfaces
{
    using System.Collections.Generic;

    using CodingTest.Common.Plugins;

    public interface IAssemblyExtractor<TPlugin> where TPlugin : class, IPlugin
    {
        IEnumerable<TPlugin> Extract(string fileName);
    }
}