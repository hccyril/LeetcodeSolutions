using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1
{
    // 2022/01/27 US Daily 位运算
    // 进阶：你可以在 O(n) 的时间解决这个问题吗？// 题解意思是用TrieTree
    internal class P0421数组中两个数的最大异或值
    {
        // 位运算，二分，改了N版之后终于通过！！
        public int FindMaximumXOR(int[] nums)
        {
            nums = nums.Where(t => t != 0).OrderByDescending(t => t).Distinct().ToArray();
            if (!nums.Any()) return 0;
            else if (nums.Length <= 2) return nums.First() ^ nums.Last();
            int map = 1, n = -1;
            for (; n < 31 && map < nums.First(); map <<= 1, ++n) ; // 这里要加上n < 31，卡溢出太恶心
            (int i, int j) = FindOne(nums, 0, nums.Length - 1, n);
            return nums[i] ^ nums[j];
        }

        int FindMid(int[] nums, int start, int end, int n)
        {
            int map = 1 << n;
            if ((nums[start] & map) == (nums[end] & map))
            {
                return (nums[start] & map) != 0 ? -1 : -2;
            }
            else
            {
                int l = start, r = end;
                while (l < r)
                {
                    int mid = l + r + 1 >> 1; // 不是l + r >> 1，二分写来写去都不对= =
                    if ((nums[mid] & map) != 0) l = mid; // 是nums[mid]不是mid！这种低级错误能不能不要再犯
                    else r = mid - 1;
                }
                return l;
            }
        }

        (int i, int j) FindOne(int[] nums, int start, int end, int n)
        {
            if (n == 0 || start + 1 >= end) return (start, end);
            int mid = FindMid(nums, start, end, n);
            if (mid < 0)
                return FindOne(nums, start, end, n - 1);
            else
                return FindTwo(nums, start, mid, mid + 1, end, n - 1);
        }

        (int i, int j) FindTwo(int[] nums, int ls, int le, int rs, int re, int n)
        {
            if (n < 0) return (ls, rs);
            int i = -1, j = -1;
            int ml = FindMid(nums, ls, le, n), mr = FindMid(nums, rs, re, n);
            if ((ml >= 0 || ml == -1) && (mr >= 0 || mr == -2))
            {
                (i, j) = FindTwo(nums, ls, ml == -1 ? le : ml, mr == -2 ? rs : mr + 1, re, n - 1);
            }
            if ((ml >= 0 || ml == -2) && (mr >= 0 || mr == -1)) // 是if 不是else if !!!
            {
                (int i2, int j2) = FindTwo(nums, ml == -2 ? ls : ml + 1, le, rs, mr == -1 ? re : mr, n - 1);
                if (i < 0 || (nums[i] ^ nums[j]) < (nums[i2] ^ nums[j2]))
                {
                    i = i2; j = j2;
                }
            }
            if (i < 0)
            {
                return FindTwo(nums, ls, le, rs, re, n - 1);
            }
            return (i, j);
        }

        internal static void Run()
        {
            var sln = new P0421数组中两个数的最大异或值();
            int[] input1 = { 3, 10, 5, 25, 2, 8 };
            Console.WriteLine(sln.FindMaximumXOR(input1)); // pass

            int[] input2 = { 0 };
            Console.WriteLine(sln.FindMaximumXOR(input2)); // pass

            int[] input3 = { 2, 4 };
            Console.WriteLine(sln.FindMaximumXOR(input3)); // pass

            int[] input4 = { 8, 10, 2 };
            Console.WriteLine(sln.FindMaximumXOR(input4)); // wrong // fixed

            int[] input5 = { 14, 70, 53, 83, 49, 91, 36, 80, 92, 51, 66, 70 };
            Console.WriteLine(sln.FindMaximumXOR(input5)); // pass

            int[] input6 = { 2147483647, 2147483646, 2147483645 }; // TLE // fixed, 被卡溢出
            Console.WriteLine(sln.FindMaximumXOR(input6));

            int[] input7 = { 32, 18, 33, 42, 29, 20, 26, 36, 15, 46 }; // WA: output 60, expect 62
            Test(input7);
            Console.WriteLine(sln.FindMaximumXOR(input7));
        }

        private static void Test(int[] nums)
        {
            int a, b, max = 0;
            for (int i = 0; i < nums.Length; ++i)
                for (int j = i + 1; j < nums.Length; ++j)
                {
                    a = nums[i]; b = nums[j];
                    if ((a ^ b) > max)
                    {
                        max = a ^ b;
                        Console.WriteLine("{0} ^ {1} = {2}", a, b, max);
                    }
                }
        }
    }
}
