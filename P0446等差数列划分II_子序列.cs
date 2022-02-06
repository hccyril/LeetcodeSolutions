using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1
{
    /**446. 等差数列划分 II - 子序列 （和413基本一样，除了数据范围，还有子数组可以不连续）
     * 给你一个整数数组 nums ，返回 nums 中所有 等差子序列 的数目。

如果一个序列中 至少有三个元素 ，并且任意两个相邻元素之差相同，则称该序列为等差序列。

例如，[1, 3, 5, 7, 9]、[7, 7, 7, 7] 和 [3, -1, -5, -9] 都是等差序列。
再例如，[1, 1, 2, 5, 7] 不是等差序列。
数组中的子序列是从数组中删除一些元素（也可能不删除）得到的一个序列。

例如，[2,5,10] 是 [1,2,1,2,4,1,5,10] 的一个子序列。
题目数据保证答案是一个 32-bit 整数。

链接：https://leetcode-cn.com/problems/arithmetic-slices-ii-subsequence
     * */
    class P0446等差数列划分II_子序列
    {
        int N;
        int[,] dp;
        Dictionary<long, List<int>> dic = new Dictionary<long, List<int>>();
        public IEnumerable<int> FindK(int i, int x, int y)
        {
            long diff = (long)x - (long)y;
            long key = ((long)i << 36) | (diff + (1L << 33));
            if (dic.ContainsKey(key))
                return dic[key];
            return new int[0];
        }
        public void AddK(int i, int j, int x, int y)
        {
            long diff = (long)x - (long)y;
            long key = ((long)i << 36) | (diff + (1L << 33));
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
                for (int j = 0; j < i; ++j)
                {
                    dp[i, j] = 0;
                    foreach (var k in FindK(j, nums[i], nums[j]))
                    {
                        dp[i, j] += dp[j, k] + 1;
                    }
                    count += dp[i, j];
                    AddK(i, j, nums[i], nums[j]);
                }
            }
            return count;
        }

        internal static void Run()
        {
            int[] input = { 2147483638, 2147483639, 2147483640, 2147483641, 2147483642, 2147483643, 2147483644, 2147483645, 2147483646, 2147483647, -2147483648, -2147483647, -2147483646, -2147483645, -2147483644, -2147483643, -2147483642, -2147483641, -2147483640, -2147483639 };
            //int[] input = { -2147483648, 0, -2147483648 };
            Console.WriteLine(new P0446等差数列划分II_子序列().NumberOfArithmeticSlices(input));
        }
    }
}
