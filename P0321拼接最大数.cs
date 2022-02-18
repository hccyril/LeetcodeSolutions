using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1
{
    // hard, 相当难
    // 2022/2/15 单调栈
    internal class P0321拼接最大数
    {
        // ver5 - 加上记忆化回溯，担心overflow或者TLE
        /** 终于过了！吓死我= =
         * Runtime: 1500 ms, faster than 14.29% of C# online submissions for Create Maximum Number.
         * Memory Usage: 299.9 MB, less than 28.57% of C# online submissions for Create Maximum Number.
         * */
        Stack<int> BuildStack(int[] nums, int i)
        {
            Stack<int> stk = new();
            int n = -1;
            for (; i < nums.Length; ++i)
                if (nums[i] > n)
                {
                    n = nums[i];
                    stk.Push(i);
                }
            return stk;
        }
        Dictionary<int, string> dic = new();
        int Key(int i1, int i2, int k) => k << 18 | i1 << 9 | i2;
        string DpDfs(int[] nums1, int[] nums2, int k, int i1, int i2)
        {
            if (k == 0) return "";
            int key = Key(i1, i2, k);
            if (dic.ContainsKey(key)) return dic[key];

            //string log = $"k={k} i1={i1} i2={i2}";

            Stack<int> st1 = BuildStack(nums1, i1), st2 = BuildStack(nums2, i2);
            List<int> list = new();
            int r1 = nums1.Length - i1, r2 = nums2.Length - i2;
            //i1 = i2 = 0xFFFF;
            //i1 = st1.Any() ? 0xFFFF : nums1.Length;
            //i2 = st2.Any() ? 0xFFFF : nums2.Length;
            bool canPick1 = false, canPick2 = false;
            string s = null;

            while (st1.Any())
            {
                i1 = st1.Pop();
                if (canPick1 = nums1.Length - i1 + r2 >= k) break;
            }
            while (st2.Any())
            {
                i2 = st2.Pop();
                if (canPick2 = nums2.Length - i2 + r1 >= k) break;
            }
            // 原代码，已经写到晕了- -
            //while (!(canPick1 = nums1.Length - i1 + r2 >= k && st1.Any()) && st1.Any()) i1 = st1.Pop();
            //while (!(canPick2 = nums2.Length - i2 + r1 >= k && st2.Any()) && st2.Any()) i2 = st2.Pop();
            
            if (canPick1 && canPick2 && nums1[i1] != nums2[i2])
                canPick1 = !(canPick2 = nums2[i2] > nums1[i1]);

            if (canPick1)
            {
                s = nums1[i1].ToString() + DpDfs(nums1, nums2, k - 1, i1 + 1, nums2.Length - r2);
                // 下标算错= =
                //s = nums1[i1].ToString() + DpDfs(nums1, nums2, k - 1, i1 + 1, i2 /*这里不对*/);
            }
            if (canPick2)
            {
                string s2 = nums2[i2].ToString() + DpDfs(nums1, nums2, k - 1, nums1.Length - r1, i2 + 1);
                //string s2 = nums2[i2].ToString() + DpDfs(nums1, nums2, k - 1, i1, i2 + 1);
                if (s2.CompareTo(s) > 0) s = s2;
            }

            //Console.WriteLine($"{log} => {s}");

            return dic[key] = s;
        }
        public int[] MaxNumber(int[] nums1, int[] nums2, int k)
            => DpDfs(nums1, nums2, k, 0, 0).Select(c => c - '0').ToArray();

        // ver4 仍然出错！这题有毒！！Input: nums1 = [6,7], nums2 = [6,0,4], k = 5 Output: [6,7,6,0,4]
        //Stack<int> BuildStack(int[] nums, Stack<int> stk = null, int i = 0)
        //{
        //    if (stk == null) stk = new(); else stk.Clear();
        //    int n = -1; 
        //    for (; i < nums.Length; ++i)
        //        if (nums[i] > n)
        //        {
        //            n = nums[i];
        //            stk.Push(i);
        //        }
        //    return stk;
        //}
        //// n1接r2[], n2接r1[], 比谁更大
        //bool Compare(int n1, int n2, int[] nums1, int r1, int[] nums2, int r2)
        //{
        //    if (n1 != n2) return n1 > n2;
        //    int i1 = nums1.Length - r1, i2 = nums2.Length - r2;
        //    for (; i1 < nums1.Length && i2 < nums2.Length && nums1[i1] == nums2[i2]; ++i1, ++i2) ;
        //    return i2 < nums2.Length && (i1 >= nums1.Length || nums2[i2] > nums1[i1]);
        //}
        //public int[] MaxNumber(int[] nums1, int[] nums2, int k)
        //{
        //    Stack<int> st1 = BuildStack(nums1), st2 = BuildStack(nums2);
        //    List<int> list = new();
        //    int r1 = nums1.Length, r2 = nums2.Length, i1 = 0xFFFF, i2 = i1;
        //    bool canPick1 = false, canPick2 = false;
        //    while (k > 0)
        //    {
        //        while (!(canPick1 = nums1.Length - i1 + r2 >= k) && st1.Any()) i1 = st1.Pop();
        //        while (!(canPick2 = nums2.Length - i2 + r1 >= k) && st2.Any()) i2 = st2.Pop();
        //        if (canPick2 && (!canPick1 || Compare(nums2[i2], nums1[i1], nums2, r2, nums1, r1))) canPick1 = false;
        //        //else canPick2 = false;
        //        if (canPick1)
        //        {
        //            list.Add(nums1[i1]); --k;
        //            st1 = BuildStack(nums1, st1, i1 + 1);
        //            r1 = nums1.Length - 1 - i1;  i1 = 0xFFFF;
        //        }
        //        else
        //        {
        //            list.Add(nums2[i2]); --k;
        //            st2 = BuildStack(nums2, st2, i2 + 1);
        //            r2 = nums2.Length - 1 - i2;  i2 = 0xFFFF;
        //        }
        //    }
        //    return list.ToArray();
        //}

        // ver3 - Compare方法写错！！注意拼接时使用的不应该是i1 i2指针，而应该是r1 r2
        //bool Compare(int[] nums1, int i1, int[] nums2, int i2)
        //{
        //    if (nums1[i1] != nums2[i2]) return nums1[i1] > nums2[i2];
        //    for (; i1 < nums1.Length && i2 < nums2.Length && nums1[i1] == nums2[i2]; ++i1, ++i2) ;
        //    return i2 < nums2.Length && (i1 >= nums1.Length || nums2[i2] > nums1[i1]);
        //}

        // ver2 - 考虑不周，WA: [8,9] [3,9] 3, 出现了相同的9，但必须先选后面的9不是前面的

        //public int[] MaxNumber(int[] nums1, int[] nums2, int k)
        //{
        //    Stack<(int, int)> st1 = BuildStack(nums1), st2 = BuildStack(nums2);
        //    List<int> list = new();
        //    int r1 = nums1.Length, r2 = nums2.Length, i1 = 0xFFFF, i2 = i1, n1 = -1, n2 = -1;
        //    bool canPick1 = false, canPick2 = false;
        //    while (k > 0)
        //    {
        //        while (!(canPick1 = nums1.Length - i1 + r2 >= k) && st1.Any()) (i1, n1) = st1.Pop();
        //        while (!(canPick2 = nums2.Length - i2 + r1 >= k) && st2.Any()) (i2, n2) = st2.Pop();

        /** ## 在这里出错了，不应该直接n2 > n1 **/
        //        if (canPick2 && (!canPick1 || n2 > n1)) canPick1 = false;

        //        //else canPick2 = false;
        //        if (canPick1)
        //        {
        //            list.Add(n1); --k;
        //            st1 = BuildStack(nums1, st1, i1 + 1);
        //            r1 = nums1.Length - 1 - i1; i1 = 0xFFFF;
        //        }
        //        else
        //        {
        //            list.Add(n2); --k;
        //            st2 = BuildStack(nums2, st2, i2 + 1);
        //            r2 = nums2.Length - 1 - i2; i2 = 0xFFFF;
        //        }
        //    }
        //    return list.ToArray();
        //}

        // ver1: dp, 想多了
        //class NumStruct : IComparable<NumStruct>
        //{
        //    public bool first;
        //    public int idx, num;

        //    public int CompareTo(NumStruct other)
        //    {
        //        if (first == other.first)
        //        {
        //            return idx.CompareTo(other.idx);
        //        }
        //        else
        //        {
        //            return other.num.CompareTo(num);
        //        }
        //    }
        //}

        internal static void Run()
        {
            //int[] nums1 = { 6, 7 }, nums2 = { 6, 0, 4 };
            //int k = 5;
            int[] nums1 = { 8, 9 }, nums2 = { 3, 9 };
            int k = 3;
            var sln = new P0321拼接最大数();
            var ans = sln.MaxNumber(nums1, nums2, k);
            Console.WriteLine(string.Join(" ", ans));


            //string UnKey(int key) => $"i1={key >> 9 & ((1 << 9) - 1)} i2={key & ((1 << 9) - 1)} k={key >> 18}";
            //foreach (var kv in sln.dic)
            //    Console.WriteLine("{0} => {1}", UnKey(kv.Key), kv.Value);

        }
    }
}
