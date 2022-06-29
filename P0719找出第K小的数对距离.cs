using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1
{
    // hard, 2022/6/15
    // 二分, O(logN * logN * N）
    internal class P0719找出第K小的数对距离
    {
        public int SmallestDistancePair(int[] nums, int k)
        {
            Array.Sort(nums);
            int l = 0, r = nums.Last() - nums.First();

            int Count(int diff)
            {
                int cnt = 0;
                for (int i = 1; i < nums.Length; ++i)
                {
                    int f = nums.LowerBound(nums[i] - diff, 0, i);
                    cnt += i - f;
                }
                return cnt;
            }

            while (l < r)
            {
                int mid = l + r >> 1;
                int n = Count(mid);
                if (n < k) l = mid + 1;
                else r = mid;
            }
            return l;
        }
    }
}
