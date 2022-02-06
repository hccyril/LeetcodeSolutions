using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1
{
    // medium, 可能是二维二分搜索 // TODO 2021/7/7 Daily
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
        /**
         * 没时间做，抄官方的题解
         * */
        public int KthSmallest(int[][] matrix, int k)
        {
            throw new NotImplementedException();
        }

        public int KthSmallest_official(int[][] matrix, int k)
        {
            int n = matrix.Length;
            int left = matrix[0][0];
            int right = matrix[n - 1][n - 1];
            while (left < right)
            {
                int mid = left + ((right - left) >> 1);
                if (check_official(matrix, mid, k, n))
                {
                    right = mid;
                }
                else
                {
                    left = mid + 1;
                }
            }
            return left;
        }

        public bool check_official(int[][] matrix, int mid, int k, int n)
        {
            int i = n - 1;
            int j = 0;
            int num = 0;
            while (i >= 0 && j < n)
            {
                if (matrix[i][j] <= mid)
                {
                    num += i + 1;
                    j++;
                }
                else
                {
                    i--;
                }
            }
            return num >= k;
        }
    }
}
