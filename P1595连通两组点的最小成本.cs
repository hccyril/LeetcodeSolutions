using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1;

// hard, 2023/6/20 Daily
// dijkstra but TLE
// 2023/7/8 再次尝试求解（动态规划版本已完成）
// 2024/3/23 再次尝试迭代解法（类似dijkstra），2024/4/4完成
internal class P1595连通两组点的最小成本
{
    // ver5 - real dijkstra
    // 维护当前bm和最后一个点的连接，例如 { (a, a1), (b, b2) }, 枚举：要么b换下一个，或者加上c，即 { (a, a1), (b, b3) } 以及 { (a, a1), (b, b2), (c, c1) }
    // ver5_II 用SHeap优化去重: 终于AC，但是用时2649ms，比起DP版本(159ms)还是劣势明显 
    public int ConnectTwoGroups_ver5_II(IList<IList<int>> cost)
    {
        int m = cost.Count, n = cost[0].Count, capacity = Math.Max(m, n);
        List<(int c, int b)>[] a = new List<(int, int)>[m + n];
        Dictionary<int, int> id = new();
        for (int i = 0; i < m + n; ++i)
        {
            a[i] = new List<(int, int)>(capacity);
            id[1 << i] = i;
        }
        for (int i = 0; i < m; ++i)
            for (int j = 0; j < n; ++j)
            {
                int bm = 1 << i | 1 << m + j;
                a[i].Add((cost[i][j], bm));
                a[m + j].Add((cost[i][j], bm));
            }
        for (int i = 0; i < m + n; ++i)
            a[i].Sort();

        SHeap<(int b, int u, int i), int> hp = new((x, y) => x < y, true);
        hp.Add((0, 0, 0), a[0][0].c);

        int endmap = (1 << m + n) - 1;
        while (hp.Any())
        {
            //Console.WriteLine("Count=" + pq.Count);// DEBUG

            ((int b, int u, int i), int s) = hp.Pop();
            int bm = b | a[u][i].b;
            if (bm == endmap) return s;

            // select 1: change (u, i) to (u, i + 1)
            int s0 = s - a[u][i].c, s1 = 0;
            if (i + 1 < a[u].Count)
            {
                s1 = s0 + a[u][i + 1].c;
                hp.Add((b, u, i + 1), s1);
            }

            // select 2: add (u + 1, 0)
            int v = id[~bm & bm + 1]; // 公式：取从右到左第一个0的位置
            s1 = s + a[v][0].c;
            hp.Add((bm, v, 0), s1);
        }
        return -1;
    }
    // TLE & Out of memory: 见test case #5
    public int ConnectTwoGroups_ver5(IList<IList<int>> cost)
    {
        int m = cost.Count, n = cost[0].Count, capacity = Math.Max(m, n);
        List<(int c, int b)>[] a = new List<(int, int)>[m + n];
        Dictionary<int, int> id = new();
        for (int i = 0; i < m + n; ++i)
        {
            a[i] = new List<(int, int)>(capacity);
            id[1 << i] = i;
        }
        for (int i = 0; i < m; ++i)
            for (int j = 0; j < n; ++j)
            {
                int bm = 1 << i | 1 << m + j;
                a[i].Add((cost[i][j], bm));
                a[m + j].Add((cost[i][j], bm));
            }
        for (int i = 0; i < m + n; ++i)
            a[i].Sort();

        PriorityQueue<(int b, int u, int i, int s), int> pq = new();
        pq.Enqueue((0, 0, 0, a[0][0].c), a[0][0].c);

        int endmap = (1 << m + n) - 1;
        while (pq.Count > 0)
        {
            Console.WriteLine("Count=" + pq.Count);// DEBUG

            (int b, int u, int i, int s) = pq.Dequeue();
            int bm = b | a[u][i].b;
            if (bm == endmap) return s;

            // select 1: change (u, i) to (u, i + 1)
            int s0 = s - a[u][i].c, s1 = 0;
            if (i + 1 < a[u].Count)
            {
                s1 = s0 + a[u][i + 1].c;
                pq.Enqueue((b, u, i + 1, s1), s1);
            }

            // select 2: add (u + 1, 0)
            int v = id[~bm & bm + 1]; // 公式：取从右到左第一个0的位置
            s1 = s + a[v][0].c;
            pq.Enqueue((bm, v, 0, s1), s1);
        }
        return -1;
    }


    // ver4-2 - 2024/4/3
    // 采用另一个迭代的方法：例如当前(1,2)，则枚举加上3，或者2变成3，即(1,2,3), (1,3)
    // 这样枚举的话，只要遇到无效解，就可以跳过（因为后面再加也是无效），一直枚举到有效解为止
    // WA for case 3 - 暂时未知原因
    public int ConnectTwoGroups_ver4_II(IList<IList<int>> cost)
    {
        int m = cost.Count, n = cost[0].Count;
        List<(int c, int b)> a = new(m * n + 1);
        for (int i = 0; i < m; ++i)
            for (int j = 0; j < n; ++j)
                a.Add((cost[i][j], 1 << i | 1 << m + j));
        a.Sort();
        BitSetTree bs = new(m + n);
        Dictionary<int, int> di = new(); // map<bitmap, minsum>

        // orderedSumOfList
        PriorityQueue<(int b, int i), int> pq = new(); // <bitmap, last_i> => min_sum, 注意bitmap是未加上a[i]之前的
        pq.Enqueue((0, 0), a[0].c);
        di[a[0].b] = a[0].c;
        int endmap = (1 << m + n) - 1;
        while (pq.Count > 0)
        {
            (int b, int i) = pq.Dequeue();
            int bm = b | a[i].b;
            if (bm == endmap) return di[bm];
            else if (bs[bm]) continue;
            int s = di[bm];
            bs[bm] = true;

            // iter#1: add (i+1)
            for (int j = i + 1; j < a.Count; ++j)
            {
                int b1 = bm | a[j].b, s1 = s + a[j].c;
                if (!di.TryGetValue(b1, out var s0) || s1 < s0) // DEBUG: 暂时把bs去掉
                //if (!bs[b1] && (!di.TryGetValue(b1, out var s0) || s1 < s0))
                {
                    pq.Enqueue((bm, j), di[b1] = s1);
                    break;
                }
            }
            // iter#2: change i to (i+1)
            s -= a[i].c;
            for (int j = i + 1; j < a.Count; ++j)
            {
                int b1 = b | a[j].b, s1 = s + a[j].c;
                if (!di.TryGetValue(b1, out var s0) || s1 < s0) // DEBUG: 暂时把bs去掉
                //if (!bs[b1] && (!di.TryGetValue(b1, out var s0) || s1 < s0))
                {
                    pq.Enqueue((b, j), di[b1] = s1);
                    break;
                }
            }
        }
        return -1;
    }
    // 同ver4_II，去掉了所有优化代码，检查WA原因
    // ans=209正确，time=7000ms，证明字典去重会导致WA
    public int ConnectTwoGroups_ver4DEBUG(IList<IList<int>> cost)
    {
        int m = cost.Count, n = cost[0].Count;
        List<(int c, int b)> a = new(m * n + 1);
        for (int i = 0; i < m; ++i)
            for (int j = 0; j < n; ++j)
                a.Add((cost[i][j], 1 << i | 1 << m + j));
        a.Sort();

        // orderedSumOfList
        PriorityQueue<(int b, int i, int s), int> pq = new(); // <bitmap, last_i> => min_sum, 注意bitmap是未加上a[i]之前的
        pq.Enqueue((0, 0, a[0].c), a[0].c);
        int endmap = (1 << m + n) - 1;
        while (pq.Count > 0)
        {
            (int b, int i, int s) = pq.Dequeue();
            int bm = b | a[i].b;
            if (bm == endmap) return s;

            // iter#1: add (i+1)
            //for (int j = i + 1; j < a.Count; ++j)
            if (i + 1 < a.Count)
            {
                int j = i + 1;
                int b1 = bm | a[j].b, s1 = s + a[j].c;
                pq.Enqueue((bm, j, s1), s1);
            }
            // iter#2: change i to (i+1)
            s -= a[i].c;
            //for (int j = i + 1; j < a.Count; ++j)
            if (i + 1 < a.Count)
            {
                int j = i + 1;
                int b1 = b | a[j].b, s1 = s + a[j].c;
                pq.Enqueue((b, j, s1), s1);
            }
        }
        return -1;
    }

    // ver4 - 2024/3/23
    // WA, 不应该在“替换”操作中加去重判断，例如(4,14)重了，但是(5,14),(6,14)...仍应该搜索
    // 考虑改进：如果前n个全选都构不成完整解，第一条边选(n-1)是没有意义的，因为这之后只会往前迭代
    //          所以应该先找到t，使得前t个全选能构成完整解，然后从t,t+1,t+2开始迭代
    public int ConnectTwoGroups_ver4(IList<IList<int>> cost)
    {
        int m = cost.Count, n = cost[0].Count;
        List<(int c, int b)> a = new(m * n + 1);
        for (int i = 0; i < m; ++i)
            for (int j = 0; j < n; ++j)
                a.Add((cost[i][j], 1 << i | 1 << m + j));
        a.Sort();
        BitSetTree bs = new(m + n);
        Dictionary<int, int> di = new();
        //BitArray red = new(1 << m + n); // 即dijkstra的红色标记 // 用bs代替？

        // orderedSumOfList
        PriorityQueue<(int b, int i, int limit, int inc), int> pq = new(); // inc:维护因为插入i而产生的增量
        pq.Enqueue((a[0].b, 0, a.Count - 1, a[0].b), a[0].c);
        di[a[0].b] = a[0].c;
        int fm = (1 << m + n) - 1;
        while (pq.Count > 0)
        {
            (int bm, int i, int limit, int im) = pq.Dequeue();
            if (bm == fm) return di[bm];
            else if (bs[bm]) continue;
            int sm = di[bm];
            bs[bm] = true;

            if (i + 1 <= limit)
            {
                int nxs = sm - a[i].c + a[i + 1].c;
                int b0 = bm ^ im;
                int b1 = b0 | a[i + 1].b;
                if (b1 != b0 && !bs[b1] && (!di.TryGetValue(b1, out var tv) || nxs < tv))
                    pq.Enqueue((b1, i + 1, limit, b1 ^ b0), di[b1] = nxs);
            }
            if (i > 0)
            {
                int nxs = sm + a[0].c;
                int b1 = bm | a[0].b;
                if (b1 != bm && !bs[b1] && (!di.TryGetValue(b1, out var tv) || nxs < tv))
                    pq.Enqueue((b1, 0, i - 1, b1 ^ bm), di[b1] = nxs);
            }
        }
        return -1;
    }

    // ver3 - 应用排列组合：因为(1,3)和(3,1）是一样的，所以维护最后一个选择的连接，下一个连接不能往前反选
    // 实测结果：625ms
    // 提交到美版网站通过（3202ms），国内平台总时间超时
    public int ConnectTwoGroups_ver3(IList<IList<int>> cost)
    {
        int m = cost.Count, n = cost[0].Count;
        List<(int, int)> li = new();
        for (int i = 0; i < m; ++i)
            for (int j = 0; j < n; ++j)
                li.Add((1 << i | 1 << m + j, cost[i][j]));
        int mmp = (1 << m) - 1;
        int fm = (1 << m + n) - 1;
        SHeap<(int, int), int> hp = new((a, b) => a < b, true);
        hp.Add((0, 0), 0);
        while (hp.Any())
        {
            ((int bm, int i), int p) = hp.Pop();
            if (bm == fm) return p;
            else if (i == li.Count) continue;

            for (; i < li.Count; ++i) // 添加规则1：必须从上一个选择开始往下选
            {
                (int b, int c) = li[i];
                int bn = b | bm;
                // 添加规则2：既然不能返回来选，那漏掉是不行的
                if ((bn + 1 & bn & mmp) != 0) // 这里位运算的意思是，后m位末尾必须是连续的1，否则break
                    break;
                if (bn != bm)
                    hp.Add((bn, i + 1), p + c);
            }
        }
        return -1;
    }

    // 动态规划 - 看题解后自己写
    // 枚举i，枚举j的所有子集，O(2^2 * 12 * 12) = 589824
    // 实测：159ms，对比dijkstra 3202ms，确实快很多（但想不明白为什么）
    public int ConnectTwoGroups_DP(IList<IList<int>> cost)
    {
        int m = cost.Count, n = cost[0].Count;
        int[] dp0 = new int[1 << n], dp1 = new int[1 << n];
        Array.Fill(dp1, -1);
        dp1[0] = 0;
        for (int i = 0; i < m; ++i)
        {
            // step1 在 dp0的基础上加一条(i,x)得到dp1
            (dp0, dp1) = (dp1, dp0);
            Array.Fill(dp1, -1);
            for (int jm = 0; jm < 1 << n; ++jm)
                if (dp0[jm] >= 0)
                    for (int j = 0; j < n; ++j)
                    {
                        int jn = jm | 1 << j;
                        int p = dp0[jm] + cost[i][j];
                        dp1[jn] = dp1[jn] < 0 ? p : Math.Min(dp1[jn], p);
                    }

            // step2 在dp1的基础上加更多的(i, x)边是否能有更优解
            for (int jm = 0; jm < 1 << n; ++jm)
                if (dp1[jm] >= 0)
                    for (int j = 0; j < n; ++j)
                        if ((jm & 1 << j) == 0)
                        {
                            int jn = jm | 1 << j;
                            int p = dp1[jm] + cost[i][j];
                            dp1[jn] = dp1[jn] < 0 ? p : Math.Min(dp1[jn], p);
                        }
        }
        return dp1[(1 << n) - 1];
    }

    // WA again - Bak
    //public int ConnectTwoGroups_DP(IList<IList<int>> cost)
    //{
    //    int m = cost.Count, n = cost[0].Count;
    //    int[] dp = new int[1 << n];
    //    Array.Fill(dp, -1);
    //    dp[0] = 0;
    //    for (int i = 0; i < m; ++i)
    //        for (int j = 0; j < n; ++j)
    //        {
    //            for (int jm = 0; jm < (1 << n); ++jm)
    //                if (dp[jm] >= 0)
    //                {
    //                    int jn = jm | 1 << j;
    //                    int p = dp[jm] + cost[i][j];
    //                    dp[jn] = dp[jn] < 0 ? p : Math.Min(dp[jn], p);
    //                }
    //        }
    //    return dp[(1 << n) - 1];
    //}

    // WA - bak

    //public int ConnectTwoGroups_DP(IList<IList<int>> cost)
    //{
    //    int m = cost.Count, n = cost[0].Count;
    //    int[] dp0 = new int[1 << n], dp1 = new int[1 << n];
    //    Array.Fill(dp1, -1);
    //    dp1[0] = 0;
    //    for (int i = 0; i < m; ++i)
    //    {
    //        Console.WriteLine("i=" + i);
    //        (dp0, dp1) = (dp1, dp0);
    //        Array.Fill(dp1, -1);
    //        for (int jm = 0; jm < (1 << n); ++jm)
    //            if (dp0[jm] >= 0)
    //                for (int j = 0; j < n; ++j)
    //                {
    //                    int jn = jm | 1 << j;
    //                    int p = dp0[jm] + cost[i][j];
    //                    dp1[jn] = dp1[jn] < 0 ? p : Math.Min(dp1[jn], p);
    //                }
    //        for (int j = 0; j < (1 << n); ++j)
    //            if (dp1[j] >= 0)
    //                Console.Write("   {0}:{1}", j, dp1[j]);
    //        Console.WriteLine();
    //    }
    //    return dp1[(1 << n) - 1];
    //}



    // ver2 - 尝试优化 - 添加BitSetTree去重：优化到大约700ms
    // 提交后后面的用例还是超时
    public int ConnectTwoGroups_2(IList<IList<int>> cost)
    {
        int m = cost.Count, n = cost[0].Count;
        List<(int, int)> li = new();
        for (int i = 0; i < m; ++i)
            for (int j = 0; j < n; ++j)
                li.Add((1 << i | 1 << m + j, cost[i][j]));

        BitSetTree bs = new(m + n);

        int fm = (1 << m + n) - 1;
        SHeap<int, int> hp = new((a, b) => a < b, true);
        hp.Add(0, 0);
        while (hp.Any())
        {
            ++c1; // DEBUG
            (int bm, int p) = hp.Pop();
            if (bm == fm) return p;
            else if (bs[bm]) continue;
            else bs[bm] = true;
            ++c2; // DEBUG

            //// 进一步优化: 8891ms -> 1297ms BUT WA -_-|||
            //for (int i = li.Count - 1; i >= 0; --i)
            //{
            //    (int b, int c) = li[i];
            //    if (bs[b]) li.RemoveAt(i);
            //    else if ((b | bm) != bm)
            //        hp.Add(b | bm, p + c);
            //}
            foreach ((int b, int c) in li)
                if ((b | bm) != bm)
                    hp.Add(b | bm, p + c);
        }
        return -1;
    }
    internal static int c1 = 0, c2 = 0;

    // ver1 - dijkstra - 超时(3657ms)
    public int ConnectTwoGroups_1(IList<IList<int>> cost)
    {
        int m = cost.Count, n = cost[0].Count;
        List<(int, int)> li = new();
        for (int i = 0; i < m; ++i)
            for (int j = 0; j < n; ++j)
                li.Add((1 << i | 1 << m + j, cost[i][j]));
        int fm = (1 << m + n) - 1;
        SHeap<int, int> hp = new((a, b) => a < b, true);
        hp.Add(0, 0);
        while (hp.Any())
        {
            (int bm, int p) = hp.Pop();
            if (bm == fm) return p;
            foreach ((int b, int c) in li)
                if ((b | bm) != bm)
                    hp.Add(b | bm, p + c);
        }
        return -1;
    }

    internal static void Run()
    {
        // test case sample
        //var raw = "[[15,96],[36,2]]"; // ans=17

        // test case #0: ans=4
        //var raw = "[[1,3,5],[4,1,1],[1,5,3]]";

        // test case #1: ans=102; 36xx -> 7xx ms
        //var raw = "[[32,11,79,32,75,39,27,67],[53,8,1,70,42,68,79,92],[89,0,64,57,4,15,55,59],[68,4,75,29,5,20,89,95],[70,82,44,6,63,41,92,67],[23,96,34,13,98,72,92,35],[93,9,63,42,65,47,50,38],[86,89,5,32,55,53,29,20],[77,33,79,64,0,44,82,6],[69,55,18,12,89,54,97,10],[56,91,30,2,30,83,67,60]]";

        // test case #2: ans=120; 8891ms
        //var raw = "[[66,78,9,28,3,70,92,79,18,63,80],[56,7,12,6,1,90,1,34,21,36,62],[4,68,28,93,17,38,72,96,26,89,63],[11,94,47,60,46,36,0,69,52,47,35],[18,76,87,51,37,9,38,60,45,98,9],[64,78,41,95,59,23,90,9,31,10,23],[25,52,84,48,44,69,3,9,50,13,15],[33,90,38,6,65,9,11,99,67,64,61],[29,30,79,46,78,74,89,80,98,92,89],[41,18,58,65,25,18,91,21,11,17,8],[41,39,38,47,100,55,96,29,62,38,94]]";

        // test case #3: ans=209 my=215
        //var raw = "[[25,60,37,24],[2,30,29,63],[8,76,58,76],[12,43,50,72],[100,77,83,34],[24,0,51,74],[2,28,49,35],[62,38,56,55],[55,43,26,33],[91,54,24,18],[59,97,55,38],[16,80,56,7]]";

        // test case #4: (TLE) ans=242
        //var raw = "[[64,30,99,80,100],[97,61,12,65,30],[84,15,58,74,11],[98,52,100,66,59],[83,58,56,55,32],[91,44,89,48,84],[40,90,2,27,63],[61,35,59,16,54],[11,0,97,86,7],[93,3,95,77,90],[69,29,96,79,51]]";

        // test case #5 (Out of Memory)
        var raw = "[[1,1,1,1,1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1,1,1,1,1],[1,1,1,1,1,1,1,1,1,1,1,1]]";

        var a = raw.ToTestInput<int[][]>();
        var sln = new P1595连通两组点的最小成本();
        var ans = sln.ConnectTwoGroups_ver5_II(a); // ver4_II(a);
        //var ans = sln.ConnectTwoGroups_DP(a);
        Console.WriteLine("ans=" + ans);
        Console.WriteLine("c1={0} c2={1}", c1, c2);
    }
}
