using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Identity.Client;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace E13.Common.Api
{
    public abstract class AuthorizedControllerBase : ControllerBase
    {
        protected ILogger Logger { get; }
        private IConfiguration Configuration { get; }
        //protected bool IsDevelopment => Environment.IsDevelopment();

        protected AuthorizedControllerBase(ILogger logger, IConfiguration configuration)
        {
            Logger = logger;
            Configuration = configuration;
        }
        
        protected string Name
            => User.Claims.First(c => c.Type == "name").Value;

        protected string Username
        { 
            get
            {
                var upn = User.Claims.FirstOrDefault(c => c.Type == "preferred_username");
                if (upn != null)
                    return upn.Value;

                var un = User.Claims.FirstOrDefault(c => c.Type == "unique_name");
                if (un != null)
                    return un.Value;

                dynamic? me = CallGraphApiOnBehalfOfUser().Result ?? throw new Exception("Unable to retrieve user information from Graph API");

                return me.userPrincipalName;
            }
        }

        protected string BearerToken
        {
            get
            {
                var auth = Request.Headers["Authorization"];

                if (auth.Count < 1 || auth[0]!.Length < 8)
#pragma warning disable CA1065 // Do not raise exceptions in unexpected locations
                    throw new ArgumentException("Request Header does not contain a valid Bearer Token");
#pragma warning restore CA1065 // Do not raise exceptions in unexpected locations
                var bearer = auth[0]![7..];
                return bearer;
            }
        }

        private async Task<dynamic?> CallGraphApiOnBehalfOfUser()
        {
            // Get the token to use on behalf of the user
            string[] scopes = { "user.read.all" };

            string? appKey = Configuration.GetValue<string>("AzureAd:ClientSecret");
            string? clientId = Configuration.GetValue<string>("AzureAd:ClientId");

            var app = ConfidentialClientApplicationBuilder.Create(clientId)
              .WithClientSecret(appKey)
              .Build();
            var userAssertion = new UserAssertion(BearerToken, "urn:ietf:params:oauth:grant-type:jwt-bearer");
            var result = app.AcquireTokenOnBehalfOf(scopes, userAssertion).ExecuteAsync().Result;

            //
            // Call the Graph API and retrieve the user's profile.
            //
            using var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", result.AccessToken);

            using HttpResponseMessage response = await client.GetAsync(new Uri("https://graph.microsoft.com/v1.0/me")).ConfigureAwait(false);
            var content = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                dynamic? me = JsonConvert.DeserializeObject(content);
                return me;
            }

            throw new Exception(content);
        }
    }
}