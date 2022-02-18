using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1
{
    // hard, 2022/2/17
    // 前缀和+BST
    internal class P0327区间和的个数
    {
        public int CountRangeSum(int[] nums, int lower, int upper)
        {
            int count = 0;
            RBT bst = new();
            long sum = 0, llow = lower, lup = upper;
            bst.Add(0L);
            foreach (int n in nums)
            {
                sum += n;
                int cl = bst.Find(sum - lup - 1), cu = bst.Find(sum - llow);
                count += cu - cl;
                bst.Add(sum);
            }
            return count;
        }

        // ver1 - WA，没想到相同的和也要算
        //int count = 0;
        //BitArray barr;
        //public int CountRangeSum(int[] nums, int lower, int upper)
        //{
        //    RBT bst = new();
        //    barr = new(upper - lower + 1);
        //    long sum = 0, llow = lower, lup = upper;
        //    bst.Add(0L);
        //    foreach (int n in nums)
        //    {
        //        sum += n;
        //        foreach (long pre in bst.Range(sum - lup, sum - llow))
        //        {
        //            int idx = (int)(sum - pre) - lower;
        //            if (!barr[idx])
        //            {
        //                barr[idx] = true;
        //                ++count;
        //            }
        //        }
        //        bst.Add(sum);
        //    }
        //    return count;
        //}

        internal static void Run()
        {
            var sln = new P0327区间和的个数();
            int[] nums = { -2, 5, -1 };
            int low = -2, up = 2;
            int count = sln.CountRangeSum(nums, low, up);
            Console.WriteLine(count);
        }
    }

    // 本题红黑树需要用到的功能：
    // 1. 支持插入重复元素，因此维护一个NodeCount变量储存相同的元素有多少个，相应的Count和LeftCount方法也要更新
    // 2. 查询：支持查询小于等于val的元素有多少个，进一步求解区间[low,up]范围内的元素有多少个
    public class RBT
    {
        class TNS
        {
            public TNS Parent { get; private set; }

            private void UpdateCount()
            {
                int orgCount = Count;
                Count = (_left?.Count ?? 0) + (_right?.Count ?? 0) + NC;
                if (Count != orgCount && Parent != null) Parent.UpdateCount();
            }

            public TNS Left
            {
                get => _left;
                set
                {
                    _left = value;
                    if (_left != null) _left.Parent = this;
                    UpdateCount();
                }
            }
            TNS _left;

            public TNS Right
            {
                get => _right;
                set
                {
                    _right = value;
                    if (_right != null) _right.Parent = this;
                    UpdateCount();
                }
            }
            TNS _right;
            public int Count { get; private set; } = 1;
            public int LeftCount => (_left?.Count ?? 0) + NC;
            public long Val { get; private set; }
            public int NC { get; private set; } = 1; 
            public void Inc()
            {
                ++NC;
                TNS cur = this;
                while (cur != null)
                {
                    ++cur.Count;
                    cur = cur.Parent;
                }
            }
            public TNS(long e) => Val = e;
            bool red = false;
            public bool Red { get => red; set => red = value; }
            public bool Black { get => !red; set => red = !value; }
            public void SetRed() => Red = true;
            public void SetBlack() => Black = true;
            public TNS SetAsRoot()
            {
                Parent = null;
                return this;
            }
        }

        TNS root;
        public int Count => root?.Count ?? 0;
        public void Add(long e)
        {
            if (root == null) root = new(e);
            else
            {
                TNS parent = null;
                TNS current = root;
                bool pleft = false;
                while (current != null)
                    if (e < current.Val)
                    {
                        parent = current;
                        current = current.Left;
                        pleft = true;
                    }
                    else if (e > current.Val)
                    {
                        parent = current;
                        current = current.Right;
                        pleft = false;
                    }
                    else
                    {
                        current.Inc();
                        return;
                    }

                if (pleft)
                    current = parent.Left = new(e);
                else
                    current = parent.Right = new(e);

                ESR(current);
            }
        }
        void ESR(TNS te)
        {
            TNS u = te;
            TNS v = te.Parent;
            u.SetRed();
            if (v.Red) FDR(u, v);
        }

        private void FDR(TNS u, TNS v)
        {
            TNS w = v.Parent;
            TNS parentOfw = w?.Parent;
            TNS x = w.Left == v ? w.Right : w.Left;
            if (x == null || x.Black)
            {
                if (w.Left == v && v.Left == u)
                {
                    RR(u, v, w, w, parentOfw);
                    w.Left = v.Right;
                    v.Right = w;
                }
                else if (w.Left == v && v.Right == u)
                {
                    RR(v, u, w, w, parentOfw);
                    v.Right = u.Left;
                    w.Left = u.Right;
                    u.Left = v;
                    u.Right = w;
                }
                else if (w.Right == v && v.Right == u)
                {
                    RR(w, v, u, w, parentOfw);
                    w.Right = v.Left;
                    v.Left = w;
                }
                else
                {
                    RR(w, u, v, w, parentOfw);
                    w.Right = u.Left;
                    v.Left = u.Right;
                    u.Left = w;
                    u.Right = v;
                }
            }
            else
            {
                w.SetRed();
                u.SetRed();
                w.Left.SetBlack();
                w.Right.SetBlack();
                if (w == root)
                {
                    w.SetBlack();
                }
                else if (parentOfw.Red)
                {
                    u = w;
                    v = parentOfw;
                    FDR(u, v);
                }
            }
        }
        void RR(TNS a, TNS b, TNS c, TNS w, TNS parentOfw)
        {
            if (parentOfw == null)
                root = b.SetAsRoot();
            else if (parentOfw.Left == w)
                parentOfw.Left = b;
            else
                parentOfw.Right = b;

            b.SetBlack();
            a.SetRed();
            c.SetRed();
        }

        // 返回小于等于val的元素有多少个
        public int Find(long val)
        {
            int count = 0;
            TNS current = root;
            while (current != null)
                if (val.CompareTo(current.Val) < 0)
                    current = current.Left;
                else if (val.CompareTo(current.Val) > 0)
                {
                    count += current.LeftCount;
                    current = current.Right;
                }
                else
                {
                    count += current.LeftCount;
                    break;
                }
            return count;
        }
    }
#if OLD_ARCHIVE
    public class RBT
    {
        class TNS
        {
            public TNS Parent { get; private set; }
            public int Count { get; private set; } = 1;
            void UpdateCount()
            {
                int orgCount = Count;
                Count = (_left?.Count ?? 0) + (_right?.Count ?? 0) + 1;
                if (Count != orgCount && Parent != null) Parent.UpdateCount();
            }

            public TNS Left;
            public TNS Right;
            public long val;
            public TNS(long e) => val = e;
            bool red = false;
            public bool Red { get => red; set => red = value; }
            public bool Black { get => !red; set => red = !value; }
        }
        TNS root;
        public int Count { get; private set; }
        public bool Add(long e)
        {
            if (root == null)
            {
                root = new(e);
                return ++Count > 0;
            }
            else
            {
                TNS parent = null;
                TNS current = root;
                List<TNS> path = new();
                bool pleft = false;
                while (current != null)
                {
                    path.Add(current);
                    if (e.CompareTo(current.val) < 0)
                    {
                        parent = current;
                        current = current.Left;
                        pleft = true;
                    }
                    else if (e.CompareTo(current.val) > 0)
                    {
                        parent = current;
                        current = current.Right;
                        pleft = false;
                    }
                    else return false;
                }
                if (pleft)
                    current = parent.Left = new(e);
                else
                    current = parent.Right = new(e);
                path.Add(current);
                Count++;
                ESR(current, path);
                return true;
            }
        }
        void ESR(TNS te, List<TNS> path)
        {
            int i = path.Count - 1;
            TNS u = te;
            TNS v = path[i - 1];
            u.Red = true;
            if (v.Red) FDR(u, v, path, i);
        }
        void FDR(TNS u, TNS v, List<TNS> path, int i)
        {
            TNS w = path[i - 2];
            TNS parentOfw = (w == root) ? null : path[i - 3];
            TNS x = w.Left == v ? w.Right : w.Left;
            if (x == null || x.Black)
            {
                if (w.Left == v && v.Left == u)
                {
                    RR(u, v, w, w, parentOfw);
                    w.Left = v.Right;
                    v.Right = w;
                }
                else if (w.Left == v && v.Right == u)
                {
                    RR(v, u, w, w, parentOfw);
                    v.Right = u.Left;
                    w.Left = u.Right;
                    u.Left = v;
                    u.Right = w;
                }
                else if (w.Right == v && v.Right == u)
                {
                    RR(w, v, u, w, parentOfw);
                    w.Right = v.Left;
                    v.Left = w;
                }
                else
                {
                    RR(w, u, v, w, parentOfw);
                    w.Right = u.Left;
                    v.Left = u.Right;
                    u.Left = w;
                    u.Right = v;
                }
            }
            else
            {
                w.Red = true;
                u.Red = true;
                w.Left.Black = true;
                w.Right.Black = true;

                if (w == root)
                    w.Black = true;
                else if (parentOfw.Red)
                {
                    u = w;
                    v = parentOfw;
                    FDR(u, v, path, i - 2);
                }
            }
        }
        void RR(TNS a, TNS b, TNS c, TNS w, TNS parentOfw)
        {
            if (parentOfw == null)
                root = b;
            else if (parentOfw.Left == w)
                parentOfw.Left = b;
            else
                parentOfw.Right = b;
            b.Black = true;
            a.Red = true;
            c.Red = true;
        }
        public IEnumerable<long> Range(long start, long end)
        {
            Stack<TNS> stk = new();
            TNS current = root;
            while (current != null)
                if (start.CompareTo(current.val) <= 0)
                {
                    stk.Push(current);
                    current = current.Left;
                }
                else if (start.CompareTo(current.val) > 0)
                    current = current.Right;
            while (stk?.Any() == true)
            {
                current = stk.Pop();
                if (current.val.CompareTo(end) > 0) break;
                yield return current.val;
                current = current.Right;
                while (current != null)
                {
                    stk.Push(current);
                    current = current.Left;
                }
            }
        }
    }
#endif
}
