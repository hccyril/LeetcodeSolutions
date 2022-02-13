using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1
{
    // medium, c++用map（红黑树），这里用自己实现的红黑树
    internal class P0220存在重复元素III
    {
        class NumStruct : IComparable<NumStruct>
        {
            public long num;
            public int idx;
            public int CompareTo(NumStruct other) 
                => num == other.num ? idx.CompareTo(other.idx) : num.CompareTo(other.num);
        }
        public bool ContainsNearbyAlmostDuplicate(int[] nums, int k, int t)
        {
            TreeList<NumStruct> rbt = new();
            int i = 0;
            long lt = t;
            foreach (int n in nums)
            {
                NumStruct ns = new NumStruct { num = n, idx = i },
                    begin = new NumStruct { num = n - lt, idx = 0 },
                    end = new NumStruct { num = n + lt, idx = int.MaxValue };
                foreach (var it in rbt.Range(begin, end))
                    if (i - it.idx <= k) return true;
                rbt.Add(ns);
                ++i;
            }
            return false;
        }
    }
}
