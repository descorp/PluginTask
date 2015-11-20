using Cirrious.MvvmCross.ViewModels;

namespace CodingTest.Core
{
    using CodingTest.Core.ViewModels;

    public class App : MvxApplication
    {
        public override void Initialize()
        {
            

            RegisterAppStart<RootViewModel>();
        }
    }
}
