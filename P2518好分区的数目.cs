using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Intrinsics.Arm;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1;

// 难题挑战2023/12/23 rating 2414
// 完成 time left 4:04
// 简单转换之后计数dp，C#需要处理mod比较麻烦
internal class P2518好分区的数目
{
    /**
     * Python - 不考虑取模问题，快速求解
    def countPartitions(self, nums: List[int], k: int) -> int:
        if sum(nums) < 2 * k:
            return 0
        n = len(nums)
        di = defaultdict(lambda: 0)
        di[0] = 1
        for x in nums:
            dp = defaultdict(lambda: 0)
            for s, c in di.items():
                dp[s] += c
                if s + x < k:
                    dp[s + x] += c
            di = dp
        return (2 ** n - sum(di.values()) * 2) % 1000000007
     * */
    public int CountPartitions(int[] nums, int k)
    {
        bool lessThan2k = true;
        int sum = 0;
        // 注意直接Sum()会整数溢出，所以只能手写循环
        foreach (int x in nums)
        {
            sum += x;
            if (sum >= (k << 1))
            {
                lessThan2k = false;
                break;
            }
        }
        if (lessThan2k) return 0;

        int n = nums.Length;
        Dictionary<int, int> di = new();
        di[0] = 1;
        foreach (int x in nums)
        {
            Dictionary<int, int> dp = new();
            foreach ((int s, int c) in di) {
                if (!dp.TryAdd(s, c))
                    dp[s] = dp[s].Add(c);
                int t = s + x;
                if (t < k) 
                {
                    if (!dp.TryAdd(t, c))
                        dp[t] = dp[t].Add(c);
                }
            }
            di = dp;
        }

        int ans = 1;
        for (int i = 0; i < n; ++i) ans = ans.Add(ans);
        int b = 0;
        foreach ((_, int v) in di)
            b = b.Add(v);
        b = b.Add(b);

        return ans.Sub(b); //(2 ** n - sum(di.values()) * 2) % 1000000007
    }
}
