using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1
{
    /*
     * 47. 全排列 II
     * https://leetcode-cn.com/problems/permutations-ii/
给定一个可包含重复数字的序列 nums ，按任意顺序 返回所有不重复的全排列。

 

示例 1：

输入：nums = [1,1,2]
输出：
[[1,1,2],
 [1,2,1],
 [2,1,1]]
示例 2：

输入：nums = [1,2,3]
输出：[[1,2,3],[1,3,2],[2,1,3],[2,3,1],[3,1,2],[3,2,1]]
 

提示：

1 <= nums.length <= 8
-10 <= nums[i] <= 10
     * */
    class P0047全排列II
    {
        //// 以下是字符的全排列（剑指Offer 38）
        //List<string> resultList = new List<string>();

        //char[] arr;
        //char[] list;
        //int bits = 0;

        //void Rec(int i, int bits)
        //{
        //    if (i == arr.Length)
        //    {
        //        resultList.Add(new string(list));
        //        return;
        //    }
        //    char c = '\n';
        //    for (int k = 0; k < arr.Length; ++k)
        //    {
        //        int b = 1 << k;
        //        if ((bits & b) == 0)
        //        {
        //            if (arr[k] != c)
        //            {
        //                c = list[i] = arr[k];
        //                Rec(i + 1, bits | b);
        //            }
        //        }
        //    }
        //}

        //public string[] Permutation(string s)
        //{
        //    List<char> charList = new List<char>();
        //    foreach (var c in s)
        //    {
        //        //if (!charList.Contains(c)) {
        //        charList.Add(c);
        //        //}
        //    }
        //    charList.Sort();
        //    arr = charList.ToArray();
        //    list = new char[arr.Length];

        //    Rec(0, 0);
        //    return resultList.ToArray();
        //}

        IList<IList<int>> resultList = new List<IList<int>>();

        int[] arr;
        int[] list;

        void Rec(int i, int bits)
        {
            if (i == arr.Length)
            {
                resultList.Add(new List<int>(list));
                return;
            }
            int c = -111111;
            for (int k = 0; k < arr.Length; ++k)
            {
                int b = 1 << k;
                if ((bits & b) == 0)
                {
                    if (arr[k] != c)
                    {
                        c = list[i] = arr[k];
                        Rec(i + 1, bits | b);
                    }
                }
            }
        }

        public IList<IList<int>> PermuteUnique(int[] nums)
        {
            List<int> intList = new List<int>(nums);
            intList.Sort();
            arr = intList.ToArray();
            list = new int[arr.Length];

            Rec(0, 0);
            return resultList.ToArray();
        }
    }
}
