using E13.Common.Nunit.Extensions;
using FluentAssertions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Runtime.Versioning;
using System.Text;

namespace E13.Common.Nunit.Tests.Extensions
{
    public class BitmapExtensionsTests
    {
        private readonly static string CWD = TestContext.CurrentContext.WorkDirectory;
        private readonly static string Extensions = "Extensions";
        private readonly static string BitmapExtensionsFiles = "BitmapExtensionsFiles";
        private readonly static string ImageA = Path.Combine(CWD, Extensions, BitmapExtensionsFiles, "TestImageA.jpg");
        private readonly static string ImageB = Path.Combine(CWD, Extensions, BitmapExtensionsFiles, "TestImageB.jpg");
        private readonly static string CompareA2B = Path.Combine(CWD, Extensions, BitmapExtensionsFiles, "CompareAtoB.jpg");

        [Test]
        [SupportedOSPlatform("windows")]
        public void Compare_SameImage_NullResult()
        {
            Console.WriteLine(CWD);
            Console.WriteLine(ImageA);
            Console.WriteLine(ImageB);
            Console.WriteLine(CompareA2B);
            using var imageA1 = new Bitmap(ImageA);
            using var imageA2 = new Bitmap(ImageA);

            imageA1.Compare(imageA2)
                .Should().BeNull();
        }

        [Test]
        [SupportedOSPlatform("windows")]
        public void Compare_DifferentImages_GenerateExpected()
        {
            var tempPath = Path.Combine(CWD, Extensions, BitmapExtensionsFiles, $"{nameof(Compare_DifferentImages_GenerateExpected)}.jpg");
            using var imageA = new Bitmap(ImageA);
            using var imageB = new Bitmap(ImageB);

            var difference = imageA.Compare(imageB);
            difference.Save(tempPath);

            using var compare = new Bitmap(tempPath);
            using var preCompare = new Bitmap(CompareA2B);

            preCompare.IsIdentical(compare)
                .Should().BeTrue();
        }

        [Test]
        [SupportedOSPlatform("windows")]
        public void IsIdentical_SameImage_True()
        {
            using var imageA1 = new Bitmap(ImageA);
            using var imageA2 = new Bitmap(ImageA);

            imageA1.IsIdentical(imageA2)
                .Should().BeTrue();
        }

        [Test]
        [SupportedOSPlatform("windows")]
        public void IsIdentical_SameImage_False()
        {
            using var imageA = new Bitmap(ImageA);
            using var imageB = new Bitmap(ImageB);

            imageA.IsIdentical(imageB)
                .Should().BeFalse();
        }
    }
}
