namespace CodingTest.Core.Services
{
    using System.Collections.Generic;

    using Cirrious.MvvmCross.ViewModels;

    public class ObservedFileItem<TData> : MvxNotifyPropertyChanged
    {
        private string name;

        public string Name
        {
            get
            {
                return this.name;
            }
            set
            {
                this.name = value;
                this.RaisePropertyChanged(() => this.Name);
            }
        }

        private string lastUpdate;

        public string LastUpdate
        {
            get
            {
                return this.lastUpdate;
            }
            set
            {
                this.lastUpdate = value;
                this.RaisePropertyChanged(() => this.LastUpdate);
            }
        }

        public List<TData> Data { get; set; }

        public string Count => (this.Data?.Count ?? 0).ToString();
    }
}