using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using NUnit.Framework;
using System;
using System.IO;
using System.Net.Http;

namespace E13.Common.Nunit.Api
{
    public class BaseApiFixture : LaunchSettingsFixture
    {
        private TestServer? Server { get; set; }
        private string? BaseUrl { get; }
        private string ProjectName { get; }
        private Func<IWebHostBuilder> BuilderFunc { get; }

        public BaseApiFixture(string projectName, Func<IWebHostBuilder> builderFunc)
        {
            BaseUrl = Environment.GetEnvironmentVariable("BASE_MICROSERVICE_URL");
            ProjectName = projectName;
            BuilderFunc = builderFunc;
        }

        private static HttpClient? _singleton;

        public HttpClient? GetHttpClient()
        {
            // only init a single HttpClient
            if (_singleton != null)
                return _singleton;

            if (!string.IsNullOrWhiteSpace(BaseUrl))
                return new HttpClient { BaseAddress = new Uri(BaseUrl) };


            if (Server != null) return Server.CreateClient();

            Initialize(
                Directory.GetCurrentDirectory().ParentSiblingDirectory(ProjectName),
                BuilderFunc());

            _singleton = Server?.CreateClient();

            return _singleton;
        }

        private void Initialize(string projectPath, IWebHostBuilder builder)
        {
            builder
                .UseEnvironment("Development")
                .UseContentRoot(projectPath);

            Server = new TestServer(builder);
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            Server?.Dispose();

        }
    }
}