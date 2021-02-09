using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security;
using System.Text;

namespace System
{ 
    public static class StringExtensions
    {
        /// <summary>
        /// Extension to help make code requiring a Secure password from a string literal more readable
        /// </summary>
        /// <param name="s">clear text string to convert</param>
        /// <returns>The secured representation of the string</returns>
        public static SecureString Secure(this string s)
        {
            return new NetworkCredential("", s).SecurePassword;
        }
    }
}
