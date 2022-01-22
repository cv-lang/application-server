using Cvl.ApplicationServer.Processes.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cvl.ApplicationServer.Processes.Services
{
    public class ProcessNumberGenerator : IProcessNumberGenerator
    {
        public string GenerateProcessNumber(long processId)
        {
            var linearNumberInString36 = BaseXConverter.ConvertTo(processId + 50000); 
            var random = new Random(DateTime.Now.Millisecond + DateTime.Now.Second * 1000);
            var r = random.Next(46657, 1679616); //36^3 , 36^4
            var randStr36 = BaseXConverter.ConvertTo(r);

            var number = linearNumberInString36[^3..] + randStr36[^3..];//ostatnie 3 znaki
            var crc = 0;
            foreach (var item in number)
            {
                var i = BaseXConverter.Chars.IndexOf(item);
                crc ^= i;
            }

            number += BaseXConverter.ConvertTo(crc % BaseXConverter.Base);
            return $"C-{number}";
        }

        public static class BaseXConverter
        {
            
            public static string Chars = "0123456789ABCDEFGHJKMNPQRSTUVWXYZ"; //without o,i,l
            public static int Base = Chars.Length;
            public static string ConvertTo(long value)
            {
                string result = "";

                while (value > 0)
                {
                    result = Chars[(int)(value % Base)] + result; // use StringBuilder for better performance
                    value /= Base;
                }

                return result;
            }
        }
    }
}
