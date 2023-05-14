using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1.contest
{
    /*
     * # 2023/2/12 最后一分钟AC
# 排名	用户名	得分	完成时间 	题目1 (3)	题目2 (4)	题目3 (5)	题目4 (6)
# 272 / 4547	呱呱编程实验室 	18	1:49:04	 0:03:07	 0:21:09 * 1	 1:29:04	 1:17:53 * 3
     * */
    internal class LC_WC332_20230212
    {
        #region Problem A
        /*
        class SolutionA:
            def findTheArrayConcVal(self, nums: List[int]) -> int:
                ans = 0
                while nums:
                    if len(nums) == 1:
                        ans += nums[0]
                    else:
                        ans += int(str(nums[0]) + str(nums[-1]))
                    nums = [] if len(nums) == 1 else nums[1:-1]
                return ans
         * */
        #endregion

        #region Problem B
        /*
        class SolutionB:
            def countFairPairs(self, nums: List[int], lower: int, upper: int) -> int:
                n, ans = len(nums), 0
                nums.sort()
                print(nums)
                i, j1, j2 = 0, n - 1, n - 1
                while i < n - 1:
                    while j2 > i and nums[i] + nums[j2] > upper:
                        j2 -= 1
                    if j1 <= i:
                        j1 = i + 1
                    while j1 > i + 1 and nums[i] + nums[j1 - 1] >= lower:
                        j1 -= 1
                    print(i, j1, j2)
                    if j1 > i and j2 > i and j2 >= j1 and nums[i] + nums[j1] >= lower and nums[i] + nums[j2] <= upper:
                        ans += j2 - j1 + 1
                    #else:
                    #   break
                    i += 1

                return ans
         * */
        #endregion

        #region Problem C
        /* C题最后一分钟暴力过了就离谱
        class SolutionC:
            def substringXorQueries(self, s: str, queries: List[List[int]]) -> List[List[int]]:
                a = [bin(b ^ a)[2:] for a, b in queries]
                ans = []
                for t in a:
                    i = s.find(t)
                    if i >= 0:
                        ans.append([i, i + len(t) - 1])
                    else:
                        ans.append([-1, -1])
                return ans
         * */
        #endregion

        #region Problem D
        // 很快想到解法，但做来做去就是错= =
        /* 附：测试用例
        "abacaba"
        "bzaa"
        "cde"
        "xyz"
        "abecdebe"
        "eaebceae"
        "acdedcdbabecdbebda"
        "bbecddb"
         * 
         * */
        public int MinimumScore(string s, string t)
        {
            int n = t.Length;
            int[] dp = new int[n], dr = new int[n];
            Array.Fill(dp, -1);
            Array.Fill(dr, -1);
            int j = 0, left = -1, right = n;
            for (int i = 0; i < n; ++i)
            {
                while (j < s.Length && s[j] != t[i]) j++;
                if (j < s.Length && s[j] == t[i])
                {
                    dp[i] = j++;
                    left = Math.Max(left, i);
                }
                else
                    break;
            }
            j = s.Length - 1;

            for (int i = n - 1; i >= 0; --i)
            {
                while (j >= 0 && s[j] != t[i]) j--;
                if (j >= 0 && s[j] == t[i])
                {
                    dr[i] = j--;
                    right = i;
                }
                else
                    break;
            }

            Console.WriteLine(string.Join(" ", dp));
            Console.WriteLine(string.Join(" ", dr));

            Console.WriteLine($"{left} {right}");

            int cnt = Math.Max(left + 1, n - right);
            for (int i = 0; i <= left; ++i)
            {
                j = Math.Max(right, i + 1);
                if (j < n)
                {
                    int ind = Array.BinarySearch(dr, j, n - j, dp[i] + 1);
                    if (ind < 0) ind = ~ind;
                    if (ind > i && ind < n)
                    {
                        cnt = Math.Max(cnt, n - ind + i + 1);
                        Console.WriteLine($"cnt: {i} {ind} {n - ind + i + 1} {cnt}");
                    }
                }

            }

            return n - cnt;
        }

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
            LC_WC332_20230212 sln = new();

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
            return 0;
        }

        int RunTestE()
        {
            return 0;
        }
        #endregion
    }
}
