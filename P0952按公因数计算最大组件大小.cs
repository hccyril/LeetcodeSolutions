using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1
{
    // hard
    // 2021/11/24 just official
    // 2022/7/30 Daily - UnionFind
    internal class P0952按公因数计算最大组件大小
    {
        public const string 相似题 = "1998. 数组的最大公因数排序"; // 1998 于C++实现

        static int[] f = MathEX.FactorTable(100000);

        public int LargestComponentSize(int[] nums)
        {
            UnionFind uni = new(nums.Length);
            Dictionary<int, int> dic = new();
            for (int i = 0; i < nums.Length; i++)
            {
                int n = nums[i];
                while (n >= 2)
                {
                    int p = f[n];
                    if (dic.ContainsKey(p))
                    {
                        uni.Union(dic[p], i);
                        dic[p] = uni.Find(i);
                    }
                    else
                        dic[p] = uni.Find(i);
                    n /= p;
                }
            }
            return Enumerable.Range(0, nums.Length).GroupBy(i => uni.Find(i)).Max(g => g.Count());
        }
    }
}
