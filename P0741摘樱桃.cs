using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1
{
    // hard, DP // 2022/2/12
    internal class P0741摘樱桃
    {
        int N;
        int Zip(int i1, int j1, int i2, int j2) => i1 << 18 | j1 << 12 | i2 << 6 | j2;
        (int i1, int j1, int i2, int j2) Unzip(int key) => (key >> 18, key >> 12 & 63, key >> 6 & 63, key & 63);
        IEnumerable<(int i1, int j1, int i2, int j2)> MoveNext(int i, int j, int i0, int j0)
        {
            if (i < N - 1)
            {
                if (i0 < N - 1 && i <= i0) yield return (i + 1, j, i0 + 1, j0);
                if (j0 < N - 1 && i + 1 <= i0) yield return (i + 1, j, i0, j0 + 1);
            }
            if (j < N - 1)
            {
                if (i0 < N - 1 && i <= i0 + 1) yield return (i, j + 1, i0 + 1, j0);
                if (j0 < N - 1 && i <= i0) yield return (i, j + 1, i0, j0 + 1);
            }
        }
        public int CherryPickup(int[][] grid)
        {
            N = grid.Length;
            Dictionary<int, int> dic = new(), dp = new(), temp;
            dic[0] = grid[0][0];
            for (int m = grid.Length * 2 - 2; m > 0 && dic.Any(); --m)
            {
                foreach ((int key, int count) in dic)
                {
                    (int i, int j, int i0, int j0) = Unzip(key);
                    foreach ((int i1, int j1, int i2, int j2) in MoveNext(i, j, i0, j0))
                    {
                        if (grid[i1][j1] != -1 && grid[i2][j2] != -1)
                        {
                            int pickup = count;
                            if (grid[i1][j1] == 1) ++pickup;
                            if (grid[i2][j2] == 1 && i2 != i1) ++pickup;
                            int kn = Zip(i1, j1, i2, j2);
                            if (!dp.ContainsKey(kn) || dp[kn] < pickup)
                                dp[kn] = pickup;
                        }
                    }
                }
                dic.Clear(); temp = dic; dic = dp; dp = temp;
            }
            return dic.Any() ? dic.First().Value : 0;
        }
    }
}
