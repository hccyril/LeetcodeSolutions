using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1
{
    // Hard, 2023/4/20 Daily -> 4/23才完成
    // DP+单调栈：dp[c,t]=true 表示存在修改c次并得到递增序列最大值t的方案，c更大时t更小
    internal class P1187使数组严格递增
    {
        void StkAdd(List<(int, int)> stk, int c, int t)
        {
            bool add = true;
            while (stk.Any())
            {
                (int cn, int tp) = stk.Last();
                if (t >= tp)
                {   // t >= stk.last, cannot improve, discard new 
                    add = false;
                    break;
                }
                else
                {   // t < stk.last, can improve, add to stk
                    if (c > cn) break;
                    else stk.RemoveAt(stk.Count - 1); // c == cn, new is better, remove old
                }
            }
            if (add) stk.Add((c, t));
        }
        public int MakeArrayIncreasing(int[] arr1, int[] arr2)
        {
            Array.Sort(arr2);
            List<(int, int)> dp = new(), dp1 = new();
            foreach (int a in arr1)
            {
                if (!dp.Any())
                {
                    dp.Add((0, a));
                    if (arr2[0] < a)
                        dp.Add((1, arr2[0]));
                    continue;
                }
                foreach ((int cn, int tp) in dp)
                {
                    // op1: if already increasing, no change
                    if (a > tp)
                        StkAdd(dp1, cn, a);

                    // op2: change a to make tp less
                    int f = Array.BinarySearch(arr2, tp + 1); if (f < 0) f = ~f;
                    if (f < arr2.Length)
                        StkAdd(dp1, cn + 1, arr2[f]);
                }
                if (!dp1.Any()) return -1;
                (dp, dp1) = (dp1, dp);
                dp1.Clear();
            }
            return dp.First().Item1;
        }
    }
}
