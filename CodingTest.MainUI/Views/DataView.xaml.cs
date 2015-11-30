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
