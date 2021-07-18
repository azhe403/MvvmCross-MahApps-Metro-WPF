using MvvmCross.Platforms.Wpf.Views;
using MvvmCross.Core;
using HelloMvx.Core;
using MvvmCross.Platforms.Wpf.Core;

namespace HelloMvx.Wpf
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : MvxApplication
    {
        protected override void RegisterSetup()
        {
            this.RegisterSetupType<Setup>();
        }
    }
}
