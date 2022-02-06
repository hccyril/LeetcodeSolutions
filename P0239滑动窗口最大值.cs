using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1
{
    // Hard // 2021/09/12
    // 剑指 Offer 59 - I. 滑动窗口的最大值（https://leetcode-cn.com/problems/hua-dong-chuang-kou-de-zui-da-zhi-lcof/）
    class P0239滑动窗口最大值
    {
        // 2021/12/25 HashHeap version
        public int[] MaxSlidingWindow_Heap(int[] nums, int k)
        {
            if (!nums.Any()) return Array.Empty<int>(); // 剑指offer里面多了一个空的测试用例
            HashHeap hp = new(true);
            int[] ans = new int[nums.Length - k + 1];
            int count = 0;
            for (int i = 0; i < k; ++i) hp.Push(i, nums[i]);
            ans[count++] = hp.Head;
            for (int head = 0, tail = k; tail < nums.Length; ++head, ++tail)
            {
                hp.Remove(head);
                hp.Push(tail, nums[tail]);
                ans[count++] = hp.Head;
            }
            return ans;
        }

        // 以下是0912旧版本

        class WinStruct : IComparable<WinStruct>
        {
            public int start;
            public int end;
            public int val;
            public bool invalid => end < start;
            public int CompareTo(WinStruct other) => end - other.end;
        }
        public int[] MaxSlidingWindow(int[] nums, int k)
        {
            List<WinStruct> list = new List<WinStruct>();
            WinStruct wstemp = new WinStruct();
            int slideEnd = nums.Length - k;
            for (int i = 0; i < nums.Length; ++i)
            {
                int start = i + 1 - k; if (start < 0) start = 0;
                int end = i; if (end > slideEnd) end = slideEnd; // i + k - 1; if (end >= nums.Length) end = nums.Length - 1;
                int val = nums[i];

                // 二分查找确定开始搜索的位置
                wstemp.end = start; 
                int si = list.BinarySearch(wstemp);
                if (si < 0) si = -si - 1;

                for (int j = list.Count - 1; j >= si && list[j].end >= start; --j)
                {
                    if (list[j].val < val)
                    {
                        list[j].end = start - 1;
                        if (list[j].invalid) list.RemoveAt(j);
                    }
                    else
                    {
                        start = list[j].end + 1;
                    }
                }

                if (end >= start)
                    list.Add(new WinStruct
                    {
                        start = start,
                        end = end,
                        val = val
                    });

                //list.Sort();
            }

//#if DEBUG
//            foreach (var ws in list)
//                Console.WriteLine($"{ws.start} {ws.end} {ws.val}");
//#endif

            int[] ans = new int[nums.Length - k + 1];
            foreach (var ws in list)
            {
                for (int i = ws.start; i <= ws.end && i < ans.Length; ++i)
                    ans[i] = ws.val;
            }
            return ans;
        }

        internal static void Run()
        {
            //{ 7, 2, 4 };
            //{ 1, 3, -1, -3, 5, 3, 6, 7 };
            int[] input = TestCase0239.input;
            int k = TestCase0239.k;

            var output = new P0239滑动窗口最大值().MaxSlidingWindow(input, k);
            Console.WriteLine(output.Length);
            //Console.WriteLine(string.Join(' ', output));

            //List<WinStruct> list = new List<WinStruct>();
            //for (int i = 0; i < 5; ++i) list.Add(new WinStruct());
            //list[0].end = 10;
            //list[1].end = 20;
            //list[2].end = 30;
            //list[3].end = 40;
            //list[4].end = 50;
            //WinStruct ws = new WinStruct(); 
            //ws.end = 25; // -3
            //Console.WriteLine(list.BinarySearch(ws));
            //ws.end = 35; // -4
            //Console.WriteLine(list.BinarySearch(ws));
            //ws.end = 55; // -6
            //Console.WriteLine(list.BinarySearch(ws));
            //ws.end = 20; // 1
            //Console.WriteLine(list.BinarySearch(ws));
        }
    }
}
