using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1.contest
{
    // 20231008 on US site, AC
    internal class LC_WC0366_20231008
    {
        #region Problem D
        // 题目的操作相当于每个bit都可以任意的移动，因此将所有bit分配到最大的k个数再求结果即可
        public int MaxSum(IList<int> nums, int k)
        {
            Dictionary<int, int> cnt = new();
            for (int i = 0; i < 31; ++i)
                cnt[1 << i] = 0;

            foreach (int num in nums)
            {
                int x = num;
                while (x != 0)
                {
                    int lb = x & -x;
                    cnt[lb]++;
                    x &= (x - 1);
                }
            }

            long sum = 0L;
            for (int i = 0; i < k; ++i)
            {
                int x = 0;
                for (int j = 30; j >= 0; --j)
                    if (cnt[1 << j] > 0)
                    {
                        cnt[1 << j]--;
                        x |= 1 << j;
                    }
                sum += (long)x * x;
                sum %= 1000000007L;
            }
            return (int)sum;
        }
        #endregion

        #region Run Test
        internal static int Run()
        {
            char p = 'D';
            LC_WC0366_20231008 sln = new();

            return p switch
            {
                'A' => sln.RunTestA(),
                'B' => sln.RunTestB(),
                'C' => sln.RunTestC(),
                'D' => sln.RunTestD(),
                'E' => sln.RunTestE(),
                _ => -1
            };
        }

        int RunTestA()
        {
            return 0;
        }

        int RunTestB()
        {
            return 0;
        }

        int RunTestC()
        {
            return 0;
        }

        int RunTestD()
        {
            return 0;
        }

        int RunTestE()
        {
            return 0;
        }
        #endregion
    }
}
