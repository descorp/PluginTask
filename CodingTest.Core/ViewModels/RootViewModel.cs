using Cirrious.MvvmCross.ViewModels;

namespace CodingTest.Core.ViewModels
{
    using Cirrious.CrossCore;

    using CodingTest.Core.ViewModels.Tabs;

    public class RootViewModel : MvxViewModel
    {
        public RootViewModel()
        {
            Data = Mvx.IocConstruct<DataViewModel>();
            Plugins = Mvx.IocConstruct<PluginsViewModel>();
            Folder = Mvx.IocConstruct<FolderControllerViewModel>();
        }


        public DataViewModel Data { get; set; }

        public PluginsViewModel Plugins { get; set; }

        public FolderControllerViewModel Folder { get; set; }

    }
}
