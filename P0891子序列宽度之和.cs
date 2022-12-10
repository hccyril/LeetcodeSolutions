using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1
{
    /*
[1, 2, 3, 4]
    1  1  1
       2  2
          4
-1 -1 -1
-2 -2
-4

(2 - 1) x 1
(3 - 1) x 2
(4 - 1) x 4
(3 - 2) x 1
(4 - 2) x 2
(4 - 3) x 1

1 4 12 1 4 1 = 23
     * */

    // hard, 2022/11/18 Daily
    // math
    internal class P0891子序列宽度之和
    {
        const int MAX_N = 100000;
        static int[] dp;
        static void Init()
        {
            if (dp == null)
            {
                dp = new int[MAX_N];
                dp[1] = 1;
                int b = 1, c = 1;
                for (int i = 2; i < MAX_N; ++i)
                {
                    b = b.Add(b);
                    c = c.Add(b);
                    dp[i] = c;
                }
            }
        }
        public int SumSubseqWidths(int[] nums)
        {
            Init();
            Array.Sort(nums);
            int sum = 0;
            for (int i = 0, j = nums.Length - 1; j >= 0; ++i, --j)
                sum = dp[i].Sub(dp[j]).Multi(nums[i]).Add(sum);
                //sum = sum.Add(dp[i]).Sub(dp[j]).Multi(nums[i]);

            return sum;
        }
    }
}
