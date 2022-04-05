using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1
{
    // hard, WC91-D, 2022/4/4
    // rank: 2307
    // 单调栈
    internal class P0862和至少为K的最短子数组
    {
        class NumStruct : IComparable<NumStruct>
        {
            public int i;
            public long n;
            public int CompareTo(NumStruct other) => n.CompareTo(other.n);
        }
        public int ShortestSubarray(int[] nums, int k)
        {
            int ans = int.MaxValue;
            long sum = 0;
            List<NumStruct> list = new();
            list.Add(new() { i = 0, n = 0 });
            for (int i = 1; i <= nums.Length; ++i)
            {
                sum += nums[i - 1];
                int f = list.BinarySearch(new() { n = sum - k }); // 有一个用例在这里卡溢出（sum为负数最小而k为正数最大）
                if (f < 0) f = ~f - 1;
                if (f >= 0)
                    ans = Math.Min(ans, i - list[f].i);
                NumStruct ns = new() { i = i, n = sum };
                while (list.Any() && ns.n <= list.Last().n) list.RemoveAt(list.Count - 1);
                list.Add(ns);
            }
            if (ans == int.MaxValue) ans = -1;
            return ans;
        }
    }
}
