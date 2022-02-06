using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1
{
    class P0673最长递增子序列的个数
    {
        class NumStruct : IComparable<NumStruct>
        {
            public int num;
            public int cnt;

            public int CompareTo(NumStruct other) => num - other.num;
        }
        public int FindNumberOfLIS(int[] nums)
        {
            int si;
            List<List<NumStruct>> list = new List<List<NumStruct>>();
            list.Add(new List<NumStruct>());
            foreach (var n in nums)
            {
                var item = new NumStruct { num = n, cnt = 0 };
                int itemLen = 1;
                for (int len = list.Count - 1; len > 0; --len)
                {
                    si = list[len].BinarySearch(item);
                    if (si < 0) si = -si - 1;
                    if (si > 0)
                    {
                        itemLen = len + 1;
                        for (int i = si - 1; i >= 0; --i)
                        {
                            item.cnt += list[len][i].cnt;
                        }
                        break;
                    }
                }

                if (item.cnt == 0) item.cnt = 1;
                while (list.Count <= itemLen) list.Add(new List<NumStruct>());
                if (list[itemLen].Count > 0 && (si = list[itemLen].BinarySearch(item)) >= 0)
                {
                    list[itemLen][si].cnt += item.cnt;
                }
                else
                {
                    list[itemLen].Add(item);
                    list[itemLen].Sort();
                }
            }
            return list[list.Count - 1].Sum(t => t.cnt);
        }
    }
}
