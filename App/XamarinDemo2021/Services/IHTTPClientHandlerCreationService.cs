// -----------------------------------------------------------------
//    Class:		IHTTPClientHandlerCreationService.cs
//    Description:	<Description>
//    Author:		Bastian Noffer <b.noffer@mac.com>	Date: 09.01.2021
//    Copyright:	©2021 Bastian Noffer
// -----------------------------------------------------------------
using System;
using System.Net.Http;

namespace XamarinDemo2021.Services
{
    public interface IHTTPClientHandlerCreationService
    {
        HttpClientHandler GetInsecureHandler();
    }
}
