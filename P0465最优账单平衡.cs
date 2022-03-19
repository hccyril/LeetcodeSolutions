using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1
{
    /** 465. 最优账单平衡
    一群朋友在度假期间会相互借钱。比如说，小爱同学支付了小新同学的午餐共计 10 美元。如果小明同学支付了小爱同学的出租车钱共计 5 美元。我们可以用一个三元组 (x, y, z) 表示一次交易，表示 x 借给 y 共计 z 美元。用 0, 1, 2 表示小爱同学、小新同学和小明同学（0, 1, 2 为人的标号），上述交易可以表示为 [[0, 1, 10], [2, 0, 5]]。

    给定一群人之间的交易信息列表，计算能够还清所有债务的最小次数。

    注意：

    一次交易会以三元组 (x, y, z) 表示，并有 x ≠ y 且 z > 0。
    人的标号可能不是按顺序的，例如标号可能为 0, 1, 2 也可能为 0, 2, 6。
 

    示例 1：

    输入：
    [[0,1,10], [2,0,5]]

    输出：
    2

    解释：
    人 #0 给人 #1 共计 10 美元。
    人 #2 给人 #0 共计 5 美元。

    需要两次交易。一种方式是人 #1 分别给人 #0 和人 #2 各 5 美元。
 

    示例 2：

    输入：
    [[0,1,10], [1,0,1], [1,2,5], [2,0,5]]

    输出：
    1

    解释：
    人 #0 给人 #1 共计 10 美元。Person #0 gave person #1 $10.
    人 #1 给人 #0 共计 1 美元。Person #1 gave person #0 $1.
    人 #1 给人 #2 共计 5 美元。Person #1 gave person #2 $5.
    人 #2 给人 #0 共计 5 美元。Person #2 gave person #0 $5.

    因此，人 #1 需要给人 #0 共计 4 美元，所有的债务即可还清。

     * */
    // hard, plus, 2022/3/1
    // NP Hard, 回溯
    internal class P0465最优账单平衡
    {
        // ver1: 这就过啦？这么暴力= =
        //执行用时：164 ms, 在所有 C# 提交中击败了100.00%的用户
        //内存消耗：61.7 MB, 在所有 C# 提交中击败了100.00%的用户

        class BalanceStruct
        {
            public int m = 0;
        }
        Dictionary<int, BalanceStruct> dic = new();
        void AddBalance(int i, int m)
        {
            if (!dic.ContainsKey(i)) dic[i] = new();
            dic[i].m += m;
        }
        Dictionary<string, int> dp = new();
        int Dfs()
        {
            if (!dic.Values.Any(t => t.m != 0)) return 0;
            var key = string.Join(",", dic.Values.Where(t => t.m != 0).OrderBy(t => t.m).Select(t => t.m));
            if (dp.ContainsKey(key)) return dp[key];
            var debit = dic.Values.Where(t => t.m < 0).ToList();
            var credit = dic.Values.Where(t => t.m > 0).ToList();
            int minop = int.MaxValue;
            for (int i = 0; i < debit.Count; ++i)
                for (int j = 0; j < credit.Count; ++j)
                {
                    int m = Math.Min(-debit[i].m, credit[j].m);
                    debit[i].m += m;
                    credit[j].m -= m;
                    minop = Math.Min(minop, 1 + Dfs());
                    debit[i].m -= m;
                    credit[j].m += m;
                }
            return dp[key] = minop;
        }
        public int MinTransfers(int[][] transactions)
        {
            foreach (var trans in transactions)
            {
                AddBalance(trans[0], -trans[2]);
                AddBalance(trans[1], trans[2]);
            }
            int ans = 0;
            var list = dic.Values.ToList();
            for (int i = 0; i < list.Count - 1; ++i)
                for (int j = i + 1; j < list.Count; ++j)
                    if (list[i].m != 0 && list[i].m + list[j].m == 0)
                    {
                        list[i].m = list[j].m = 0;
                        ++ans;
                        break;
                    }
            ans += Dfs();
            return ans;
        }

        internal static void Run()
        {
            // [[0,1,10],[1,0,1],[1,2,5],[2,0,5]]
            var input = new int[][] { new int[] { 0, 1, 10 }, new int[] { 1, 0, 1 }, new int[] { 1, 2, 5 }, new int[] { 2, 0, 5 } };
            var sln = new P0465最优账单平衡();
            var ans = sln.MinTransfers(input);
            foreach (var kv in sln.dp)
                Console.WriteLine("{0} => {1}", kv.Key, kv.Value);
            Console.WriteLine("ans=" + ans);
        }
    }
}
