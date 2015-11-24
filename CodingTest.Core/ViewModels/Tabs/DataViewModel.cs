namespace CodingTest.Core.ViewModels.Tabs
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;

    using Cirrious.MvvmCross.ViewModels;

    using CodingTest.Common.Model;
    using CodingTest.Core.Services;

    public class DataViewModel : MvxViewModel
    {
        private readonly IDataStorage<DataSample> data;

        public DataViewModel(IDataStorage<DataSample> data)
        {
            this.data = data;
        }

        public ObservableCollection<ObservedFileItem<DataSample>> DataList => new ObservableCollection<ObservedFileItem<DataSample>>(this.data.ObservedFiles);
    }
}
