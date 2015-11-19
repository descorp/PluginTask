using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CodingTest.Core.Plugins
{
    public interface IPlugin
    {
        string Name { get; set; }
        IPluginHost Host { get; set; }
        void Show();
    }

    public interface IPluginHost
    {
        bool Register(IPlugin ipi);
    }
}
