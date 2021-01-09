// -----------------------------------------------------------------
//    Class:		Product.cs
//    Description:	<Description>
//    Author:		Bastian Noffer <b.noffer@mac.com>	Date: 09.01.2021
//    Copyright:	©2021 Bastian Noffer
// -----------------------------------------------------------------

using System;
namespace XamarinDemo2021.Shared.Models
{
    public class Product
    {
        public string name { get; set; }
        public string type { get; set; }
        public int id { get; set; }
        public string color { get; set; }
        public string imageURL { get; set; }
        public string colorCode { get; set; }
        public bool available { get; set; }
        public int releaseDate { get; set; }
        public string description { get; set; }
        public string longDescription { get; set; }
        public double rating { get; set; }
        public Price price { get; set; }
    }
}
