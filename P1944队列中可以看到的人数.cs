using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1
{
    // hard, 2022/3/2, 第57双周赛题D
    // 单调栈
    internal class P1944队列中可以看到的人数
    {
        public int[] CanSeePersonsCount(int[] heights)
        {
            List<int> stk = new();
            int[] ans = new int[heights.Length];
            for (int i = 0; i < heights.Length; i++)
            {
                while (stk.Any() && heights[i] >= heights[stk.Last()])
                {
                    ans[stk.Last()]++;
                    stk.RemoveAt(stk.Count - 1);
                }
                if (stk.Any()) ans[stk.Last()]++;
                stk.Add(i);
            }
            return ans;
        }
    }
}
