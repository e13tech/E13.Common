using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace System.Net.Http
{
    public static class HttpClientExtensions
    {
#pragma warning disable CA1707 // Identifiers should not contain underscores - this is an exception because it is used primarily in unit tests
        public static void TokenForAAD_AutoBasic(this HttpClient client, string[] apiScopes = null)
#pragma warning restore CA1707 // Identifiers should not contain underscores
        {
            client.TokenForAAD("test.enabled@e13.tech", @"YGb=d83'""\a|".Secure(), apiScopes);
        }
    }
}
