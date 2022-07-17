using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1
{
    // medium, 可能是二维二分搜索 // 2021/7/7 Daily
    // 2022/7/16 Done
    /*
     * 378. 有序矩阵中第 K 小的元素
     * https://leetcode-cn.com/problems/kth-smallest-element-in-a-sorted-matrix/
    给你一个 n x n 矩阵 matrix ，其中每行和每列元素均按升序排序，找到矩阵中第 k 小的元素。
    请注意，它是 排序后 的第 k 小元素，而不是第 k 个 不同 的元素。

    示例 1：
    输入：matrix = [[1,5,9],[10,11,13],[12,13,15]], k = 8
    输出：13
    解释：矩阵中的元素为 [1,5,9,10,11,12,13,13,15]，第 8 小元素是 13

    示例 2：
    输入：matrix = [[-5]], k = 1
    输出：-5

     * */
    class P0378有序矩阵中第K小的元素
    {
        public int KthSmallest(int[][] matrix, int k)
        {
            int N = matrix.Length, LC = k - 1, RC = N * N - k;
            int l = matrix[0][0], r = matrix.Last().Last();
            int Check(int t)
            {
                int le = 0, mo = 0, i = N, j = N;
                foreach (var rw in matrix)
                {
                    while (i > 0 && rw[i - 1] >= t) --i;
                    while (j > 0 && rw[j - 1] > t) --j;
                    if ((le += i) > LC) return 1;
                    if ((mo += N - j) > RC) return -1;
                }
                return 0;
            }
            while (l < r)
            {
                int t = l + r + 1 >> 1;
                switch (Check(t))
                {
                    case 0: return t;
                    case -1: l = t + 1; break;
                    case 1: r = t - 1; break;
                }
            }
            return l;
        }
    }
}
