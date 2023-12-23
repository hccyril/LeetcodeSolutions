using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1.contest
{
    // medium, 2023/6/21 Daily, 当天没时间做
    // 模拟, 2023/7/24 重做AC
    internal class LCP041
    {
        public int FlipChess(string[] chessboard)
        {
            char[][] a = chessboard.Select(s => s.Select(c => c).ToArray()).ToArray();
            char[][] bak = a.Select(r => r.Select(c => c).ToArray()).ToArray();
            int m = a.Length, n = a[0].Length, maxn = 0;

            IEnumerable<(int, int)> EightDir(int i, int j)
            {
                if (i > 0)
                {
                    yield return (-1, 0);
                    if (j > 0) yield return (-1, -1);
                    if (j < n - 1) yield return (-1, 1);
                }
                if (j > 0) yield return (0, -1);
                if (j < n - 1) yield return (0, 1);
                if (i < m - 1)
                {
                    yield return (1, 0);
                    if (j > 0) yield return (1, -1);
                    if (j < n - 1) yield return (1, 1);
                }
            }

            Queue<(int, int)> qu = new();
            int Check(int i, int j)
            {
                int eat = 0;
                foreach ((int dx, int dy) in EightDir(i, j))
                {
                    int t = 0;
                    List<(int, int)> li = new();
                    for (int ni = i + dx, nj = j + dy; ni >= 0 && ni < m && nj >= 0 && nj < n; ni += dx, nj += dy)
                    {
                        if (a[ni][nj] == 'X')
                        {
                            eat += t;
                            foreach ((int ii, int jj) in li)
                            {
                                a[ii][jj] = 'X';
                                qu.Enqueue((ii, jj));
                            }
                            break;
                        }
                        else if (a[ni][nj] == 'O')
                        {
                            ++t;
                            li.Add((ni, nj));
                        }
                        else
                        {
                            break;
                        }
                    }
                }
                if (qu.Any())
                {
                    (i, j) = qu.Dequeue();
                    eat += Check(i, j);
                }
                return eat;
            }

            for (int i = 0; i < m; ++i)
                for (int j = 0; j < n; ++j)
                    if (a[i][j] == '.')
                    {
                        maxn = Math.Max(maxn, Check(i, j));
                        for (int k = 0; k < m; ++k)
                            bak[k].CopyTo(a[k], 0);
                    }
            return maxn;
        }
    }
}
