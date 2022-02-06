using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1
{
    // medium // 前缀和 - 二维数组
    public class NumMatrix
    {
        int[,] sums;
        public NumMatrix(int[][] matrix)
        {
            sums = new int[matrix.Length, matrix[0].Length];
            Span<int> sr = stackalloc int[matrix[0].Length];
            for (int i = 0; i < matrix.Length; ++i)
                for (int j = 0; j < matrix[i].Length; ++j)
                {
                    sr[j] = matrix[i][j] + (j > 0 ? sr[j - 1] : 0);
                    sums[i, j] = sr[j] + (i > 0 ? sums[i - 1, j] : 0);
                }
        }

        public int SumRegion(int row1, int col1, int row2, int col2)
            => sums[row2, col2] - (row1 > 0 ? sums[row1 - 1, col2] : 0)
                - (col1 > 0 ? (sums[row2, col1 - 1] - (row1 > 0 ? sums[row1 - 1, col1 - 1] : 0)) : 0);
    }

    internal class P0304二维区域和检索_矩阵不可变
    {
    }
}
