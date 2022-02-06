using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1
{
    class P0915分割数组
    {
        public class ListHeap
        {
            bool Compare(int i, int j)
            {
                return GetAt(i)[0] < GetAt(j)[0];
            }

            int[] swap;

            private List<int[]> _list = new List<int[]>();
            private List<int[]> _arr = new List<int[]>();

            public int[] GetAt(int index) => _list[index - 1];
            public int Count => _list.Count;

            public int Head => _list[0][0];
            public void Swap(int i, int j)
            {
                swap = _list[i - 1];
                _list[i - 1] = _list[j - 1];
                _list[i - 1][1] = i;
                _list[j - 1] = swap;
                _list[j - 1][1] = j;
            }

            public void Push(int n)
            {
                int[] item = { n, 0 };
                _arr.Add(item);
                _list.Add(item);
                int i = item[1] = Count;
                int inext = i >> 1;
                while (inext > 0 && Compare(i, inext))
                {
                    Swap(i, inext);
                    i = inext; inext = i >> 1;
                }
            }

            public void RemoveAt(int index)
            {
                index = _arr[index][1];
                if (index == Count)
                {
                    _list.RemoveAt(index - 1);
                    return;
                }

                Swap(index, Count);
                _list.RemoveAt(_list.Count - 1);

                int i = index, inext = i >> 1;

                // swap up
                while (inext > 0 && Compare(i, inext))
                {
                    Swap(i, inext);
                    i = inext; inext = i >> 1;
                }

                // swap down
                if (i == index) // i如果没有改变说明没有swap up，如果有swap up就不需要swap down了
                {
                    while (true)
                    {
                        inext = i << 1;
                        if (inext <= Count)
                        {
                            if (inext < Count && Compare(inext + 1, inext)) inext++;
                            if (Compare(inext, i))
                            {
                                Swap(i, inext);
                                i = inext;
                                continue;
                            }
                        }
                        break;
                    }
                }
            }
        }

        public int PartitionDisjoint(int[] nums)
        {
            Heap<int> left = new Heap<int>((a, b) => a > b);
            ListHeap right = new ListHeap();
            foreach (var n in nums) right.Push(n);
            for (int i = 0; i < nums.Length - 1; ++i)
            {
                left.Push(nums[i]);
                right.RemoveAt(i);
                if (left.Head <= right.Head) return left.Count;
            }
            return -1;
        }

        /// <summary>
        /// 看了评论才发现维护最大最小值即可
        /// </summary>
        public int PartitionDisjoint2(int[] nums)
        {
            int[] left = new int[nums.Length];
            for (int i = 0; i < nums.Length; ++i)
                left[i] = i == 0 ? nums[i] : Math.Max(left[i - 1], nums[i]);
            int[] right = new int[nums.Length];
            for (int i = nums.Length - 1; i >= 0; --i)
                right[i] = i == nums.Length - 1 ? nums[i] : Math.Min(right[i + 1], nums[i]);
            for (int i = 0; i < nums.Length; ++i)
                if (left[i] <= right[i])
                    return i + 1;
            return -1;
        }

        public static void Run()
        {
            int[] input = { 90, 47, 69, 10, 43, 92, 31, 73, 61, 97 };
            int output = new P0915分割数组().PartitionDisjoint(input);
            Console.WriteLine(output);
        }
    }
}
