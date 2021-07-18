using HelloMvx.Core.ViewModels;
using MvvmCross.IoC;
using MvvmCross.ViewModels;
using System;

namespace HelloMvx.Core
{
    public class Bootstrapper : MvxApplication
    {
        public override void Initialize()
        {
            CreatableTypes()
                .EndingWith("Service")
                .AsInterfaces()
                .RegisterAsLazySingleton();

            RegisterAppStart<AboutViewModel>();
            
            //base.Initialize();
        }
    }
}
