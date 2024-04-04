using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1;

// hard, 2024/2/29 Daily
// 无向树难题
// rating只有2200+，做成难题是因为自己理解错题意了，题目说guess必定是相邻边，我这边做成不是相邻边也可以求解
// 不过正好下次出题可以出给学生做
internal class P2581统计可能的树根数目
{
    // 递归可能会stack overflow，需要用栈模拟递归
    public int RootCount(int[][] edges, int[][] guesses, int k)
    {
        // Tree Graph
        int n = edges.Length + 1;
        var tg = Enumerable.Range(0, n).Select(_ => new List<int>()).ToArray();
        foreach (var ed in edges)
        {
            tg[ed[0]].Add(ed[1]);
            tg[ed[1]].Add(ed[0]);
        }

        // dfs标号
        int[] ds = new int[n], ns = new int[n];
        int id = 0;

        Stack<(int, int)> stk = new();
        int t = 0, i = 0;
        while (true)
        {
            if (i == 0)
            {
                ds[t] = id++;
            }

            if (i < tg[t].Count && stk.Any() && stk.Peek().Item1 == tg[t][i]) ++i;

            if (i == tg[t].Count)
            {
                ns[ds[t]] = id;

                if (stk.Any()) (t, i) = stk.Pop();
                else break;
            }
            else
            {
                stk.Push((t, i + 1));
                (t, i) = (tg[t][i], 0);
            }
        }
        //void Dfs1(int i = 0, int p = -1)
        //{
        //    ds[i] = id++;
        //    foreach (int j in tg[i])
        //        if (j != p)
        //            Dfs1(j, i);
        //    ns[ds[i]] = id;
        //}
        //Dfs1();
        var temp = new List<int>[n];
        for (i = 0; i < n; ++i)
            temp[ds[i]] = tg[i].Select(t => ds[t]).ToList();
        tg = temp;

        // process guesses
        int correct = 0; // 当前猜对的总数
        List<int>[] cl = new List<int>[n];
        for (i = 0; i < n; ++i) cl[i] = new();
        foreach (var p in guesses)
        {
            int father = ds[p[0]], son = ds[p[1]];
            cl[father].Add(son);
            if (son > father && son < ns[father])
                ++correct;
        }
        for (i = 0; i < n; ++i) cl[i].Sort();

        int ans = 0;

        // DFS again, 遍历当作根节点的点
        // 当根节点从1转到2时：
        //   - 将1的子节点里，属于2的子节点全部取否(这些节点原来认1为父，现在不认了）
        //   - 将2的子节点里，不属于2的子节点的全部取是（这些节点原来不认2是父，现在认了）
        Stack<(int, int, int)> dfsStack = new();
        int nodeIndex = 0, childIndex = 0;
        while (true)
        {
            if (childIndex == 0)
            {
                // DEBUG
                Console.WriteLine("Root={0}, correct={1}",
                    Enumerable.Range(0, n).First(j => ds[j] == nodeIndex),
                    correct);

                if (correct >= k) ++ans;
            }

            // skip parent
            if (childIndex < tg[nodeIndex].Count && dfsStack.Any() && dfsStack.Peek().Item1 == tg[nodeIndex][childIndex]) ++childIndex;

            // recursion
            if (childIndex == tg[nodeIndex].Count)
            {
                if (dfsStack.Any()) (nodeIndex, childIndex, correct) = dfsStack.Pop();
                else break;
            }
            else
            {
                dfsStack.Push((nodeIndex, childIndex + 1, correct));
                correct = correct
                - cl[nodeIndex].BinaryRangeSearch(tg[nodeIndex][childIndex], ns[tg[nodeIndex][childIndex]] - 1).RangeCount()
                + cl[tg[nodeIndex][childIndex]].Count - cl[tg[nodeIndex][childIndex]].BinaryRangeSearch(tg[nodeIndex][childIndex] + 1, ns[tg[nodeIndex][childIndex]] - 1).RangeCount();
                (nodeIndex, childIndex) = (tg[nodeIndex][childIndex], 0);
            }
        }
        //void Dfs2(int i = 0, int p = -1)
        //{
        //    ds[i] = id++;
        //    foreach (int j in tg[i])
        //        if (j != p)
        //            Dfs2(j, i);
        //    ns[ds[i]] = id;
        //}
        //Dfs2();

        return ans;
    }
}
