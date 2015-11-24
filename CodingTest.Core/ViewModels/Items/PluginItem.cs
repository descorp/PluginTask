using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingTest.Core.ViewModels.Items
{
    using Cirrious.MvvmCross.ViewModels;

    public class PluginItem : MvxNotifyPropertyChanged
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

        private string fileFormat;

        public string FileFormat
        {
            get
            {
                return this.fileFormat;
            }
            set
            {
                this.fileFormat = value;
                this.RaisePropertyChanged(() => this.FileFormat);
            }
        }

        
    }
}
