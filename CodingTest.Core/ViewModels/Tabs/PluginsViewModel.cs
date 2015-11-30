namespace CodingTest.Core.ViewModels.Tabs
{
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Windows.Input;

    using Cirrious.MvvmCross.ViewModels;

    using CodingTest.Common.Model;
    using CodingTest.Common.Plugins;
    using CodingTest.Core.Services.Interfaces;
    using CodingTest.Core.Utils;

    public class PluginsViewModel : MvxViewModel
    {
        private readonly IPluginManager<IParserPlugin<DataSample>> plugins;

        public PluginsViewModel(IPluginManager<IParserPlugin<DataSample>> plugins)
        {
            this.plugins = plugins;
        }

        private MvxCommand refreshPluginsCommand;

        public ICommand RefreshPluginsCommand => this.refreshPluginsCommand ?? (this.refreshPluginsCommand = new MvxCommand(this.DoRefreshPluginsCommand));

        private string defaultFolder = FileSystemHelper.AppLocalFolder + "\\Plugins";

        public string DefaultFolder
        {
            get
            {
                return this.defaultFolder;
            }
            set
            {
                this.defaultFolder = value;
                this.RaisePropertyChanged(() => this.DefaultFolder);
            }
        }

        public ObservableCollection<PluginItem> PluginsList => new ObservableCollection<PluginItem>(this.plugins.Plugins.Select(n => new PluginItem(n)));

        private void DoRefreshPluginsCommand()
        {
            this.plugins.ScanFolder(this.DefaultFolder);
            this.RaisePropertyChanged(() => this.PluginsList);
        }
    }
}
