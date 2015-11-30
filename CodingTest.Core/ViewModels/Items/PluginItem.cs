namespace CodingTest.Core.ViewModels.Tabs
{
    using Cirrious.MvvmCross.ViewModels;

    using CodingTest.Common.Model;
    using CodingTest.Common.Plugins;

    public class PluginItem : MvxNotifyPropertyChanged
    {
        private readonly IParserPlugin<DataSample> context;

        public PluginItem(IParserPlugin<DataSample> plugin)
        {
            this.context = plugin;
        }

        public string Name => this.context.Name;

        public string Format => this.context.FileFormat;

        public string Status => this.context.Parser != null ? this.context.Parser.Status : "error";
    }
}