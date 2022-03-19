using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1
{
    // medium, WC257-C, 2022/3/10
    // rank: 2260 
    // CF：https://codeforces.com/problemset/problem/407/B (CFRank: 1600)
    // DP (看了Hint才想出来）// 补充：DP还不够，会超时，要再加个前缀和
    internal class P1997访问完所有房间的第一天
    {
        public int FirstDayBeenInAllRooms(int[] nextVisit)
        {
            Span<int> odd = stackalloc int[nextVisit.Length]; // 第一次访问到i所需天数
            //Span<int> even = stackalloc int[nextVisit.Length]; // 第二次访问到i所需天数
            Span<int> comeback = stackalloc int[nextVisit.Length]; // 从i再次访问到i所需天数
            Span<int> presum = stackalloc int[nextVisit.Length]; // comeback的前缀和
            odd[0] = 1;
            comeback[0] = 1;
            presum[0] = 1;
            int even = 2;
            for (int i = 1; i < nextVisit.Length - 1; i++)
            {
                odd[i] = even.Add(1);
                // 还要维护前缀和！否则会超时！！
                int sum = presum[i - 1].Add(i + 1);
                if (nextVisit[i] > 0) sum = sum.Sub(presum[nextVisit[i] - 1]).Sub(nextVisit[i]);
                presum[i] = presum[i - 1].Add(comeback[i] = sum);
                //for (int back = nextVisit[i]; back < i; back++)
                //    sum = sum.Add(comeback[back]).Add(1);
                comeback[i] = sum;
                even = odd[i].Add(comeback[i]);
            }
            return even;
        }

        internal static void Run()
        {
            int[] input = { 0, 0, 2 };
            int ans = new P1997访问完所有房间的第一天().FirstDayBeenInAllRooms(input);
            Console.WriteLine(ans);
        }
    }
}
