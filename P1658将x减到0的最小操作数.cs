using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1
{
    // medium, 2022/6/11 US Daily
    // rank: 1817 （这题顶得一道普通的困难题）
    // ver1 BFS but TIMEOUT
    // ver2 线性规划 but 提交了4次才过
    internal class P1658将x减到0的最小操作数
    {
        public int MinOperations(int[] nums, int x)
        {
            int[] sort = new int[nums.Length];

            int cnt = -1;

            for (int i = 0; i < nums.Length; i++)
            {
                sort[i] = i == 0 ? nums[i] : nums[i] + sort[i - 1];
                // 第二个WA！！[5,2,3,1,1] 5
                if (sort[i] == x && cnt < 0) cnt = i + 1;
            }
            int sum = 0;
            for (int r = 1; r < nums.Length; ++r)
            {
                sum += nums[nums.Length - r]; if (sum > x) break;
                if (sum == x)
                {   // 没加这个条件，一个WA！[1, 1, 4, 2, 3] 5
                    cnt = Math.Min(cnt == -1 ? nums.Length : cnt, r);
                }
                else
                {
                    int i = Array.BinarySearch(sort, 0, nums.Length - r, x - sum);
                    if (i >= 0) cnt = Math.Min(cnt == -1 ? nums.Length : cnt, i + r + 1);
                }
            }
            return cnt;
        }

        // ver1 BFS TLE
        public int MinOperations_ver1(int[] nums, int x)
        {
            if (nums.Sum() < x) return -1;
            Queue<(int, int, int)> qu = new();
            qu.Enqueue((0, 0, 0));

            bool Test(int l, int r, bool addLeft, int sum)
            {
                sum += addLeft ? nums[l++] : nums[nums.Length - ++r];
                if (sum == x) return true;
                if (sum < x) qu.Enqueue((l, r, sum));
                return false;
            }

            while (qu.Any())
            {
                (int i, int j, int sum) = qu.Dequeue();
                if (i + j < nums.Length)
                {
                    if (j == 0) if (Test(i, j, true, sum)) return i + j + 1;
                    if (Test(i, j, false, sum)) return i + j + 1;
                }
            }

            return -1;
        }

        internal static void Run()
        {
            // ans: 52465, time: 86641ms
            int x = 261951936;
            int[] input = Common.ReadArray(1658);

            var sln = new P1658将x减到0的最小操作数();
            int ans = sln.MinOperations(input, x);
            Console.WriteLine(nameof(P1658将x减到0的最小操作数) + ": " + ans);
        }
    }
}
