// -----------------------------------------------------------------
//    Class:		DataModel.cs
//    Description:	<Description>
//    Author:		Bastian Noffer <b.noffer@mac.com>	Date: 09.01.2021
//    Copyright:	©2021 Bastian Noffer
// -----------------------------------------------------------------

using System;
using System.Collections.Generic;

namespace XamarinDemo2021.Shared.Models
{
    public class DataModel
    {
        public Header header { get; set; }
        public List<string> filters { get; set; }
        public List<Product> products { get; set; }
    }
}
