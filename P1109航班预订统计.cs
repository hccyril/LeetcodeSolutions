using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1
{
    class P1109航班预订统计
    {
        public int[] CorpFlightBookings(int[][] bookings, int n)
        {
            SortedList<int, int> sorts = new SortedList<int, int>();
            foreach (var book in bookings)
            {
                int start = book[0] - 1, end = book[1];
                if (!sorts.ContainsKey(start)) sorts.Add(start, 0);
                if (!sorts.ContainsKey(end)) sorts.Add(end, 0);
                sorts[start] += book[2];
                sorts[end] -= book[2];
            }
            int[] ans = new int[n];
            int i = 0;
            foreach (var j in sorts.Keys)
            {
                while (i < j && i + 1 < n)
                    ans[++i] = ans[i - 1];
                if (j < n) ans[j] += sorts[j];
            }
            return ans;
        }

        // heap
        //public int[] CorpFlightBookings(int[][] bookings, int n)
        //{
        //    Heap<int[]> hp = new Heap<int[]>((a, b) => a[0] < b[0]);
        //    foreach (var book in bookings)
        //    {
        //        int[] start = { book[0], book[2] };
        //        int[] end = { book[1] + 1, -book[2] };
        //        hp.Push(start);
        //        hp.Push(end);
        //    }
        //    int[] ans = new int[n];
        //    int i = 1, j = 0, cost = 0;
        //    while (hp.Any())
        //    {
        //        var arr = hp.Pop();
        //        j = arr[0]; cost = arr[1];
        //        while (i < j && i < n)
        //        {
        //            ans[i] = ans[i - 1];
        //            ++i;
        //        }
        //        if (j <= n) ans[j - 1] += cost;
        //    }
        //    return ans;
        //}
    }
}
