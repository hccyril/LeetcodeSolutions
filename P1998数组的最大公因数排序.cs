using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1
{
    // hard, 2022/7/16 rank: 2429
    // 并查集
    internal class P1998数组的最大公因数排序
    {
        static int[] p;
        static void InitPrimes()
        {
            if (p != null) return;
            p = new int[100001];
            for (int i = 2; i <= 100000; i += 2) p[i] = 2;
            for (int i = 3; i <= 100000; i += 2)
                if (p[i] == 0)
                    for (int j = i; j <= 100000; j += i)
                        p[j] = i;
        }
        public bool GcdSort(int[] nums)
        {
            InitPrimes();
            int[] tgts = nums.OrderBy(n => n).ToArray();
            UnionFind uni = new(100001);
            foreach (int n in nums)
            {
                for (int r = n, f = p[n]; f < n; )
                {
                    uni.Union(f, n);
                    if (r == f) break;
                    while (r > f && p[r] == f) r /= f;
                    f = p[r];
                }
            }
            for (int i = 0; i < nums.Length; ++i)
                if (!uni.Check(nums[i], tgts[i]))
                    return false;
            return true;
        }

        internal static int Run()
        {
            Console.WriteLine("P1998 Run");
            return 0;
        }
    }
}
