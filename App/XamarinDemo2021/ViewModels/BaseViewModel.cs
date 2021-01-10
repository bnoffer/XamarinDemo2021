// -----------------------------------------------------------------
//    Class:		BaseViewModel.cs
//    Description:	<Description>
//    Author:		Bastian Noffer <b.noffer@mac.com>	Date: 10.01.2021
//    Copyright:	©2021 Bastian Noffer
// -----------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

using Xamarin.Forms;

using XamarinDemo2021.Shared.Models;
using XamarinDemo2021.Interfaces;

namespace XamarinDemo2021.ViewModels
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        #region Private fields

        private bool _isBusy = false;
        private string _title = string.Empty;

        #endregion

        #region Properties

        public IDataStore<Product> DataStore => DependencyService.Get<IDataStore<Product>>();
        
        public bool IsBusy
        {
            get { return _isBusy; }
            set { SetProperty(ref _isBusy, value); }
        }

        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        protected bool SetProperty<T>(ref T backingStore, T value,
            [CallerMemberName] string propertyName = "",
            Action onChanged = null)
        {
            if (EqualityComparer<T>.Default.Equals(backingStore, value))
                return false;

            backingStore = value;
            onChanged?.Invoke();
            OnPropertyChanged(propertyName);
            return true;
        }

        #endregion

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            var changed = PropertyChanged;
            if (changed == null)
                return;

            changed.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
