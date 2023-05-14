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
    /// <summary>
    /// 数论库
    /// </summary>
    static class MathEX
    {
        internal static int Gcd(int a, int b) => b != 0 ? Gcd(b, a % b) : a;

        // factor[n] 表示 n的最大因子，所以 n 是质数 iff factor[n] == n
        static int[] factor = Array.Empty<int>();

        internal static int[] FactorTable(int n)
        {
            factor = new int[n + 1];
            int i, j;
            for (i = 1; i <= n; factor[i] = i, i += 2) ;
            for (i = 2; i <= n; factor[i] = 2, i += 2) ;
            for (i = 2; i <= n >> 1; i++)
            {
                for (; factor[i] != i; i++) ;
                for (j = i + i; j <= n; factor[j] = i, j += i) ;
            }
            return factor;
        }

        internal static bool IsPrime(int n)
        {
            if (factor == null || factor.Length <= n) FactorTable(n);
            return factor[n] == n;
        }

        static int[] phi;

        // 在数论，对正整数n，欧拉函数是小于等于n的正整数中与n互质的数的数目.
        // 打表 phi[n] will store Euler's Phi(n).
        internal static int[] EulerPhiTable(int n)
        {
            FactorTable(n);
            phi = new int[n + 1];
            int i, j;
            phi[1] = 1;
            for (i = 3; i <= n; i += 2)
            {
                j = i / factor[i];
                phi[i] = phi[j] * (factor[i] - (j % factor[i] != 0 ? 1 : 0));
            }
            for (i = 2; i <= n; i += 2)
                phi[i] = (i & 3) != 0 ? phi[i >> 1] : (phi[i >> 1] << 1);
            return phi;
        }

        // 直接计算
        internal static long GetEulerPhi(long x)
        {
            long res = x, a = x;
            for (int i = 2; i * i <= a; ++i)
                if (a % i == 0)
                {
                    res = res / i * (i - 1);
                    while (a % i == 0) a /= i;
                }
            if (a > 1) res = res / a * (a - 1);
            return res;
        }

        // ### 阶乘求余 Power Mod
        // Returns a^b (mod c). Assume c>0 and 0^0=1.
        internal static int PowerMod(long a, int b, int m)
        {
            long r;
            if (m == 1 || a % m == 0 && b != 0) return 0;
            for (a %= m, r = 1L; b > 0; r = (b & 1) != 0 ? r * a % m : r, a = a * a % m, b >>= 1) ;
            return (int)(r % m);
        }
    }

        /// <summary>
    /// 图论库
        /// </summary>
    static class GraphEX
    {
        #region 图论-矩阵

        // pairwise(d)即为四个方向的增量
        internal static int[] d = { 0, -1, 0, 1, 0 };

        /// <summary>
        /// 矩阵枚举上下左右四个方向
        /// </summary>
        internal static IEnumerable<(int ni, int nj)> FourDir<T>(this T[][] mx, int i, int j)
        {
            if (i > 0) yield return (i - 1, j);
            if (i < mx.Length - 1) yield return (i + 1, j);
            if (j > 0) yield return (i, j - 1);
            if (j < mx[i].Length - 1) yield return (i, j + 1);
        }
        internal static IEnumerable<(int ni, int nj)> FourDir(int m, int n, int i, int j)
        {
            if (i > 0) yield return (i - 1, j);
            if (i < m - 1) yield return (i + 1, j);
            if (j > 0) yield return (i, j - 1);
            if (j < n - 1) yield return (i, j + 1);
        }

        /// <summary>
        /// 矩阵枚举周围8个方向
        /// </summary>
        internal static IEnumerable<(int ni, int nj)> EightDir(this int[][] mx, int i, int j)
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

        /// <summary>
        /// 枚举最外围的所有点
        /// </summary>
        internal static IEnumerable<(int, int)> Outers(this int[][] mx)
        {
            for (int i = 0; i < mx.Length; ++i)
            {
                if (i == 0 || i == mx.Length - 1)
                    for (int j = 0; j < mx[i].Length; ++j)
                        yield return (i, j);
                else
                {
                    yield return (i, 0);
                    if (mx[i].Length > 1) yield return (i, mx[i].Length - 1);
                }
            }
        }
        #endregion 
		
        #region 图论-最短路径
        /// <summary>
        /// 曼哈顿距离：d = |x1 - x2| + |y1 - y2|
        /// </summary>
        internal static int Dist(this (int x, int y) p1, (int x, int y) p2)
            => Math.Abs(p1.x - p2.x) + Math.Abs(p1.y - p2.y);
        internal static IEnumerable<(int, int, int)> EnumLengthEdges(this int[][] edges, bool isReverse = false)
            => isReverse ?
            edges.OrderBy(t => t[1]).ThenBy(t => t[0]).ThenBy(t => t[2]).Select(ed => (ed[1], ed[0], ed[2])) :
            edges.OrderBy(t => t[0]).ThenBy(t => t[1]).ThenBy(t => t[2]).Select(ed => (ed[0], ed[1], ed[2]));
        
        /// <summary>
        /// 有向图，无边长
        /// </summary>
        internal static Dictionary<int, List<int>> DirectedGraphNoLength(this int[][] edges)
        {
            Dictionary<int, List<int>> dg = new();
            foreach (var edge in edges)
            {
                int a = edge[0], b = edge[1];
                if (!dg.ContainsKey(a)) dg[a] = new();
                dg[a].Add(b);
            }
            return dg;
        }

        /// <summary>
        /// edge=(src,dst,len),支持重复边（取最短边），最终生成字典表
        /// </summary>
        internal static Dictionary<int, List<(int, int)>> DirectedGraphWithLength(this int[][] edges, bool isReverse = false)
        {
            Dictionary<int, List<(int, int)>> dg = new();
            int s0 = -1, t0 = -1;
            foreach ((int sr, int dt, int le) in edges.EnumLengthEdges(isReverse))
            {
                if (sr == s0 && dt == t0) continue;
                if (sr != s0) dg[sr] = new();
                dg[sr].Add((dt, le));
                s0 = sr; t0 = dt;
            }
            return dg;
        }

        internal static Dictionary<int, List<(int, int)>> UndirectedGraphWithLength(this int[][] edges)
        {
            Dictionary<int, List<(int, int)>> dg = new();
            foreach ((int a, int b, int l) in edges.EnumLengthEdges())
            {
                if (!dg.ContainsKey(a)) dg[a] = new();
                dg[a].Add((b, l));
                if (!dg.ContainsKey(b)) dg[b] = new();
                dg[b].Add((a, l));
            }
            return dg;
        }

        /// <summary>
        /// 无向图，边长为1
        /// </summary>
        /// <param name="edges"></param>
        /// <param name="n">需要创建空列表时请输入n</param>
        /// <returns>目标节点字典表</returns>
        internal static Dictionary<int, List<int>> UndirectedGraphNoLength(this int[][] edges, int n = 0)
        {
            Dictionary<int, List<int>> ug = new();
            if (n > 0)
            {
                for (int i = 0; i < n; ++i) ug[i] = new();
                foreach (var ed in edges)
                {
                    int a = ed[0], b = ed[1];
                    ug[a].Add(b);
                    ug[b].Add(a);
                }
            }
            else
            {
                foreach (var ed in edges)
                {
                    int a = ed[0], b = ed[1];
                    if (!ug.ContainsKey(a)) ug[a] = new();
                    ug[a].Add(b);
                    if (!ug.ContainsKey(b)) ug[b] = new();
                    ug[b].Add(a);
                }
            }
            return ug;
        }

        internal static long Dijkstra(this int[][] edges, int src, int dest, bool isDirectGraph = true)
        {
            var dg = isDirectGraph ?
                edges.DirectedGraphWithLength() :
                edges.UndirectedGraphWithLength();
            SHeap<int, long> hp = new((a, b) => a < b, true);
            hp.Add(src, 0L);
            while (hp.Any())
            {
                (int s, long p) = hp.Pop();
                if (s == dest) return p;
                if (!dg.ContainsKey(s)) continue;
                foreach ((int nt, int w) in dg[s])
                    hp.Add(nt, p + w);
            }
            return -1;
        }
        internal static long[] DijkstraAll(this int[][] edges, int n, int src, bool isReverse = false)
        {
            long[] arr = new long[n];
            Array.Fill(arr, -1L);
            var dg = edges.DirectedGraphWithLength(isReverse);
            Queue<int> qu = new();
            qu.Enqueue(src);
            arr[src] = 0L;
            while (qu.Any())
            {
                int s = qu.Dequeue();
                if (!dg.ContainsKey(s)) continue;
                foreach ((int nt, int w) in dg[s])
                {
                    long l = arr[s] + w;
                    if (arr[nt] < 0L || l < arr[nt])
                    {
                        arr[nt] = l;
                        qu.Enqueue(nt);
                    }
                }
            }
            return arr;
        }

        /// <summary>
        /// 适用于无向图且边长为1，求源点到所有点之间的最短路径
        /// </summary>
        internal static int[] BfsPaths(this int[][] edges, int n, int src = 0)
        {
            int[] arr = new int[n];
            Array.Fill(arr, -1);
            var ug = edges.UndirectedGraphNoLength();
            Queue<int> qu = new();
            qu.Enqueue(src);
            arr[src] = 0;
            while (qu.Any())
            {
                int s = qu.Dequeue();
                foreach (int next in ug[s].Where(t => arr[t] < 0))
                {
                    arr[next] = arr[s] + 1;
                    qu.Enqueue(next);
                }
            }
            return arr;
        }
        #endregion
    }
    static class ReuseFunctions
    {
        /// <summary>
        /// KMP关键算法-next映射
        /// </summary>
        internal static int[] Kmp(this string s)
        {
            int[] next = new int[s.Length];
            next[0] = -1;

            for (int i = 1; i < next.Length; ++i)
            {
                int nexti = next[i - 1] + 1;
                while (nexti >= 0 && s[nexti] != s[i])
                    nexti = nexti > 0 ? next[nexti - 1] + 1 : -1;
                next[i] = nexti;
            }

            return next;
        }

        // 单调栈
        internal static Stack<(int, int)> BuildStack(this int[] nums)
        {
            Stack<(int, int)> stk = new();
            int n = -1;
            for (int i = 0; i < nums.Length; ++i)
                if (nums[i] > n)
                {
                    n = nums[i];
                    stk.Push((i, n));
                }
            return stk;
        }

        /// <summary>
        /// 最长递增子序列（贪心+二分解法）(from 300)
        /// </summary>
        internal static int LIS(this IEnumerable<int> nums)
        {
            List<int> inds = new();
            foreach (int n in nums)
            {
                int i = ~inds.BinarySearch(n);
                if (i == inds.Count) inds.Add(n);
                else if (i >= 0) inds[i] = n;
            }
            return inds.Count;
        }

        internal static BitArray ToBitArray(this int n, Func<int, bool> tf)
        {
            BitArray ba = new(n);
            for (int i = 0; i < n; ++i)
                ba[i] = tf(i);
            return ba;
        }

        /// <summary>
        /// 统计二进制中1的个数
        /// </summary>
        internal static int CountOne(this int n) => n == 0 ? 0 : n == -2147483648 ? 1 : 1 + CountOne(n & (n - 1));

        /// <summary>
        /// 返回第一个大于等于target的位置
        /// </summary>
        public static int LowerBound(this IList<int> list, int target, int l = 0, int r = -1)
        {
            if (r < 0) r = list.Count;
            if (l == r) return l;
            int mid = l + r >> 1;
            if (list[mid] < target) return list.LowerBound(target, mid + 1, r);
            else return list.LowerBound(target, l, mid);
        }

        /// <summary>
        /// 返回第一个严格大于target的位置
        /// </summary>
        public static int UpperBound(this IList<int> list, int target, int l = 0, int r = -1)
        {
            if (r < 0) r = list.Count;
            if (l == r) return l;
            int mid = l + r >> 1;
            if (list[mid] <= target) return list.UpperBound(target, mid + 1, r);
            else return list.UpperBound(target, l, mid);
        }
    }

    // Comparer
    /// <summary>
    /// 根据权值比较，然后是ij坐标
    /// </summary>
    class MatrixComparer : IComparer<(int i, int j, int l)>
    {
        public int Compare((int i, int j, int l) a, (int i, int j, int l) b)
            => a.l != b.l ? a.l.CompareTo(b.l) :
               a.i != b.i ? a.i.CompareTo(b.i) :
               a.j.CompareTo(b.j);
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
        List<T> _list = new List<T>();
        T GetAt(int index) => _list[index - 1];
        public bool Any() => _list.Any();
        public int Count => _list.Count;
        public T Head => _list[0];
        void Swap(int i, int j) => (_list[i - 1], _list[j - 1]) = (_list[j - 1], _list[i - 1]);
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
        public bool PushPop(T val)
        {
            if (comp(Head, val))
            {
                _list[0] = val;
                SwapDown(1);
                return true;
            }
            return false;
        }
        public T Pop()
        {
            T val = _list.Last();
            _list.RemoveAt(_list.Count - 1);
            return PopPush(val);
        }
        public T PopPush(T val)
        {
            T ans = _list[0];
            _list[0] = val;
            SwapDown(1);
            return ans;
        }
        void SwapDown(int i)
        {
            int inext = i << 1;
            if (inext <= Count)
            {
                if (inext < Count && Compare(inext + 1, inext)) ++inext;
                if (Compare(inext, i))
                {
                    Swap(i, inext);
                    SwapDown(inext);
                }
            }
        }
        public T[] ToArray() => _list.ToArray();
    }

    /// <summary>
    /// 超级堆
    /// </summary>
    public class SHeap<K, V>
    {
        Func<V, V, bool> comp;
        bool _keepKey = false;
        /// <summary>
        /// 在Dijkstra等需要维护一次访问的场景记得设置KeepKey=true
        /// </summary>
        public SHeap(Func<V, V, bool> comp, bool keepKey = false)
        {
            this.comp = comp;
            _keepKey = keepKey;
        }
        bool Compare(int i, int j) => comp(GetAt(i), GetAt(j));

        V GetAt(int index) => _dic[_list[index - 1]].val;

        public V this[K key]
        {
            get => _dic[key].val;
            set => Add(key, value);
        }
 
        List<K> _list = new();
        Dictionary<K, (V val, int ind)> _dic = new();
        public bool Any() => _list.Any();
        public int Count => _list.Count;
        public bool ContainsKey(K key) => _dic.ContainsKey(key);

        public (K, V) Head => (_list[0], _dic[_list[0]].val);
        public K HeadKey => _list[0];
        public V HeadValue => _dic[_list[0]].val;
        void Swap(int i, int j)
        {
            (_list[j - 1], _list[i - 1]) = (_list[i - 1], _list[j - 1]);
            _dic[_list[i - 1]] = (_dic[_list[i - 1]].val, i);
            _dic[_list[j - 1]] = (_dic[_list[j - 1]].val, j);
        }

        public bool Add(K key, V val)
        {
            if (ContainsKey(key))
            {
                if (comp(val, _dic[key].val))
                {
                    (V _, int i) = _dic[key];
                    _dic[key] = (val, i);
                    SwapUp(i);
                    return true;
                }
                else return false;
            }
            _list.Add(key);
            _dic.Add(key, (val, Count));
            SwapUp(Count);
            return true;
        }

        public (K key, V val) Pop()
        {
            (K key, V val) = Head;
            Remove(key);
            return (key, val);
        }

        public void Remove(K key)
        {
            (V _, int i) = _dic[key];
            RemoveAt(i);
            if (!_keepKey) _dic.Remove(key);
        }

        void RemoveAt(int i)
        {
            if (i == Count)
            {
                _list.RemoveAt(i - 1);
                return;
            }

            Swap(i, Count);
            _list.RemoveAt(_list.Count - 1);

            if (!SwapUp(i)) SwapDown(i);
        }

        bool SwapUp(int ind)
        {
            int i = ind, inext = i >> 1;
            while (inext > 0 && Compare(i, inext))
            {
                Swap(i, inext);
                i = inext; inext = i >> 1;
            }
            return i != ind;
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
        public UnionFind(int n) => parent = Enumerable.Range(0, n).ToArray();
        public bool Check(int index1, int index2) => Find(index1) == Find(index2);
        public void Union(int index1, int index2) => parent[Find(index2)] = Find(index1);
        public int Find(int index) => parent[index] != index ? (parent[index] = Find(parent[index])) : parent[index];
        public int GroupCount() => Enumerable.Range(0, parent.Length).Select(i => Find(i)).Distinct().Count();
    }

	/* 并查集 - 连通图专用（节点编号1-n，实时维护GroupCount）- P1579
	class UnionFind
	{
		int[] parent;
		public int GroupCount {get; private set;}
		public UnionFind(int n) { 
			parent = Enumerable.Range(0, n).ToArray();
			GroupCount = n;
		}
		public bool Check(int index1, int index2) => Find(index1) == Find(index2);
		public void Union(int index1, int index2) {
			if (!Check(index1, index2)) {
				parent[Find(index2)] = Find(index1);
				--GroupCount;
			}
		}
		public int Find(int index) => parent[index - 1] != index - 1 ? (parent[index - 1] = Find(parent[index - 1] + 1)) : parent[index - 1];
	}
	*/
	
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
                    {
                        // Duplicate node not inserted	
                        return false; 
                    }
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
            // Index to the current node in the path
            int i = path.Count - 1; 

            // u is the last node in the path. u contains element e
            TNode<E> u = te; // path[i];

            // v is the parent of of u, if exists
            TNode<E> v = path[i - 1];

            // It is OK to set u red    
            u.SetRed(); 

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
            { 
                // Case 2: v's sibling x is red 
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

            // b becomes the root in the subtree
            b.SetBlack();
            // a becomes the left child of b
            a.SetRed();
            // c becomes the right child of b
            c.SetRed(); 
        }

        #region search node
        public IEnumerable<E> Range(E start, E end)
        {
            // step 1: search start and build stack
            Stack<TNode<E>> stk = new();
            TNode<E> current = root;
            while (current != null)
            {
                if (start.CompareTo(current.val) <= 0)
                {
                    stk.Push(current);
                    current = current.left;
                }
                else if (start.CompareTo(current.val) > 0)
                {
                    current = current.right;
                }
            }

            // step 2 range search
            while (stk?.Any() == true)
            {
                current = stk.Pop();
                if (current.val.CompareTo(end) >= 0) break;
                yield return current.val;
                current = current.right;
                while (current != null)
                {
                    stk.Push(current);
                    current = current.left;
                }
            }
        }
        //public bool Search(E e)
        //{
        //    TNode<E> current = root; // Start from the root
        //    while (current != null)
        //    {
        //        if (e.CompareTo(current.val) < 0)
        //        {
        //            current = current.left;
        //        }
        //        else if (e.CompareTo(current.val) > 0)
        //        {
        //            current = current.right;
        //        }
        //        else // element matches current.element
        //            return true; // Element is found
        //    }
        //    return false;
        //}
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
    
    // 红黑树（支持下标操作）
    public class TreeSet<E>
    {
        class TSN<T>
        {
            public TSN<T> Parent { get; private set; }

            private void UpdateCount()
            {
                int orgCount = Count;
                Count = (_left?.Count ?? 0) + (_right?.Count ?? 0) + 1;
                if (Count != orgCount && Parent != null) Parent.UpdateCount();
            }

            public TSN<T> Left
            {
                get => _left;
                set
                {
                    //if (_left != null) _left.Parent = null;
                    _left = value;
                    if (_left != null) _left.Parent = this;
                    UpdateCount();
                }
            }
            TSN<T> _left;

            public TSN<T> Right
            {
                get => _right;
                set
                {
                    //if (_right != null) _right.Parent = null;
                    _right = value;
                    if (_right != null) _right.Parent = this;
                    UpdateCount();
                }
            }
            TSN<T> _right;

            public int Count { get; private set; } = 1;
            public int LeftCount => (_left?.Count ?? 0) + 1;
            public T Val { get; private set; }
            public TSN(T e) => Val = e;
            bool red = false;
            public bool Red { get => red; set => red = value; }
            public bool Black { get => !red; set => red = !value; }

            public TSN<T> SetAsRoot()
            {
                Parent = null;
                return this;
            }
        }

        Func<E, E, int> comp;
        int Compare(E a, E b)
            => comp != null ? comp(a, b) : (a as IComparable<E>).CompareTo(b);
        public TreeSet(Func<E, E, int> cmp = null) => comp = cmp;

        TSN<E> root;
        public int Count => root?.Count ?? 0;
        public int Add(E e)
        {
            if (root == null) return (root = new(e)).Count - 1;
            else
            {
                int ind = 0;
                TSN<E> parent = null;
                TSN<E> current = root;
                bool pleft = false;
                while (current != null)
                {
                    int r = Compare(e, current.Val);
                    if (r < 0)
                    {
                        parent = current;
                        current = current.Left;
                        pleft = true;
                    }
                    else if (r > 0)
                    {
                        ind += current.LeftCount;
                        parent = current;
                        current = current.Right;
                        pleft = false;
                    }
                    else return ~ind;
                }

                if (pleft)
                    current = parent.Left = new(e);
                else
                    current = parent.Right = new(e);

                EST(current);
                return ind;
            }
        }

        public E this[int i]
        {
            get
            {
                if (i < 0 || i >= Count) throw new IndexOutOfRangeException();
                var current = root;
                int ind = current.LeftCount - 1;
                while (ind != i)
                {
                    if (i < ind)
                    {
                        ind = ind - current.LeftCount + current.Left.LeftCount;
                        current = current.Left;
                    }
                    else
                    {
                        ind += current.Right.LeftCount;
                        current = current.Right;
                    }
                }
                return current.Val;
            }
        }
        void EST(TSN<E> te)
        {
            TSN<E> u = te;
            TSN<E> v = te.Parent;
            u.Red = true;
            if (v.Red) FDR(u, v);
        }

        private void FDR(TSN<E> u, TSN<E> v)
        {
            TSN<E> w = v.Parent;
            TSN<E> parentOfw = w?.Parent;
            TSN<E> x = w.Left == v ? w.Right : w.Left;

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
                {
                    w.Black = true;
                }
                else if (parentOfw.Red)
                {
                    u = w;
                    v = parentOfw;
                    FDR(u, v);
                }
            }
        }

        private void RR(TSN<E> a, TSN<E> b, TSN<E> c, TSN<E> w, TSN<E> parentOfw)
        {
            if (parentOfw == null)
                root = b.SetAsRoot();
            else if (parentOfw.Left == w)
                parentOfw.Left = b;
            else
                parentOfw.Right = b;

            b.Black = true;
            a.Red = true;
            c.Red = true;
        }
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

    /// <summary>
    /// 树状数组Fenwick Tree
    /// </summary>
    class Fenwick
    {
        int[] arr;
        public Fenwick(int n) => arr = new int[n + 1];
        public void Update(int i, int inc = 1)
        {
            for (++i; i < arr.Length; arr[i] += inc, i += i & -i);
        }
        public int Sum(int i)
        {
            int sum = 0;
            for (++i; i > 0; sum += arr[i], i -= i & -i);
            return sum;
        }
        public int Get(int i)
        {
            int sum = arr[++i];
            for (int next = i - 1, end = i - (i & -i); next > end; sum -= arr[next], next -= next & -next) ;
            return sum;
        }
    }

    class ModInt
    {
        int mod;
        public int Value { get; private set; } = 0;
        public ModInt(int m = 1000000007) => mod = m;
        
        public int Add(int x)
        {
            Value += x;
            if (Value >= mod) Value -= mod;
            return Value;
        }
        public int Sub(int x)
        {
            Value -= x;
            if (Value < 0) Value += mod;
            return Value;
        }
        public int Multi(int x)
        {
            long p = (long)Value * x;
            p %= mod;
            return Value = (int)p;
        }

        public int MultiPow(long a, int b)
        {
            long r;
            if (mod == 1 || a % mod == 0 && b != 0) return 0;
            for (a %= mod, r = 1L; b > 0; r = (b & 1) != 0 ? r * a % mod : r, a = a * a % mod, b >>= 1) ;
            return Value = (int)(r * Value % mod);
        }

        // WRONG
        //public int Pow(int x)
        //{
        //    int r;
        //    if (mod == 1 || Value % mod == 0 && x != 0) return Value = 0;
        //    for (Value %= mod, r = 1; x != 0; r = (x & 1) != 0 ? r * Value % mod : r, Value = Value * Value % mod, x >>= 1) ;
        //    return Value = r % mod;
        //}
        #region 复制
        ModInt(ModInt b)
        {
            mod = b.mod;
            Value = b.Value;
        }
        public static ModInt operator +(ModInt a, int x)
        {
            ModInt b = new(a);
            b.Add(x);
            return b;
        }
        public static ModInt operator -(ModInt a, int x)
        {
            ModInt b = new(a);
            b.Sub(x);
            return b;
        }
        public static ModInt operator *(ModInt a, int x)
        {
            ModInt b = new(a);
            b.Multi(x);
            return b;
        }
        #endregion 
    }

    // 位数组拓展，每个元素取值1-4，占2个位
    class B4Array
    {
        BitArray ba;
        int map;
        public B4Array(int n) => ba = n > 16 ? new(n << 1) : null;
        int Get(int bi) => ba != null ? ba[bi] ? 1 : 0 : (map & 1 << bi) != 0 ? 1 : 0;
        void Set(int bi, bool val) 
        {
            if (ba != null) ba[bi] = val;
            else
            {
                int bit = 1 << bi;
                if ((map & bit) != 0 != val) map ^= bit;
            }
        }
        public int this[int index]
        {
            get => Get(index << 1) << 1 | Get(index << 1 | 1);
            set
            {
                Set(index << 1, (value & 2) != 0);
                Set(index << 1 | 1, (value & 1) != 0);
            }
        }
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

        internal static int[] ReadArray(int id)
        {
            string path = $"..\\..\\..\\TestCase{id:D4}.json";
            return JsonConvert.DeserializeObject<int[]>(File.ReadAllText(path));
        }

        internal static T ReadInput<T>(int id)
            => ToTestInput<T>(File.ReadAllText($"..\\..\\..\\TestCase{id:D4}.json"));

        internal static T ToTestInput<T>(this string json)
            => JsonConvert.DeserializeObject<T>(json);
    }
    #region 区间类

    // 更通用的封装类 - 850
    // TODO 应用于之前的题解
    public class InSet : SortedSet<Interval>
    {
        public int Length { get; private set; }

        public void Update(Interval r)
        {
            int val = r.val;
            Dictionary<int, InSet> di = new();
            InSet ir = new(); ir.Add(r);
            Interval sk = new() { start = r.start - 1, end = r.end + 1 };
            while (TryGetValue(sk, out var r0))
            {
                Remove(r0); Length -= r0.Count;
                if (ir.TryGetValue(r0, out r))
                {
                    ir.Remove(r);
                    var it = r0.Intersect(r);
                    it.val += r.val;
                    if (it.Valid)
                    {
                        if (!di.ContainsKey(it.val)) di[it.val] = new();
                        di[it.val].Combine(it);
                    }

                    foreach (var rs in r.Cut(it))
                        ir.Combine(rs);

                    foreach (var r0s in r0.Cut(it))
                    {
                        if (!di.ContainsKey(r0s.val)) di[r0s.val] = new();
                        di[r0s.val].Combine(r0s);
                    }
                }
                else
                {
                    if (!di.ContainsKey(r0.val)) di[r0.val] = new();
                    di[r0.val].Combine(r0);
                }
            }
            if (val > 0)
            {
                if (!di.ContainsKey(val)) di[val] = new();
                foreach (var rs in ir)
                    di[val].Combine(rs);
            }

            foreach ((int k, var s) in di)
                foreach (var si in s)
                {
                    Add(si);
                    Length += si.Count;
                }
        }

        bool Combine(Interval ra) // 就是Merge，为了不重复所以改名
        {
            while (TryGetValue(new() { start = ra.start - 1, end = ra.end + 1 }, out var r))
            {
                Remove(r);
                (ra.start, ra.end) = (Math.Min(r.start, ra.start), Math.Max(r.end, ra.end));
            }
            return Add(ra);
        }
    }

    /**
     * 注意：只适用于不相交区间
     * */

    public class Interval : IComparable<Interval>
    {
        public int val = 1; // new added
        public int start, end;
        public Interval() { }
        public Interval(int val) => start = end = val;
        public int Count => end - start + 1;
        public bool Valid => end >= start && val > 0;
        public int CompareTo(Interval other)
            => Math.Max(start, other.start) <= Math.Min(end, other.end) ? 0 :
               start < other.start ? -1 : 1;

        public Interval Merge(Interval other)
            => new() { start = Math.Min(start, other.start), end = Math.Max(end, other.end), val = val };

        public Interval Intersect(Interval other)
            => new() { start = Math.Max(start, other.start), end = Math.Min(end, other.end), val = val };

        // 截取一段区间，返回剩下的区间（有可能是0-2个）
        public Interval[] Cut(Interval other)
        {
            if (other.Count <= 0) return new Interval[] { new() { start = start, end = end, val = val } }; 
            List<Interval> li = new();
            if (other.start > start) li.Add(new() { start = start, end = Math.Min(end, other.start - 1), val = val });
            if (other.end < end) li.Add(new() { start = Math.Max(start, other.end + 1), end = end, val = val });
            return li.ToArray();
        }
    }
    public static class IntervalExtensions
    {
        // 最新最优 // from 731
        public static bool Merge(this SortedSet<Interval> s, Interval ra)
        {
            while (s.TryGetValue(new() { start = ra.start - 1, end = ra.end + 1 }, out var r))
            {
                s.Remove(r);
                (ra.start, ra.end) = (Math.Min(r.start, ra.start), Math.Max(r.end, ra.end));
            }
            return s.Add(ra);
        }

        // 该方法添加任意区间 // from P715
        public static void AddRange(this SortedSet<Interval> sort, int left, int right)
        {
            Interval rs = new() { start = left, end = right },
                lv = new Interval(rs.start - 1), rv = new Interval(rs.end + 1);
            foreach (var r in new List<Interval>(sort.GetViewBetween(lv, rv)))
            {
                sort.Remove(r);
                if (r.start < rs.start) rs.start = r.start;
                if (r.end > rs.end) rs.end = r.end;
            }
            sort.Add(rs);
        }

        // 移除任意区间 // from P715
        public static void RemoveRange(this SortedSet<Interval> sort, int left, int right)
        {
            Interval lv = new Interval(left), rv = new Interval(right);
            foreach (var r in new List<Interval>(sort.GetViewBetween(lv, rv)))
            {
                if (r.start >= left && r.end <= right)
                    sort.Remove(r);
                else if (r.start < left && r.end > right)
                {
                    sort.Remove(r);
                    sort.Add(new() { start = r.start, end = left - 1 });
                    sort.Add(new() { start = right + 1, end = r.end });
                }
                else if (r.start < left)
                    r.end = left - 1;
                else if (r.end > right)
                    r.start = right + 1;
            }
        }

        // 该方法假设添加的区间一定和现有的不相交
        public static void MergeOne(this SortedSet<Interval> sort, Interval rs)
        {
            // if (sort.Contains(rs)) return; // 注意！如果题目没有假设新增的区间一定是不相交的，则这句是必要的！
            Interval left = new Interval(rs.start - 1),
                right = new Interval(rs.end + 1);
            if (sort.TryGetValue(left, out var rl))
            {
                if (sort.TryGetValue(right, out var rr))
                {
                    sort.Remove(rr);
                    rl.end = rr.end;
                }
                else
                {
                    rl.end = rs.end;
                }
            }
            else
            {
                if (sort.TryGetValue(right, out var rr))
                {
                    rr.start = rs.start;
                }
                else
                    sort.Add(rs);
            }
        }
        public static void MergeInterval(this SortedSet<Interval> sort, SortedSet<Interval> s1)
        {
            foreach (var rs in s1)
                MergeOne(sort, rs);
        }
    }
    #endregion
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
        public static int Sub(this int x, int y)
        {
            int ans = x - y;
            if (ans < 0) ans += MOD;
            return ans;
        }
        public static int Multi(this int x, int y)
        {
            long p = (long)x * y;
            p %= MOD;
            return (int)p;
        }

        public static int Divide(this int x, int y)
        {
            long p = x;
            while (p % x != 0) p += MOD;
            return (int)(p / x);
        }
    }

    /* Divide: 除法：求 t = x / y (mod 1e9+7)
     * 例如 3 / 2 = 500000005 （= (3 + 1000000007) / 2)
     * 除法貌似只能暴力解
     */
}
