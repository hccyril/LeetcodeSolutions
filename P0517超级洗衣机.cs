using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1;

// hard, 应用题（也许是贪心 2021/09/29 Daily
// Done 2024/2/13
// 从是否balance > 0 以及 是否 x > mean 分四种情况分类讨论
class P0517超级洗衣机
{
    public int FindMinMoves(int[] machines)
    {
        int n = machines.Length, sm = machines.Sum();
        if (sm % n != 0)
            return -1;
        int mean = sm / n, balance = 0, ans = 0;
        foreach (int x in machines)
        {
            if (x > mean)
            {
                ans = Math.Max(ans, Math.Max(x - mean, balance += x - mean));
                //if (balance < 0)
                //{
                //    ans = Math.Max(ans, x - mean);
                //    balance += x - mean;
                //}
                //else
                //{
                //    ans = Math.Max(ans, balance += x - mean);
                //}
            }
            else
            {
                // ans = Math.Max(ans, Math.Max(mean - x, -(balance += x - mean))); // WA
                ans = Math.Max(ans, -(balance += x - mean));
            }
        }
        return ans;
    }

    // test case: [0,0,14,0,10,0,0,0] (ans=11)
    // test case: [9,1,8,8,9] (ans=4)

    public int FindMinMoves_Official(int[] machines)
    {
        var sum = machines.Sum();
        if (sum % machines.Length != 0)
            return -1;

        int fill = sum / machines.Length;
        int diff = 0;

        int result = 0;
        foreach (var machine in machines)
        {
            diff += machine - fill;
            result = Math.Max(result, Math.Abs(diff));
            result = Math.Max(result, machine - fill);
        }

        return result;
    }
}
