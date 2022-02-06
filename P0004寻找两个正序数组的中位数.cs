using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1
{
    /* Hard
     * 给定两个大小分别为 m 和 n 的正序（从小到大）数组 nums1 和 nums2。请你找出并返回这两个正序数组的 中位数 。
     * 进阶：你能设计一个时间复杂度为 O(log (m+n)) 的算法解决此问题吗？
    */
    // solved 2021/6/3
    public class P0004寻找两个正序数组的中位数
    {
        public int BinarySearch(ArraySegment<int> arrs, int t, int offset, int count)
        {
            // if (count == 1) return arrs[offset];
            if (count <= 0) return offset;
            int compare_index = offset + count / 2;

            if (t < arrs[compare_index])
            {
                return BinarySearch(arrs, t, offset, compare_index - offset);
            }
            else
            {
                return BinarySearch(arrs, t, compare_index + 1, offset + count - compare_index - 1);
            }
        }

        public bool IsEven(int n)
        {
            return (n & 1) == 0;
        }

        public double Mid(ArraySegment<int> arrs)
        {
            if (IsEven(arrs.Count))
            {
                int i = arrs.Count / 2;
                return (arrs[i] + arrs[i - 1]) / 2.0;
            }
            return arrs[arrs.Count / 2];
        }

        public double Calc(ArraySegment<int> arrs, int index, int t)
        {
            return (arrs[index] + arrs[index - t]) / 2.0;
        }

        public ArraySegment<int> MakeAs(ArraySegment<int> arrs, int offset, int count)
        {
            return new ArraySegment<int>(arrs.Array, arrs.Offset + offset, count);
        }

        public double FindMid(ArraySegment<int> as1, ArraySegment<int> as2, int t, int e)
        {
            if (as1.Count < as2.Count)
            {
                var temp = as1;
                as1 = as2;
                as2 = temp;
            }
            if (as2.Count == 0)
            {
                return Calc(as1, t, e);
            }
            else if (as1.Count == 1 && as2.Count == 1)
            {
                int[] list = new int[2];
                list[0] = as1[0] < as2[0] ? as1[0] : as2[0];
                list[1] = as1[0] + as2[0] - list[0];
                return Calc(new ArraySegment<int>(list), t, e);
            }
            int index1 = as1.Count / 2;
            int mid_of_1 = as1[index1];
            int index2 = BinarySearch(as2, mid_of_1, 0, as2.Count);
            int left_count = index1 + index2;
            int right_count = as1.Count + as2.Count - left_count;

            if (left_count > t)
            {
                // search left
                return FindMid(MakeAs(as1, 0, index1), MakeAs(as2, 0, index2), t, e);
            }
            else
            {
                if (left_count == t && e == 1)
                {
                    var d1 = FindMid(MakeAs(as1, 0, index1), MakeAs(as2, 0, index2), t - 1, 0);
                    var d2 = FindMid(MakeAs(as1, index1, as1.Count - index1), MakeAs(as2, index2, as2.Count - index2), t - left_count, 0);
                    return (d1 + d2) / 2.0;
                }
                // search right
                return FindMid(MakeAs(as1, index1, as1.Count - index1), MakeAs(as2, index2, as2.Count - index2), t - left_count, e);
            }
        }

        public double FindMedianSortedArrays(int[] nums1, int[] nums2)
        {
            ArraySegment<int> as1 = new ArraySegment<int>(nums1);
            ArraySegment<int> as2 = new ArraySegment<int>(nums2);
            int t = (as1.Count + as2.Count) / 2;
            int e = IsEven(as1.Count + as2.Count) ? 1 : 0;
            return FindMid(as1, as2, t, e);
        }
    }
}
