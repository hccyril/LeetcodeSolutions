using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1;

// Hard, 2023/12/12 Daily
// 单调栈+红黑树
internal class P2454下一个更大元素IV
{
    public int[] SecondGreaterElement(int[] nums)
    {
        int[] ans = nums.Select(_ => -1).ToArray();
        List<int> stk = new();
        SortedSet<(int, int)> srt = new();
        for (int i = 0; i < nums.Length; ++i)
        {
            int x = nums[i];
            while (srt.Any())
            {
                (int s, int j) = srt.Min;
                if (x > s)
                {
                    ans[j] = x;
                    srt.Remove((s, j));
                }
                else break;
            }
            while (stk.Any() && nums[stk[^1]] < x)
            {
                srt.Add((nums[stk[^1]], stk[^1]));
                stk.RemoveAt(stk.Count - 1);
            }
            stk.Add(i);
        }
        return ans;
    }
}
