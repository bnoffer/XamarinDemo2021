// -----------------------------------------------------------------
//    Class:		LocalDataStore.cs
//    Description:	<Description>
//    Author:		Bastian Noffer <b.noffer@mac.com>	Date: 09.01.2021
//    Copyright:	©2021 Bastian Noffer
// -----------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using XamarinDemo2021.Shared.Models;
using Newtonsoft.Json;

namespace XamarinDemo2021.Api.Services
{
    public class LocalDataStore
    {
        #region Singleton

        private static LocalDataStore _instance;
        private static object _syncRoot = new object();

        public static LocalDataStore Instance
        {
            get
            {
                lock (_syncRoot)
                    if (_instance == null)
                        _instance = new LocalDataStore();
                return _instance;
            }
        }

        #endregion

        #region Private fields

        private DataModel _data;

        #endregion

        #region Properties

        public bool IsDataLoaded
        {
            get { return _data != null; }
        }

        public DataModel Data
        {
            get { return _data; }
        }

        #endregion

        #region Ctor

        private LocalDataStore()
        {
        }

        #endregion

        #region Public methods

        public void LoadLocalData()
        {
            string myJsonResponse = "";
            var file = File.OpenRead("rawdata.json");
            using (TextReader reader = new StreamReader(file))
            {
                myJsonResponse = reader.ReadToEnd();
            }
            _data = JsonConvert.DeserializeObject<DataModel>(myJsonResponse);
        }

        #endregion

        #region Private methods



        #endregion
    }
}
