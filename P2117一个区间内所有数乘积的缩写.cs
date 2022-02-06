using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1
{
    // hard, 周赛题
    internal class P2117一个区间内所有数乘积的缩写
    {
        decimal upper = 1m, lower = 1m, Limit = 1m; // limit: 10^24
        int zero = 0, two = 0, five = 0;
        void Multiply(decimal val)
        {
            upper *= val;
            while (upper > Limit) upper /= 10m;
            if (lower > Limit) lower %= Limit;
            lower *= val;
        }

        void Calc(decimal val)
        {
            while (val % 2m == 0)
            {
                ++two;
                val /= 2m;
            }
            while (val % 5m == 0)
            {
                ++five;
                val /= 5m;
            }
            int min = Math.Min(two, five);
            zero += min;
            two -= min;
            five -= min;
            Multiply(val);
        }

        string Output()
        {
            if (lower < 10000000000m) return $"{lower}e{zero}";
            while (upper > 100000m) upper /= 10m;
            int up = (int)upper;
            int low = (int)(lower % 100000m);
            return $"{up:d5}...{low:d5}e{zero}";
        }

        public string AbbreviateProduct(int left, int right)
        {
            for (int i = 0; i < 24; ++i) Limit *= 10m;
            for (int val = left; val <= right; ++val)
            {
                decimal d = val;
                Calc(d);
            }
            for (int i = 0; i < two; ++i) Multiply(2m);
            for (int i = 0; i < five; ++i) Multiply(5m);
            return Output();
        }

        internal static void Run()
        {
            string result = new P2117一个区间内所有数乘积的缩写().AbbreviateProduct(44, 9556);
            Console.WriteLine(result);
        }
    }
}
