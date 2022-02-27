using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace ProblemTests
{
    [TestClass]
    public class ContestBiweekD
    {
        public class TNode
        {
            public TNode Parent { get; private set; }

            private void UpdateCount()
            {
                int orgCount = Count;
                long tre = TreCount;
                Count = (_left?.Count ?? 0) + (_right?.Count ?? 0) + 1;
                TreCount = (_left?.TreCount ?? 0L) + (_right?.TreCount ?? 0L) + LessCount;
                if ((Count != orgCount || TreCount != tre) && Parent != null) Parent.UpdateCount();
            }

            public TNode Left
            {
                get => _left;
                set
                {
                    _left = value;
                    if (_left != null) _left.Parent = this;
                    UpdateCount();
                }
            }
            TNode _left;

            public TNode Right
            {
                get => _right;
                set
                {
                    _right = value;
                    if (_right != null) _right.Parent = this;
                    UpdateCount();
                }
            }
            TNode _right;

            public int Count { get; private set; } = 1;
            public int LessCount { get; private set; } = 0;
            public long TreCount { get; private set; } = 0L;
            public int LeftCount => (_left?.Count ?? 0) + 1;
            public int Val { get; private set; }
            public TNode(int e, int less)
            {
                Val = e;
                LessCount = less;
                TreCount = less;
            }
            bool red = false;
            public bool Red { get => red; set => red = value; }
            public bool Black { get => !red; set => red = !value; }
            public void SetRed() => Red = true;
            public void SetBlack() => Black = true;

            public TNode SetAsRoot()
            {
                Parent = null;
                return this;
            }
        }

        public class TreeList
        {
            TNode root;
            public int Count => root?.Count ?? 0;
            public (int, long) Add(int e)
            {
                if (root == null)
                {
                    root = new(e, 0);
                    return (0, 0L);
                }
                else
                {
                    int ind = 0;
                    long tre = 0;
                    TNode parent = null;
                    TNode current = root;
                    bool pleft = false;
                    while (current != null)
                        if (e < current.Val)
                        {
                            parent = current;
                            current = current.Left;
                            pleft = true;
                        }
                        else
                        {
                            ind += current.LeftCount;
                            tre += (current.Left?.TreCount ?? 0L) + current.LessCount;
                            parent = current;
                            current = current.Right;
                            pleft = false;
                        }

                    if (pleft)
                        current = parent.Left = new(e, ind);
                    else
                        current = parent.Right = new(e, ind);

                    EnsureRBT(current);
                    return (ind, tre);
                }
            }
            void EnsureRBT(TNode te)
            {
                TNode u = te;

                TNode v = te.Parent;

                u.SetRed();

                if (v.Red) FixDoubleRed(u, v);
            }

            private void FixDoubleRed(TNode u, TNode v)
            {
                TNode w = v.Parent;
                TNode parentOfw = w?.Parent;

                TNode x = w.Left == v ? w.Right : w.Left;

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
                        FixDoubleRed(u, v);
                    }
                }
            }

            private void RR(TNode a, TNode b, TNode c, TNode w, TNode parentOfw)
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
        }

        public long GoodTriplets(int[] nums1, int[] nums2)
        {
            TreeList tree = new();
            long ans = 0;

            int[] map1 = new int[nums1.Length];
            for (int i = 0; i < nums1.Length; ++i)
            {
                map1[nums1[i]] = i;
            }
            for (int i = 0; i < nums2.Length; ++i)
            {
                int n = map1[nums2[i]];
                (int _, long tre) = tree.Add(n);
                ans += tre;
            }
            return ans;
        }

        public List<int> CountSmaller_MyRedBlackTree(int[] nums)
        {
            //// debug test
            //TreeList<NumStruct> test = new();
            //test.Add(new NumStruct() { num = 1 });
            //test.Add(new NumStruct() { num = 6 });
            //test.Add(new NumStruct() { num = 2 });


            TreeList tree = new();
            for (int i = 0;  i <= nums.Length - 1; ++i)
            {
                (int ind, long _) = tree.Add(nums[i]);
                nums[i] = ind;
            }
            return nums.ToList();
        }
        

        [TestMethod]
        public void Run()
        {
            int[] arr = { 1, 2, 0, 4, 3, 6, 5 };
            var ret = CountSmaller_MyRedBlackTree(arr);
            int[] exp = { 0, 1, 0, 3, 3, 5, 5 };
            for (int i = 0; i < 7; ++i)
                if (ret[i] != exp[i])
                    Assert.Fail();
            int x = 1;
            Assert.AreEqual(1, x);
        }
    }
}
