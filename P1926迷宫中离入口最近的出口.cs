using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1
{
    // 2022/11/21 US Daily
    // 最典型的迷宫题
    internal class P1926迷宫中离入口最近的出口
    {
        
        public int NearestExit(char[][] maze, int[] entrance)
        {
            bool IsExit(int x, int y) => (x, y) != (entrance[0], entrance[1]) && (x == 0 || x == maze.Length - 1 || y == 0 || y == maze[0].Length - 1);
            Dictionary<(int, int), int> di = new();
            di[(entrance[0], entrance[1])] = 0;
            Queue<(int, int)> qu = new();
            qu.Enqueue((entrance[0], entrance[1]));
            while (qu.Any())
            {
                (int i, int j) = qu.Dequeue();
                int p = di[(i, j)] + 1;
                foreach ((int x, int y) in maze.FourDir(i, j))
                    if (maze[x][y] == '.' && !di.ContainsKey((x, y)))
                    {
                        if (IsExit(x, y)) return p;
                        di[(x, y)] = p;
                        qu.Enqueue((x, y));
                    }
            }
            return -1;
        }
    }
}
