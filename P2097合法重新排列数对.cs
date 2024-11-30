using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1;

// hard, 2024/11/30 US Daily
// rating 2650
// 欧拉回路
internal class P2097合法重新排列数对
{
    public int[][] ValidArrangement(int[][] pairs)
    {
        var dg = pairs.DirectedGraphNoLength();
        int[][] ans = new int[pairs.Length][];
        int s = -1, i = ans.Length - 1;
        foreach (int t in dg.HierholzerDfs())
            if (s < 0) s = t;
            else ans[i--] = new int[] { t, s + (0 & (s = t)) }; // 纯粹因为不想写两行
        return ans;
    }

    internal static void Run()
    {
        var p = "[[5,1],[4,5],[11,9],[9,4]]".ToTestInput<int[][]>();
        var ans = new P2097合法重新排列数对().ValidArrangement(p);
        Console.WriteLine("P2097合法重新排列数对 ans = " + ans);
    }
}
