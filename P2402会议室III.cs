using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1;

// hard, 2024/2/18 US Daily
// 规划问题（优先队列）
internal class P2402会议室III
{
    /**
     * 超时，同样算法换成python就过了，以下python代码
```Python3
class Solution:
    def mostBooked(self, n: int, meetings: List[List[int]]) -> int:
        rooms = list(range(n))
        heapq.heapify(rooms)
        books = []
        used = [0] * n
        for start, finish in sorted(meetings):
            while books and books[0][0] <= start:
                _, id = heapq.heappop(books)
                heapq.heappush(rooms, id)
            if rooms:
                id = heapq.heappop(rooms)
                used[id] += 1
                heapq.heappush(books, (finish, id))
            else:
                end, id = books[0]
                used[id] += 1
                heapq.heapreplace(books, (finish - start + end, id))
        print(used)
        return max(range(n), key = lambda i: used[i])
```
    */    


    // 超时，可能是平台问题-__-
    public int MostBooked(int n, int[][] meetings)
    {
        SortedSet<int> rooms = new(Enumerable.Range(0, n));
        SortedSet<(long end, int id)> books = new();
        int[] used = new int[n];
        foreach (var meeting in meetings.OrderBy(t => t[0]))
        {
            long start = meeting[0], duration = meeting[1] - meeting[0];
            while (books.Any() && books.Min().end <= start)
            {
                (_, int id) = books.Min();
                books.Remove(books.Min());
                rooms.Add(id);
            }
            if (rooms.Any())
            {
                int id = rooms.Min();
                rooms.Remove(id);
                ++used[id];
                books.Add((start + duration, id));
            }
            else
            {
                (long end, int id) = books.Min();
                books.Remove(books.Min());
                ++used[id];
                books.Add((end + duration, id));
            }
        }
        return Enumerable.Range(0, n).Min(i => (-used[i], i)).i;
    }

    // WA - 疑似又卡int溢出了！
    //public int MostBooked(int n, int[][] meetings)
    //{
    //    SortedSet<int> rooms = new(Enumerable.Range(0, n));
    //    SortedSet<(int end, int id)> books = new(); 
    //    int[] used = new int[n];
    //    foreach (var meeting in meetings.OrderBy(t => t[0]))
    //    {
    //        int start = meeting[0], duration = meeting[1] - meeting[0];
    //        while (books.Any() && books.Min().end <= start)
    //        {
    //            (_, int id) = books.Min();
    //            books.Remove(books.Min());
    //            rooms.Add(id);
    //        }
    //        if (rooms.Any())
    //        {
    //            int id = rooms.Min();
    //            rooms.Remove(id);
    //            ++used[id];
    //            books.Add((start + duration, id));
    //        }
    //        else
    //        {
    //            (int end, int id) = books.Min();
    //            books.Remove(books.Min());
    //            ++used[id];
    //            books.Add((end + duration, id));
    //        }
    //    }
    //    return Enumerable.Range(0, n).Min(i => (-used[i], i)).i;
    //}
}
