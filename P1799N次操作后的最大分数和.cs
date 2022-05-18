using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1
{
    // hard, 2022/5/2
    // 记忆化回溯
    internal class P1799N次操作后的最大分数和
    {
        int N;
        Dictionary<int, int> dic = new();
        int[] arr;
        int DpDfs(int map, int n)
        {
            if (map == 0) return 0;
            else if (dic.ContainsKey(map)) return dic[map];
            int score = 0;
            for (int i = 0; i < N - 1; ++i)
                if ((map & (1 << i)) != 0)
                    for (int j = i + 1; j < N; ++j)
                        if ((map & (1 << j)) != 0)
                        {
                            int nm = map ^ (1 << i) ^ (1 << j);
                            score = Math.Max(score, n * MathEX.Gcd(arr[i], arr[j]) + DpDfs(nm, n - 1));
                        }
            return dic[map] = score;
        }
        public int MaxScore(int[] nums)
        {
            N = nums.Length;
            arr = nums;
            return DpDfs((1 << N) - 1, N >> 1);
        }
    }
}
