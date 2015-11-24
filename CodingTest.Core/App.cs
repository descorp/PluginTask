using Cirrious.CrossCore;
using Cirrious.CrossCore.Plugins;
using Cirrious.MvvmCross.ViewModels;
using CodingTest.Common.Model;
using CodingTest.Common.Plugins;
using CodingTest.Core.Services;

namespace CodingTest.Core
{
    using CodingTest.Core.ViewModels;

    public class App : MvxApplication
    {
        public override void Initialize()
        {           
            RegisterAppStart<RootViewModel>();
        }

        public override void LoadPlugins(IMvxPluginManager pluginManager)
        {
            base.LoadPlugins(pluginManager);

            Mvx.Resolve<IPluginManager<IParserPlugin<DataSample>>>().Start();
        }
    }
}
