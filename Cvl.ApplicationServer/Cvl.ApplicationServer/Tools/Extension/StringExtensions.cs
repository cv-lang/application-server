using System;
using System.Collections.Generic;
using System.Text;

namespace Cvl.ApplicationServer.Tools.Extension
{
    public static class StringExtensions
    {
        public static string TruncateLongString(this string str, int maxLength)
        {
            if (str == null)
            {
                return null;
            }
            return str.Substring(0, Math.Min(str.Length, maxLength));
        }
    }
}
