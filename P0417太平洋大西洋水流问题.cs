using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1
{
    // medium, 2022/4/27 Daily
    // BFS
    internal class P0417太平洋大西洋水流问题
    {
        public IList<IList<int>> PacificAtlantic(int[][] heights)
        {
            int[,] check = new int[heights.Length, heights[0].Length];
            Queue<(int, int)> qu = new();
            foreach ((int i, int j) in heights.Outers())
            {
                if (i == 0 || j == 0) check[i, j]++;
                if (i == heights.Length - 1 || j == heights[i].Length - 1)
                    check[i, j] += 2;
                qu.Enqueue((i, j));
            }

            while (qu.Any())
            {
                (int i, int j) = qu.Dequeue();
                foreach ((int ni, int nj) in heights.FourDir(i, j).Where(t => heights[t.ni][t.nj] >= heights[i][j]))
                {
                    if ((check[i,j] & check[ni,nj]) != check[i, j])
                    {
                        check[ni, nj] |= check[i, j];
                        qu.Enqueue((ni, nj));
                    }
                }
            }

            IList<IList<int>> ans = new List<IList<int>>();
            for (int i = 0; i < heights.Length; i++)
                for (int j = 0; j < heights[i].Length; j++)
                    if (check[i, j] == 3)
                        ans.Add(new List<int>() { i, j });
            return ans;
        }
    }
}
