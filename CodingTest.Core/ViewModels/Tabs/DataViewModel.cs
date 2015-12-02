namespace CodingTest.Core.ViewModels.Tabs
{
    using System.Collections.ObjectModel;

    using Cirrious.MvvmCross.ViewModels;

    using CodingTest.Common.Model;
    using CodingTest.Core.Services;

    using MvvmCross.Plugins.Messenger;

    public class DataViewModel : MvxViewModel
    {
        private readonly IMvxMessenger hub;

        private readonly IDataStorage<DataSample> data;

        private MvxSubscriptionToken mvxSubscriptionToken;

        public DataViewModel(IMvxMessenger hub, IDataStorage<DataSample> data)
        {
            this.hub = hub;
            this.data = data;

            this.mvxSubscriptionToken = hub.Subscribe<NewDataMessage>(a => this.RaisePropertyChanged(() => this.DataList));
        }

        public ObservableCollection<ObservedFileItem<DataSample>> DataList => new ObservableCollection<ObservedFileItem<DataSample>>(this.data.ObservedFiles);
    }
}
