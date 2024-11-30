using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1;

// hard, 2024/6/19 Daily
// rating 2300+
// BFS，难点在于如果同一行/列有多个格子的值一样，要当作一个group考虑，最终考虑用面向对象实现
// AC之后看题解，也就10来行的DP，也不知为什么自己写了个这么复杂的递推。。
internal class P2713矩阵中严格递增的单元格数
{
    abstract class MergeCellStruct : IComparable<MergeCellStruct>
    {
        public readonly int x, y;
        public MergeCellStruct(int x, int y, int v)
        {
            this.x = x;
            this.y = y;
            CellValue = v;
            AddCell(x, y);
        }
        public int CellValue { get; private set; }
        public int Count { get; set; } = 1;
        public int CompareTo(MergeCellStruct other) => CellValue.CompareTo(other.CellValue);
        public abstract IEnumerable<(int x, int y)> EnumCells();
        public abstract void AddCell(int x, int y);
        public abstract bool IsNull { get; }
    }

    // 同一个x，有多个y
    class HorizontalMergeCellStruct : MergeCellStruct
    {
        List<int> yy = new();
        public override bool IsNull => x < 0;

        public HorizontalMergeCellStruct(int x, int y, int v) : base(x, y, v)
        {
        }

        public override void AddCell(int x, int y) => yy.Add(y);

        public override IEnumerable<(int x, int y)> EnumCells() => yy.Select(y => (x, y));
    }

    class VerticalMergeCellStruct : MergeCellStruct
    {
        List<int> xx = new();
        public override bool IsNull => y < 0;
        public VerticalMergeCellStruct(int x, int y, int v) : base(x, y, v)
        {
            //if (x < 0 || y < 0)
            //    Debug.WriteLine("break");
        }

        public override void AddCell(int x, int y) => xx.Add(x);
        public override IEnumerable<(int x, int y)> EnumCells() => xx.Select(x => (x, y));
    }

    public int MaxIncreasingCells(int[][] mat)
    {
        int m = mat.Length, n = mat[0].Length, ans = 0;
        List<MergeCellStruct> a = new();
        // key - 当前cellvalue，value-指向下一个更大的节点
        Dictionary<int, MergeCellStruct>[] nextx = Enumerable.Range(0, m).Select(_ => new Dictionary<int, MergeCellStruct>()).ToArray(),
            nexty = Enumerable.Range(0, n).Select(_ => new Dictionary<int, MergeCellStruct>()).ToArray();
        Dictionary<(int x, int y), MergeCellStruct> cacheDic = new(); // 去重用，如果一个mergeCell只有单个格子，那放在nextx和放在nexty是等价的，所以要用这个字典去重

        // add horizontal
        for (int r = 0; r < m; ++r)
        {
            HorizontalMergeCellStruct mc = new(-1, -1, -1); // NullValueObject
            bool multi = false;
            foreach ((int v, int c) in Enumerable.Range(0, n).Select(c => (mat[r][c], c)).OrderBy(t => t))
            {
                if (mc.IsNull || v > mc.CellValue)
                {
                    if (mc.IsNull)
                    {
                        mc = new(r, c, v);
                    }
                    else
                    {
                        if (!multi) cacheDic[(mc.x, mc.y)] = mc;
                        int v0 = mc.CellValue;
                        nextx[r][v0] = mc = new(r, c, v);
                    }
                    multi = false;
                    a.Add(mc);
                }
                else
                {
                    multi = true;
                    mc.AddCell(r, c);
                }
            }
            if (!multi) cacheDic[(mc.x, mc.y)] = mc;
        }
        // add vertical 
        for (int c = 0; c < n; ++c)
        {
            VerticalMergeCellStruct mc = new(-1, -1, -1); // NullValueObject
            bool multi = false;
            int v0 = 0;
            foreach ((int v, int r) in Enumerable.Range(0, m).Select(r => (mat[r][c], r)).OrderBy(t => t))
            {
                if (mc.IsNull || v > mc.CellValue)
                {
                    if (mc.IsNull)
                    {
                        mc = new(r, c, v);
                        v0 = v;
                    }
                    else
                    {
                        if (!multi)
                        {
                            if (cacheDic.TryGetValue((mc.x, mc.y), out var mx))
                            {
                                if (v0 < mx.CellValue)
                                    nexty[c][v0] = mx;
                            }
                            else
                            {
                                a.Add(mc);
                                if (v0 < mc.CellValue)
                                    nexty[c][v0] = mc;
                            }
                        }
                        v0 = mc.CellValue;
                        mc = new(r, c, v);
                    }
                    multi = false;
                }
                else
                {
                    if (!multi)
                    {
                        multi = true;
                        if (v0 < mc.CellValue)
                            nexty[c][v0] = mc;
                        a.Add(mc);
                    }
                    mc.AddCell(r, c);
                }
            }
            if (!multi) 
            {
                if (cacheDic.TryGetValue((mc.x, mc.y), out var mx))
                {
                    if (v0 < mx.CellValue)
                        nexty[c][v0] = mx;
                }
                else
                {
                    a.Add(mc);
                    if (v0 < mc.CellValue)
                        nexty[c][v0] = mc;
                }
            }
        }
        a.Sort();
        foreach (var mc in a)
        {
            ans = Math.Max(ans, mc.Count);
            if (mc is HorizontalMergeCellStruct)
            {
                if (nextx[mc.x].TryGetValue(mc.CellValue, out var mx))
                {
                    mx.Count = Math.Max(mx.Count, mc.Count + 1);
                }
                foreach ((int r, int c) in mc.EnumCells())
                {
                    if (nexty[c].TryGetValue(mc.CellValue, out var my))
                    {
                        my.Count = Math.Max(my.Count, mc.Count + 1);
                    }
                }
            }
            else // mc is vertical
            {
                if (nexty[mc.y].TryGetValue(mc.CellValue, out var my))
                {
                    my.Count = Math.Max(my.Count, mc.Count + 1);
                }
                foreach ((int r, int c) in mc.EnumCells())
                {
                    if (nextx[r].TryGetValue(mc.CellValue, out var mx))
                    {
                        mx.Count = Math.Max(mx.Count, mc.Count + 1);
                    }
                }
            }
        }
        return ans;
    }

    internal static void Run()
    {
        var sln = new P2713矩阵中严格递增的单元格数();
        var args = "[[5,0,1,-2,8,0,2]]" // output 8, expected 6 // 找到原因，是VerticalMergeCell的v0的初始化问题
            // "[[7,6,3],[-7,-5,6],[-7,0,-4],[6,6,0],[-8,6,0]]" // WA, OOP版已解决
            //"[[3,1,6],[-9,5,7]]"
            .ToTestInput<int[][]>();
        var ans = sln.MaxIncreasingCells(args);
        Console.WriteLine("ans=" + ans);
    }

    // 第一版错误的版本
    // 保存下来方便后面做案例
    public int MaxIncreasingCells_Wrong(int[][] mat)
    {
        int m = mat.Length, n = mat[0].Length, i = 0, ans = 0;
        int[,] t = new int[m, n], nextx = new int[m, n], nexty = new int[m, n];
        (int v, int x, int y)[] a = new (int v, int x, int y)[m * n];
        for (int r = 0; r < m; ++r)
            for (int c = 0; c < n; ++c)
            {
                t[r, c] = 1;
                nextx[r, c] = nexty[r, c] = -1;
                a[i++] = (mat[r][c], r, c);
            }
        for (int r = 0; r < m; ++r)
        {
            List<int> b = new();
            foreach ((int v, int c) in Enumerable.Range(0, n).Select(c => (mat[r][c], c)).OrderBy(t => t))
            {
                if (b.Any() && v > mat[r][b.First()])
                {
                    foreach (int c0 in b) nextx[r, c0] = c;
                    b.Clear();
                }
                b.Add(c);
            }
        }
        for (int c = 0; c < n; ++c)
        {
            List<int> b = new();
            foreach ((int v, int r) in Enumerable.Range(0, m).Select(r => (mat[r][c], r)).OrderBy(t => t))
            {
                if (b.Any() && v > mat[b.First()][c])
                {
                    foreach (int r0 in b) nextx[r0, c] = r; // (1) 应该是nexty
                    b.Clear();
                }
                b.Add(r);
            }
        }
        Array.Sort(a);
        foreach ((int v, int x, int y) in a)
        {
            // (2) 以下两行应该是 nextx[x, y] >= 0
            if (nextx[x, y] > 0) t[x, nextx[x, y]] = Math.Max(t[x, nextx[x, y]], t[x, y] + 1);
            if (nexty[x, y] > 0) t[nexty[x, y], y] = Math.Max(t[nexty[x, y], y], t[x, y] + 1);
            ans = Math.Max(ans, t[x, y]);
        }
        return ans;
    }
}
