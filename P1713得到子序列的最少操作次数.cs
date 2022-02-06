using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1
{
    class P1713得到子序列的最少操作次数
    {
        // 该方法参考了P300的题解（贪心+二分）
        public int LengthOfLIS(IList<int> nums)
        {
            int len = 1, n = nums.Count;
            if (n == 0)
            {
                return 0;
            }
            int[] d = new int[n + 1];
            d[len] = nums[0];
            for (int i = 1; i < n; ++i)
            {
                if (nums[i] > d[len])
                {
                    d[++len] = nums[i];
                }
                else
                {
                    int l = 1, r = len, pos = 0; 
                    while (l <= r)
                    {
                        int mid = (l + r) >> 1;
                        if (d[mid] < nums[i])
                        {
                            pos = mid;
                            l = mid + 1;
                        }
                        else
                        {
                            r = mid - 1;
                        }
                    }
                    d[pos + 1] = nums[i];
                }
            }
            return len;
        }

        public int MinOperations(int[] target, int[] arr)
        {
            int m = target.Length, n = arr.Length;
            Dictionary<int, int> dic = new Dictionary<int, int>();
            for (int i = 0; i < m; ++i)
            {
                dic.Add(target[i], i);
            }

            List<int> list = new List<int>();
            foreach (var a in arr)
                if (dic.ContainsKey(a))
                    list.Add(dic[a]);

            return m - LengthOfLIS(list);
        }

        /**
         * 方法1：二维动态规划：超时
         * */
        //public int MinOperations(int[] target, int[] arr)
        //{
        //    int m = target.Length, n = arr.Length;
        //    int[,] dp = new int[m + 1, n + 1];
        //    for (int i = 1; i <= m; ++i)
        //    {
        //        for (int j = 1; j <= n; ++j)
        //        {
        //            var d12 = Math.Max(dp[i - 1, j], dp[i, j - 1]);
        //            var d3 = (target[i - 1] == arr[j - 1] ? 1 : 0) + dp[i - 1, j - 1];
        //            dp[i, j] = Math.Max(d12, d3);
        //        }
        //    }
        //    return target.Length - dp[m, n];
        //}
    }
}
