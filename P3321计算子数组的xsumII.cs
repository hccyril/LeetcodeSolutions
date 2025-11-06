using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1;

// hard, 2025/11/5 Daily
// rating 2598
// Priority Queue, Sliding Window
internal class P3321计算子数组的xsumII
{
    // 版本4: 之前怀疑是优先队列的懒删除过多导致超时，但在版本3时发现了个bug(没有处理好c=0的数据)并优化了
    // [AC]结论：优先队列懒删除可行，而且比有序集合效率高很多
    public long[] FindXSum(int[] nums, int k, int x)
    {
        int n = nums.Length;
        Counter<int> cn = new();
        PriorityQueue<(int t, int c), (int, int)> pq1 = new(), pq2 = new();
        HashSet<int> hs = new();
        long sm = 0;

        long Update1()
        {
            //Console.Write("U1 ");
            for (int i = 0; i < k; ++i)
                ++cn[nums[i]];
            foreach ((int t, int c) in cn)
            {
                if (hs.Count < x)
                {
                    hs.Add(t);
                    sm += (long)t * c;
                    pq1.Enqueue((t, c), (c, t));
                }
                else
                {
                    (int tr, int cr) = pq1.EnqueueDequeue((t, c), (c, t));
                    if (t != tr)
                    {
                        sm += (long)t * c - (long)tr * cr;
                        hs.Remove(tr);
                        hs.Add(t);
                    }
                    pq2.Enqueue((tr, cr), (-cr, -tr));
                }
            }
            // Console.WriteLine(" => " + sm);
            return sm;
        }

        void Update2(int t, int inc = 1)
        {
            // Console.WriteLine("U2 " + t + " + " + inc + " = " + (cn[t] + inc));
            int c = cn[t] += inc;
            if (hs.Contains(t))
            {
                // already in q1, adjust
                sm += (long)t * inc;
                if (c > 0)
                {
                    pq1.Enqueue((t, c), (c, t));
                }
                else
                {
                    hs.Remove(t);
                }
            }
            else
            {
                // add to q2 first
                if (c > 0)
                {
                    pq2.Enqueue((t, c), (-c, -t));
                }
            }
            // if (hs.Add(t))
            // {
            //     sm += (long)t * c;
            // }
            // else
            // {   // t present in hs, just update changes
            //     sm += (long)t * inc;
            // }
            // pq1.Enqueue((t, c), (c, t));

        }

        long Adjust()
        {
            // Console.WriteLine("Adjust");
            bool ok = false;
            while (!ok)
            {
                (int t1, int c1) = pq1.Count > 0 ? pq1.Peek() : (-1, -1);
                while (pq1.Count > 0 && (!hs.Contains(t1) || cn[t1] != c1))
                {
                    pq1.Dequeue();
                    (t1, c1) = pq1.Count > 0 ? pq1.Peek() : (-1, -1);
                }
                (int t2, int c2) = pq2.Count > 0 ? pq2.Peek() : (-1, -1); // be careful, pq2 may be empty
                while (pq2.Count > 0 && (hs.Contains(t2) || cn[t2] != c2))
                {
                    pq2.Dequeue();
                    (t2, c2) = pq2.Count > 0 ? pq2.Peek() : (-1, -1);
                }

                if (hs.Count > x)
                {
                    hs.Remove(t1);
                    sm -= (long)t1 * c1;
                    pq1.Dequeue();
                    pq2.Enqueue((t1, c1), (-c1, -t1));
                }
                else if (pq2.Count > 0 && hs.Count < x || (c2 > c1 || c2 == c1 && t2 > t1))
                {
                    if (hs.Count == x)
                    {
                        sm += (long)t2 * c2 - (long)t1 * c1;
                        hs.Remove(t1);
                        hs.Add(t2);
                        pq1.DequeueEnqueue((t2, c2), (c2, t2));
                        pq2.DequeueEnqueue((t1, c1), (-c1, -t1));
                    }
                    else
                    {
                        hs.Add(t2);
                        sm += (long)t2 * c2;
                        pq1.Enqueue((t2, c2), (c2, t2));
                        pq2.Dequeue();
                    }
                }
                else
                {
                    ok = true;
                }
            }
            // Console.WriteLine(" => " + sm);
            // Console.WriteLine(string.Join(" ", hs.Select(t => string.Format("{0}:{1}", t, cn[t]))));
            return sm;
        }

        long[] ans = new long[n - k + 1];
        ans[0] = Update1();
        for (int i = 0, j = k; j < n; ++i, ++j)
        {
            Update2(nums[j]);
            Update2(nums[i], -1);
            ans[i + 1] = Adjust();
        }
        return ans;
    }

    // 版本3：换成有序集合
    public long[] FindXSum_ver3(int[] nums, int k, int x)
    {
        int n = nums.Length;
        Counter<int> cn = new();
        SortedSet<(int, int)> s1 = new(), s2 = new();
        HashSet<int> hs = new();
        long sm = 0;

        long Update1()
        {
            //Console.Write("U1 ");
            for (int i = 0; i < k; ++i)
                ++cn[nums[i]];
            foreach ((int t, int c) in cn)
            {
                if (hs.Count < x)
                {
                    hs.Add(t);
                    sm += (long)t * c;
                    s1.Add((c, t));
                }
                else
                {
                    (int c0, int t0) = s1.Min;
                    if (c > c0 || c == c0 && t > t0)
                    {
                        s1.Remove((c0, t0));
                        s1.Add((c, t));
                        s2.Add((c0, t0));
                        sm += (long)t * c - (long)t0 * c0;
                        hs.Remove(t0);
                        hs.Add(t);
                    }
                    else
                    {
                        s2.Add((c, t));
                    }
                }
            }
            // Console.WriteLine(" => " + sm);
            return sm;
        }

        long Update2(int t, int inc = 1)
        {
            //Console.Write("U2 " + t + " " + inc);
            int c0 = cn[t], c = cn[t] += inc;
            if (!hs.Contains(t))
            {   // t not present in hs, add to s2
                if (c0 > 0)
                    s2.Remove((c0, t));
                if (c > 0)
                    s2.Add((c, t));
                // sm += (long)t * c;
            }
            else
            {   // t present in hs, just update changes
                s1.Remove((c0, t));
                sm += (long)t * inc;
                if (c > 0)
                    s1.Add((c, t));
                else
                    hs.Remove(t);
            }
            // s1.Add((c, t));

            bool ok = false;
            while (!ok)
            {
                (int c1, int t1) = s1.Min;
                (int c2, int t2) = s2.Count > 0 ? s2.Max : (-1, -1); // be careful, q2 may be empty

                if (hs.Count > x)
                {
                    hs.Remove(t1);
                    sm -= (long)t1 * c1;
                    s1.Remove((c1, t1));
                    s2.Add((c1, t1));
                }
                else if (c2 > c1 || c2 == c1 && t2 > t1)
                {
                    hs.Add(t2);
                    sm += (long)t2 * c2;
                    s1.Add((c2, t2));
                    s2.Remove((c2, t2));
                }
                else
                {
                    ok = true;
                }
            }
            //Console.WriteLine(" => " + sm);
            //Console.WriteLine(string.Join(" ", hs.Select(t => string.Format("{0}:{1}", t, cn[t]))));
            return sm;
        }

        long[] ans = new long[n - k + 1];
        ans[0] = Update1();
        for (int i = 0, j = k; j < n; ++i, ++j)
        {
            Update2(nums[j]);
            ans[i + 1] = Update2(nums[i], -1);
        }
        return ans;
    }

    // 版本2(TLE)：引入2个堆，答案正确，但超时
    // 整体思路：维护两个群q1和q2，q1为答案需要的x个元素，q2为剩余元素（作为候选）
    public long[] FindXSum_Ver2_TLE(int[] nums, int k, int x)
    {
        int n = nums.Length;
        Counter<int> cn = new();
        PriorityQueue<(int t, int c), (int, int)> pq1 = new(), pq2 = new();
        HashSet<int> hs = new();
        long sm = 0;

        long Update1()
        {
            //Console.Write("U1 ");
            for (int i = 0; i < k; ++i)
                ++cn[nums[i]];
            foreach ((int t, int c) in cn)
            {
                if (hs.Count < x)
                {
                    hs.Add(t);
                    sm += (long)t * c;
                    pq1.Enqueue((t, c), (c, t));
                }
                else
                {
                    (int tr, int cr) = pq1.EnqueueDequeue((t, c), (c, t));
                    if (t != tr)
                    {
                        sm += (long)t * c - (long)tr * cr;
                        hs.Remove(tr);
                        hs.Add(t);
                    }
                    pq2.Enqueue((tr, cr), (-cr, -tr));
                }
            }
            // Console.WriteLine(" => " + sm);
            return sm;
        }

        long Update2(int t, int inc = 1)
        {
            //Console.Write("U2 " + t + " " + inc);
            int c = cn[t] += inc;
            if (hs.Add(t))
            {
                sm += (long)t * c;
            }
            else
            {   // t present in hs, just update changes
                sm += (long)t * inc;
            }
            pq1.Enqueue((t, c), (c, t));

            bool ok = false;
            while (!ok)
            {
                (int t1, int c1) = pq1.Peek();
                while (!hs.Contains(t1) || cn[t1] != c1)
                {
                    pq1.Dequeue();
                    (t1, c1) = pq1.Peek();
                }
                (int t2, int c2) = pq2.Count > 0 ? pq2.Peek() : (-1, -1); // be careful, pq2 may be empty
                while (pq2.Count > 0 && (hs.Contains(t2) || cn[t2] != c2))
                {
                    pq2.Dequeue();
                    (t2, c2) = pq2.Count > 0 ? pq2.Peek() : (-1, -1);
                }

                if (c2 > c1 || c2 == c1 && t2 > t1)
                {
                    hs.Add(t2);
                    sm += (long)t2 * c2;
                    pq1.Enqueue((t2, c2), (c2, t2));
                    pq2.Dequeue();
                }
                else if (hs.Count > x)
                {
                    hs.Remove(t1);
                    sm -= (long)t1 * c1;
                    pq1.Dequeue();
                    pq2.Enqueue((t1, c1), (-c1, -t1));
                }
                else
                {
                    ok = true;
                }
            }
            //Console.WriteLine(" => " + sm);
            //Console.WriteLine(string.Join(" ", hs.Select(t => string.Format("{0}:{1}", t, cn[t]))));
            return sm;
        }

        long[] ans = new long[n - k + 1];
        ans[0] = Update1();
        for (int i = 0, j = k; j < n; ++i, ++j)
        {
            Update2(nums[j]);
            ans[i + 1] = Update2(nums[i], -1);
        }
        return ans;
    }

    // 版本1(WA)：只维护了hs一个群，无法处理中间元素中途退群后又加回来的情况
    // 在此版本上需要再优化，维护一个大群和一个小群
    public long[] FindXSum_Ver1_WA(int[] nums, int k, int x)
    {
        int n = nums.Length;
        Counter<int> cn = new();
        PriorityQueue<(int t, int c), (int, int)> pq = new();
        HashSet<int> hs = new();
        long sm = 0;

        long Update1()
        {
            Console.Write("U1 ");
            for (int i = 0; i < k; ++i)
            {
                int t = nums[i];
                int c = ++cn[t];
                pq.Enqueue((t, c), (c, t));
                if (hs.Contains(t))
                {
                    sm += t;
                }
                else
                {
                    hs.Add(t);
                    sm += (long)t * c;
                }
            }
            while (hs.Count > x)
            {
                (int t, int cc) = pq.Dequeue();
                if (!hs.Contains(t) || cn[t] != cc)
                    continue;
                hs.Remove(t);
                sm -= (long)t * cc;
            }
            Console.WriteLine(" => " + sm);
            return sm;
        }

        long Update2(int tu, int td)
        {
            Console.Write("U2 " + tu + " " + td);

            int c = ++cn[tu];
            pq.Enqueue((tu, c), (c, tu));
            if (hs.Contains(tu))
            {
                sm += tu;
            }
            else
            {
                hs.Add(tu);
                sm += (long)tu * c;
            }
            c = --cn[td];
            pq.Enqueue((td, c), (c, td));
            if (hs.Contains(td))
            {
                sm -= td;
            }
            else
            {
                hs.Add(td);
                sm += (long)td * c;
            }
            while (hs.Count > x)
            {
                (int t, int cc) = pq.Dequeue();
                if (!hs.Contains(t) || cn[t] != cc)
                    continue;
                hs.Remove(t);
                sm -= (long)t * cc;
            }
            Console.WriteLine(" => " + sm);
            return sm;
        }

        long[] ans = new long[n - k + 1];
        ans[0] = Update1();
        for (int i = 0, j = k; j < n; ++i, ++j)
            ans[i + 1] = Update2(nums[j], nums[i]);
        return ans;
    }

    internal static void Run()
    {
        P3321计算子数组的xsumII p = new();
        //var ans = p.FindXSum(new int[] { 1, 1, 2, 2, 3, 4, 2, 3 }, 6, 2);
        var ans = p.FindXSum(new int[] { 3, 8, 7, 8, 7, 5 }, 2, 2);
        Console.WriteLine(string.Join(", ", ans));
    }
}
