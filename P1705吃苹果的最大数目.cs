using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1
{
    // medium
    internal class P1705吃苹果的最大数目
    {
        class AppleDay
        {
            public int start, end, count;
        }

        // ver 2 - 贪心+优先队列（堆）（参考了官方题解才写出来）
        public int EatenApples(int[] apples, int[] days)
        {
            int count = 0;
            Heap<AppleDay> hp = new((a, b) => a.end < b.end);
            for (int i = 0; i < apples.Length || hp.Any(); i++)
            {
                if (i < apples.Length && apples[i] > 0)
                    hp.Push(new AppleDay
                    {
                        start = i,
                        end = i + days[i],
                        count = apples[i]
                    });
                while (hp.Any() && hp.Head.end <= i) hp.Pop();
                if (hp.Any())
                {
                    count++; 
                    hp.Head.count--;
                    if (hp.Head.count == 0)
                        hp.Pop();
                }
            }
            return count;
        }

        // version 1 - failed
        //public int EatenApples(int[] apples, int[] days)
        //{
        //    var arr = (from i in Enumerable.Range(0, apples.Length)
        //               where apples[i] > 0
        //               let ed = i + days[i]
        //               orderby ed, i
        //               select new AppleDay
        //               {
        //                   start = i,
        //                   end = ed,
        //                   count = Math.Min(apples[i], days[i])
        //               }).ToArray();
        //    int start = -1, end = -1, count = 0;
        //    foreach (var ad in arr)
        //    {
        //        if (ad.start >= end)
        //        {
        //            count += end - start;
        //            start = end = ad.start;
        //        }
        //        if (ad.end > end)
        //        {
        //            end += Math.Min(ad.end - end, ad.count);
        //        }
        //    }
        //    if (end > start) count += end - start;
        //    return count;
        //}


        // bad code
        //int count = 0, start = -1, end = -1;
        //for (int i = 0; i < apples.Length; ++i)
        //{
        //    if (apples[i] > 0)
        //    {
        //        if (start < 0) start = i;
        //        end = Math.Max(end, end + Math.Min(apples[i], i + days[i] - end));
        //    }
        //    else if (i >= end)
        //    {
        //        count += end - start;
        //        start = end = -1;
        //    }
        //}
        //if (end > 0) count += end - start;
        //return count;


        internal static void Run()
        {
            int[] apples = { 1, 2, 3, 5, 2 };
            int[] days = { 3, 2, 1, 4, 2 };
            Console.WriteLine(new P1705吃苹果的最大数目().EatenApples(apples, days));
        }
    }
}
