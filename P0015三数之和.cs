using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1
{
    /*
     * 给你一个包含 n 个整数的数组 nums，判断 nums 中是否存在三个元素 a，b，c ，使得 a + b + c = 0 ？请你找出所有和为 0 且不重复的三元组。

注意：答案中不可以包含重复的三元组。

示例 1：

输入：nums = [-1,0,1,2,-1,-4]
输出：[[-1,-1,2],[-1,0,1]]
示例 2：

输入：nums = []
输出：[]
示例 3：

输入：nums = [0]
输出：[]
 

提示：

0 <= nums.length <= 3000
-10^5 <= nums[i] <= 10^5
通过次数531,157提交次数1,644,113

来源：力扣（LeetCode）
链接：https://leetcode-cn.com/problems/3sum
著作权归领扣网络所有。商业转载请联系官方授权，非商业转载请注明出处。
     * */
    class P0015三数之和
    {
        /**
         * 最终搜索k的时候必须使用binary search，否则超时
         * */
        public int BS(IList<int> nums, int target, int start, int end)
        {
            if (end - start < 1) return -1;
            if (end - start == 1)
                return target == nums[start] ? start : -1;
            int mid = start + (end - start) / 2;
            if (nums[mid] == target) return mid;
            if (target < nums[mid])
                return BS(nums, target, start, mid);
            else
                return BS(nums, target, mid + 1, end);
        }

        public IList<IList<int>> ThreeSum(int[] nums)
        {
            IList<IList<int>> resultList = new List<IList<int>>();

            List<int> numsList = new List<int>(nums);
            numsList.Sort();

            int a = -1000001, b = a;

            for (int i = 0; i < numsList.Count - 2 && numsList[i] <= 0; ++i)
            {
                if (numsList[i] == a) continue;
                a = numsList[i];
                for (int j = i + 1; j < numsList.Count - 1; ++j)
                {
                    if (b == numsList[j]) continue;
                    b = numsList[j];
                    int c = 0 - a - b;
                    if (c < 0) break;

                    if (BS(numsList, c, j + 1, numsList.Count) >= 0)
                        resultList.Add(new List<int>()
                            {
                                a,b,c
                            });

                }
            }

            return resultList;
        }

        // ======================================================
        // 以下是超时的解法
        // ======================================================

        //HashSet<string> hs = new HashSet<string>();
        //private void TryAdd(IList<IList<int>> list, List<int> newSet)
        //{
        //    string key = string.Join('~', newSet);
        //    if (!hs.Contains(key))
        //    {
        //        hs.Add(key);
        //        list.Add(newSet);
        //    }
        //    //if (list.Count > 0)
        //    //{
        //    //    var item = list[list.Count - 1];
        //    //    if (item[0] == newSet[0] && item[1] == newSet[1] && item[2] == newSet[2])
        //    //        return;
        //    //}
        //    //list.Add(newSet);
        //}

        //public IList<IList<int>> ThreeSum(int[] nums)
        //{
        //    IList<IList<int>> resultList = new List<IList<int>>();

        //    List<int> numsList = new List<int>(nums);
        //    numsList.Sort();

        //    int a = -1000001, b = a;

        //    for (int i = 0; i < numsList.Count - 2 && numsList[i] <= 0; ++i)
        //    {
        //        if (numsList[i] == a) continue;
        //        a = numsList[i];
        //        for (int j = i + 1; j < numsList.Count - 1; ++j)
        //        {
        //            if (b == numsList[j]) continue;
        //            b = numsList[j];
        //            int c = 0 - a - b;
        //            if (c < 0) break;

        //            for (int k = j + 1; k < numsList.Count; ++k)
        //            {
        //                if (numsList[k] == c)
        //                {
        //                    //TryAdd(resultList, new List<int>()
        //                    resultList.Add(new List<int>()
        //                    {
        //                        a,b,c
        //                    });
        //                    break;
        //                }
        //                else if (numsList[k] > c) break;
        //            }
        //        }

        //    }

        //    return resultList;
        //}
    }
}
