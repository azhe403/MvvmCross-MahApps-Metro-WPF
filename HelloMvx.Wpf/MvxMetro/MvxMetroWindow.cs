﻿//
// Source: https://github.com/MvvmCross/MvvmCross/blob/develop/MvvmCross/Platforms/Wpf/Views/MvxWindow.cs
// From: https://stackoverflow.com/a/63314896
//

using MahApps.Metro.Controls;
using MvvmCross;
using MvvmCross.Binding.BindingContext;
using MvvmCross.Platforms.Wpf.Views;
using MvvmCross.ViewModels;
using System;
using System.Windows;
using MvxApplication = MvvmCross.Platforms.Wpf.Views.MvxApplication;

namespace HelloMvx.Wpf.MvxMetro
{
    public class MvxMetroWindow : MetroWindow, IMvxWindow, IMvxWpfView, IDisposable
    {
        private IMvxViewModel _viewModel;
        private IMvxBindingContext _bindingContext;
        private bool _unloaded = false;

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

        public string Identifier { get; set; }

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

        public MvxMetroWindow()
        {
            Closed += MvxWindow_Closed;
            Unloaded += MvxWindow_Unloaded;
            Loaded += MvxWindow_Loaded;
            Initialized += MvxWindow_Initialized;
        }

        private void MvxWindow_Initialized(object sender, EventArgs e)
        {
            if (this == Application.Current.MainWindow)
            {
                (Application.Current as MvxApplication).ApplicationInitialized();
            }
        }

        private void MvxWindow_Closed(object sender, EventArgs e) => Unload();

        private void MvxWindow_Unloaded(object sender, RoutedEventArgs e) => Unload();

        private void MvxWindow_Loaded(object sender, RoutedEventArgs e)
        {
            ViewModel?.ViewAppearing();
            ViewModel?.ViewAppeared();
        }

        private void Unload()
        {
            if (!_unloaded)
            {
                ViewModel?.ViewDisappearing();
                ViewModel?.ViewDisappeared();
                ViewModel?.ViewDestroy();
                _unloaded = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~MvxMetroWindow()
        {
            Dispose(false);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                Unloaded -= MvxWindow_Unloaded;
                Loaded -= MvxWindow_Loaded;
                Closed -= MvxWindow_Closed;
            }
        }
    }

    public class MvxMetroWindow<TViewModel> : MvxWindow, IMvxWpfView<TViewModel>
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
