﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1
{
    /**656. 金币路径
    给定一个数组 A（下标从 1 开始）包含 N 个整数：A1，A2，……，AN 和一个整数 B。你可以从数组 A 中的任何一个位置（下标为 i）跳到下标 i+1，i+2，……，i+B 的任意一个可以跳到的位置上。如果你在下标为 i 的位置上，你需要支付 Ai 个金币。如果 Ai 是 -1，意味着下标为 i 的位置是不可以跳到的。

    现在，你希望花费最少的金币从数组 A 的 1 位置跳到 N 位置，你需要输出花费最少的路径，依次输出所有经过的下标（从 1 到 N）。

    如果有多种花费最少的方案，输出字典顺序最小的路径。

    如果无法到达 N 位置，请返回一个空数组。

 

    样例 1 :

    输入: [1,2,4,-1,2], 2
    输出: [1,3,5]
 

    样例 2 :

    输入: [1,2,4,-1,2], 1
    输出: []
 

    注释 :

    路径 Pa1，Pa2，……，Pan 是字典序小于 Pb1，Pb2，……，Pbm 的，当且仅当第一个 Pai 和 Pbi 不同的 i 满足 Pai < Pbi，如果不存在这样的 i 那么满足 n < m。
    A1 >= 0。 A2, ..., AN （如果存在） 的范围是 [-1, 100]。
    A 数组的长度范围 [1, 1000].
    B 的范围 [1, 100].
     * */
    // hard, plus, 2022/3/2
    // DP
    internal class P0656金币路径
    {
        public IList<int> CheapestJump(int[] coins, int maxJump)
        {
            int[] next = new int[coins.Length];
            long[] dp = new long[coins.Length];
            Array.Fill(next, -1);
            List<int> res = new();
            for (int i = coins.Length - 2; i >= 0; i--)
            {
                long min_cost = long.MaxValue;
                for (int j = i + 1; j <= i + maxJump && j < coins.Length; j++)
                {
                    if (coins[j] >= 0)
                    {
                        long cost = coins[i] + dp[j];
                        if (cost < min_cost)
                        {
                            min_cost = cost;
                            next[i] = j;
                        }
                    }
                }
                dp[i] = min_cost;
            }
            int k;
            for (k = 0; k < coins.Length && next[k] > 0; k = next[k])
                res.Add(k + 1);
            if (k == coins.Length - 1 && coins[k] >= 0)
                res.Add(coins.Length);
            else
                return new List<int>();
            return res;
        }

        /* Official-Java
       public List < Integer > cheapestJump(int[] A, int B) {
            int[] next = new int[A.length];
            long[] dp = new long[A.length];
            Arrays.fill(next, -1);
            List < Integer > res = new ArrayList();
            for (int i = A.length - 2; i >= 0; i--) {
                long min_cost = Integer.MAX_VALUE;
                for (int j = i + 1; j <= i + B && j < A.length; j++) {
                    if (A[j] >= 0) {
                        long cost = A[i] + dp[j];
                        if (cost < min_cost) {
                            min_cost = cost;
                            next[i] = j;
                        }
                    }
                }
                dp[i] = min_cost;
            }
            int i;
            for (i = 0; i < A.length && next[i] > 0; i = next[i])
                res.add(i + 1);
            if (i == A.length - 1 && A[i] >= 0)
                res.add(A.length);
            else
                return new ArrayList < Integer > ();
            return res;
        }
         * */
    }
}
