using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1
{
    // hard, DP+二分 // 2021/8/28 US Daily 
    class P1235规划兼职工作
    {
        public int JobScheduling(int[] startTime, int[] endTime, int[] profit)
        {
            int n = startTime.Length;
            var arr = Enumerable.Range(0, n).Select(i => new
            {
                start = startTime[i],
                end = endTime[i],
                pf = profit[i]
            }).OrderBy(t => t.end).ThenBy(t => t.start).ToArray();
            for (int i = 0; i < n; ++i) endTime[i] = arr[i].end;
            int[] dp = new int[n];
            dp[0] = arr[0].pf;
            for (int i = 1; i < n; ++i)
            {
                dp[i] = Math.Max(dp[i - 1], arr[i].pf);
                int index = Array.BinarySearch(endTime, 0, i, arr[i].start);
                if (index < 0) index = -1 - index;
                while (endTime[index] <= arr[i].start) index++;
                if (index > 0) dp[i] = Math.Max(dp[i], arr[i].pf + dp[index - 1]);
            }
            return dp.Last();
        }
    }
}
