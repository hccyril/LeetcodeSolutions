using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1
{
    // hard, 2023/7/18 Daily
    // 区间问题，用自己的InSet做出来了但是超时
    // 后面改用线段树通过
    // 但是看题解似乎有更好的方法

    // copied from common and modify
    // NO_T-O-D-O replace common version
    public class InSetNew : SortedSet<Interval>
    {
        readonly Func<int, int, int> valFunc;

        public InSetNew()
            => valFunc = (a, b) => a + b;

        public InSetNew(Func<int, int, int> vf)
            => valFunc = vf;

        public int Length { get; private set; }

        public void Update2(Interval r)
        {
            if (TryGetValue(r, out var r0))
            {
                foreach (var rs in r.Cut(r0))
                    Update2(rs);
            }
            else if (r.Valid)
            {
                Add(r);
            }
        }

        public void Update(Interval r)
        {
            if (TryGetValue(r, out var r0))
            {
                Remove(r0); Length -= r0.Count;
                var it = r0.Intersect(r);
                it.val = valFunc(r0.val, r.val);
                Combine(it);
                foreach (var rs in r0.Cut(it))
                    Combine(rs);
                foreach (var rs in r.Cut(it))
                    Update(rs);
            }
            else if (r.Valid)
            {
                Combine(r);
            }
        }

        // 就是Merge，为了不重复所以改名
        // 这里假设InSet与ra已经没有重叠区间
        bool Combine(Interval ra) 
        {
            while (TryGetValue(new() { start = ra.start - 1, end = ra.start - 1 }, out var r) && r.val == ra.val)
            {
                Remove(r);
                Length -= r.Count;
                ra.start = Math.Min(r.start, ra.start);
            }

            while (TryGetValue(new() { start = ra.end + 1, end = ra.end + 1 }, out var r) && r.val == ra.val)
            {
                Remove(r);
                Length -= r.Count;
                ra.end = Math.Max(r.end, ra.end);
            }

            Length += ra.Count;
            return Add(ra);
        }
    }

    internal class P1851包含每个查询的最小区间
    {
        // ver3 线段树 - 844ms
        public int[] MinInterval(int[][] intervals, int[] queries)
        {
            int maxl = -1, minl = int.MaxValue;
            foreach (var r in intervals)
            {
                if (r[0] < minl) minl = r[0];
                if (r[1] > maxl) maxl = r[1];
            }

            int[] a = new int[maxl - minl << 2 | 1];

            // 线段树区间更新
            void Update(int i, int il, int ir, int l, int r, int v)
            {
                if (il == l && ir == r) {
                    a[i] = a[i] == 0 ? v : Math.Min(a[i], v);
                    return;
                }

                int mid = il + (ir - il >> 1);
                if (l <= mid)
                    Update(i << 1, il, mid, l, Math.Min(r, mid), v);
                if (r > mid)
                    Update(i << 1 | 1, mid + 1, ir, Math.Max(l, mid + 1), r, v);
            }

            // 线段树单点查询
            int Check(int x, int i, int il, int ir)
            {
                int ans = a[i] == 0 ? -1 : a[i];
                if (il < ir)
                {
                    int mid = il + (ir - il >> 1);
                    int next = x <= mid ? Check(x, i << 1, il, mid) : Check(x, i << 1 | 1, mid + 1, ir);
                    if (ans < 0) ans = next;
                    else if (next > 0) ans = Math.Min(ans, next);
                }
                return ans;
            }

            foreach (var r in intervals)
                Update(1, minl, maxl, r[0], r[1], r[1] - r[0] + 1);

            return queries.Select(x => x < minl || x > maxl ? -1 : Check(x, 1, minl, maxl)).ToArray();
        }

        // 在ver1的基础上进行优化
        // 优化到11891ms，还是不达标。。
        // 实测排序用时485ms，那所以应该还是sortedset比较慢
        public int[] MinInterval_TLE2(int[][] intervals, int[] queries)
        {
            InSetNew ins = new();
            Interval ra = null;
            foreach (var it in intervals
                .Select(r => new Interval() { start = r[0], end = r[1], val = r[1] - r[0] + 1 })
                .OrderBy(i => i.val).ThenBy(i => i.start))
            {
                if (ra?.val == it.val && ra.end >= it.start)
                    ra.end = it.end;
                else
                {
                    if (ra != null)
                    {
                        ins.Update2(ra);
                        ra = null;
                    }
                    ra = it;
                }
            }

            if (ra != null) ins.Update2(ra);
            return queries.Select(t => ins.TryGetValue(new() { start = t, end = t }, out var r) ? r.val : -1).ToArray();
        }

        // ver1: 41063ms 怎么会这么慢。。。
        public int[] MinInterval_TLE1(int[][] intervals, int[] queries)
        {
            InSetNew ins = new(Math.Min);
            foreach (var iv in intervals)
            {
                Interval it = new() { start = iv[0], end = iv[1], val = iv[1] - iv[0] + 1 };
                ins.Update(it);
            }
            return queries.Select(t => ins.TryGetValue(new() { start = t, end = t }, out var r) ? r.val : -1).ToArray();
        }

        internal static void Run()
        {
            var sln = new P1851包含每个查询的最小区间();

            //int[][] ia = "[[1,4],[2,4],[3,6],[4,4]]"
            //    .ToTestInput<int[][]>();
            //int[] b = { 2, 3, 4, 5 };
            //var ans = sln.MinInterval(ia, b);
            //Console.WriteLine(string.Join(" ", ans));

            var input = Common.ReadInput<InputStruct>(1851);
            var ans = sln.MinInterval(input.intervals, input.queries);
            Console.WriteLine("Count=" + ans.Length);
        }

        class InputStruct
        {
            public int[][] intervals { get; set; }
            public int[] queries { get; set; }
        }
    }
}
