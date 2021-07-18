//
// Source: https://github.com/MvvmCross/MvvmCross/blob/develop/MvvmCross/Platforms/Wpf/Views/MvxWpfView.cs
// From: https://stackoverflow.com/a/63314896
//

using MvvmCross;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Platforms.Wpf.Views;
using MvvmCross.ViewModels;
using System;
using System.Windows;
using System.Windows.Controls;

namespace HelloMvx.Wpf.MvxMetro
{
    public class MvxMetroView : UserControl, IMvxWpfView, IDisposable
    {
        private IMvxViewModel _viewModel;
        private IMvxBindingContext _bindingContext;

        public IMvxViewModel ViewModel
        {
            get => _viewModel;
            set
            {
                _viewModel = value;
                DataContext = value;
                BindingContext.DataContext = value;
            }
        }

        public IMvxBindingContext BindingContext
        {
            get
            {
                if (_bindingContext != null)
                    return _bindingContext;

                if (Mvx.IoCProvider != null)
                    this.CreateBindingContext();

                return _bindingContext;
            }
            set => _bindingContext = value;
        }

        public MvxMetroView()
        {
            Unloaded += MvxWpfView_Unloaded;
            Loaded += MvxWpfView_Loaded;
        }

        private void MvxWpfView_Unloaded(object sender, RoutedEventArgs e)
        {
            ViewModel?.ViewDisappearing();
            ViewModel?.ViewDisappeared();
            ViewModel?.ViewDestroy();
        }

        private void MvxWpfView_Loaded(object sender, RoutedEventArgs e)
        {
            ViewModel?.ViewAppearing();
            ViewModel?.ViewAppeared();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~MvxMetroView()
        {
            Dispose(false);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                Unloaded -= MvxWpfView_Unloaded;
                Loaded -= MvxWpfView_Loaded;
            }
        }
    }

    public class MvxMetroView<TViewModel> : MvxWpfView, IMvxWpfView<TViewModel>
        where TViewModel : class, IMvxViewModel
    {
        public new TViewModel ViewModel
        {
            get { return (TViewModel)base.ViewModel; }
            set { base.ViewModel = value; }
        }

        public MvxFluentBindingDescriptionSet<IMvxWpfView<TViewModel>, TViewModel> CreateBindingSet()
        {
            return this.CreateBindingSet<IMvxWpfView<TViewModel>, TViewModel>();
        }
    }
}

