// -----------------------------------------------------------------
//    Class:		IDataStore.cs
//    Description:	<Description>
//    Author:		Bastian Noffer <b.noffer@mac.com>	Date: 09.01.2021
//    Copyright:	©2021 Bastian Noffer
// -----------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using XamarinDemo2021.Shared.Models;

namespace XamarinDemo2021.Services
{
    public interface IDataStore<T>
    {
        #region Generic methods
        Task<bool> AddItemAsync(T item);
        Task<bool> UpdateItemAsync(T item);
        Task<bool> DeleteItemAsync(string id);
        Task<T> GetItemAsync(string id);
        Task<IEnumerable<T>> GetItemsAsync(bool forceRefresh = false);

        #endregion

        #region Specific methods

        Task<IEnumerable<string>> GetFiltersAsync();
        Task<Header> GetInfoAsync();

        #endregion
    }
}
