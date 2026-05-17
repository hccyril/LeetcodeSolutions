using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1;

// 20260517 WC502-C
// ST表
internal class P3933矩阵中的局部最大值II
{
    public int CountLocalMaximums(int[][] matrix)
    {
        int n = matrix.Length, m = matrix[0].Length;
        var sa = matrix.Select(r => new SparseTable<int>(r, (a, b) => Math.Max(a, b))).ToArray();
        int ans = 0;
        for (int i = 0; i < n; ++i)
            for (int j = 0; j < m; ++j)
                if (matrix[i][j] > 0)
                {
                    ++ans;
                    int d = matrix[i][j];
                    for (int u = Math.Max(0, i - d); u <= Math.Min(n - 1, i + d); ++u)
                    {
                        int l = j - d, r = j + d;
                        if (u == i - d || u == i + d)
                            (l, r) = (j - d + 1, j + d - 1);
                        l = Math.Max(0, l);
                        r = Math.Min(m - 1, r); // WA BUG: 应该是m - 1不是n - 1
                        if (sa[u].Query(l, r) > d)
                        {
                            --ans;
                            break;
                        }
                    }
                }
        return ans;
    }
}
