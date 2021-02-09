using NUnit.Framework.Internal;
using System;
using System.Collections.Generic;
using System.Text;
using static NUnit.Framework.TestContext;

namespace E13.Common.Nunit.UI.Extensions
{
    public static class TestAdapterExtensions
    {
        private const int StartingIndex = 0;
        private static readonly Dictionary<string, int> ScreenIndex = new Dictionary<string, int>();

        public static int GetScreenIndex(this TestAdapter t, string name = null)
        {
            if (t == null)
                throw new ArgumentNullException(nameof(t));

            if(name == null)
            {
                name = t.Name;
            }
            // if this test has not run before then reset the starting index
            if (!ScreenIndex.ContainsKey(name))
                ScreenIndex[name] = StartingIndex;

            var currentIndex = ScreenIndex[name];
            currentIndex++;

            ScreenIndex[name] = currentIndex;

            return currentIndex;
        }
    }
}
