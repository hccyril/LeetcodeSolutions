using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1
{
    // hard, 2023/10/22 US Daily
    // rating 1945 (个人觉得放到今天不应该这么低）
    // 沿着k左右两边创建单调栈，从大到小枚举下一个最小数，以及最小数所能覆盖的最大区间
    internal class P1793好子数组的最大分数
    {
        public int MaximumScore(int[] nums, int k)
        {
            List<int> si = new(), sj = new();
            int mi = nums[k];
            si.Add(k);
            for (int t = k - 1; t >= 0; --t)
                if (nums[t] < mi)
                {
                    si.Add(t);
                    mi = nums[t];
                }
            mi = nums[k];
            sj.Add(k);
            for (int t = k + 1; t < nums.Length; ++t)
                if (nums[t] < mi)
                {
                    sj.Add(t);
                    mi = nums[t];
                }
            int i = k, j = k, ki = 0, kj = 0, ma = mi = nums[k];
            while (i > 0 || j < nums.Length - 1)
            {
                if (ki == si.Count - 1 && i > 0)
                    i = 0;
                else if (ki < si.Count - 1 && i > si[ki + 1] + 1)
                    i = si[ki + 1] + 1;
                if (kj == sj.Count - 1 && j < nums.Length - 1)
                    j = nums.Length - 1;
                else if (kj < sj.Count - 1 && j < sj[kj + 1] - 1)
                    j = sj[kj + 1] - 1;

                ma = Math.Max(ma, mi * (j - i + 1));

                /*DEBUG*/Console.WriteLine("i={0} j={1} mi={2} ma={3}", i, j, mi, ma); 

                if (i > 0 && j < nums.Length - 1)
                {
                    if (nums[si[ki + 1]] == nums[sj[kj + 1]])
                    {
                        mi = nums[si[++ki]];
                        ++kj;
                    }
                    else if (nums[si[ki + 1]] > nums[sj[kj + 1]])
                        mi = nums[si[++ki]];
                    else
                        mi = nums[sj[++kj]];
                }
                else if (i > 0)
                {
                    mi = nums[si[++ki]];
                }
                else if (j < nums.Length - 1)
                {
                    mi = nums[sj[++kj]];
                }
            }

            return ma;
        }

        internal static void Run()
        {
            var sln = new P1793好子数组的最大分数();
            int[] nums = { 1, 4, 3, 7, 4, 5 };
            int k = 3;
            Console.WriteLine("1793 ans=" + sln.MaximumScore(nums, k));
        }
    }
}
