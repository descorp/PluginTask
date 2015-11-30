using System.Windows.Threading;
using Cirrious.CrossCore.Platform;
using Cirrious.MvvmCross.ViewModels;
using Cirrious.MvvmCross.Wpf.Platform;
using Cirrious.MvvmCross.Wpf.Views;

namespace CodingTest.MainUI
{
    using Cirrious.CrossCore;

    using Common.Model;
    using Common.Plugins;
    using Core.Services.Interfaces;
    using Service;

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

            Mvx.ConstructAndRegisterSingleton<IAssemblyExtractor<IParserPlugin<DataSample>>, AssemblyExtractor<IParserPlugin<DataSample>>>();
            Mvx.ConstructAndRegisterSingleton<IAppContextService, AppContextService >();
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
