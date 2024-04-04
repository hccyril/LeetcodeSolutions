using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1;

// hard, 2022/4/16
// rank: 2410 // 一开始的转化没想到，看完提示后才自己写出来
// DP
// 2024/2/8 补做贪心法 ref: https://leetcode.cn/problems/p0NxJO/solutions/2633172/fan-hui-tan-xin-fu-ti-dan-pythonjavacgoj-hxup
// 另外实测确认本算法也适用于《打家劫舍II》，但在打家劫舍中动态规划的效率比贪心法要高，算是“贪心法通常比动态规划效率高”的一个反例
internal class P1388_3n块披萨
{
    // 贪心（优先队列）
    public int MaxSizeSlices(int[] a)
    {
        int n = a.Length;
        // 用数组模拟链表
        int[] lp = Enumerable.Range(0, n).Select(i => i - 1).ToArray(); lp[0] = n - 1;
        int[] rp = Enumerable.Range(0, n).Select(i => i + 1).ToArray(); rp[n - 1] = 0;
        BitArray rd = new(n); // rd[i] = true when i has popped

        PriorityQueue<int, int> pq = new(Enumerable.Range(0, n).Select(i => (i, -a[i])));
        int k = n / 3, sm = 0;
        while (k-- > 0)
        {
            int i = pq.Dequeue();
            while (rd[i]) i = pq.Dequeue(); // discard lazy removes

            sm += a[i];
            a[i] = a[lp[i]] + a[rp[i]] - a[i]; // core - pick a[i] or pick l + r
            rd[lp[i]] = rd[rp[i]] = true;
            lp[i] = lp[lp[i]]; rp[i] = rp[rp[i]];
            rp[lp[i]] = lp[rp[i]] = i;

            pq.Enqueue(i, -a[i]);
        }
        return sm;
    }

    // ver2: 进阶版打家劫舍II
    // 但还没想明白为什么题目可以转化成数组中取n个不相邻的数
    public int MaxSizeSlices_DP(int[] slices)
    {
        int n = slices.Length / 3;
        int[,] dp1 = new int[slices.Length - 1, n + 1],
            dp2 = new int[slices.Length, n + 1];

        for (int i = 0; i < slices.Length; i++)
        {
            if (i < slices.Length - 1)
                dp1[i, 1] = Math.Max(slices[i], i == 0 ? 0 : dp1[i - 1, 1]);
            if (i > 0)
                dp2[i, 1] = Math.Max(slices[i], dp2[i - 1, 1]);
        }

        for (int pizza = 2; pizza <= n; ++pizza)
        {
            for (int i = 0; i < slices.Length; ++i)
            {
                if (i < slices.Length - 1)
                {
                    dp1[i, pizza] = i == 0 ? 0 : dp1[i - 1, pizza];
                    if (i > 1 && dp1[i - 2, pizza - 1] > 0)
                        dp1[i, pizza] = Math.Max(dp1[i, pizza], slices[i] + dp1[i - 2, pizza - 1]);
                }
                if (i > 0)
                {
                    dp2[i, pizza] = dp2[i - 1, pizza];
                    if (i > 1 && dp2[i - 2, pizza - 1] > 0)
                        dp2[i, pizza] = Math.Max(dp2[i, pizza], slices[i] + dp2[i - 2, pizza - 1]);
                }
            }
        }
#if DEBUG_x
        for (int i = 0; i <= n; ++i)
        {
            for (int j = 0; j < slices.Length; ++j)
                Console.Write(" " + dpx[j, i]);
            Console.WriteLine();
        }
#endif
        return Math.Max(dp1[slices.Length - 2, n], dp2[slices.Length - 1, n]);
    }

    int[,] dp;
    int[] arr;
    int Adjust(int i)
    {
        if (i >= arr.Length) i -= arr.Length;
        if (i < 0) i += arr.Length;
        return i;
    }
    int DpDfs(int start, int end)
    {
        start = Adjust(start);
        end = Adjust(end);
        if (Adjust(end + 1) == start) return 0;
        if (dp[start, end] > 0) return dp[start, end];
        if (Adjust(start + 2) == end) return dp[start, end] = arr[Adjust(start + 1)];
        int ans = 0;
        int loop_end = end;
        if (end < start) loop_end += arr.Length;
        for (int i = start; i <= loop_end - 2; i += 3)
            for (int j = i + 1; j <= loop_end - 1; j += 3)
                for (int k = j + 1; k <= loop_end; k += 3)
                {
                    int ai = Adjust(i), aj = Adjust(j), ak = Adjust(k);
                    ans = Math.Max(ans,
                        arr[aj] + DpDfs(start, ai - 1) + DpDfs(ai + 1, aj - 1) + DpDfs(aj + 1, ak - 1) + DpDfs(ak + 1, end));
                }
#if DEBUG
        //Console.WriteLine($"dp {start} {end} => {ans}");
#endif
        return dp[start, end] = ans;
    }
    public int MaxSizeSlices_ver1(int[] slices)
    {
        arr = slices;
        dp = new int[slices.Length, slices.Length];
        int ans = 0;
        for (int i = 0; i < slices.Length - 2; i++)
            for (int j = i + 1; j < slices.Length - 1; j += 3)
                for (int k = j + 1; k < slices.Length; k += 3)
                {
                    ans = Math.Max(ans,
                        Math.Max(Math.Max(slices[i], slices[j]), slices[k]) +
                        DpDfs(i + 1, j - 1) + DpDfs(j + 1, k - 1) + DpDfs(k + 1, i - 1));
                }
        return ans;
    }

    internal static void Run()
    {
        int[] input = { 1, 2, 3, 4, 5, 6 };

        // input1
        //int[] input = { 4, 1, 2, 5, 8, 3, 1, 9, 7 };

        // input2 - TLE
        // n = 222
        // ans: 28174
        // Total Time: 50781ms
        //int[] input = { 120, 134, 146, 20, 66, 339, 7, 328, 354, 415, 337, 90, 22, 198, 87, 307, 83, 334, 242, 243, 369, 332, 328, 198, 266, 480, 392, 87, 471, 201, 455, 168, 291, 116, 269, 282, 166, 338, 149, 465, 464, 48, 138, 1, 298, 493, 485, 401, 310, 32, 473, 102, 324, 271, 78, 355, 234, 16, 41, 207, 82, 424, 498, 15, 384, 241, 336, 309, 429, 41, 461, 121, 491, 381, 331, 360, 330, 145, 17, 295, 368, 352, 318, 47, 462, 59, 29, 97, 302, 328, 251, 306, 403, 217, 145, 46, 362, 283, 88, 468, 39, 30, 163, 371, 336, 62, 500, 28, 490, 354, 218, 211, 378, 120, 214, 173, 444, 238, 495, 6, 432, 220, 67, 147, 313, 57, 347, 140, 64, 473, 337, 90, 394, 267, 287, 415, 320, 141, 254, 128, 60, 110, 421, 313, 434, 157, 113, 40, 65, 429, 378, 312, 327, 105, 77, 26, 121, 380, 481, 452, 428, 464, 470, 82, 406, 427, 62, 86, 87, 20, 276, 360, 265, 174, 120, 85, 211, 104, 309, 387, 383, 228, 236, 129, 233, 240, 121, 63, 95, 250, 299, 378, 154, 404, 428, 198, 91, 322, 85, 386, 57, 422, 301, 83, 17, 217, 162, 385, 63, 193, 367, 137, 385, 13, 77, 182, 219, 319, 48, 68, 404, 358 };

        // input3: WA
        // expected: 30, ans: 32 (反而还算多了)
        //int[] input = { 9, 5, 1, 7, 8, 4, 4, 5, 5, 8, 7, 7 };

        var sln = new P1388_3n块披萨();
        Random rand = new();
        //Console.WriteLine(sln.MaxSizeSlices(input));

        // 速度对比测试: 结果：DP大约29秒，贪心法57ms
        int n = 20001;
        int[] a = new int[n];
        for (int i = 0; i < n; ++i) a[i] = rand.Next(5000) + 1;

        var t = System.Diagnostics.Stopwatch.StartNew();
        Console.WriteLine("ans=" + sln.MaxSizeSlices_DP(a));
        t.Stop();
        Console.WriteLine("t1 = " + t.ElapsedMilliseconds);

        t.Restart();
        Console.WriteLine("ans=" + sln.MaxSizeSlices(a));
        t.Stop();
        Console.WriteLine("t2 = " + t.ElapsedMilliseconds);

    }
}
