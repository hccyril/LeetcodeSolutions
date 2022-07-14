using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1
{
    // hard, 2022/7/11
    // rank: 2415
    // reversed 堆（往里面删元素而不是加元素），代码实现用SortedSet代替
    internal class P2003每棵子树内缺失的最小基因值
    {
        // ver3 - 并查集 AC
        public int[] SmallestMissingValueSubtree(int[] parents, int[] nums)
        {
            int N = parents.Length, next = 2;
            UnionFind uni = new(N + 2);
            int[] cnt = new int[N];
            foreach (int p in parents.Skip(1)) ++cnt[p];
            Queue<int> qu = new(Enumerable.Range(0, parents.Length).Where(i => cnt[i] == 0));

            int[] ans = new int[N];
            while (qu.Any())
            {
                int i = qu.Dequeue();

                // answer
                int n = nums[i];
                if (uni.Find(1) == n)
                {
                    while (uni.Find(next) == n) ++next;
                    ans[i] = next;
                }
                else ans[i] = 1;

                // process
                int par = parents[i];
                if (par >= 0)
                    uni.Union(nums[par], n);

                // next
                if (i > 0 && --cnt[parents[i]] == 0)
                    qu.Enqueue(parents[i]);
            }

            return ans;
        }

        // ver2 TLE 6016ms -> 2531ms -> 1812ms 优化以后还是超时。。
        // moved to Common
        //void MergeOne(SortedSet<Interval> sort, Interval rs)
        //{
        //    Interval left = new Interval(rs.start - 1),
        //        right = new Interval(rs.end + 1);
        //    if (sort.TryGetValue(left, out var rl))
        //    {
        //        if (sort.TryGetValue(right, out var rr))
        //        {
        //            sort.Remove(rr);
        //            rl.end = rr.end;
        //        }
        //        else
        //        {
        //            rl.end = rs.end;
        //        }
        //    }
        //    else
        //    {
        //        if (sort.TryGetValue(right, out var rr))
        //        {
        //            rr.start = rs.start;
        //        }
        //        else 
        //            sort.Add(rs);
        //    }
        //    //RangeStruct left = new RangeStruct(rs.start - 1);
        //    //if (sort.TryGetValue(left, out var rl))
        //    //{
        //    //    sort.Remove(rl);
        //    //    rs.start = rl.start;
        //    //}
        //    //RangeStruct right = new RangeStruct(rs.end + 1);
        //    //if (sort.TryGetValue(right, out var rr))
        //    //{
        //    //    sort.Remove(rr);
        //    //    rs.end = rr.end;
        //    //}
        //    //sort.Add(rs);
        //}

        public int[] SmallestMissingValueSubtree_ver2_TLE(int[] parents, int[] nums)
        {
            int[] cnt = new int[parents.Length];
            foreach (int p in parents.Skip(1)) ++cnt[p];
            var arr = new SortedSet<Interval>[parents.Length];
            for (int i = 0; i < parents.Length; ++i)
            {
                arr[i] = new();
                arr[i].Add(new(nums[i]));
            }
            Queue<int> qu = new(Enumerable.Range(0, parents.Length).Where(i => cnt[i] == 0));

            int[] ans = new int[parents.Length];
            while (qu.Any())
            {
                int i = qu.Dequeue();

                // answer
                var r1 = arr[i].Min;
                ans[i] = r1.start == 1 ? arr[i].Min.end + 1 : 1;

                // process
                int par = parents[i];
                if (par >= 0)
                    arr[par].MergeInterval(arr[i]);
                    //MergeRange(arr[par], arr[i]);

                // end
                arr[i].Clear();
                arr[i] = null;

                // next
                if (i > 0 && --cnt[parents[i]] == 0)
                    qu.Enqueue(parents[i]);
            }

            return ans;
        }

        // ver1: WA
        // input: parents = [-1,0,1,0,3,3], nums = [5,4,6,2,1,3]
        // my answer: [7,1,1,5,2,1]
        // expected:  [7,1,1,4,2,1]
        // 原因：基因4在左边，不应该影响到右边子树
        public int[] SmallestMissingValueSubtree_ver1_WA(int[] parents, int[] nums)
        {
            int[] cnt = new int[parents.Length];
            foreach (int p in parents.Skip(1)) ++cnt[p];
            SortedSet<int> sort = new(Enumerable.Range(1, nums.Max() + 1));
            SortedSet<(int, int)> qu = new(Enumerable.Range(0, parents.Length)
                .Where(i => cnt[i] == 0).Select(i => (nums[i], i)));

            int[] ans = new int[parents.Length];
            while (qu.Any())
            {
                (int n, int i) = qu.Max; qu.Remove((n, i));
                sort.Remove(n);
                ans[i] = sort.Min;
                if (i > 0 && --cnt[parents[i]] == 0)
                    qu.Add((nums[parents[i]], parents[i]));
            }

            return ans;
        }

        internal static void Run()
        {
            var sln = new P2003每棵子树内缺失的最小基因值();
            //            int[] a1 = {
            //                -1,
            //                0,
            //                0,
            //                2
            //            },
            //a2 = { 1, 2, 3, 4
            //            };
            var input = Common.ReadInput<InputStruct>(2003);
            var ans = sln.SmallestMissingValueSubtree(input.parents, input.nums);
            Console.WriteLine("len=" + ans.Length);
        }

        class InputStruct
        {
            public int[] parents { get; set; }
            public int[] nums { get; set; }
        }
    }
}