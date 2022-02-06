using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1
{
    // medium, binary test
    // same problem: https://leetcode-cn.com/problems/nZZqjQ/
    internal class P0875爱吃香蕉的珂珂
    {
        public int MinEatingSpeed(int[] piles, int h)
        {
            if (h == piles.Length) return piles.Max();
            Array.Sort(piles); 
            int l = 1, r = piles.Last();
            while (l < r)
            {
                int mid = l + r >> 1;
                if (CanEat(mid, piles, h))
                    r = mid;
                else
                    l = mid + 1;
            }
            return l;
        }

        private bool CanEat(int n, int[] piles, int h)
        {
            int i = -1, eat = 0, sum = 0, t = 0;
            while (eat < piles.Last() && sum <= h)
            {
                i = i < 0 ? 0 : UpperBound(piles, eat, i);
                t = (piles[i] - eat) / n;
                eat += t * n; if (eat < piles[i]) { eat += n; ++t; }
                sum += (piles.Length - i) * t;
            }
            return sum <= h;
        }

        private int UpperBound(int[] piles, int eat, int i)
        {
            int bi = Array.BinarySearch(piles, i, piles.Length - i, eat);
            return bi >= 0 ? bi + 1 : -bi - 1;
        }

        internal static void Run()
        {
            // ver1: 超时
            int[] piles = {332484035,524908576,855865114,632922376,222257295,690155293,112677673,679580077,337406589,290818316,877337160,901728858,679284947,688210097,692137887,718203285,629455728,941802184};
            int h = 823855818;

            //int[] piles = { 30, 11, 23, 4, 20 };
            //int h = 6; // 5: 30, 6: 23
            int r = new P0875爱吃香蕉的珂珂().MinEatingSpeed(piles, h);
            Console.WriteLine(r);
        }
    }
}
