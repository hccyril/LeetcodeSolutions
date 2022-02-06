using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1
{
    // 双周赛题C，medium，但该场比赛这题最难
    // 核心就是BFS + 堆排序，但题目多了一堆杂七杂八的东西弄
    internal class P2146价格范围内最高排名的K样物品
    {
        class ProductStruct
        {
            public int dist;
            public int price;
            public int row;
            public int col;
            public bool Compare(ProductStruct that)
            {
                if (dist == that.dist)
                {
                    if (price == that.price)
                    {
                        if (row == that.row)
                        {
                            return col < that.col;
                        }
                        else return row < that.row;
                    }
                    else return this.price < that.price;
                }
                else
                {
                    return dist < that.dist;
                }
            }
        }

        IEnumerable<(int ni, int nj)> Move(int[][] grid, int i, int j)
        {
            int ni = i, nj = j;
            ni = i - 1;
            if (ni >= 0 && grid[ni][nj] > 0) yield return (ni, nj);
            ni = i + 1;
            if (ni < grid.Length && grid[ni][nj] > 0) yield return (ni, nj);
            ni = i;
            nj = j - 1;
            if (nj >= 0 && grid[ni][nj] > 0) yield return (ni, nj);
            nj = j + 1;
            if (nj < grid[0].Length && grid[ni][nj] > 0) yield return (ni, nj);
        }

        public IList<IList<int>> HighestRankedKItems(int[][] grid, int[] pricing, int[] start, int k)
        {
            Heap<ProductStruct> hp = new((a, b) => !a.Compare(b));
            int i = start[0], j = start[1];
            if (grid[i][j] >= pricing[0] && grid[i][j] <= pricing[1])
                hp.Push(new ProductStruct
                {
                    dist = 0,
                    price = grid[i][j],
                    row = i,
                    col = j
                });
            grid[i][j] = -1;
            Queue<(int i, int j, int dist)> qu = new();
            qu.Enqueue((i, j, 0));
            while (qu.Any())
            {
                int dist;
                (i, j, dist) = qu.Dequeue(); ++dist;
                foreach ((int ni, int nj) in Move(grid, i, j))
                {
                    int price = grid[ni][nj];
                    if (price >= pricing[0] && price <= pricing[1])
                    {
                        var ps = new ProductStruct
                        {
                            dist = dist,
                            price = price,
                            row = ni,
                            col = nj
                        };
                        if (hp.Count < k || ps.Compare(hp.Head))
                        {
                            if (hp.Count == k) hp.Pop();
                            hp.Push(ps);
                        }
                    }
                    grid[ni][nj] = -1;
                    qu.Enqueue((ni, nj, dist));
                }
            }
            IList<IList<int>> ansList = new List<IList<int>>();
            Stack<ProductStruct> stk = new();
            while (hp.Any()) stk.Push(hp.Pop());
            while (stk.Any())
            {
                var ps = stk.Pop();
                ansList.Add(new List<int>() { ps.row, ps.col });
            }
            return ansList;
        }
    }
}
