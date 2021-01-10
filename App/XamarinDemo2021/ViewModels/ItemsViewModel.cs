// -----------------------------------------------------------------
//    Class:		ItemsViewModel.cs
//    Description:	<Description>
//    Author:		Bastian Noffer <b.noffer@mac.com>	Date: 09.01.2021
//    Copyright:	©2021 Bastian Noffer
// -----------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Microsoft.AppCenter.Crashes;
using Xamarin.Essentials;
using Xamarin.Forms;

using XamarinDemo2021.Models;
using XamarinDemo2021.Shared.Models;
using XamarinDemo2021.Views;

namespace XamarinDemo2021.ViewModels
{
    public class ItemsViewModel : BaseViewModel
    {
        #region Private fields

        private Product _selectedItem;
        private IEnumerable<Product> _allProducts;
        private List<int> _favorites;

        private string _headerTitle;
        private string _headerSubtitle;

        #endregion

        #region Properties

        public ObservableCollection<Product> Items { get; }
        public ObservableCollection<string> Filters { get; }
        public Command LoadItemsCommand { get; }
        public Command<Product> ItemTapped { get; }
        public Command<string> FilterTapped { get; }

        public string HeaderTitle
        {
            get { return _headerTitle; }
            set { SetProperty(ref _headerTitle, value); }
        }

        public string HeaderSubtitle
        {
            get { return _headerSubtitle; }
            set { SetProperty(ref _headerSubtitle, value); }
        }

        #endregion

        #region Ctor

        public ItemsViewModel()
        {
            _favorites = new List<int>();

            Title = "Browse";
            Items = new ObservableCollection<Product>();
            Filters = new ObservableCollection<string>();
            LoadItemsCommand = new Command(async () => await ExecuteLoadItemsCommand());
            
            ItemTapped = new Command<Product>(OnItemSelected);
            FilterTapped = new Command<string>(OnFilterSelected);
        }

        #endregion

        #region Public methods

        public void OnAppearing()
        {
            IsBusy = true;
            SelectedItem = null;

            LoadFavorites();
        }

        public void OnDisappearing()
        {
            SaveFavorites();
        }

        public Product SelectedItem
        {
            get => _selectedItem;
            set
            {
                SetProperty(ref _selectedItem, value);
                OnItemSelected(value);
            }
        }

        #endregion

        #region Private methods

        async Task ExecuteLoadItemsCommand()
        {
            IsBusy = true;

            try
            {
                Items.Clear();
                Filters.Clear();
                _allProducts = await DataStore.GetItemsAsync(true);
                foreach (var item in _allProducts)
                {
                    Items.Add(item);
                }

                var filters = await DataStore.GetFiltersAsync();
                foreach (var filter in filters)
                {
                    Filters.Add(filter);
                }

                var header = await DataStore.GetInfoAsync();
                HeaderTitle = header.headerTitle;
                HeaderSubtitle = header.headerDescription;
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }

        async void OnItemSelected(Product item)
        {
            if (item == null)
                return;

            // This will push the ItemDetailPage onto the navigation stack
            await Shell.Current.GoToAsync($"{nameof(ItemDetailPage)}?{nameof(ItemDetailViewModel.ItemId)}={item.id}");
        }

        void OnFilterSelected(string filter)
        {
            if (string.IsNullOrEmpty(filter))
                return;

            Items.Clear();

            if (filter.Equals("Alle"))
            {
                foreach (var item in _allProducts)
                {
                    Items.Add(item);
                }
            }
            else if (filter.Equals("Verfügbar"))
            {
                foreach (var item in _allProducts)
                {
                    if (item.available)
                        Items.Add(item);
                }
            }
            else if (filter.Equals("Vorgemerkt"))
            {
                foreach (var item in _allProducts)
                {
                    if (_favorites.Contains(item.id))
                        Items.Add(item);
                }
            }
        }

        void LoadFavorites()
        {
            try
            {
                if (Preferences.ContainsKey("my_favorites"))
                {
                    var favoritesPrefs = Preferences.Get("my_favorites", "");
                    var model = JsonConvert.DeserializeObject<FavoritesModel>(favoritesPrefs);

                    _favorites.Clear();
                    if (model.FavoriteIds != null)
                    {
                        foreach (var favorite in model.FavoriteIds)
                            _favorites.Add(favorite);
                    }
                }
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
            }
        }

        void SaveFavorites()
        {
            try
            {
                var favoritesPrefs = JsonConvert.SerializeObject(new FavoritesModel(_favorites));
                Preferences.Set("my_favorites", favoritesPrefs);
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
            }
        }

        #endregion
    }
}