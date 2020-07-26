using System;
using System.Collections.Generic;
using System.Text;

namespace Cvl.ApplicationServer.ProcessesEngine.Model
{
    public class ProcessId
    {
        private int id;

        public ProcessId(int id)
        {
            this.id = id;
        }

        public override string ToString()
        {
            return DecimalToArbitrarySystem(id);
        }

        /// <summary>
        /// Converts the given decimal number to the numeral system 
        /// </summary>
        /// <param name="decimalNumber">The number to convert.</param>
        /// <returns></returns>
        private static string DecimalToArbitrarySystem(long decimalNumber)
        {
            const int BitsInLong = 64;
            const string Digits = "0123456789ABCDEFGHJKLMNPRSTUWXY";
            int radix = Digits.Length;

            if (decimalNumber == 0)
                return "0";

            int index = BitsInLong - 1;
            long currentNumber = Math.Abs(decimalNumber);
            char[] charArray = new char[BitsInLong];

            while (currentNumber != 0)
            {
                int remainder = (int)(currentNumber % radix);
                charArray[index--] = Digits[remainder];
                currentNumber = currentNumber / radix;
            }

            string result = new string(charArray, index + 1, BitsInLong - index - 1);
            if (decimalNumber < 0)
            {
                result = "-" + result;
            }

            return result;
        }

        /// <summary>
        /// Converts the given number from the numeral system with the specified
        /// radix (in the range [2, 36]) to decimal numeral system.
        /// </summary>
        /// <param name="number">The numeral system number to convert.</param>
        /// <returns></returns>
        private static long ArbitraryToDecimalSystem(string number)
        {
            const string Digits = "0123456789ABCDEFGHJKLMNPRSTUWXY";
            int radix = Digits.Length;


            if (string.IsNullOrEmpty(number))
                return 0;

            // Make sure the arbitrary numeral system number is in upper case
            number = number.ToUpperInvariant();

            long result = 0;
            long multiplier = 1;
            for (int i = number.Length - 1; i >= 0; i--)
            {
                char c = number[i];
                if (i == 0 && c == '-')
                {
                    // This is the negative sign symbol
                    result = -result;
                    break;
                }

                int digit = Digits.IndexOf(c);
                if (digit == -1)
                    throw new ArgumentException(
                        "Invalid character in the arbitrary numeral system number",
                        "number");

                result += digit * multiplier;
                multiplier *= radix;
            }

            return result;
        }
    }
}
