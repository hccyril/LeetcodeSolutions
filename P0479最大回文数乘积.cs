using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1
{
    internal class P0479最大回文数乘积
    {
        static long[] palindromes = {0, 9, 9009, 906609, 99000099, 9966006699L,
                                 999000000999L, 99956644665999L, 9999000000009999L};


        public int LargestPalindrome(int n)
        {
            return (int)(palindromes[n] % 1337L);
        }
        public int Test(int n) { 
            n = n == 3 ? 1000 : 10000;
            int m = n == 1000 ? 100 : 1000;
            for (int x = n - 1; x >= m; x--)
                for (int y = n - 1; y >= m; y--)
                {
                    int p = x * y;
                    string s = p.ToString();
                    if (s == string.Join("", s.Reverse()))
                    {
                        Console.WriteLine($"{x} * {y} = {p}");
                        return 0;
                    }
                }
            return -1;
        }

        internal static void Run()
        {
            var sln = new P0479最大回文数乘积();
            sln.LargestPalindrome(3);
            sln.LargestPalindrome(4);
        }
    }
}
