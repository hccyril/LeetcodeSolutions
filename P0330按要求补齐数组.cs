using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1
{
    class P0330按要求补齐数组
    {
        public int MinPatches(int[] nums, int n)
        {
            long sum = 0, N = n;
            int i = 0, patch = 0;
            while (sum < N)
            {
                long next = sum + 1;
                if (i < nums.Length && nums[i] <= next)
                {
                    sum += nums[i++];
                }
                else
                {
                    sum += next;
                    patch++;
                }
            }
            return patch;
        }
    }
}
