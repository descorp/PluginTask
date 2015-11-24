using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingTest.Core.ViewModels.Items
{
    using Cirrious.MvvmCross.ViewModels;

    public class FileInfoItem : MvxNotifyPropertyChanged
    {
        private string nAME;

        public string NAME
        {
            get
            {
                return this.nAME;
            }
            set
            {
                this.nAME = value;
                this.RaisePropertyChanged(() => this.NAME);
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
