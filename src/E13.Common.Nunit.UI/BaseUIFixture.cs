//using FluentAssertions;
//using Microsoft.Extensions.Hosting;
//using Newtonsoft.Json;
//using Newtonsoft.Json.Linq;
//using NUnit.Framework;
//using NUnit.Framework.Internal;
//using PuppeteerSharp;
//using PuppeteerSharp.Input;
//using System;
//using System.Collections.Generic;
//using System.IO;
//using System.Linq;
//using System.Text;
//using System.Threading;
//using System.Threading.Tasks;

//namespace E13.Common.Nunit.UI
//{
//    public abstract class BaseUIFixture : LaunchSettingsFixture, IDisposable
//    {
//        protected static readonly bool InMemory = Environment.GetEnvironmentVariable("InMemory").DefaultParse(false);

//        protected static readonly string TestGoodUserName = Environment.GetEnvironmentVariable("TestGoodUserName");
//        protected static readonly string TestDisabledUserName = Environment.GetEnvironmentVariable("TestDisabledUserName");
//        protected static readonly string TestInvalidUserName = Environment.GetEnvironmentVariable("TestInvalidUserName");

//        protected static readonly string TestPassword = Environment.GetEnvironmentVariable("TestPassword");

//        private readonly CancellationTokenSource CancelSource;
//        private readonly Func<IHostBuilder> BuilderFunc;
//        private readonly int PageWidth;
//        private readonly int PageHeight;

//        protected IBrowser Browser { get; set; }
//        protected IPage Page { get; set; }

//        protected BaseUIFixture(int w, int h, Func<IHostBuilder> builderFunc)
//        {
//            PageWidth = w;
//            PageHeight = h;
//            BuilderFunc = builderFunc;

//            CancelSource = new CancellationTokenSource();
//        }

//        [SetUp]
//        public void SetUp()
//        {
//            // print out the directory the screens are saved to for ease of navigating locally
//            WriteLine($"Screens Directory: {IPageExtensions.ScreensDirectory}");
//        }
//        [OneTimeSetUp]
//        public void OneTimeSetUp()
//        {
//            // Init the InMemory server if required
            
//            if (InMemory)
//                BuilderFunc().RunConsoleAsync(CancelSource.Token);

//            new BrowserFetcher().DownloadAsync(BrowserFetcher.DefaultChromiumRevision).Wait();
//            Browser = Puppeteer.LaunchAsync(new LaunchOptions
//            {
//                Headless = true,
//                IgnoreHTTPSErrors = true,

//            }).Result;

//            Page = Browser.NewPageAsync().Result;

//            Page.SetViewportAsync(new ViewPortOptions { Width = PageWidth, Height = PageHeight });
//        }

//        protected static void WriteLine(string s) 
//            => TestContext.WriteLine(s);

//        #region IDisposable Support
//        private bool disposedValue = false; // To detect redundant calls

//        protected virtual void Dispose(bool disposing)
//        {
//            if (!disposedValue)
//            {
//                if (disposing)
//                {
//                    Browser.CloseAsync().Wait();
//                    CancelSource.Cancel();
//                    CancelSource.Dispose();
//                }

//                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
//                // TODO: set large fields to null.

//                disposedValue = true;
//            }
//        }

//        [OneTimeTearDown]
//        // This code added to correctly implement the disposable pattern.
//        public void Dispose()
//        {
//            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
//            Dispose(true);
//            GC.SuppressFinalize(this);
//        }
//        #endregion
//    }
//}
