using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1.contest;

// 本场比赛用hccyril账号用python完成
/*
# 第144场双周赛，2024/11/23 by hccyril
# AK and rank 151: 19 01:24:02 00:03:03 00:16:28 01:24:02 00:48:05
# C题在23:49时样例过不了要调试，好在最后有惊无险
*/
// C#代码为赛后根据python重写
internal class LC_BiC144_20241123
{
    #region Problem A
    public bool CanAliceWin(int n)
    {
        bool Win(int n, int c)
            => n >= c && c > 0 && !Win(n - c, c - 1);
        return Win(n, 10);
    }
    #endregion

    #region Problem B
    // Floyd最短路径
    public long ShiftDistance(string s, string t, int[] nextCost, int[] previousCost)
    {
        long INF = 100000000000L;
        long[,] a = new long[26, 26];
        for (int i = 0; i < 26; ++i)
            for (int j = 0; j < 26; ++j)
                a[i, j] = INF;
        for (int i = 0; i < 26; ++i)
        {
            a[i, i] = 0;
            a[i, (i + 1) % 26] = Math.Min(a[i, (i + 1) % 26], nextCost[i]);
            int j = i == 0 ? 25 : i - 1;
            a[i, j] = Math.Min(a[i, j], previousCost[i]);
        }
        for (int k = 0; k < 26; ++k)
            for (int i = 0; i < 26; ++i)
                for (int j = 0; j < 26; ++j)
                    if (a[i, k] + a[k, j] < a[i, j])
                        a[i, j] = a[i, k] + a[k, j];
        long sm = 0;
        for (int i = 0; i < s.Length; ++i)
            sm += a[s[i] - 'a', t[i] - 'a'];
        return sm;
    }
    #endregion

    #region Problem C
    // 3362. 零数组转化 III
    // 题意：nums[i]表示至少要有多少个区间包含位置i，在queries中找到满足条件的最少的区间
    /*
    # 最后做出来，认为应该是最难
    # 反悔贪心：反过来想能覆盖的最小数量的区间是什么，
    # 首先区间按(l, -r)进行排序
    # 然后扫描nums，每一步把接触到的区间加到“货舱”里
    # 然后判断当前架起的区间数是否达到nums[i]，如果不够，就从“货舱”里取一个架上去，此时可以用贪心：显然要架就架一个右区间最大的（可以尽量覆盖后面的数值）
    # 架上区间时用差分维护一下尾部标记即可
    */
    public int MaxRemoval(int[] nums, int[][] queries)
    {
        int n = nums.Length, m = queries.Length;
        var qs = Enumerable.Range(0, m).Select(i => (queries[i][0], -queries[i][1])).OrderBy(t => t).ToArray();
        Span<int> a = stackalloc int[n + 1];
        int cur = 0, j = 0, need = 0;
        PriorityQueue<int, int> hp = new();
        for (int i = 0; i < n; ++i)
        {
            cur += a[i];
            while (j < qs.Length && qs[j].Item1 <= i) 
            {
                (int l, int rt) = qs[j];
                hp.Enqueue(j++, rt);
            }
            while (cur < nums[i])
            {
                if (hp.Count == 0)
                    return -1;
                int k = hp.Dequeue(), r = -qs[k].Item2;
                if (r >= i)
                {
                    --a[r + 1];
                    ++cur;
                    ++need;
                }
            }
        }

        return qs.Length - need;
    }
    #endregion

    #region Problem D
    // DP (similar to 摘樱桃II)
    //def maxCollectedFruits(self, fruits: List[List[int]]) -> int:
    //    n = len(fruits)
    //    if n == 2:
    //        return sum(sum(r) for r in fruits)
    //    elif n == 3:
    //        return sum(sum(r) for r in fruits) - fruits[0][1] - fruits[1][0]
    //    a = fruits
    //    def calc():
    //        nonlocal a, n
    //        dp = [0]* n
    //        dp[n - 1] = a[-2][-1]
    //        for i in range(n - 3, -1, -1) :
    //            c = min(n - 1 - i, i + 1)
    //            dp[n - c:] = (a[i][j] + max(dp[k] for k in range(j - 1, j + 2) if 0 <= k<n) for j in range(n - c, n))
    //        return dp[-1]
    //    ans = sum(a[i][i] for i in range(n))
    //    ans += calc()
    //    a = [list(c) for c in zip(*a)]
    //    ans += calc()
    //    return ans
    #endregion

    #region Problem E
    public int SolveE(int x)
    {
        return x;
    }
    #endregion

    #region Run Test
    internal static int Run()
    {
        char p = 'D';
        LC_BiC144_20241123 sln = new();

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
