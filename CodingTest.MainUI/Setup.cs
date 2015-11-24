using System.Windows.Threading;
using Cirrious.CrossCore.Platform;
using Cirrious.MvvmCross.ViewModels;
using Cirrious.MvvmCross.Wpf.Platform;
using Cirrious.MvvmCross.Wpf.Views;

namespace CodingTest.MainUI
{
    using Cirrious.CrossCore;

    using CodingTest.Common.Model;
    using CodingTest.Common.Plugins;
    using CodingTest.Core.Services;

    public class Setup
        : MvxWpfSetup
    {
        public Setup(Dispatcher dispatcher, IMvxWpfViewPresenter presenter)
            : base(dispatcher, presenter)
        {
        }

        #region Overrides of MvxSetup

        protected override void InitializeFirstChance()
        {
            base.InitializeFirstChance();

            Mvx.RegisterSingleton<IPluginManager<IParserPlugin<DataSample>>>(new PluginLoaderService<IParserPlugin<DataSample>>("Plugins"));
        }

        #endregion

        protected override IMvxApplication CreateApp()
        {
            return new Core.App();
        }
		
        protected override IMvxTrace CreateDebugTrace()
        {
            return new DebugTrace();
        }
    }
}
