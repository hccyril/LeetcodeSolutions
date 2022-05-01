using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1
{
    // hard, 2022/4/17
    // LIS (最长递增子序列）
    internal class P1671得到山形数组的最少删除次数
    {
        // a[i] 表示以nums[i]为峰值的最大递增子序列的个数
        void LIS(int[] nums, int[] a)
        {
            List<int> list = new() { nums[0] };
            for (int i = 1; i < nums.Length; i++)
            {
                int p = list.BinarySearch(nums[i]); if (p < 0) p = ~p;
                if (p == list.Count) list.Add(nums[i]);
                else if (nums[i] < list[p]) list[p] = nums[i];
                a[i] = p + 1;
            }
        }
        public int MinimumMountainRemovals(int[] nums)
        {
            int[] a = new int[nums.Length], b = new int[nums.Length];
            LIS(nums, a);
            Array.Reverse(nums);
            LIS(nums, b);
            Array.Reverse(b);
            return nums.Length - Enumerable.Range(0, nums.Length).Where(i => a[i] > 1 && b[i] > 1).Select(i => a[i] + b[i] - 1).Max();
        }
    }
}
