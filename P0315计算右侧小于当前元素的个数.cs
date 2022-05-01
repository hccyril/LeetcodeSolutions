using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1
{
    /* first solve: 2021/6/27
     * review: 2021/9/13 (java)
     * review: 2022/2/1 mergesort + P0493 《Reverse Pairs》
     * 315. Count of Smaller Numbers After Self
     * https://leetcode.com/problems/count-of-smaller-numbers-after-self/
     * 
     * 遗留问题：
     * [Solved]1. 用BinarySearchTree怎么解？
     * 2.1 Segment Tree到底是什么？
     * [Solved]2.2 Fenwick Tree? 
     * [Solved]3. MergeSort如何解？
     * */
    // 相似题目：《剑指 Offer 51. 数组中的逆序对》https://leetcode-cn.com/problems/shu-zu-zhong-de-ni-xu-dui-lcof/comments/
    // 相似题目：493 Reverse Pairs (逆序的定义改成了a > b * 2)
    class P0315计算右侧小于当前元素的个数
    {
        #region ----- ver6 - 树状数组（Fenwick Tree) --------
        // C# 272ms, 100% 暂时所有方法中最快 (2022/3/8)
        public IList<int> CountSmaller_6(int[] nums)
        {
            Fenwick fwt = new(20002);
            List<int> list = new();
            foreach (int n in nums.Reverse())
            {
                int i = n + 10001;
                list.Add(fwt.Sum(i - 1));
                fwt.Update(i, 1);
            }
            list.Reverse();
            return list;
        }
        #endregion // ver6

        #region ----- ver5 - 自己写的红黑树版本 --------

        public List<int> CountSmaller_MyRedBlackTree(int[] nums)
        {
            //// debug test
            //TreeList<NumStruct> test = new();
            //test.Add(new NumStruct() { num = 1 });
            //test.Add(new NumStruct() { num = 6 });
            //test.Add(new NumStruct() { num = 2 });


            TreeSet<(int n, int i)> tree = new((a, b) => a.n == b.n ? a.i.CompareTo(b.i) : b.n.CompareTo(a.n));
            for (int i = nums.Length - 1; i >= 0; --i)
            {
                int ind = tree.Add((nums[i], i));
                nums[i] = tree.Count - 1 - ind;
            }
            return nums.ToList();
        }
        #endregion // -------------- end of ver5 -----------

        #region ----- ver4 - 自己写的merge sort 版本 -------
        class NumStruct : IComparable<NumStruct> 
        {
            public int num, ind, cnt;

            public int CompareTo(NumStruct ns2)
            {
                return num > ns2.num ? -1 : 1;
                //return ns2.num > num ? 1 : ns2.num < num ? -1 : 0;
                //return ns2.num - num; // 注意这样的写法有溢出风险
            }
        }
        NumStruct[] MergeSort(List<NumStruct> list, int start, int end)
        {
            if (start > end) return new NumStruct[0];
            if (start == end) return new NumStruct[] { list[start] };

            // 分
            int mid = start + (end - start) / 2;
            NumStruct[] part1 = MergeSort(list, start, mid);
            NumStruct[] part2 = MergeSort(list, mid + 1, end);

            // 合
            NumStruct[] arr = new NumStruct[part1.Length + part2.Length];
            int i1 = 0, i2 = 0;
            while (i1 < part1.Length && i2 < part2.Length)
            {
                NumStruct n1 = part1[i1];
                NumStruct n2 = part2[i2];
                if (n1.CompareTo(n2) < 0)
                {
                    /**
                     * 直接统计part2里有多少个元素比n1少，全部加进cnt里面
                     */
                    n1.cnt += part2.Length - i2;
                    arr[i1 + i2] = n1;
                    i1++;
                }
                else
                {
                    arr[i1 + i2] = n2;
                    i2++;
                }
            }
            if (i1 < part1.Length)
            {
                for (; i1 < part1.Length; ++i1)
                {
                    arr[i1 + i2] = part1[i1];
                }
            }
            if (i2 < part2.Length)
            {
                for (; i2 < part2.Length; ++i2)
                {
                    arr[i1 + i2] = part2[i2];
                }
            }
            return arr;
        }
        public List<int> CountSmaller_MyMergeSort(int[] nums)
        {
            List<NumStruct> list = new();
            for (int i = 0; i < nums.Length; ++i)
            {
                NumStruct data = new();
                data.num = nums[i];
                data.ind = i;
                list.Add(data);
            }
            NumStruct[] sorted_arr = MergeSort(list, 0, nums.Length - 1);
            return list.OrderBy(t => t.ind).Select(t => t.cnt).ToList();
            // 如果是《剑指 Offer 51. 数组中的逆序对》则返回以下这句
            //return list.Select(t => t.cnt).Sum();
        }
        #endregion // -------- end of ver4 ---------------

        #region ----- ver3 加强版 - 使用 Span ------
        class MyCompare : IComparer<int>
        {
            public int Compare(int x, int y) => x > y ? -1 : 1;
        }
        public IList<int> CountSmaller_3plus(int[] nums)
        {
            MyCompare mycom = new();

            Span<int> sorted = stackalloc int[nums.Length];
            int count = 1; 
            sorted[0] = nums[nums.Length - 1];
            nums[nums.Length - 1] = 0;
            for (int i = nums.Length - 2; i >= 0; --i)
            {
                int n = nums[i];
                int index = ~sorted[..count].BinarySearch(n, mycom);
                if (index < count)
                    sorted.Slice(index, count - index).CopyTo(sorted.Slice(index + 1, count - index));
                sorted[index] = n;
                nums[i] = count++ - index;
            }
            return new List<int>(nums);
        }
        #endregion 
        // ver 3
        int BinarySearch(List<int[]> list, int n, int start, int end)
        {
            if (start == end) return start;
            int mid = (start + end) >> 1;
            if (list[mid][0] > n)
                return BinarySearch(list, n, mid + 1, end);
            else
                return BinarySearch(list, n, start, mid);
        }

        int BinarySearch3(List<int> list, int n, int start, int end)
        {
            if (start == end) return start;
            int mid = (start + end) >> 1;
            if (list[mid] < n)
                return BinarySearch3(list, n, mid + 1, end);
            else
                return BinarySearch3(list, n, start, mid);
        }

        // ver3: sort - 终于过了！但只击败了7.69%。。。。
        // 最后一个case大约800ms
        public IList<int> CountSmaller3(int[] nums)
        {
            List<int> list = new List<int>();
            for (int i = nums.Length - 1; i >= 0; --i)
            {
                int n = nums[i];
                int index = BinarySearch3(list, n, 0, list.Count);
                list.Insert(index, n);
                nums[i] = index;
            }
            return new List<int>(nums);
        }

        // ver2: modified dp - still timeout
        IList<int> CountSmaller2(int[] nums)
        {
            List<int[]> list = new List<int[]>();
            for (int i = 0; i < nums.Length; ++i)
            {
                int n = nums[i]; //, k = 0;
                int[] arr = { n, i };
                nums[i] = 0;

                // use binary search 
                int k = BinarySearch(list, n, 0, list.Count);
                for (int j = 0; j < k; ++j) nums[list[j][1]]++;
                //while (k < list.Count && list[k][0] > n)
                //    nums[list[k++][1]]++;
                
                list.Insert(k, arr);
            }
            return new List<int>(nums);
        }

        // ver1: timeout
        IList<int> CountSmaller1(int[] nums)
        {
            IList<int> list = new List<int>();
            int[] dp = new int[20001];
            for (int i = nums.Length - 1; i >= 0; --i)
            {
                int n = nums[i] + 10000;
                for (int j = n + 1; j <= 20000; ++dp[j++]) ;
                list.Insert(0, dp[n]);
            }
            return list;
        }

        public IList<int> CountSmaller(int[] nums)
        {
            //if (nums[0] == -10000)
                return CountSmaller2(nums);
            //else
            //    return CountSmaller1(nums);
        }


        internal static void Run()
        {
            var sln = new P0315计算右侧小于当前元素的个数();

            //int[] input = { 5, 2, 6, 1 };
            //var list = sln.CountSmaller_3plus(input);
            //Console.WriteLine("右侧小于当前元素的个数: " + string.Join(" ", list));

            int[][] big = Common.ReadInput<int[][]>(315);
            int i = 0, t0 = System.Environment.TickCount, t1 = 0;
            foreach (var nums in big)
            {
                //sln.CountSmaller_MyRedBlackTree(nums); // ~600ms
                //sln.CountSmaller3(nums); // ~800ms
                sln.CountSmaller_3plus(nums); // ~50ms !!!
                t1 = System.Environment.TickCount;
                Console.WriteLine("TestCase {0} done in {1} ms", ++i, t1 - t0);
                t0 = t1;
            }

            //List<int> numList = new List<int>();
            //for (int n = -10000; n <= 10000; ++n)
            //{
            //    foreach (var i in Enumerable.Range(1, 5))
            //    {
            //        numList.Add(n);
            //    }
            //}
            //var list = new P0315计算右侧小于当前元素的个数().CountSmaller(numList.ToArray());
            //Console.WriteLine("done");
        }
    }

    //// 这是参考了别人的 (MergeSort)
    //public class Solution0315_MergeSort
    //{
    //    public IList<int> CountSmaller(int[] nums)
    //    {
    //        int[] counts = new int[nums.Length];

    //        KeyValuePair<int, int>[] numsIndexes = new KeyValuePair<int, int>[nums.Length];

    //        //keys are the indexes, values are the nums
    //        for (int i = 0; i < nums.Length; i++)
    //        {
    //            numsIndexes[i] = new KeyValuePair<int, int>(i, nums[i]);
    //        }

    //        FindCounts(numsIndexes, counts, 0, nums.Length - 1);

    //        return counts;
    //    }

    //    private void FindCounts(KeyValuePair<int, int>[] nums, int[] counts, int startIndex, int endIndex)
    //    {
    //        if (startIndex < endIndex)
    //        {
    //            int middleIndex = startIndex + (endIndex - startIndex) / 2;

    //            FindCounts(nums, counts, startIndex, middleIndex);
    //            FindCounts(nums, counts, middleIndex + 1, endIndex);

    //            CountHelper(nums, counts, startIndex, middleIndex, endIndex);
    //        }
    //    }

    //    private void CountHelper(KeyValuePair<int, int>[] nums, int[] counts, int startIndex, int middleIndex, int endIndex)
    //    {
    //        //create 2 aux arrays
    //        int leftSize = middleIndex - startIndex + 1;
    //        int rightSize = endIndex - middleIndex;

    //        KeyValuePair<int, int>[] left = new KeyValuePair<int, int>[leftSize];
    //        KeyValuePair<int, int>[] right = new KeyValuePair<int, int>[rightSize];

    //        Array.Copy(nums, startIndex, left, 0, leftSize);
    //        Array.Copy(nums, middleIndex + 1, right, 0, rightSize);

    //        //merge sort the 2 sub arrays and track counts
    //        int leftIndex = 0, rightIndex = 0;
    //        int mergedSubArrayIndex = startIndex;
    //        int smallerNums = 0; //how many numbers to the right are bigger than us

    //        while (leftIndex < leftSize && rightIndex < rightSize)
    //        {
    //            if (left[leftIndex].Value <= right[rightIndex].Value) //grab item from left
    //            {
    //                counts[left[leftIndex].Key] += smallerNums;
    //                PerformMerge(left, nums, ref leftIndex, ref mergedSubArrayIndex);
    //            }
    //            else //grab item from right
    //            {
    //                smallerNums++;
    //                PerformMerge(right, nums, ref rightIndex, ref mergedSubArrayIndex);
    //            }
    //        }

    //        //grab remaining from left
    //        while (leftIndex < leftSize)
    //        {
    //            counts[left[leftIndex].Key] += smallerNums;
    //            PerformMerge(left, nums, ref leftIndex, ref mergedSubArrayIndex);
    //        }

    //        //grab remaining from right
    //        while (rightIndex < rightSize)
    //        {
    //            PerformMerge(right, nums, ref rightIndex, ref mergedSubArrayIndex);
    //        }
    //    }

    //    private void PerformMerge(KeyValuePair<int, int>[] src, KeyValuePair<int, int>[] dst, ref int srcIndex, ref int dstIndex)
    //    {
    //        dst[dstIndex] = src[srcIndex];
    //        srcIndex++;
    //        dstIndex++;
    //    }
    //}
}
