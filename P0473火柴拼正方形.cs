using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1
{
    // medium, 2022/6/1 Daily
    // 回溯，B4Array首秀
    internal class P0473火柴拼正方形
    {
        int len;
        bool Dfs(B4Array ba, int[] ms, int i)
        {
            if (i == ms.Length) return true;
            Span<int> arr = stackalloc int[4];
            for (int j = 0; j < i; ++j)
                arr[ba[j]] += ms[j];
            int start = 0, end = 3;
            if (i > 0 && ms[i] == ms[i - 1]) start = ba[i - 1];
            while (end > 0 && arr[end - 1] == 0) --end;
            for (int j = start; j <= end; ++j)
            {
                if (arr[j] + ms[i] <= len)
                {
                    ba[i] = j;
                    if (Dfs(ba, ms, i + 1)) return true;
                }
            }
            return false;
        }
        public bool Makesquare(int[] matchsticks)
        {
            Array.Sort(matchsticks);
            int sum = matchsticks.Sum();
            if (sum % 4 != 0) return false;
            len = sum / 4;
            B4Array ba = new(matchsticks.Length);
            return Dfs(ba, matchsticks, 0);
        }
    }
}
