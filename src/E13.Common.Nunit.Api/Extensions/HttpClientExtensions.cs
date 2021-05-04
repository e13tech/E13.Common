using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace System.Net.Http
{
    public static class HttpClientExtensions
    {
        private static readonly string? TestUser = Environment.GetEnvironmentVariable("TokenForAAD_TestUser");
        private static readonly string? TestPass = Environment.GetEnvironmentVariable("TokenForAAD_TestPass");

#pragma warning disable CA1707 // Identifiers should not contain underscores - this is an exception because it is used primarily in unit tests
        public static void TokenForAAD_TestEnabled(this HttpClient client, string[] apiScopes)
#pragma warning restore CA1707 // Identifiers should not contain underscores
        {
            client.TokenForAAD(TestUser, TestPass.Secure(), apiScopes);
        }
    }
}
