using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cvl.ApplicationServer.Core.Extensions
{
    public static class StringExtension
    {
        public static string? Truncate(this string? value, int maxLength)
        {
            if (!string.IsNullOrEmpty(value) && value.Length > maxLength-5)
            {
                return value.Substring(0, maxLength) + "...";
            }

            return value;
        }
    }
}
