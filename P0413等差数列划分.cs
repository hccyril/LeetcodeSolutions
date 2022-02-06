using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1
{
    /** 413. 等差数列划分
     * 如果一个数列 至少有三个元素 ，并且任意两个相邻元素之差相同，则称该数列为等差数列。

        例如，[1,3,5,7,9]、[7,7,7,7] 和 [3,-1,-5,-9] 都是等差数列。
        给你一个整数数组 nums ，返回数组 nums 中所有为等差数组的 子数组 个数。

        子数组 是数组中的一个连续序列。

        链接：https://leetcode-cn.com/problems/arithmetic-slices
     */
    class P0413等差数列划分
    {
        int N;
        int[,] dp;
        Dictionary<int, List<int>> dic = new Dictionary<int, List<int>>();
        public IEnumerable<int> FindK(int i, int diff)
        {
            int key = (i << 12) | (diff + 2000);
            if (dic.ContainsKey(key))
                return dic[key];
            return new int[0];
        }
        public void AddK(int i, int j, int diff)
        {
            int key = (i << 12) | (diff + 2000);
            if (!dic.ContainsKey(key))
                dic.Add(key, new List<int>());
            dic[key].Add(j);
        }
        public int NumberOfArithmeticSlices(int[] nums)
        {
            N = nums.Length;
            dp = new int[N, N];
            for (int i = 0; i < N; ++i)
                for (int j = 0; j < N; ++j)
                    dp[i, j] = -1;

            int count = 0;
            for (int i = 1; i < N; ++i)
            {
                /**
                 * 没看清以为子数组可以是不连续的
                 * 对于数组一定连续，修改下面一行即可，j只可以为i前面的下标
                 * */
                int j = i - 1;
                // for (int j = 0; j < i; ++j)
                {
                    dp[i, j] = 0;
                    int diff = nums[i] - nums[j];
                    foreach (var k in FindK(j, diff))
                    {
                        dp[i, j] += dp[j, k] + 1;
                    }
                    count += dp[i, j];
                    AddK(i, j, diff);
                }
            }
            return count;
        }
    }
}
