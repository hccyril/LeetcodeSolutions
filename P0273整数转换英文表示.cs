using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1
{
    class P0273整数转换英文表示
    {
        static readonly string[] NUMS = { "Zero", "One", "Two", "Three", "Four", "Five", "Six", "Seven", "Eight", "Nine", "Ten", "Eleven", "Twelve", "Thirteen", "Fourteen", "Fifteen", "Sixteen", "Seventeen", "Eighteen", "Nineteen" };
        static readonly string[] TENS = { "", "", "Twenty", "Thirty", "Forty", "Fifty", "Sixty", "Seventy", "Eighty", "Ninety" };
        string Append(string s1, string s2)
        {
            string s = s1 ?? "";
            if (!string.IsNullOrEmpty(s2) && s2 != NUMS[0])
            {
                if (s != "") s += " ";
                s += s2;
            }
            return s == "" ? NUMS[0] : s;
        }
        public string NumberToWords(int num)
        {
            if (num >= 1000000000)
                return Append(NumberToWords(num / 1000000000) + " Billion", NumberToWords(num % 1000000000));

            if (num >= 1000000)
                return Append(NumberToWords(num / 1000000) + " Million", NumberToWords(num % 1000000));

            if (num >= 1000)
                return Append(NumberToWords(num / 1000) + " Thousand", NumberToWords(num % 1000));

            if (num >= 100)
                return Append(NumberToWords(num / 100) + " Hundred", NumberToWords(num % 100));

            if (num >= 20)
                return Append(TENS[num / 10], NumberToWords(num % 10));

            return NUMS[num];
        }
    }
}
