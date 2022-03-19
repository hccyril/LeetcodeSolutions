using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1
{
    // medium, 2022/3/11
    // 树状数组（Fenwick Tree）范例题
    internal class P0307区域和检索_数组可修改
    {
        Fenwick fwt;
        public void NumArray(int[] nums)
        {
            fwt = new(nums.Length);
            for (int i = 0; i < nums.Length; i++)
                fwt.Update(i, nums[i]);
        }

        public void Update(int index, int val)
        {
            int inc = val - fwt.Get(index);
            fwt.Update(index, inc);
        }

        public int SumRange(int left, int right)
        {
            int sum = fwt.Sum(right);
            if (left > 0) sum -= fwt.Sum(left - 1);
            return sum;
        }
    }
}
