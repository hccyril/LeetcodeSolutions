using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1
{
    // medium, 2022/3/4 daily
    // 单调栈
    internal class P2104子数组范围和
    {
        public long SubArrayRanges(int[] nums)
        {
            Stack<(int, long)> stk = new();
            long ans = 0L;
            for (int i = 0; i < nums.Length; i++)
            {
                while (stk.Any() && nums[stk.Peek().Item1] <= nums[i])
                    stk.Pop();
                (int p, long r) = stk.Any() ? stk.Peek() : (-1, 0L);
                r += (long)(i - p) * nums[i];
                stk.Push((i, r));
                ans += r;
            }
            stk.Clear();
            for (int i = 0; i < nums.Length; i++)
            {
                while (stk.Any() && nums[stk.Peek().Item1] >= nums[i])
                    stk.Pop();
                (int p, long r) = stk.Any() ? stk.Peek() : (-1, 0L);
                r += (long)(i - p) * nums[i];
                stk.Push((i, r));
                ans -= r;
            }
            return ans;
        }

        internal static void Run()
        {
            int[] input = { 1, 2, 3 };
            var sln = new P2104子数组范围和();
            var ans = sln.SubArrayRanges(input);
            Console.WriteLine("SubArrayRanges=" + ans);
        }
    }
}
