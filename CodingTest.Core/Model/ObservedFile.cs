namespace CodingTest.Core.Services
{
    using System.Collections.Generic;

    using Cirrious.MvvmCross.ViewModels;

    using CodingTest.Common.Model;

    public class ObservedFile : MvxNotifyPropertyChanged
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

        private List<DataSample> Data { get; set; }
    }
}