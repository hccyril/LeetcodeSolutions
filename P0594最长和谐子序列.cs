using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1
{
    // easy, 只是为了测试SuperHeap的使用
    // 2021/11/20
    // 2022/01/23 修改Update方法
    class P0594最长和谐子序列
    {
        public int FindLHS(int[] nums)
        {
            HashHeap hhp = new HashHeap(true);
            Dictionary<int, int> lowDic = new Dictionary<int, int>(), // has lower but no upper
                upDic = new Dictionary<int, int>(); // has upper but no lower
            for (int i = 0; i < nums.Length; ++i)
            {
                int n = nums[i], lw = n - 1, up = n + 1;
               
                // (n-1, n)组合
                if (hhp.ContainsKey(lw))
                {
                    hhp.Update(lw, 1);
                    //(int index, int val) = hhp.Get(lw);
                    //hhp.UpdateAt(index, val + 1);
                }
                else if (lowDic.ContainsKey(lw))
                {
                    hhp.Push(lw, lowDic[lw] + 1);
                    lowDic.Remove(lw);
                }
                else
                {
                    int count = upDic.ContainsKey(n) ? upDic[n] + 1 : 1;
                    upDic[n] = count;
                }
                // (n, n+1)组合
                if (hhp.ContainsKey(n))
                {
                    hhp.Update(n, 1);
                    //(int index, int val) = hhp.Get(n);
                    //hhp.UpdateAt(index, val + 1);
                }
                else if (upDic.ContainsKey(up))
                {
                    hhp.Push(n, upDic[up] + 1);
                    upDic.Remove(up);
                }
                else
                {
                    int count = lowDic.ContainsKey(n) ? lowDic[n] + 1 : 1;
                    lowDic[n] = count;
                }
            }
            return hhp.Any() ? hhp.Head : 0;
        }

        /* 第一次提交（java）
         * 
        public int findLHS(int[] nums) {
            int max = 0, lower = -1, count = 0, prev = -2147483648;
            Arrays.sort(nums);
            for (int n : nums) {
                if (n == prev) {
                    count++; 
                } else {
                    if (n == prev + 1) lower = count;
                    else lower = -1;
                    prev = n; count = 1;
                }
                if (lower > 0) 
                        max = Math.max(lower + count, max);
            }
            return max;
        }
         * */
    }
}
