using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1
{
    // hard, 2023/3/4 US Daily
    // 排列组合计数
    internal class P2444统计定界子数组的数目
    {
        public long CountSubarrays(int[] nums, int minK, int maxK)
        {
            // 得到一个区间全部在边界范围内，求此区间的答案数
            long Count(int start, int end)
            {
                long cnt = 0L;
                SortedSet<(int, int)> sort = new();
                int i = start;
                for (int j = i; j <= end; ++j)
                {
                    sort.Add((nums[j], j));
                    while (sort.Any() && sort.Min.Item1 == minK && sort.Max.Item1 == maxK)
                    {
                        cnt += end - j + 1;
                        sort.Remove((nums[i], i));
                        ++i;
                    }
                }
                return cnt;
            }

            long ans = 0L;
            int start = 0, end = -1;
            for (int i = 0; i < nums.Length; ++i)
            {
                if (nums[i] < minK || nums[i] > maxK)
                {
                    end = i - 1;
                    if (end >= start) ans += Count(start, end);
                    start = i + 1;
                }
            }
            end = nums.Length - 1;
            if (end >= start) ans += Count(start, end);
            return ans;
        }
    }
}
