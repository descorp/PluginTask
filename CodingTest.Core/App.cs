using Cirrious.CrossCore;
using Cirrious.MvvmCross.ViewModels;
using CodingTest.Common.Model;
using CodingTest.Common.Plugins;
using CodingTest.Core.Services;

namespace CodingTest.Core
{
    using CodingTest.Core.Services.Interfaces;
    using CodingTest.Core.ViewModels;

    public class App : MvxApplication
    {
        public override void Initialize()
        {

            Mvx.ConstructAndRegisterSingleton<IPluginManager<IParserPlugin<DataSample>>, PluginLoaderService<IParserPlugin<DataSample>>>();
            Mvx.ConstructAndRegisterSingleton<IFolderObserver, FolderObserverService>();
            Mvx.ConstructAndRegisterSingleton<IParserService<DataSample>, ParserService>();
            Mvx.ConstructAndRegisterSingleton<IDataStorage<DataSample>, DataStorageService>();

            RegisterAppStart<RootViewModel>();
        }
    }
}
