using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1
{
    /** 499. 迷宫 III
    由空地和墙组成的迷宫中有一个球。球可以向上（u）下（d）左（l）右（r）四个方向滚动，但在遇到墙壁前不会停止滚动。当球停下时，可以选择下一个方向。迷宫中还有一个洞，当球运动经过洞时，就会掉进洞里。

    给定球的起始位置，目的地和迷宫，找出让球以最短距离掉进洞里的路径。 距离的定义是球从起始位置（不包括）到目的地（包括）经过的空地个数。通过'u', 'd', 'l' 和 'r'输出球的移动方向。 由于可能有多条最短路径， 请输出字典序最小的路径。如果球无法进入洞，输出"impossible"。

    迷宫由一个0和1的二维数组表示。 1表示墙壁，0表示空地。你可以假定迷宫的边缘都是墙壁。起始位置和目的地的坐标通过行号和列号给出。

    示例1:

    输入 1: 迷宫由以下二维数组表示

    0 0 0 0 0
    1 1 0 0 1
    0 0 0 0 0
    0 1 0 0 1
    0 1 0 0 0

    输入 2: 球的初始位置 (rowBall, colBall) = (4, 3)
    输入 3: 洞的位置 (rowHole, colHole) = (0, 1)

    输出: "lul"

    解析: 有两条让球进洞的最短路径。
    第一条路径是 左 -> 上 -> 左, 记为 "lul".
    第二条路径是 上 -> 左, 记为 'ul'.
    两条路径都具有最短距离6, 但'l' < 'u'，故第一条路径字典序更小。因此输出"lul"。

    示例 2:

    输入 1: 迷宫由以下二维数组表示

    0 0 0 0 0
    1 1 0 0 1
    0 0 0 0 0
    0 1 0 0 1
    0 1 0 0 0

    输入 2: 球的初始位置 (rowBall, colBall) = (4, 3)
    输入 3: 洞的位置 (rowHole, colHole) = (3, 0)

    输出: "impossible"

    示例: 球无法到达洞。

    注意:

    迷宫中只有一个球和一个目的地。
    球和洞都在空地上，且初始时它们不在同一位置。
    给定的迷宫不包括边界 (如图中的红色矩形), 但你可以假设迷宫的边缘都是墙壁。
    迷宫至少包括2块空地，行数和列数均不超过30。

     * */

    // hard, plus, 2022/2/26
    // 尝试使用SortedDictionary
    internal class P0499迷宫III
    {
        //执行结果：通过
        //执行用时：96 ms, 在所有 C# 提交中击败了100.00% 的用户
        //内存消耗：41 MB, 在所有 C# 提交中击败了100.00%的用户
        //通过测试用例：64 / 64
        public string FindShortestWay(int[][] maze, int[] ball, int[] hole)
        {
            int M = maze.Length, N = maze[0].Length;
            char[] dirs = { 'u', 'd', 'l', 'r' };
            static int Zip(int path, int x, int y) => path << 10 | x << 5 | y;
            static (int, int, int) Unzip(int z) => (z >> 10, z >> 5 & 31, z & 31);

            SortedDictionary<int, string> hp = new();

            hp.Add(Zip(0, ball[0], ball[1]), "");
            while (hp.Any())
            {
                (int k, string v) = hp.First();
                hp.Remove(k);
                (int path, int i, int j) = Unzip(k);
                if (i == hole[0] && j == hole[1]) return v;
                if (maze[i][j] != 0) continue;
                maze[i][j] = 2;
                foreach (var d in dirs)
                {
                    int x = i, y = j, nx = i, ny = j, p = path;
                    while (true)
                    {
                        nx = d switch
                        {
                            'u' => x - 1,
                            'd' => x + 1,
                            _ => x
                        };
                        ny = d switch
                        {
                            'l' => y - 1,
                            'r' => y + 1,
                            _ => y
                        };
                        if (nx < 0 || nx >= M || ny < 0 || ny >= N || maze[nx][ny] == 1) break;
                        x = nx; y = ny; ++p;
                        if (x == hole[0] && y == hole[1]) break;
                    }
                    if (maze[x][y] == 0)
                    {
                        int key = Zip(p, x, y);
                        string val = v + d;
                        if (!hp.TryAdd(key, val))
                            if (val.CompareTo(hp[key]) < 0)
                                hp[key] = val;
                    }
                }
            }
            return "impossible";
        }

        internal static void Run()
        {
            SortedDictionary<int, string> hp = new();
            hp.Add(5, "5a");
            hp.Add(6, "6a");
            hp.Add(7, "7a");
            hp.Add(1, "1a");
            hp.Add(2, "2a");

            var first = hp.First();
            Console.WriteLine("First: {0} {1}", first.Key, first.Value);
            hp.Remove(first.Key);

            first = hp.First();
            Console.WriteLine("second: {0} {1}", first.Key, first.Value);

        }
    }
}
