//using FluentAssertions;
//using Microsoft.Extensions.Hosting;
//using NUnit.Framework;
//using NUnit.Framework.Internal;
//using PuppeteerSharp;
//using PuppeteerSharp.Input;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Reflection;
//using System.Text;
//using System.Threading;
//using System.Threading.Tasks;

//namespace E13.Common.Nunit.UI
//{
//    public abstract class BaseAuthUIFixture : BaseUIFixture
//    {
//        private bool Authorized = false;
//        protected abstract string AuthUrl { get; }

//        protected BaseAuthUIFixture(int w, int h, Func<IHostBuilder> builderFunc) : base(w, h, builderFunc) {}

//        [SetUp] 
//        public void AuthSetUp()
//        {
//            var testObj = TestExecutionContext.CurrentContext.TestObject as BaseAuthUIFixture;
//            var testType = testObj.GetType();

//            var method = testType.GetMethods().SingleOrDefault(x => x.Name == TestContext.CurrentContext.Test.MethodName);


//            if (method?.GetCustomAttributes(typeof(RequiresAuthAttribute), true).SingleOrDefault() is RequiresAuthAttribute raa)
//                testObj.AzureADLogin().Wait();
//        }

//        [OneTimeTearDown]
//        public void AuthTearDown()
//        {
//            Authorized = false;
//        }

//        [OneTimeSetUp]
//        public void AuthOneTimeSetUp()
//        {

//        }
//        public async Task<IResponse> AzureADLogin()
//        {
//            IResponse response;
//            if (!Authorized)
//            {
//                var screensName = "_AzureADLogin";
//                var delay = 1000;

//                response = await Page.GoToAsync(AuthUrl, WaitUntilNavigation.Networkidle2).ConfigureAwait(false);

//                Thread.Sleep(delay);

//                if(OperatingSystem.IsWindows())
//                    await Page.ConfirmScreenshot(screensName).ConfigureAwait(false);

//                response.Url.Should().Contain("login.microsoftonline.com");
//                await Page.WaitForSelectorAsync("input[name='loginfmt']").ConfigureAwait(false);
//                await Page.Keyboard.SendCharacterAsync(TestGoodUserName).ConfigureAwait(false);
//                await Page.Keyboard.PressAsync(Key.Enter).ConfigureAwait(false);
//                await Page.WaitForSelectorAsync("input[name='passwd']:not(.moveOffScreen)").ConfigureAwait(false);

//                await Page.Keyboard.SendCharacterAsync(TestPassword).ConfigureAwait(false);
//                await Page.Keyboard.PressAsync(Key.Enter).ConfigureAwait(false);
//                await Page.WaitForSelectorAsync("input[id='idSIButton9']").ConfigureAwait(false); ;

//                Thread.Sleep(delay);
//                if (OperatingSystem.IsWindows())
//                    await Page.ConfirmScreenshot(screensName).ConfigureAwait(false);

//                await Page.Keyboard.PressAsync(Key.Enter).ConfigureAwait(false);
//                await Page.GoToAsync(AuthUrl, options: new NavigationOptions { WaitUntil = new[] { WaitUntilNavigation.Networkidle0 } }).ConfigureAwait(false);
//                //response = await page.WaitForNavigationAsync().ConfigureAwait(false);
//                Authorized = true;
//            }
//            else
//            {
//                response = await Page.GoToAsync(AuthUrl).ConfigureAwait(false); ;
//            }
//            return response;
//        }
//    }
//}
