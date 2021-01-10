// -----------------------------------------------------------------
//    Class:		ItemDetailViewModel.cs
//    Description:	<Description>
//    Author:		Bastian Noffer <b.noffer@mac.com>	Date: 10.01.2021
//    Copyright:	©2021 Bastian Noffer
// -----------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Diagnostics;
using XamarinDemo2021.Models;
using Xamarin.Essentials;
using Xamarin.Forms;
using Newtonsoft.Json;
using Microsoft.AppCenter.Crashes;

namespace XamarinDemo2021.ViewModels
{
    [QueryProperty(nameof(ItemId), nameof(ItemId))]
    public class ItemDetailViewModel : BaseViewModel
    {
        #region Private fields

        private List<int> _favorites;
        private string _itemId;
        private string _text;
        private string _description;
        private string _longDescription;
        private string _imageUrl;
        private bool _isFavorite;
        private string _buttonText;

        #endregion

        #region Properties

        public string Id { get; set; }

        public string Text
        {
            get => _text;
            set => SetProperty(ref _text, value);
        }

        public string Description
        {
            get => _description;
            set => SetProperty(ref _description, value);
        }

        public string ItemId
        {
            get { return _itemId; }
            set
            {
                _itemId = value;
                LoadItemId(value);
            }
        }

        public string LongDescription
        {
            get => _longDescription;
            set => SetProperty(ref _longDescription, value);
        }

        public string ImageUrl
        {
            get => _imageUrl;
            set => SetProperty(ref _imageUrl, value);
        }

        public bool IsFavorite
        {
            get => _isFavorite;
            set => SetProperty(ref _isFavorite, value);
        }

        public string ButtonText
        {
            get => _buttonText;
            set => SetProperty(ref _buttonText, value);
        }

        public Command FavoriteCommand { get; }

        #endregion

        #region Ctor

        public ItemDetailViewModel()
        {
            _favorites = new List<int>();
            LoadFavorites();

            FavoriteCommand = new Command(OnFavoriteCommand);
        }

        #endregion

        #region Public methods

        public async void LoadItemId(string itemId)
        {
            try
            {
                var item = await DataStore.GetItemAsync(itemId);
                Id = item.id + "";
                Text = item.name;
                Description = item.description;
                LongDescription = item.longDescription;
                ImageUrl = item.imageURL;

                if (_favorites.Contains(item.id))
                {
                    IsFavorite = true;
                    ButtonText = "Not my favorite";
                }
                else
                {
                    IsFavorite = false;
                    ButtonText = "Make this a favorite";
                }
            }
            catch (Exception)
            {
                Debug.WriteLine("Failed to Load Item");
            }
        }

        #endregion

        #region Private methods

        private void OnFavoriteCommand()
        {
            var id = int.Parse(Id);

            if (IsFavorite)
                _favorites.Remove(id);
            else
                _favorites.Add(id);

            SaveFavorites();

            if (_favorites.Contains(id))
            {
                IsFavorite = true;
                ButtonText = "Not my favorite";
            }
            else
            {
                IsFavorite = false;
                ButtonText = "Make this a favorite";
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
