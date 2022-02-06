using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1
{
    internal class P2134最少交换次数来组合所有的1II
    {
        public int MinSwaps(int[] nums)
        {
            int n = nums.Length;
            int count = nums.Sum();
            if (count == 0 || count == n) return 0;
            nums[0] = nums[0] == 0 ? 1 : 0;
            for (int i = 1; i < n; ++i)
                nums[i] = (nums[i] == 0 ? 1 : 0) + nums[i - 1];
            int min = n;
            for (int i = 0; i < n; ++i)
            {
                int ct = i == 0 ? nums[count - 1] : (i < (i + count - 1) % n) ? nums[(i + count - 1) % n] - nums[i - 1]
    : nums[(i + count - 1) % n] + nums[n - 1] - nums[i - 1];
                if (ct < min) min = ct;
            }
            return min;
        }
    }
}
