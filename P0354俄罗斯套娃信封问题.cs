using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1
{
    // hard
    // 先转化为单调栈，再转化为最长递增子序列
    internal class P0354俄罗斯套娃信封问题
    {
        public int MaxEnvelopes(int[][] envelopes)
            => envelopes.OrderBy(e => e[1]).ThenByDescending(e => e[0]).Select(e => e[0]).LIS();
    }
}
