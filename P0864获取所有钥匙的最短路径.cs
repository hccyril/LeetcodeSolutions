using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1
{
    // hard, 2022/1/29
	// rank: 2259
    // 本题用了C# 8.0的switch用法
    internal class P0864获取所有钥匙的最短路径
    {
        int HashKey(int i, int j, int map) => (i << 20) | (j << 10) | map;
        int Encode(int i, int j, int map, int move) => (i << 25) | (j << 20) | (map << 14) | move;
        (int i, int j, int map, int move) Decode(int code)
        {
            int i = code >> 25, j = (code >> 20) & 31, map = (code >> 14) & 63, move = code & 16383;
            return (i, j, map, move);
        }
        IEnumerable<(int ni, int nj)> Directions(string[] grid, int i, int j)
        {
            if (i > 0) yield return (i - 1, j);
            if (i < grid.Length - 1) yield return (i + 1, j);
            if (j > 0) yield return (i, j - 1);
            if (j < grid[i].Length - 1) yield return (i, j + 1);
        }
        IEnumerable<(int ni, int nj)> Move(string[] grid, int i, int j, int map, HashSet<int> hs)
        {
            foreach ((int ni, int nj) in Directions(grid, i, j))
            {
                if (grid[ni][nj] switch
                {
                    '#' => false,
                    >= 'A' and <= 'F' => (map & (1 << (grid[ni][nj] - 'A'))) != 0 && !hs.Contains(HashKey(ni, nj, map)),
                    >= 'a' and <= 'f' => !hs.Contains(HashKey(ni, nj, map | (1 << (grid[ni][nj] - 'a')))),
                    _ => !hs.Contains(HashKey(ni, nj, map))
                })
                    yield return (ni, nj);

                //bool ok = true;
                //if (c == '#') ok = false;
                //else if (char.IsUpper(c))
                //{
                //    int ac = 1 << (c - 'A');
                //    ......
                //}
                // ......
                //if (ok) yield return (ni, nj);
            }
        }
        public int ShortestPathAllKeys(string[] grid)
        {
            HashSet<int> hs = new();
            Queue<int> qu = new();
            int complete = 0;
            for (int i = 0; i < grid.Length; ++i)
                for (int j = 0; j < grid[i].Length; ++j)
                    if (grid[i][j] == '@')
                    {
                        hs.Add(HashKey(i, j, 0));
                        qu.Enqueue(Encode(i, j, 0, 0));
                    }
                    else if (char.IsLower(grid[i][j]))
                        complete |= 1 << (grid[i][j] - 'a');

            while (qu.Any())
            {
                (int i, int j, int map, int move) = Decode(qu.Dequeue());
                Console.WriteLine("Dequeue: i={0} j={1} map={2} move={3}", i, j, map, move); // DEBUG
                ++move;
                foreach ((int ni, int nj) in Move(grid, i, j, map, hs))
                {
                    int nmp = map; // 上一个版本直接就在map上面改了，导致状态出错然后WA= =
                    if (char.IsLower(grid[ni][nj]))
                    {
                        nmp |= 1 << (grid[ni][nj] - 'a');
                        if (nmp == complete)
                        {
                            Console.WriteLine("Done: {0} {1}", ni, nj); // DEBUG
                            return move;
                        }
                    }

                    // Console.WriteLine("      => i={0} j={1} map={2} move={3}", ni, nj, nmp, move); // DEBUG

                    hs.Add(HashKey(ni, nj, nmp));
                    qu.Enqueue(Encode(ni, nj, nmp, move));
                }
            }

            return -1;
        }

        internal static void Run()
        {
            var sln = new P0864获取所有钥匙的最短路径();
            string[] input =
{".#.b.", "A.#aB", "#d...", "@.cC.", "D...#"}; // 又WA了
                //{ "@.a.#", "###.#", "b.A.B" };
            var result = sln.ShortestPathAllKeys(input);
            Console.WriteLine("Result=" + result);
        }
    }
}
