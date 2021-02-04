using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace System
{
    public static class StringExtensions
    {
        /// <summary>
        /// uses a string representing a directory to navigate up the directory structure
        /// looking for a parent folder with the provided name
        /// </summary>
        /// <param name="dir">starting directory</param>
        /// <param name="parent">parent directory to look for</param>
        /// <returns>Full path to the parent directory</returns>
        public static string ParentDirectory(this string dir, string parent)
        {
            var checkDI = new DirectoryInfo($"{dir}/..");
            while (!checkDI.Exists || !checkDI.Name.Equals(parent, StringComparison.InvariantCultureIgnoreCase))
            {
                var old = checkDI.FullName;
                checkDI = new DirectoryInfo($"{checkDI.FullName}/..");
                if (checkDI.FullName.Equals(old, StringComparison.InvariantCultureIgnoreCase))
                    throw new ArgumentOutOfRangeException(nameof(parent));

            }

            return checkDI.FullName;
        }
        public static string ParentSiblingDirectory(this string dir, string sibling)
        {
            var checkRoot = $"{dir}/..";
            var checkDI = new DirectoryInfo($"{checkRoot}/{sibling}");
            while (!checkDI.Exists || !checkDI.Name.Equals(sibling, StringComparison.InvariantCultureIgnoreCase))
            {
                var old = checkDI.FullName;
                checkRoot = $"{checkRoot}/..";
                checkDI = new DirectoryInfo($"{checkRoot}/{sibling}");
                if (checkDI.FullName.Equals(old, StringComparison.InvariantCultureIgnoreCase))
                    throw new ArgumentOutOfRangeException(nameof(sibling));

            }

            return checkDI.FullName;
        }
    }
}
