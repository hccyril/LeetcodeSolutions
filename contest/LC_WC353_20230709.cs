using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1.contest
{
    // 简单题的比赛，但是十分之粗心！三道题都在第一次提交时WA了
    internal class LC_WC353_20230709
    {
        #region Problem A
        public int TheMaximumAchievableX(int num, int t)
        {
            return num + t * 2;
        }
        #endregion

        #region Problem B
        public int MaximumJumps(int[] nums, int target)
        {
            int[] dp = new int[nums.Length];
            Array.Fill(dp, -1);
            dp[0] = 0;
            for (int i = 1; i < nums.Length; ++i)
                for (int j = 0; j < i; ++j)
                    if (Math.Abs(nums[i] - nums[j]) <= target && dp[j] >= 0)
                        dp[i] = Math.Max(dp[i], dp[j] + 1);
            return dp[^1] > 0 ? dp[^1] : -1;
        }
        #endregion

        #region Problem C
        public int MaxNonDecreasingLength(int[] nums1, int[] nums2)
        {
            int n = nums1.Length, maxn = 1;
            int[][] nums = { nums1, nums2 };
            int[,] dp = new int[n, 2];
            dp[0, 0] = dp[0, 1] = 1;
            for (int i = 1; i < n; ++i)
            {
                dp[i, 0] = dp[i, 1] = 1;
                for (int j = 0; j < 2; ++j)
                    for (int k = 0; k < 2; ++k)
                    {
                        if (nums[j][i] >= nums[k][i - 1])
                            dp[i, j] = Math.Max(dp[i, j], dp[i - 1, k] + 1);
                    }
                maxn = Math.Max(maxn, Math.Max(dp[i, 0], dp[i, 1]));
            }
            return maxn;
        }
        #endregion

        #region Problem D
        public bool CheckArray(int[] nums, int k)
        {
            int n = nums.Length;
            int[] a = new int[n + 1];
            int inc = 0;
            for (int i = 0; i < n; ++i)
            {
                inc += a[i];
                if (inc > nums[i]) return false;
                else if (inc < nums[i])
                {
                    int d = nums[i] - inc;
                    a[i] += d;
                    inc += d;
                    if (i + k > n) return false;
                    else a[i + k] -= d;
                }
            }
            return true;
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
            LC_WC353_20230709 sln = new();

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
            int[] a = { 60, 72, 87, 89, 63, 52, 64, 62, 31, 37, 57, 83, 98, 94, 92, 77, 94, 91, 87, 100, 91, 91, 50, 26 };
            int k = 4;
            bool ans = CheckArray(a, k);
            Console.WriteLine("ans=" + ans);
            return 0;
        }

        int RunTestE()
        {
            return 0;
        }
        #endregion
    }
}
