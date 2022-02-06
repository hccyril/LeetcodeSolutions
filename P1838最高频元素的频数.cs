using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1
{
    class P1838最高频元素的频数
    {
        public int MaxFrequency(int[] nums, int k)
        {
            Array.Sort(nums);
            int count = 0;
            int ed = nums.Length;
            for (int st = ed - 1; st >= 0; --st)
            {
                k -= nums[ed - 1] - nums[st];
                while (k < 0)
                {
                    ed--;
                    k += (nums[ed] - nums[ed - 1]) * (ed - st);
                }
                if (ed - st > count) count = ed - st;
            }
            return count;
        }
    }
}
