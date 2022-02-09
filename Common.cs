using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1
{
    class ReuseFunctions
    {
        /// <summary>
        /// 矩阵枚举上下左右四个方向
        /// </summary>
        IEnumerable<(int ni, int nj)> FourDir(int[][] mx, int i, int j)
        {
            if (i > 0) yield return (i - 1, j);
            if (i < mx.Length - 1) yield return (i + 1, j);
            if (j > 0) yield return (i, j - 1);
            if (j < mx[i].Length - 1) yield return (i, j + 1);
        }

        /// <summary>
        /// 矩阵枚举周围8个方向
        /// </summary>
        IEnumerable<(int ni, int nj)> EightDir(int[][] mx, int i, int j)
        {
            if (i > 0)
            {
                yield return (i - 1, j);
                if (j > 0) yield return (i - 1, j - 1);
                if (j < mx[i].Length - 1) yield return (i - 1, j + 1);
            }
            if (j > 0) yield return (i, j - 1);
            if (j < mx[i].Length - 1) yield return (i, j + 1);
            if (i < mx.Length - 1)
            {
                yield return (i + 1, j);
                if (j > 0) yield return (i + 1, j - 1);
                if (j < mx[i].Length - 1) yield return (i + 1, j + 1);
            }
        }
    }
    public class ListNode
    {
        public int val;
        public ListNode next;
        public ListNode(int val = 0, ListNode next = null)
        {
            this.val = val;
            this.next = next;
        }
    }

    public class TreeNode
    {
        public virtual int val { get; set; }
        public virtual TreeNode left { get; set; }
        public virtual TreeNode right { get; set; }
        public TreeNode(int x)
        {
            val = x;
        }
        public TreeNode(int val = 0, TreeNode left = null, TreeNode right = null)
        {
            this.val = val;
            this.left = left;
            this.right = right;
        }
        public override string ToString()
        {
            return $"{val} (L:{left} R:{right})";
        }
    }

    /// <summary>
    /// heap
    /// </summary>
    public class Heap<T>
    {
        Func<T, T, bool> comp;
        public Heap(Func<T, T, bool> comp) => this.comp = comp;
        bool Compare(int i, int j) => comp(GetAt(i), GetAt(j));
        T swap;
        List<T> _list = new List<T>();
        T GetAt(int index) => _list[index - 1];
        public bool Any() => _list.Any();
        public int Count => _list.Count;
        public T Head => _list[0];
        void Swap(int i, int j)
        {
            swap = _list[i - 1];
            _list[i - 1] = _list[j - 1];
            _list[j - 1] = swap;
        }
        public void Push(T n)
        {
            _list.Add(n);
            int i = Count;
            int inext = i >> 1;
            while (inext > 0 && Compare(i, inext))
            {
                Swap(i, inext);
                i = inext; inext = i >> 1;
            }
        }
        public T Pop()
        {
            T ans = _list[0];
            _list[0] = _list[_list.Count - 1];
            _list.RemoveAt(_list.Count - 1);
            int i = 1, inext;
            while (true)
            {
                inext = i << 1;
                if (inext <= Count)
                {
                    if (inext < Count && Compare(inext + 1, inext)) inext++;
                    if (Compare(inext, i))
                    {
                        Swap(i, inext);
                        i = inext;
                        continue;
                    }
                }
                break;
            }
            return ans;
        }
        public T[] ToArray() => _list.ToArray();
    }


    /// <summary>
    /// 堆：大顶堆或者小顶堆
    /// </summary>
    public class HashHeap
    {
        Func<int, int, bool> comp;

        /// <summary>
        /// 构造方法用于指定是大顶堆还是小顶堆
        /// </summary>
        /// <param name="maxOrMin">true表示大顶堆，false表示小顶堆</param>
        public HashHeap(bool maxOrMin)
        {
            if (maxOrMin) comp = (a, b) => a > b;
            else comp = (a, b) => a < b;
        }

        public HashHeap(Func<int, int, bool> comp) => this.comp = comp;
        bool Compare(int i, int j) => comp(GetAt(i)[0], GetAt(j)[0]);

        int[] swap;
        int[] GetAt(int index) => _list[index - 1];
        List<int[]> _list = new List<int[]>(); // e[0]: 值; e[1]: 在list中对应的index; e[2]: key
        Dictionary<int, int[]> _dic = new Dictionary<int, int[]>();
        public bool Any() => Count > 0;
        public int Count => _list.Count;
        public bool ContainsKey(int key) => _dic.ContainsKey(key);

        public int Head => _list[0][0];
        public int HeadKey => _list[0][2];
        void Swap(int i, int j)
        {
            swap = _list[i - 1];
            _list[i - 1] = _list[j - 1];
            _list[i - 1][1] = i;
            _list[j - 1] = swap;
            _list[j - 1][1] = j;
        }

        public void Push(int key, int val)
        {
            int[] item = { val, 0, key };
            _dic.Add(key, item);
            _list.Add(item);
            SwapUp(item[1] = Count);
        }

        /// <summary>
        /// 基于增量的更新，留意只支持向上swap
        /// </summary>
        public void Update(int key, int inc)
        {
            int[] p = _dic[key];
            p[0] += inc;
            SwapUp(p[1]);
        }
        public int this[int key]
        {
            get => _dic[key][0];
        }
        //public (int index, int val) Get(int key)
        //{
        //    int[] p = _dic[key];
        //    return (p[1], p[0]);
        //}
        //public void UpdateAt(int index, int val)
        //{
        //    _list[index - 1][0] = val;
        //    SwapUp(index);
        //}

        public (int key, int val) Pop()
        {
            var p = GetAt(1);
            Remove(p[2]);
            return (p[2], p[0]);
        }

        public void Remove(int key)
        {
            int index = _dic[key][1];
            _dic.Remove(key);
            RemoveAt(index);
        }

        void RemoveAt(int index)
        {
            if (index == Count)
            {
                _list.RemoveAt(index - 1);
                return;
            }

            Swap(index, Count);
            _list.RemoveAt(_list.Count - 1);

            bool hasSwapUp = SwapUp(index);
            if (!hasSwapUp) SwapDown(index);
        }

        bool SwapUp(int index)
        {
            int i = index, inext = i >> 1;

            // swap up
            while (inext > 0 && Compare(i, inext))
            {
                Swap(i, inext);
                i = inext; inext = i >> 1;
            }

            return i != index; // i如果没有改变说明没有swap up，如果有swap up就不需要swap down了
        }

        void SwapDown(int i)
        {
            while (i < Count)
            {
                int inext = i << 1;
                if (inext <= Count)
                {
                    if (inext < Count && Compare(inext + 1, inext)) inext++;
                    if (Compare(inext, i))
                    {
                        Swap(i, inext);
                        i = inext;
                        continue;
                    }
                }
                break;
            }
        }
    }

    // 并查集 - 初次引用自力扣721官方题解

    /// <summary>
    /// 并查集
    /// </summary>
    class UnionFind
    {
        int[] parent;

        public UnionFind(int n)
        {
            parent = new int[n];
            for (int i = 0; i < n; i++)
            {
                parent[i] = i;
            }
        }

        public void Union(int index1, int index2)
        {
            parent[Find(index2)] = Find(index1);
        }

        public int Find(int index)
        {
            if (parent[index] != index)
            {
                parent[index] = Find(parent[index]);
            }
            return parent[index];
        }
    }

    // 单词前缀树 - 212单词搜索，1032字符流

    /// <summary>
    /// 单词前缀树
    /// </summary>
    class WordTree
    {
        Dictionary<char, WordTree> next;
        public string Word { get; private set; }
        public WordTree this[char c]
        {
            get
            {
                if (next == null) next = new Dictionary<char, WordTree>();
                if (!next.ContainsKey(c)) next[c] = new WordTree();
                return next[c];
            }
        }
        public bool ContainsChar(char c) => next?.ContainsKey(c) == true;
        public bool ContainsWord(string s, int i)
            => ContainsChar(s[i]) && (i == s.Length - 1 && next[s[i]].Word != null || ContainsWord(s, i + 1));
        public bool End() => next == null;
        public void AddWord(string word)
        {
            WordTree tree = this;
            foreach (char c in word)
            {
                tree = tree[c];
            }
            tree.Word = word;
        }
    }

    // 红黑树+链表
    public class TNode<E> where E : IComparable<E>
    {
        public TNode<E> left, right;
        public E val;
        public TNode(E e) => val = e;
        bool red = false;
        public bool Red { get => red; set => red = value; }
        public bool Black { get => !red; set => red = !value; }
        public void SetRed() => Red = true;
        public void SetBlack() => Black = true;
    }

    public class TreeList<E> where E : IComparable<E>
    {
        TNode<E> root;
        public int Count { get; private set; }
        public bool Add(E e)
        {
            if (root == null)
            {
                root = new(e);
                Count++;
                return true;
            }
            else
            {
                // Locate the parent node							
                TNode<E> parent = null;
                TNode<E> current = root;
                List<TNode<E>> path = new();
                bool pleft = false;
                while (current != null)
                {
                    path.Add(current);
                    if (e.CompareTo(current.val) < 0)
                    {
                        parent = current;
                        current = current.left;
                        pleft = true;
                    }
                    else if (e.CompareTo(current.val) > 0)
                    {
                        parent = current;
                        current = current.right;
                        pleft = false;
                    }
                    else
                        return false; // Duplicate node not inserted	
                }

                // Create the new node and attach it to the parent node
                if (pleft)
                    current = parent.left = new(e);
                else
                    current = parent.right = new(e);
                path.Add(current);

                Count++;
                EnsureRBTree(current, path);
                return true;
            }
        }

        /** Ensure that the tree is a red-black tree */
        private void EnsureRBTree(TNode<E> te, List<TNode<E>> path)
        {
            int i = path.Count - 1; // Index to the current node in the path

            // u is the last node in the path. u contains element e
            TNode<E> u = te; // path[i];

            // v is the parent of of u, if exists
            TNode<E> v = path[i - 1];

            u.SetRed(); // It is OK to set u red    

            // Fix double red violation at u
            if (v.Red) FixDoubleRed(u, v, path, i);
        }

        private void FixDoubleRed(TNode<E> u, TNode<E> v, List<TNode<E>> path, int i)
        {
            // w is the grandparent of u
            TNode<E> w = path[i - 2];
            TNode<E> parentOfw = (w == root) ? null : path[i - 3];

            // Get v's sibling named x
            TNode<E> x = w.left == v ? w.right : w.left;

            if (x == null || x.Black)
            {
                // Case 1: v's sibling x is black
                if (w.left == v && v.left == u)
                {
                    // Case 1.1: u < v < w, Restructure and recolor nodes
                    RestructureRecolor(u, v, w, w, parentOfw);

                    w.left = v.right; // v.right is y3 in Figure 11.7
                    v.right = w;
                }
                else if (w.left == v && v.right == u)
                {
                    // Case 1.2: v < u < w, Restructure and recolor nodes
                    RestructureRecolor(v, u, w, w, parentOfw);
                    v.right = u.left;
                    w.left = u.right;
                    u.left = v;
                    u.right = w;
                }
                else if (w.right == v && v.right == u)
                {
                    // Case 1.3: w < v < u, Restructure and recolor nodes
                    RestructureRecolor(w, v, u, w, parentOfw);
                    w.right = v.left;
                    v.left = w;
                }
                else
                {
                    // Case 1.4: w < u < v, Restructure and recolor nodes
                    RestructureRecolor(w, u, v, w, parentOfw);
                    w.right = u.left;
                    v.left = u.right;
                    u.left = w;
                    u.right = v;
                }
            }
            else
            { // Case 2: v's sibling x is red 
              // Recolor nodes
                w.SetRed();
                u.SetRed();
                w.left.SetBlack();
                w.right.SetBlack();

                if (w == root)
                {
                    w.SetBlack();
                }
                else if (parentOfw.Red)
                {
                    // Propagate along the path to fix new double red violation
                    u = w;
                    v = parentOfw;
                    FixDoubleRed(u, v, path, i - 2); // i - 2 propagates upward
                }
            }
        }

        /** Connect b with parentOfw and recolor a, b, c for a < b < c */
        private void RestructureRecolor(TNode<E> a, TNode<E> b,
            TNode<E> c, TNode<E> w, TNode<E> parentOfw)
        {
            if (parentOfw == null)
                root = b;
            else if (parentOfw.left == w)
                parentOfw.left = b;
            else
                parentOfw.right = b;

            b.SetBlack(); // b becomes the root in the subtree
            a.SetRed(); // a becomes the left child of b
            c.SetRed(); // c becomes the right child of b
        }

        #region search node
        public bool Search(E e)
        {
            TNode<E> current = root; // Start from the root

            while (current != null)
            {
                if (e.CompareTo(current.val) < 0)
                {
                    current = current.left;
                }
                else if (e.CompareTo(current.val) > 0)
                {
                    current = current.right;
                }
                else // element matches current.element
                    return true; // Element is found
            }

            return false;
        }
        #endregion

        #region delete node
        public bool Remove(E e)
        {
            // Locate the node to be deleted
            TNode<E> current = root;
            List<TNode<E>> path = new();

            while (current != null)
            {
                path.Add(current);
                if (e.CompareTo(current.val) < 0)
                {
                    current = current.left;
                }
                else if (e.CompareTo(current.val) > 0)
                {
                    current = current.right;
                }
                else
                    break; // Element is in the tree pointed by current
            }

            if (current == null)
                return false; // Element is not in the tree


            // current node is an internal node 
            if (current.left != null && current.right != null)
            {
                // Locate the rightmost node in the left subtree of current
                TNode<E> rightMost = current.left;
                path.Add(rightMost);
                while (rightMost.right != null)
                {
                    rightMost = rightMost.right; // Keep going to the right
                    path.Add(rightMost);
                }

                // Replace the element in current by the element in rightMost
                current.val = rightMost.val;
            }

            // Delete the last node in the path and propagate if needed
            DeleteLastNodeInPath(path);

            Count--; // After one element deleted
            return true; // Element deleted
        }

        /** Delete the last node from the path. */
        public void DeleteLastNodeInPath(List<TNode<E>> path)
        {
            int i = path.Count - 1; // Index to the node in the path
                                    // u is the last node in the path
            TNode<E> u = path[i];
            TNode<E> parentOfu = (u == root) ? null : path[i - 1];
            TNode<E> grandparentOfu = parentOfu == null || parentOfu == root ? null : path[i - 2];
            TNode<E> childOfu = (u.left == null) ? u.right : u.left;

            // Delete node u. Connect childOfu with parentOfu
            ConnectNewParent(parentOfu, u, childOfu);

            // Recolor the nodes and fix double black if needed
            if (childOfu == root || u.Red)
                return; // Done if childOfu is root or if u is red 
            else if (childOfu != null && childOfu.Red)
                childOfu.SetBlack(); // Set it black, done
            else // u is black, childOfu is null or black
                 // Fix double black on parentOfu
                FixDoubleBlack(grandparentOfu, parentOfu, childOfu, path, i);
        }

        /** Connect newParent with grandParent */
        private void ConnectNewParent(TNode<E> grandparent,
            TNode<E> parent, TNode<E> newParent)
        {
            if (parent == root)
            {
                root = newParent;
                if (root != null)
                    newParent.SetBlack();
            }
            else if (grandparent.left == parent)
                grandparent.left = newParent;
            else
                grandparent.right = newParent;
        }


        /** Fix the double black problem at node parent */
        private void FixDoubleBlack(
            TNode<E> grandparent, TNode<E> parent,
            TNode<E> db, List<TNode<E>> path, int i)
        {
            // Obtain y, y1, and y2
            TNode<E> y = (parent.right == db) ?
              (TNode<E>)(parent.left) : (TNode<E>)(parent.right);
            TNode<E> y1 = (TNode<E>)(y.left);
            TNode<E> y2 = (TNode<E>)(y.right);

            if (y.Black && y1 != null && y1.Red)
            {
                if (parent.right == db)
                {
                    // Case 1.1: y is a left black sibling and y1 is red
                    ConnectNewParent(grandparent, parent, y);
                    Recolor(parent, y, y1); // Adjust colors

                    // Adjust child links
                    parent.left = y.right;
                    y.right = parent;
                }
                else
                {
                    // Case 1.3: y is a right black sibling and y1 is red        
                    ConnectNewParent(grandparent, parent, y1);
                    Recolor(parent, y1, y); // Adjust colors

                    // Adjust child links
                    parent.right = y1.left;
                    y.left = y1.right;
                    y1.left = parent;
                    y1.right = y;
                }
            }
            else if (y.Black && y2 != null && y2.Red)
            {
                if (parent.right == db)
                {
                    // Case 1.2: y is a left black sibling and y2 is red
                    ConnectNewParent(grandparent, parent, y2);
                    Recolor(parent, y2, y); // Adjust colors

                    // Adjust child links
                    y.right = y2.left;
                    parent.left = y2.right;
                    y2.left = y;
                    y2.right = parent;
                }
                else
                {
                    // Case 1.4: y is a right black sibling and y2 is red        
                    ConnectNewParent(grandparent, parent, y);
                    Recolor(parent, y, y2); // Adjust colors

                    // Adjust child links
                    y.left = parent;
                    parent.right = y1;
                }
            }
            else if (y.Black)
            {
                // Case 2: y is black and y's children are black or null
                y.SetRed(); // Change y to red
                if (parent.Red)
                    parent.SetBlack(); // Done
                else if (parent != root)
                {
                    // Propagate double black to the parent node
                    // Fix new appearance of double black recursively
                    db = parent;
                    parent = grandparent;
                    grandparent =
                      (i >= 3) ? path[i - 3] : null;
                    FixDoubleBlack(grandparent, parent, db, path, i - 1);
                }
            }
            else
            { // y.Red
                if (parent.right == db)
                {
                    // Case 3.1: y is a left red child of parent
                    parent.left = y2;
                    y.right = parent;
                }
                else
                {
                    // Case 3.2: y is a right red child of parent
                    parent.right = y.left;
                    y.left = parent;
                }

                parent.SetRed(); // Color parent red
                y.SetBlack(); // Color y black
                ConnectNewParent(grandparent, parent, y); // y is new parent
                FixDoubleBlack(y, parent, db, path, i - 1);
            }
        }
        /** Recolor parent, newParent, and c. Case 1 removal */
        private void Recolor(TNode<E> parent,
            TNode<E> newParent, TNode<E> c)
        {
            // Retain the parent's color for newParent
            if (parent.Red)
                newParent.SetRed();
            else
                newParent.SetBlack();

            // c and parent become the children of newParent, set them black
            parent.SetBlack();
            c.SetBlack();
        }
        #endregion // delete
    }

    // 前缀和
    class PreSum
    {
        int[] sums;
        public PreSum(int[] nums)
        {
            sums = new int[nums.Length];
            sums[0] = nums[0];
            for (int i = 1; i < nums.Length; ++i)
                sums[i] = sums[i - 1] + nums[i];
        }
        public int RangeSum(int start, int end) => sums[end] - (start > 0 ? sums[start - 1] : 0);
    }


    static class Common
    {

        class Point
        {
            public int x;
            public int y;

            internal void Deconstruct(out int x, out int y)
            {
                x = this.x; y = this.y;
            }
        }

        /// <summary>
        /// 计算几何 - 叉积
        /// </summary>
        public static int CalcProduct(int[] p0, int[] p1, int[] p2)
        {
            int x1 = p1[0] - p0[0], y1 = p1[1] - p0[1];
            int x2 = p2[0] - p0[0], y2 = p2[1] - p0[1];
            return x1 * y2 - x2 * y1;
        }

        public static (int x, int y) UnPair(int[] pair) => (pair[0], pair[1]);

        internal static void test()
        {
            List<int[]> al = new List<int[]>();
            int[] ar = { 10, 20 };
            al.Add(ar);
            (int x, int y) = UnPair(al.First());
            Console.WriteLine($"x={x} y={y}");

            Point p = new Point { x = 40, y = 50 };
            (x, y) = p;

        }

        internal static int[] ReadArray(int id)
        {
            string path = $"..\\..\\..\\TestCase{id:D4}.json";
            return JsonConvert.DeserializeObject<int[]>(File.ReadAllText(path));
        }

        internal static T ReadInput<T>(int id)
        {
            string path = $"..\\..\\..\\TestCase{id:D4}.json";
            return JsonConvert.DeserializeObject<T>(File.ReadAllText(path));
        }
    }

    public static partial class SolutionExtensions
    {
        const int MOD = 1000000007;
        /// <summary>
        /// add and MOD 10^9+7
        /// </summary>
        public static int Add(this int x, int y)
        {
            int sum = x + y;
            if (sum >= MOD) sum -= MOD;
            return sum;
        }
    }
}
