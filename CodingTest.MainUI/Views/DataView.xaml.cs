using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CodingTest.MainUI.Views
{
    using Cirrious.MvvmCross.Wpf.Views;

    using CodingTest.Core.ViewModels.Tabs;

    /// <summary>
    /// Interaction logic for DataView.xaml
    /// </summary>
    public partial class DataView : MvxWpfView
    {
        public DataView()
        {
            InitializeComponent();
        }

        public new DataViewModel ViewModel
        {
            get { return base.ViewModel as DataViewModel; }
            set { base.ViewModel = value; }
        }
    }
}
