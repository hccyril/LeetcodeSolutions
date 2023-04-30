using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1.contest
{
    // 最失败的一次比赛，值得好好反省及永远铭记
    // 在7分钟做完前两题的情况下，居然C D两题都没做出来
    // C，为什么没想到是用二分法，怎么这么失败啊
    // D，没有考虑到工具人的情况，WA之后直接崩溃了，其实当时就差这一步，本质就是比较多分类讨论的暴力模拟题
    internal class LC_WC331_20230205
    {
        #region Problem A

        //    def pickGifts(self, gifts: List[int], k: int) -> int:
        //        for _ in range(k):
        //            gifts.sort()
        //            gifts[-1] = int(gifts[-1] ** 0.5)
        //        return sum(gifts)

        #endregion

        #region Problem B
        //def vowelStrings(self, words: List[str], queries: List[List[int]]) -> List[int]:
        //    aeiou = set(['a','e','i','o','u'])
        //    psm = [0] * len(words)
        //    for i, w in enumerate(words):
        //        if w[0] in aeiou and w[-1] in aeiou:
        //            psm[i] += 1
        //        if i > 0:
        //            psm[i] += psm[i - 1]
        //    return [psm[j] if i == 0 else psm[j] - psm[i - 1] for i, j in queries]

        #endregion

        #region Problem C

        // 这题的二分有想过，但是DP为什么会想不到啊！！
        // 再看了评论发现不用DP用贪心也可以（待确认），如果是这样真的觉得自己实在是笨！！
        public int MinCapability(int[] nums, int k)
        {
            int n = nums.Length;

            int left = nums.Min(), right = nums.Max();
            while (left < right)
            {
                int m = left + right >> 1;

                // check: DP: dp[i]: 前i个房屋偷取不超过m的最大数量
                int dp = 0, dp2 = 0;
                foreach (int h in nums)
                    if (h > m) dp2 = dp;
                    else (dp, dp2) = (Math.Max(dp, 1 + dp2), dp);

                if (dp >= k)
                    right = m;
                else
                    left = m + 1;
            }
            return left;
        }

        //public int MinCapability(int[] nums, int k) {
        //    if (nums.Length == 1) return nums[0];
        //    SortedSet<(int, int)> dp0 = new(), dp1 = new();
        //    dp0.Add((nums[0], 0));
        //    dp1.Add((Math.Min(nums[0], nums[1]), 1).);


        //    return -1;
        //}
        #endregion

        #region Problem D
        /**
        class Solution:
            def minCost(self, a1: List[int], a2: List[int]) -> int:
                # quick check
                cnt = Counter(a1) + Counter(a2)
                for c in cnt.values():
                    if c & 1:
                        return -1

                n, p = len(a1), 0
                a1.sort()
                a2.sort()
                mi = min(min(a1), min(a2))
                i1, i2 = 0, 0
                li = []
                while i1 < n and i2 < n:
                    while i1 < n and i2 < n and a1[i1] == a2[i2]:
                        i1 += 1
                        i2 += 1
                    if i1 < n and i2 < n and a1[i1] < a2[i2]:
                        li.append(a1[i1])
                        i1 += 2
                    elif i1 < n and i2 < n and a1[i1] > a2[i2]:
                        li.append(a2[i2])
                        i2 += 2
                while i1 < n:
                    li.append(a1[i1])
                    i1 += 2
                while i2 < n:
                    li.append(a2[i2])
                    i2 += 2

                cost = 0
                i, j = 0, len(li) - 1
                while i < j:
                    if li[i] < mi * 2:
                        cost += li[i]
                    else:
                        cost += mi * 2
                    i += 1
                    j -= 1
                
                return cost
         * */
        #endregion

        #region Problem E
        public int SolveE(int x)
        {
            return x;
        }
        #endregion

        #region Run Test
        internal static int Run()
        {
            char p = 'D';
            LC_WC331_20230205 sln = new();

            return p switch
            {
                'A' => sln.RunTestA(),
                'B' => sln.RunTestB(),
                'C' => sln.RunTestC(),
                'D' => sln.RunTestD(),
                'E' => sln.RunTestE(),
                _ => -1
            };
        }

        int RunTestA()
        {
            return 0;
        }

        int RunTestB()
        {
            return 0;
        }

        int RunTestC()
        {
            return 0;
        }

        int RunTestD()
        {
            return 20230205;
        }

        int RunTestE()
        {
            return 0;
        }
        #endregion
    }
}
