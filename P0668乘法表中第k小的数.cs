using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1
{
    // hard, 进阶（二维）的二分法advanced binary search // 2021/11/16 daily 
    // DO 2022/5/18 Daily
    class P0668乘法表中第k小的数
    {
        // 返回小于等于target的数有多少个，以及当中最大的是多少
        (int count, int max) GetNumber(int m, int n, int target)
        {
            int count = 0, max = 0;
            for (int i = 1, j = n; i <= m; ++i)
            {
                int l = i, r = j;
                while (l < r)
                {
                    int mid = l + r + 1 >> 1;
                    if (i * mid > target) r = mid - 1;
                    else l = mid;
                }
                j = l;
                int multi = i * j;
                if (multi > target) break;
                if (j < m) m = j;
                count += j - i + 1 + m - i;
                max = Math.Max(max, multi);
            }
            return (count, max);
        }

        public int FindKthNumber(int m, int n, int k)
        {
            if (m > n) (m, n) = (n, m);
            int l = 1, r = m * n;
            while (l < r)
            {
                int mid = l + r >> 1;
                (int count, int max) = GetNumber(m, n, mid);
                if (count < k) l = mid + 1;
                else r = max;
            }
            return l;
        }

        internal static void Run()
        {
            var sln = new P0668乘法表中第k小的数();
            Console.WriteLine(sln.FindKthNumber(3, 3, 5));
        }
    }
}
