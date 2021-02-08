using RestEase;
using System;
using System.Net.Http.Headers;

namespace E13.Common.ApiClient
{
    public interface IAuthenticatedApi : IRestEaseApi
    {
        [Header("Authorization")]
        AuthenticationHeaderValue Authorization { get; set; }
    }
}
