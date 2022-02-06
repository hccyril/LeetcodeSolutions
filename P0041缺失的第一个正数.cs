using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1
{
    /* 41. 缺失的第一个正数
     * hard
     * 给你一个未排序的整数数组 nums ，请你找出其中没有出现的最小的正整数。

请你实现时间复杂度为 O(n) 并且只使用常数级别额外空间的解决方案。
 
     * */
    class P0041缺失的第一个正数
    {
        public class Solution
        {
            /*
             * 评论：Saoqi 2018-12-14
            遍历一次数组把大于等于1的和小于数组大小的值放到原数组对应位置，
            然后再遍历一次数组查当前下标是否和值对应，如果不对应那这个下标就是答案，
            否则遍历完都没出现那么答案就是数组长度加1。
             * */

            public int FirstMissingPositive(int[] nums)
            {
                int temp;
                for (int i = 0; i < nums.Length; ++i)
                {
                    int comp = i + 1;
                    while (nums[i] != comp && nums[i] >= 1 && nums[i] <= nums.Length)
                    {
                        int j = nums[i] - 1;
                        if (nums[j] == nums[i]) break;
                        temp = nums[j];
                        nums[j] = nums[i];
                        nums[i] = temp;
                    }
                }
                for (int i = 0; i < nums.Length; ++i)
                {
                    int comp = i + 1;
                    if (nums[i] != comp) return comp;
                }
                return nums.Length + 1;
            }

            // 以下方法虽然能AC但不符合题目空间复杂度要求
            //public int FirstMissingPositive(int[] nums)
            //{
            //    int[] maps = new int[500001];
            //    foreach (var n in nums)
            //        if (n > 0 && n <= 500000)
            //            maps[n] = 1;
            //    int i = 0;
            //    for (i = 1; i <= 500000; ++i)
            //    {
            //        if (maps[i] == 0) return i;
            //    }
            //    return i;
            //}
        }
    }
}
