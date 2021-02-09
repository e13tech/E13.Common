using FluentAssertions;
using NUnit.Framework;
using PuppeteerSharp;
using PuppeteerSharp.Input;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using E13.Common.Nunit.UI.Extensions;

namespace PuppeteerSharp
{
    public static class PageExtensions
    {
        private static readonly bool InMemory = Environment.GetEnvironmentVariable("InMemory").DefaultParse(false);
        public static readonly string ScreensDirectory = $"{TestContext.CurrentContext.WorkDirectory}/../../../screens/";


        public static async Task ConfirmScreenshot(this Page page, string name = null)
        {
            if (page == null)
                throw new ArgumentNullException(nameof(page));

            if (name == null)
                name = TestContext.CurrentContext.Test.Name;

            var w = page.Viewport.Width;
            var h = page.Viewport.Height;

            var index = TestContext.CurrentContext.Test.GetScreenIndex($"{w}{h}{name}");

            var screenDir = $"{ScreensDirectory}{TestContext.CurrentContext.Test.ClassName}/{w}_{h}/{name}/";
            new DirectoryInfo(screenDir).Create();

            var expectedPath = InMemory ? 
                $"{screenDir}{name}_{index}_ExpectedIM.png" 
                : 
                $"{screenDir}{index}_Expected.png";
            var deltaPath = $"{screenDir}{name}_{index}_Delta.png";
            var actualPath = $"{screenDir}{name}_{index}_Actual.png";

            var deltaInfo = new FileInfo(deltaPath);
            // clean up a past failed test screenshot
            if (deltaInfo.Exists)
            {
                deltaInfo.Delete();
            }
            await page.ScreenshotAsync(actualPath).ConfigureAwait(false);
            using var actualBitmap = new Bitmap(actualPath);
            TestContext.AddTestAttachment(actualPath);
            var expectedInfo = new FileInfo(expectedPath);
            if (!expectedInfo.Exists)
            {
                // if this is the first time the test has executed then assume the current screenshot is expected
                actualBitmap.Save(expectedPath);
            }
            using var expectedBitmap = new Bitmap(expectedPath);
            var compare = actualBitmap.Compare(expectedBitmap);
            if (compare != null)
            {
                compare.Save(deltaPath);
                TestContext.AddTestAttachment(deltaPath);
                compare.Should().BeNull(); // trigger the test fail
            }
        }
    }
}
