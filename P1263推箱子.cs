using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1
{
    // hard, 2023/5/8 Daily
    // BFS - 状态处理比较复杂
    internal class P1263推箱子
    {
        public int MinPushBox(char[][] grid)
        {
            int m = grid.Length, n = grid[0].Length;

            IEnumerable<(int ni, int nj)> FourDir(int i, int j)
            {
                if (i > 0 && grid[i - 1][j] != '#') yield return (i - 1, j);
                if (i < m - 1 && grid[i + 1][j] != '#') yield return (i + 1, j);
                if (j > 0 && grid[i][j - 1] != '#') yield return (i, j - 1);
                if (j < n - 1 && grid[i][j + 1] != '#') yield return (i, j + 1);
            }

            // access map: 0-blocked; 1-accessable
            BitArray ba = new(m * n);

            // player can move to (i, j) at current situation
            bool CanGo(int i, int j) => ba[i * n + j];

            // set map[i,j] to true, return true if original value is false
			bool SetGo(int i, int j) => !ba[i * n + j] && (ba[i * n + j] = true);
            // bool SetGo(int i, int j) => !(ba[i * n + j] & (ba[i * n + j] = true));

            // assume box at (i, j) when calling this method
            // return (Pi, Pj, Bi, Bj) which P is player and B is box
            IEnumerable<(int, int, int, int)> Dirs(int i, int j)
            {
                // up-down
                if (i > 0 && grid[i - 1][j] != '#' && i < m - 1 && grid[i + 1][j] != '#')
                {
                    if (CanGo(i - 1, j))
                        yield return (i - 1, j, i + 1, j);
                    if (CanGo(i + 1, j))
                        yield return (i + 1, j, i - 1, j);
                }
                // left-right
                if (j > 0 && grid[i][j - 1] != '#' && j < n - 1 && grid[i][j + 1] != '#')
                {
                    if (CanGo(i, j - 1))
                        yield return (i, j - 1, i, j + 1);
                    if (CanGo(i, j + 1))
                        yield return (i, j + 1, i, j - 1);
                }
            }

            // mark all the pos player can go to
            void BfsMap(int bi, int bj, int pi, int pj)
            {
                ba.SetAll(false);
                grid[bi][bj] = '#';
                SetGo(pi, pj);
                Queue<(int, int)> qu = new();
                qu.Enqueue((pi, pj));
                while (qu.Any())
                {
                    (int i, int j) = qu.Dequeue();
                    foreach ((int ni, int nj) in FourDir(i, j))
                        if (SetGo(ni, nj))
                            qu.Enqueue((ni, nj));
                }
                grid[bi][bj] = '.';
            }

            // start
            int bi = 0, bj = 0, pi = 0, pj = 0;
            Dictionary<(int, int, int, int), int> di = new();
            for (int i = 0; i < m; ++i)
                for (int j = 0; j < n; ++j)
                    if (grid[i][j] == 'B')
                        (bi, bj) = (i, j);
                    else if (grid[i][j] == 'S')
                        (pi, pj) = (i, j);
            grid[pi][pj] = '.';
            BfsMap(bi, bj, pi, pj);
            Queue<(int, int, int, int)> qu = new();
            foreach ((int px, int py, int bx, int by) in Dirs(bi, bj))
            {
                if (grid[bx][by] == 'T') return 1;
                di[(px, py, bi, bj)] = 0;
                di[(bi, bj, bx, by)] = 1;
                qu.Enqueue((bi, bj, bx, by));
            }
            while (qu.Any())
            {
                (pi, pj, bi, bj) = qu.Dequeue();
                int steps = di[(pi, pj, bi, bj)];
                BfsMap(bi, bj, pi, pj);
                foreach ((int px, int py, int bx, int by) in Dirs(bi, bj))
                    if (!di.ContainsKey((bi, bj, bx, by)))
                    {
                        if (grid[bx][by] == 'T') return steps + 1;
                        di[(bi, bj, bx, by)] = steps + 1;
                        qu.Enqueue((bi, bj, bx, by));
                    }
            }
            return -1;
        }   

        internal static void Run()
        {
            char[] a1 = { '.', '.', '.' }, a2 = { '.', '.', '.' }, a3 = { '#', 'B', '.' }, a4 = { 'S', 'T', '#' };
            char[][] a = { a1, a2, a3, a4 };
            var sln = new P1263推箱子();
            int ans = sln.MinPushBox(a);
            Console.WriteLine("ans=" + ans);
        }
    }
}
