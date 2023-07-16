using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1
{
    // hard, 2023/6/30 US Daily
    // 并查集，按理说不难，但写了半天都还是报错（数组越界）
    internal class P1970你能穿过矩阵的最后一天
    {
        public int LatestDayToCross(int row, int col, int[][] cells)
        {
            EX.m = row; EX.n = col;
            UnionFind uni = new(row * col + 2);
            int start = 0, end = row * col + 1;
            HashSet<(int, int)> hs = new();
            foreach (var p in cells)
                hs.Add((--p[0], --p[1])); // !!!一开始居然没反应过来坐标是按1开头的！！！
            for (int i = 0; i < row; ++i)
                for (int j = 0; j < col; ++j)
                {
                    int p = (i, j).Pack();
                    if (!hs.Contains((i, j)))
                    {
                        if (i == 0)
                            uni.Union(start, p);
                        if (i > 0 && !hs.Contains((i - 1, j)))
                            uni.Union((i - 1, j).Pack(), p);
                        if (j > 0 && !hs.Contains((i, j - 1)))
                            uni.Union((i, j - 1).Pack(), p);
                    }
                }
            int days = cells.Length;
            while (days > 0)
            {
                if (uni.Check(start, end)) return days;
                --days;
                int i = cells[days][0], j = cells[days][1];
                hs.Remove((i, j));
                foreach ((int i1, int j1) in (i, j).FourDir())
                    if (!hs.Contains((i1, j1)))
                        uni.Union((i, j).Pack(), (i1, j1).Pack());
                if (i == 0) uni.Union(start, (i, j).Pack());
                if (i == row - 1) uni.Union(end, (i, j).Pack());
            }
            return days;
        }

        internal static void Run()
        {
            var sln = new P1970你能穿过矩阵的最后一天();
            int[][] cells =
                "[[4,2],[6,2],[2,1],[4,1],[6,1],[3,1],[2,2],[3,2],[1,1],[5,1],[5,2],[1,2]]"
                .ToTestInput<int[][]>();
            int ans = sln.LatestDayToCross(6, 2, cells);
            Console.WriteLine("ans=" + ans);
        }
    }

    internal static class EX
    {
        internal static int m, n;
        internal static int Pack(this (int, int) p)
        {
            (int i, int j) = p;
            return i * n + j + 1; // m 和 n 搞反啦！！
        }
        internal static (int, int) Unpack(this int x) => ((x - 1) / n, (x - 1) % n); // m 和 n 都能搞反啊！！
        internal static IEnumerable<(int ni, int nj)> FourDir(this (int, int) p)
        {
            (int i, int j) = p;
            if (i > 0) yield return (i - 1, j);
            if (i < m - 1) yield return (i + 1, j);
            if (j > 0) yield return (i, j - 1);
            if (j < n - 1) yield return (i, j + 1);
        }
    }
}
