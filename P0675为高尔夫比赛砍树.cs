using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1
{
    // hard, 2022/5/23 daily
    // BFS
    internal class P0675为高尔夫比赛砍树
    {
        public int CutOffTree(IList<IList<int>> forest)
        {
            // normalize
            List<int> list = new();
            Dictionary<int, int> dic = new();
            foreach (var r in forest)
                foreach (int f in r)
                    if (f > 1)
                        list.Add(f);
            list.Sort(); // 忘了！！
            for (int i = 0; i < list.Count; ++i)
                dic[list[i]] = i + 2;
            for (int i = 0; i < forest.Count; ++i)
                for (int j = 0; j < forest[i].Count; ++j)
                    if (forest[i][j] > 1)
                        forest[i][j] = dic[forest[i][j]];
            int m = forest.Count, n = forest[0].Count, end = list.Count + 1;

            // BFS
            Queue<(int, int, int, int)> qu = new();
            int[,] map = new int[m, n];
            map[0, 0] = forest[0][0] == 2 ? 2 : 1; // ver1 直接等于forest[0][0]，这是不对的，只有高度==2时可以一上来就砍了
            qu.Enqueue((0, 0, map[0, 0], 0));
            while (qu.Any())
            {
                (int i, int j, int h, int p) = qu.Dequeue();
                foreach ((int ni, int nj) in GraphEX.FourDir(m, n, i, j))
                {
                    if (forest[ni][nj] > 0 && map[ni, nj] < h)
                    {
                        if (forest[ni][nj] == h + 1)
                        {
                            ++h;
                            if (h == end) return p + 1;
                            map[ni, nj] = h;
                            qu.Enqueue((ni, nj, h, p + 1));
                            break;
                        }
                        
                        map[ni, nj] = h;
                        qu.Enqueue((ni, nj, h, p + 1));
                    }
                }
            }
            return -1;
        }

        // 没考虑到(0,0)的树不是最矮的树的情况
        // WA: [[54581641,64080174,24346381,69107959],[86374198,61363882,68783324,79706116],[668150,92178815,89819108,94701471],[83920491,22724204,46281641,47531096],[89078499,18904913,25462145,60813308]]
        // exp: 57
        // my : 35
        
        /*
input:
    [[1,2,3],[0,0,4],[7,6,5]]
    [[1,2,3],[0,0,0],[7,6,5]]
    [[2,3,4],[0,0,5],[8,7,6]]
output:
    >>>> start <<<<
    0 0 1 1 0
     => 0 1 2 2 1
    0 1 2 2 1
     => 0 0 1 2 2
     => 0 2 3 3 2
    0 0 1 2 2
    0 2 3 3 2
     => 1 2 4 4 3
    1 2 4 4 3
     => 0 2 3 4 4
     => 2 2 5 5 4
    0 2 3 4 4
     => 0 1 2 4 5
    2 2 5 5 4
     => 1 2 4 5 5
     => 2 1 6 6 5
    0 1 2 4 5
     => 0 0 1 4 6
    1 2 4 5 5
     => 0 2 3 5 6
    2 1 6 6 5
    >>>> start <<<<
    0 0 1 1 0
     => 0 1 2 2 1
    0 1 2 2 1
     => 0 0 1 2 2
     => 0 2 3 3 2
    0 0 1 2 2
    0 2 3 3 2
     => 0 1 2 3 3
    0 1 2 3 3
     => 0 0 1 3 4
    0 0 1 3 4
    >>>> start <<<<
    0 0 2 1 0
     => 0 1 3 1 1
    0 1 3 1 1
     => 0 2 4 1 2
    0 2 4 1 2
     => 1 2 5 1 3
    1 2 5 1 3
     => 2 2 6 1 4
    2 2 6 1 4
     => 2 1 7 1 5
    2 1 7 1 5
     => 2 0 8 1 6
    2 0 8 1 6

         * */
    }
}
