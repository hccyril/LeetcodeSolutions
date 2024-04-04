using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1;

// hard, 2024/2/25 US Daily (连续两天没做过的hard= =)
// 质数 / 最大公约数 + 并查集
internal class P2709最大公约数遍历
{
    const int MAXN = 100000;
    public bool CanTraverseAllPairs(int[] nums)
    {
        MAXN.FactorTable();
        int n = nums.Length;
        UnionFind uni = new(n);
        Dictionary<int, int> di = new();

        for (int i = 0; i < n; ++i)
        {
            foreach ((int f, _) in nums[i].EnumFactors())
            {
                if (di.TryGetValue(f, out var i0))
                {
                    uni.Union(i0, i);
                }
                else
                {
                    di[f] = uni[i];
                }
            }
        }

        return uni.GroupCount() == 1;
    }
}
