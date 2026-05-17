using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1.contest;

// 第148场双周赛，20250118 by main acct
// TODO C wrong answer
internal class LC_BiC148_20250118
{
    #region Problem A
    public int SolveA(int x)
    {
        return x;
    }
    #endregion

    #region Problem B
    public int SolveB(int x)
    {
        return x;
    }
    #endregion

    #region Problem C
    public int[] LongestSpecialPath(int[][] edges, int[] nums)
    {
        int n = nums.Length;
        var tg = edges.WeightedTreeGraph();
        int result_len = 0, result_n = 1;
        HashSet<int> hs_init = new()
        {
            nums[0]
        };
        HashSet<int>[] hsArr = new HashSet<int>[n];
        int[] parWei = new int[n], sumWei = new int[n];
        hsArr[0] = hs_init;

        // dfs
        const int DFS_ROOT = 0;
        Stack<(int Node, int Child)> dfsStk = new();
        int i = DFS_ROOT, childIndex = 0;
        while (true)
        {
            if (childIndex == tg[i].Count)
            {
                int parent = dfsStk.Any() ? dfsStk.Peek().Node : -1;

                var hs = hsArr[i];
                hs.Remove(nums[i]);

                if (dfsStk.Any())
                {
                    (i, childIndex) = dfsStk.Pop();
                    continue;
                }
                else break;
            }
            else if (childIndex == 0)
            {
                if (sumWei[i] > result_len)
                {
                    result_len = sumWei[i];
                    result_n = hsArr[i].Count;
                }
                else if (sumWei[i] == result_len && hsArr[i].Count < result_n)
                    result_n = hsArr[i].Count;
            }

            (int nextIndex, int w) = tg[i][childIndex];

            if (dfsStk.Any() && dfsStk.Peek().Node == nextIndex)
            {
                ++childIndex;
            }
            else
            {   // dfs next
                var hs = hsArr[i];
                if (hs.Contains(nums[nextIndex]))
                {
                    hs = new() { nums[nextIndex] };
                    hsArr[nextIndex] = hs;
                    // sumwei = parentwei = 0
                }
                else
                {
                    hs.Add(nums[nextIndex]);
                    hsArr[nextIndex] = hs;
                    sumWei[nextIndex] = sumWei[i] + w;
                    parWei[nextIndex] = w;
                }

                dfsStk.Push((i, childIndex + 1));
                (i, childIndex) = (nextIndex, 0);
            }
        }
        return new int[] { result_len, result_n };
    }
    #endregion

    #region Problem D
    public int SolveD(int x)
    {
        return x;
    }
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
        char p = 'C';
        LC_BiC148_20250118 sln = new();

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
        var e = "[[3,0,5],[3,1,6],[2,3,5]]".ToTestInput<int[][]>();
        var n = "[2,2,2,1]".ToTestInput<int[]>();
        var r = LongestSpecialPath(e, n);

        return r[0];
    }

    int RunTestD()
    {
        return 148;
    }

    int RunTestE()
    {
        return 0;
    }
    #endregion
}
