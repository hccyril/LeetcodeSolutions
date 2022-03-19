using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1
{
    // hard, 2022/2/28 Daily
    // n <= 20, requests.Length <= 16, 可直接穷举
    internal class P1601最多可达成的换楼请求数目
    {
        public int MaximumRequests(int n, int[][] requests)
        {
            int[] arr = new int[n];
            int limit = 1 << requests.Length;
            int ans = 0;
            for (int map = 1; map < limit; ++map)
            {
                Array.Fill(arr, 0);
                for (int i = 0; i < requests.Length; ++i)
                    if ((map & 1 << i) != 0)
                    {
                        --arr[requests[i][0]];
                        ++arr[requests[i][1]];
                    }
                if (!arr.Any(a => a != 0))
                    ans = Math.Max(ans, map.CountOne());
            }
            return ans;
        }
    }
}
