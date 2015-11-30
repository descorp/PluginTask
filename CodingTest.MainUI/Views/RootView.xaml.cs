using Cirrious.MvvmCross.Wpf.Views;

namespace CodingTest.MainUI.Views
{
    using CodingTest.Core.ViewModels;

    public partial class RootView : MvxWpfView
    {
        public RootView()
        {
            this.InitializeComponent();
        }

        public new RootViewModel ViewModel
        {
            get { return (RootViewModel)base.ViewModel; }
            set { base.ViewModel = value; }
        }
    }
}
