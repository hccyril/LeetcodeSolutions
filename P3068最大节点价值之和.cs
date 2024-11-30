using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1;

// hard, 2024/5/19 US Daily
// rating 2267
// Tree DP
internal class P3068最大节点价值之和
{
    // 基于上一个版本进行修正
    // dp[i].t0: 当前子树最优解 dp[i].t1: 当前子树+当前根节点再翻转一次 的最优解
    public long MaximumValueSum(int[] nums, int k, int[][] edges)
    {
        int n = nums.Length;
        var tg = edges.TreeGraph();
        (long t0, long t1)[] dp = new (long t0, long t1)[n];

        // TreeDfs模板2
        Stack<(int Node, int Child)> dfsStk = new();
        int nodeIndex = 0, childIndex = 0;
        while (true)
        {
            if (childIndex == tg[nodeIndex].Count)
            {
                if (childIndex == 1 && nodeIndex != 0)
                {
                    // leaf node
                    dp[nodeIndex] = (nums[nodeIndex], nums[nodeIndex] ^ k);
                }
                else
                {
                    int parent = dfsStk.Any() ? dfsStk.Peek().Node : -1;
                    long diff = -1, sm = 0;
                    int flg = 0;
                    foreach ((long t0, long t1) in from i in tg[nodeIndex]
                                                   where i != parent
                                                   select dp[i])
                    {
                        if (t0 >= t1)
                        {
                            sm += t0;
                            diff = diff < 0 ? t0 - t1 : Math.Min(diff, t0 - t1);
                        }
                        else
                        {
                            flg ^= 1;
                            sm += t1;
                            diff = diff < 0 ? t1 - t0 : Math.Min(diff, t1 - t0);
                        }
                    }
                    // fix1
                    long s0 = sm, s1 = sm - diff;
                    int n0 = nums[nodeIndex], n1 = nums[nodeIndex] ^ k;
                    if (flg == 0)
                    {
                        dp[nodeIndex] = (Math.Max(s0 + n0, s1 + n1), Math.Max(s0 + n1, s1 + n0));
                    }
                    else
                    {
                        dp[nodeIndex] = (Math.Max(s0 + n1, s1 + n0), Math.Max(s0 + n0, s1 + n1));
                    }
                }

                if (dfsStk.Any())
                {
                    (nodeIndex, childIndex) = dfsStk.Pop();
                    continue;
                }
                else break;
            }

            int nextIndex = tg[nodeIndex][childIndex];

            if (dfsStk.Any() && dfsStk.Peek().Node == nextIndex)
            {
                ++childIndex;
                continue;
            }
            else
            {   // dfs next
                dfsStk.Push((nodeIndex, childIndex + 1));
                (nodeIndex, childIndex) = (nextIndex, 0);
            }
        }
        // fix2
        return dp[0].t0; //Math.Max(dp[0].t0, dp[0].t1);
    }

    // ver1 WA
    // 节点翻转时的一个细节想错了，所以果然题目还是要实际做出来，之前在构思的时候都没发现这个细节
    public long MaximumValueSum_ver1(int[] nums, int k, int[][] edges)
    {
        int n = nums.Length;
        var tg = edges.TreeGraph();
        (long t0, long t1)[] dp = new (long t0, long t1)[n];

        // TreeDfs模板2
        Stack<(int Node, int Child)> dfsStk = new();
        int nodeIndex = 0, childIndex = 0;
        while (true)
        {
            if (childIndex == tg[nodeIndex].Count)
            {
                if (childIndex == 1 && nodeIndex != 0)
                {
                    // leaf node
                    dp[nodeIndex] = (nums[nodeIndex], nums[nodeIndex] ^ k);
                }
                else
                {
                    int parent = dfsStk.Any() ? dfsStk.Peek().Node : -1;
                    long diff = -1, sm = 0;
                    int flg = 0;
                    foreach ((long t0, long t1) in from i in tg[nodeIndex]
                                                   where i != parent
                                                   select dp[i])
                    {
                        if (t0 >= t1)
                        {
                            sm += t0;
                            diff = diff < 0 ? t0 - t1 : Math.Min(diff, t0 - t1);
                        }
                        else
                        {
                            flg ^= 1;
                            sm += t1;
                            diff = diff < 0 ? t1 - t0 : Math.Min(diff, t1 - t0);
                        }
                    }
                    if (flg == 0)
                    {
                        dp[nodeIndex] = (sm + nums[nodeIndex], sm - diff + (nums[nodeIndex] ^ k));
                    }
                    else
                    {
                        dp[nodeIndex] = (sm - diff + nums[nodeIndex], sm + (nums[nodeIndex] ^ k));
                    }
                }

                if (dfsStk.Any())
                {
                    (nodeIndex, childIndex) = dfsStk.Pop();
                    continue;
                }
                else break;
            }

            int nextIndex = tg[nodeIndex][childIndex];

            if (dfsStk.Any() && dfsStk.Peek().Node == nextIndex)
            {
                ++childIndex;
                continue;
            }
            else
            {   // dfs next
                dfsStk.Push((nodeIndex, childIndex + 1));
                (nodeIndex, childIndex) = (nextIndex, 0);
            }
        }
        return Math.Max(dp[0].t0, dp[0].t1);
    }

    internal static void Run()
    {
        var sln = new P3068最大节点价值之和();
        int[] nums = { 1, 2, 1 }, e1 = { 0, 1 }, e2 = { 0, 2 };
        int[][] edges = { e1, e2 };
        int k = 3;
        var ans = sln.MaximumValueSum(nums, k, edges);
        Console.WriteLine("P3068最大节点价值之和 ans=" + ans);
    }
}
