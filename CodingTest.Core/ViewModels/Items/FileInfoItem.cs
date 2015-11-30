namespace CodingTest.Core.ViewModels.Items
{
    using Cirrious.MvvmCross.ViewModels;

    public class FileInfoItem : MvxNotifyPropertyChanged
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

        private string update;

        public string Update
        {
            get
            {
                return this.update;
            }
            set
            {
                this.update = value;
                this.RaisePropertyChanged(() => this.Update);
            }
        }

        
    }
}
