using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1
{
    // hard, 2022/7/18 US Daily
    // 前缀和（二维）
    internal class P1074元素和为目标值的子矩阵数量
    {
		public const string 相似题 = "面试题 17.24. 最大子矩阵";
		
		// 虽然不能两边都枚举所有区间，但是其中一边枚举所有区间是可以的
        public int NumSubmatrixSumTarget(int[][] matrix, int target)
        {
            int m = matrix.Length, n = matrix[0].Length, ans = 0;
            Dictionary<(int, int), Dictionary<int, int>> dic = new();
            Dictionary<(int, int), int> ss = new();
            for (int i = 0; i < n; ++i)
                for (int j = i; j < n; ++j)
                {
                    dic[(i, j)] = new();
                    ss[(i, j)] = 0;
                }
            foreach (var a in matrix)
            {
                for (int i = 1; i < n; ++i) a[i] += a[i - 1];
                for (int i = 0; i < n; ++i)
                    for (int j = i; j < n; ++j)
                    {
                        int rs = a[j]; if (i > 0) rs -= a[i - 1];
                        rs = ss[(i, j)] += rs;
                        if (rs == target) ++ans; // 差点漏了这句
                        int t = rs - target;
                        if (dic[(i, j)].TryGetValue(t, out int x))
                            ans += x;
                        if (!dic[(i, j)].ContainsKey(rs)) dic[(i, j)][rs] = 1;
                        else ++dic[(i, j)][rs];
                    }
            }
            return ans;
        }
    }
}
