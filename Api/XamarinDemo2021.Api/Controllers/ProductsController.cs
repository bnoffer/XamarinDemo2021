// -----------------------------------------------------------------
//    Class:		ProductsController.cs
//    Description:	<Description>
//    Author:		Bastian Noffer <b.noffer@mac.com>	Date: 09.01.2021
//    Copyright:	©2021 Bastian Noffer
// -----------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using XamarinDemo2021.Api.Services;
using XamarinDemo2021.Shared.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace XamarinDemo2021.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : Controller
    {
        // GET: api/values
        [HttpGet]
        public DataModel Get()
        {
            var ds = LocalDataStore.Instance;

            if (ds.IsDataLoaded)
                return ds.Data;

            return null;
        }
    }
}
