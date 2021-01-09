// -----------------------------------------------------------------
//    Class:		ApiDataStore.cs
//    Description:	<Description>
//    Author:		Bastian Noffer <b.noffer@mac.com>	Date: 09.01.2021
//    Copyright:	©2021 Bastian Noffer
// -----------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using XamarinDemo2021.Abstractions;
using XamarinDemo2021.Interfaces;
using XamarinDemo2021.Shared.Models;
using Microsoft.AppCenter.Crashes;
using Polly;
using Refit;
using Xamarin.Forms;

[assembly: Dependency(typeof(XamarinDemo2021.Services.ApiDataStore))]
namespace XamarinDemo2021.Services
{
    public class ApiDataStore : BaseComponent, IDataStore<Product>
    {
        #region Private fields

        /// <summary>
        /// Interval in seconds until the date is deemed to be old
        /// </summary>
        private readonly int _secondsTillRefresh = 10;

        /// <summary>
        /// Last fetch of the data
        /// </summary>
        private DataModel _data;

        /// <summary>
        /// Last time data was fetched
        /// </summary>
        private DateTime _timestamp;

        #endregion

        #region Properties

        public bool RequiresRefresh
        {
            get
            {
                if (_data == null || (DateTime.Now >= _timestamp.AddSeconds(_secondsTillRefresh)))
                    return true;
                else
                    return false;
            }
        }

        #endregion

        #region Ctor

        /// <summary>
        /// Default constructor
        /// </summary>
        public ApiDataStore()
        {
        }

        #endregion

        #region Public methods

        public Task<bool> AddItemAsync(Product item)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteItemAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<Product> GetItemAsync(string id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Product>> GetItemsAsync(bool forceRefresh = false)
        {
            if (RequiresRefresh || forceRefresh)
                _data = await Get();

            if (_data == null)
                return new List<Product>();

            return _data.products;
        }

        public Task<bool> UpdateItemAsync(Product item)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<string>> GetFiltersAsync()
        {
            if (RequiresRefresh)
                _data = await Get();

            if (_data == null)
                return new List<string>();

            return _data.filters;
        }

        public async Task<Header> GetInfoAsync()
        {
            if (RequiresRefresh)
                _data = await Get();

            if (_data == null)
                return new Header
                {
                    headerTitle = "",
                    headerDescription = ""
                };

            return _data.header;
        }

        #endregion

        #region Private methods

        private async Task<DataModel> Get()
        {
            var result = await RequestGET();
            _timestamp = DateTime.Now;
            return result.Result;
        }

        private async Task<(HttpStatusCode Code, DataModel Result)> RequestGET()
        {
            var tag = this + ".FeedGET";
            try
            {
                var apiResponse = GetRestService<IApiEndpoints>(Resources.ApiConfig.BaseUrl);

                PolicyResult<ApiResponse<DataModel>> pollyResult = null;

                pollyResult = await Policy.ExecuteAndCaptureAsync(async () => await apiResponse.Get());

                if (pollyResult.Result != null && pollyResult.Result.Content != null)
                {
                    var result = pollyResult.Result;

                    return (HttpStatusCode.OK, result.Content);
                }
                else if (pollyResult.Result != null)
                    return (pollyResult.Result.StatusCode, null);

                return (HttpStatusCode.BadRequest, null);
            }
            catch (UriFormatException ex)
            {
                Crashes.TrackError(ex);

                return (HttpStatusCode.BadRequest, null);
            }
            catch (ArgumentException ex)
            {
                Crashes.TrackError(ex);

                return (HttpStatusCode.BadGateway, null);
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);

                return (HttpStatusCode.InternalServerError, null);
            }
        }

        #endregion
    }
}
