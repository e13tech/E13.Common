using RestEase;
using System;
using System.Collections.Generic;
using System.Text;

namespace E13.Common.ApiClient
{
    [Header("User-Agent", "RestEase")]
    [Header("Cache-Control", "no-cache")]
    public interface IRestEaseApi
    {
        IRequester Requester { get; }
    }
}
