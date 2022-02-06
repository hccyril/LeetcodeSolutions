using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1
{
    // medium, c++用map（红黑树），这里用sortedlist // TODO
    internal class P0220存在重复元素III
    {
        public bool ContainsNearbyAlmostDuplicate(int[] nums, int k, int t)
        {
            SortedList<long, int> srt = new();
            throw new NotImplementedException();
            //int i = 0;
            //foreach (int n in nums) 
            //{
            //    long lb = (long)n - t, ub = (long)n + t;
            //    srt.
            //    for (auto begin = rbt.lower_bound(lb), end = rbt.upper_bound(ub); begin != end; ++begin)
            //        if (i - begin->second <= k) return true;
            //    rbt[n] = i++;
            //}
            //return false;

            // C# 未完成，以下是c++的
            /*
            map<Long, int> rbt; // map内部是红黑树实现
            int i = 0;
            Long lt = static_cast<Long>(t);
            for (const int& n: nums) {
            Long ln = static_cast<Long>(n), lb = ln - t, ub = ln + t;
            for (auto begin = rbt.lower_bound(lb), end = rbt.upper_bound(ub); begin != end; ++begin)
            if (i - begin->second <= k) return true;
            rbt[n] = i++;
            }
            return false; 
            */
        }
    }
}
