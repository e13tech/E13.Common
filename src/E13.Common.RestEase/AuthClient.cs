//using RestEase;
//using System;
//using System.Collections.Generic;
//using System.Text;

//namespace E13.Common.ApiClient
//{
//    public static class AuthClient
//    {
//        //public static TApi For<TApi>(string baseUrl)
//        //{
//        //    var context = new AuthenticationContext();
//        //    RestClient.For<TApi>(baseUrl, async (request, cancellationToken) =>
//        //    {
//        //        // See if the request has an authorize header
//        //        var auth = request.Headers.Authorization;
//        //        if (auth != null)
//        //        {
//        //            // The AquireTokenAsync call will prompt with a UI if necessary
//        //            // Or otherwise silently use a refresh token to return a valid access token 
//        //            var token = await context.AcquireTokenAsync("http://my.service.uri/app", "clientId", new Uri("callback://complete")).ConfigureAwait(false);
//        //            request.Headers.Authorization = new AuthenticationHeaderValue(auth.Scheme, token);
//        //        }
//        //    });

//        //    return TApi;
//        //}
//    }
//}
