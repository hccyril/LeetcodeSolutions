using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1
{
    /**
     * 这题注定载入史册
     * 1 当年用C#超时，改成C之后就过了，至今仍然是唯一一题用C提交通过的题目，也是唯一一题用语言特性优化成功通过的
     * 2 后来改了算法加了前缀和优化，居然出现了诡异的“代码是错的但是提交却AC了”的神奇现象
     * 3 后来查找原因时各种坑，好在最后都最终完全弄明白
     * 再次证明计算机的世界是完完全全的逻辑世界，从来不存在魔法，只有搞不懂的BUG
     * */

    // hard
	// 2021/11/11 用C#实现后超时，改用C重新实现后通过
    // 2022/7/17 用C#重写，DP+前缀和优化
    class P0629K个逆序对数组
    {
        // ver3 将ver2的错误修正
        int[,] DP(int n, int k)
        {
            int K = k;
            int[,] dp = new int[n, k + 1];
            for (int i = 0; i < n; ++i) dp[i, 0] = 1;
            for (k = 1; k <= K; ++k)
            {
                dp[0, k] = 1; // (1) ver2漏了这一句，就因为这个导致最后阴差阳错结果又对了
                for (int i = 1; i < n; ++i)
                {
                    // dp[i, k] = dp[i - 1, k] + dp[i - 1, k - 1] + dp[i - 1, k - 2] + ... (总共i + 1)项
                    dp[i, k] = dp[i - 1, k];
                    if (k - i > 0)
                        dp[i, k] = dp[i, k].Sub(dp[i - 1, k - i - 1]);
                    //DEBUG*/ int t = dp[i,k];
                    dp[i, k] = dp[i, k].Add(dp[i, k - 1]);
                    //DEBUG*/ Console.WriteLine("dp[{0},{1}]: {2} => {3}", i + 1, k, t, dp[i,k]);
                }
            }
            return dp;
        }

        public int KInversePairs(int n, int k)
        {
            if (k == 0) return 1; // (3) 改完之后还产生了个边界问题，好在k=0的结果是固定的
            var dp = DP(n, k);
            return dp[n - 1, k].Sub(dp[n - 1, k - 1]); // (2) 这里其实要算回前缀和的差分，而且一开始还漏了Sub直接用-号了
        }

        // 仔细研究发现，ver2是错的，dp[i,k]表示的是前缀和，那直接返回应该是不对的
        // 但是因为前面也错了一个地方，阴差阳错的最后结果又是对的，详见ver3
        // ver2 - 2022/7/17 使用前缀和优化
        // 44ms (之前用C写的也要1404ms）
        int[,] DP2(int n, int k)
        {
            int K = k;
            int[,] dp = new int[n, k + 1];
            for (int i = 0; i < n; ++i) dp[i, 0] = 1;
            for (k = 1; k <= K; ++k)
            {
                for (int i = 1; i < n; ++i)
                {
                    // dp[i, k] = dp[i - 1, k] + dp[i - 1, k - 1] + dp[i - 1, k - 2] + ... (总共i + 1)项
                    dp[i, k] = dp[i - 1, k];
                    if (k - i > 0)
                        dp[i, k] = dp[i, k].Sub(dp[i - 1, k - i - 1]);
                    dp[i, k] = dp[i, k].Add(dp[i, k - 1]);
                }
            }
            return dp;
        }

        public int KInversePairs_ver2(int n, int k)
            => DP2(n, k)[n - 1, k];


        // ver1 - C# 版本超时
        // 动态规划
        int[,] DP1(int n, int k)
        {
            int[,] dp = new int[n, k + 1];
            for (int i = 0; i < n; ++i) dp[i, 0] = 1;
            for (int j = 1; j <= k; ++j)
            {
                dp[0, j] = 0;
                for (int i = 1; i < n; ++i)
                {
                    dp[i, j] = dp[i - 1, j];
                    for (int c = 0; c < i; ++c)
                    {
                        int a = i - c; // count of new pairs
                        int r = j - a; // count of prev
                        if (r >= 0)
                            dp[i, j] = dp[i, j].Add(dp[i - 1, r]);
                    }
                }
            }
            return dp;
        }

        public int KInversePairs_ver1(int n, int k)
        {
            return DP1(n, k)[n - 1, k];
        }

        internal static void Run()
        {
            var dp = new P0629K个逆序对数组().DP(3, 1);
            for (int i = 0; i < 2; ++i)
            {
                for (int j = 0; j < 3; ++j)
                    Console.Write(dp[j, i] + " ");
                Console.WriteLine();
            }
        }
    }
}
