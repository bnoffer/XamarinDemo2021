// -----------------------------------------------------------------
//    Class:		IApiEndpoints.cs
//    Description:	<Description>
//    Author:		Bastian Noffer <b.noffer@mac.com>	Date: 09.01.2021
//    Copyright:	©2021 Bastian Noffer
// -----------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Refit;
using XamarinDemo2021.Shared.Models;

namespace XamarinDemo2021.Interfaces
{
    public interface IApiEndpoints
    {
        [Get("/api/Products")]
        Task<ApiResponse<DataModel>> Get();
    }
}
