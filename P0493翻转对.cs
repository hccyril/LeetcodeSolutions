using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1
{
    // hard, 2022/2/1
    // 与 315的区别只有 (逆序的定义改成了a > b * 2)，因此尝试用MergeSort的方法解决
    internal class P0493翻转对
    {
        class NumStruct : IComparable<NumStruct>
        {
            public int num, ind, cnt;

            public int CompareTo(NumStruct ns2)
            {
                // 加上ind保证num相同时，target一定排在最前面
                return ns2.num == num ? ind - ns2.ind : ns2.num > num ? 1 : -1;
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

            // 统计part2里有多少个元素比n1少，全部加进cnt里面
            foreach (var n1 in part1)
            {
                int target_num = n1.num / 2; // n1.num >> 1; // 不能用右移，例如-3 >> 1 == -2
                if (!(n1.num > 0 && (n1.num & 1) != 0)) --target_num;
                var target = new NumStruct() { num = target_num };
                int pos = Array.BinarySearch(part2, target);
                if (pos < 0) pos = -pos - 1;
                // 另一个方法是修改compare方法，则不需要以下代码进行调整了
                //else while (pos > 0 && part2[pos - 1].num == target_num) --pos; // 被BinarySearch坑过无数回了！！
                
                // DEBUG
                //if (n1.ind == 968) // 8, expect 9
                //{
                //    Console.WriteLine("==========");
                //    Console.WriteLine("Index: {0} Num: {1} Cnt: {2}", n1.ind, n1.num, n1.cnt);
                //    Console.WriteLine(string.Join(" ", part2.Select(t => t.num)));
                //    Console.WriteLine("Cnt({0}) += Len({1}) - Pos({2}) == {3}\r\n", n1.cnt, part2.Length, pos, n1.cnt + part2.Length - pos);
                //}
                n1.cnt += part2.Length - pos;
            }

            // 合
            NumStruct[] arr = new NumStruct[part1.Length + part2.Length];
            int i1 = 0, i2 = 0;
            while (i1 < part1.Length && i2 < part2.Length)
            {
                NumStruct n1 = part1[i1];
                NumStruct n2 = part2[i2];
                if (n1.CompareTo(n2) < 0)
                {
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
        public int ReversePairs(int[] nums)
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
            return list.Select(t => t.cnt).Sum();
        }

        int Test(int[] nums)
        {
            List<NumStruct> list = new(), list_compare = new();
            for (int i = 0; i < nums.Length; ++i)
            {
                NumStruct data = new();
                data.num = nums[i];
                data.ind = i;
                list.Add(data);

                NumStruct data2 = new();
                data2.num = nums[i];
                data2.ind = i;
                list_compare.Add(data2);
            }
            //// build compare
            //for (int i = 0; i < list_compare.Count; ++i)
            //    for (int j = i + 1; j < list_compare.Count; ++j)
            //        if (list_compare[i].num > list_compare[j].num * 2)
            //            list_compare[i].cnt++;

            NumStruct[] sorted_arr = MergeSort(list, 0, nums.Length - 1);

            //// compare
            //foreach (var ns in list.OrderBy(t => t.ind))
            //    if (ns.cnt != list_compare[ns.ind].cnt)
            //    {
            //        Console.WriteLine("Index: {0} MyCnt: {1} Comp: {2}", ns.ind, ns.cnt, list_compare[ns.ind].cnt);
            //    }

            return list_compare.Select(t => t.cnt).Sum();
        }

        internal static void Run()
        {
            var sln = new P0493翻转对();
            //int[] nums = { -3, -2 };
            int[] nums = Common.ReadArray(493); // 115502 expected: 124430
            //int result = sln.Test(nums);
            int result = sln.ReversePairs(nums);
            Console.WriteLine(result);
        }
    }
}
