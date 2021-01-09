// -----------------------------------------------------------------
//    Class:		FavoritesModel.cs
//    Description:	<Description>
//    Author:		Bastian Noffer <b.noffer@mac.com>	Date: 09.01.2021
//    Copyright:	©2021 Bastian Noffer
// -----------------------------------------------------------------
using System;
using System.Collections.Generic;
namespace XamarinDemo2021.Models
{
    public class FavoritesModel
    {
        public List<int> FavoriteIds { get; set; }

        public FavoritesModel()
        { }

        public FavoritesModel(List<int> favorites)
        {
            this.FavoriteIds = favorites;
        }
    }
}
