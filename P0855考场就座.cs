using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1
{
    // medium, 2022/12/30 Daily
    // rank: 2067
    // 区间排序（可能是写过的最长的中等题）
    public class ExamRoom
    {
        int N;
        SortedSet<(int, int)> sort = new(); // (-distance, index)
        SortedSet<Interval> ss = new();
        Dictionary<Interval, (int, int)> map = new();

        public ExamRoom(int n)
        {
            N = n;
            sort.Add((-n, 0));
            ss.Add(new() { start = 0, end = n - 1 });
        }

        public int Seat()
        {
            (int d, int i) = sort.Min;
            int assignedSeat = i;
            sort.Remove((d, i));
            if (ss.TryGetValue(new(i), out var iv)) 
            {
                ss.Remove(iv);
                map.Remove(iv);
                Interval left = new() { start = iv.start, end = i - 1 }, right = new() { start = i + 1, end = iv.end };
                if (left.Valid)
                {
                    ss.Add(left);
                    if (left.start == 0)
                    {
                        sort.Add(map[left] = (-(left.Count - 1), 0));
                    }
                    else
                    {
                        d = left.Count - 1 >> 1;
                        i = left.start + d;
                        sort.Add(map[left] = (-d, i));
                    }
                }
                if (right.Valid)
                {
                    ss.Add(right);
                    if (right.end == N - 1)
                    {
                        sort.Add(map[right] = (-(right.Count - 1), N - 1));
                    }
                    else
                    {
                        d = right.Count - 1 >> 1;
                        i = right.start + d;
                        sort.Add(map[right] = (-d, i));
                    }
                }
            }
            return assignedSeat;
        }

        public void Leave(int p)
        {
            Interval iv = new(p);
            if (p > 0)
            {
                if (ss.TryGetValue(new(p - 1), out var left))
                {
                    sort.Remove(map[left]);
                    ss.Remove(left);
                    map.Remove(left);
                    iv.start = left.start;
                }
            }
            if (p < N - 1)
            {
                if (ss.TryGetValue(new(p + 1), out var right))
                {
                    sort.Remove(map[right]);
                    ss.Remove(right);
                    map.Remove(right);
                    iv.end = right.end;
                }
            }

            int d = iv.Count - 1, i = 0;
            if (iv.start == 0)
                i = 0;
            else if (iv.end == N - 1)
                i = N - 1;
            else
            {
                d = iv.Count - 1 >> 1;
                i = iv.start + d;
            }
            ss.Add(iv);
            sort.Add(map[iv] = (-d, i));
        }
    }

    internal class P0855考场就座
    {
        internal static void Run()
        {
            ExamRoom t = new(4);
            Console.WriteLine(t.Seat());
            Console.WriteLine(t.Seat());
            Console.WriteLine(t.Seat());
            Console.WriteLine(t.Seat());
            t.Leave(1);
            t.Leave(3);
            Console.WriteLine(t.Seat());
        }
    }
}
