using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1
{
    // medium, 2022/11/16 Daily
    // 树状数组 Fenwick Tree
    internal class P0775全局倒置与局部倒置
    {
        // 方法2：计算是否有偏移量超过1的（即属于全局倒置但不属于局部倒置）
        public bool IsIdealPermutation_2(int[] nums)
            => !Enumerable.Range(0, nums.Length).Any(i => Math.Abs(i - nums[i]) > 1);


        // 方法1：AC, 树状数组求逆序对
        public bool IsIdealPermutation(int[] nums)
        {
            int global = 0, local = 0, pre = -1;
            Fenwick fe = new(nums.Length);
            foreach (int n in nums)
            {
                global += n - fe.Sum(n);
                fe.Update(n);
                if (n < pre) ++local;
                pre = n;
            }
            return global == local;
        }
    }
}
