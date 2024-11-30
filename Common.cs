using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

namespace ConsoleCore1;

/// <summary>
/// 数论库
/// </summary>
static class MathEX
{
    // 返回10进制的位数
    internal static int DigitCount(this int n) => n < 10 ? 1 : 1 + DigitCount(n / 10);
    internal static int DigitCount(this long n) => n < 10L ? 1 : 1 + DigitCount(n / 10);
    // 返回2进制的位数
    internal static int BitDigitCount(this int n) => n <= 1 ? 1 : 1 + BitDigitCount(n >> 1);
    // 10进制拆位
    internal static IEnumerable<int> EnumDigits(this int n)
    {
        Stack<int> stk = new();
        while (n > 9)
        {
            stk.Push(n % 10);
            n /= 10;
        }
        stk.Push(n);
        while (stk.Any()) yield return stk.Pop();
    }

    internal static int Gcd(int a, int b) => b != 0 ? Gcd(b, a % b) : a;

    // factor[n] 表示 n的最大因子，所以 n 是质数 iff factor[n] == n
    // 注意，factor[1]设为0
    static int[] factor = Array.Empty<int>();

    /// <summary>
    /// 初始化因子表
    /// </summary>
    internal static int[] FactorTable(this int n)
    {
        if (factor.Length > n) return factor;
        factor = new int[n + 1];
        int i, j;
        for (i = 3; i <= n; factor[i] = i, i += 2) ;
        for (i = 2; i <= n; factor[i] = 2, i += 2) ;
        for (i = 2; i <= n >> 1; i++)
        {
            for (; factor[i] != i; i++) ;
            for (j = i + i; j <= n; factor[j] = i, j += i) ;
        }
        return factor;
    }

    internal static bool IsPrime(this int n)
    {
        if (factor == null || factor.Length <= n) FactorTable(n);
        return factor[n] == n;
    }

    /// <summary>
    /// 分解质因数 - 从大到小枚举返回(factor, count)
    /// </summary>
    internal static IEnumerable<(int, int)> EnumFactors(this int n)
    {
        FactorTable(n);
        while (n > 1)
        {
            int f = factor[n], c = 1;
            n /= f;
            while (n % f == 0)
            {
                n /= f;
                ++c;
            }
            yield return (f, c);
        }
    }

    /// <summary>
    /// 分解质因数（返回List，从小到大）
    /// </summary>
    internal static IList<int> GetFactors(this int n)
    {
        FactorTable(n);
        List<int> a = new();
        for (int i = 2; i * i <= n; ++i)
            if (IsPrime(i))
                while (n % i == 0)
                {
                    n /= i;
                    a.Add(i);
                }
        if (n > 1) a.Add(n);
        return a;
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
    internal static int PowerMod(this int x, int b, int m)
    {
        long r, a = x;
        if (m == 1 || a % m == 0 && b != 0) return 0;
        for (a %= m, r = 1L; b > 0; r = (b & 1) != 0 ? r * a % m : r, a = a * a % m, b >>= 1) ;
        return (int)(r % m);
    }

    #region 排列组合
    // len(a) ^ n
    internal static IEnumerable<IList<T>> Prod<T>(this IList<T> a, int n)
    {
        T[] ans = new T[n];
        int i = 0, j = 0;
        Stack<int> stk = new();
        while (j < a.Count || i > 0)
        {
            if (i == n)
            {
                yield return ans;
                j = a.Count;
            }
            if (j == a.Count)
            {
                j = stk.Pop() + 1;
                --i;
            }
            else
            {
                ans[i] = a[j];
                stk.Push(j);
                ++i;
                j = 0;
            }
        }
    }

    // len(a) ^ n general ver (a -> 0..m - 1)
    internal static IEnumerable<int[]> Prod(this int m, int n)
    {
        int[] ans = new int[n];
        int i = 0, j = 0;
        while (j < m || i > 0)
        {
            if (i == n)
            {
                yield return ans;
                j = m;
            }
            if (j == m)
            {
                j = ans[--i] + 1;
            }
            else
            {
                ans[i++] = j;
                j = 0;
            }
        }
    }

    #endregion 排列组合
}

/// <summary>
/// 位运算模板
/// </summary>
static class BitOperationsEX
{
    /// <summary>
    /// 统计二进制中1的个数
    /// </summary>
    internal static int CountOne(this int n) => n == 0 ? 0 : n == -2147483648 ? 1 : 1 + CountOne(n & (n - 1));

    /// <summary>
    /// 统计0到n中对应i位出现1的个数
    /// </summary>
    internal static long CountAllBit(this long n, int i)
    {
        int j = i + 1;
        long d = n >> j, m = n & (1L << j) - 1;
        return (d << i) + Math.Max(0L, m - (1L << i));
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

    // 当前格子是否位于外围边界上
    internal static bool IsEdge(this int[][] mx, int i, int j)
        => i == 0 || i == mx.Length - 1 || j == 0 || j == mx[0].Length - 1;

    /// <summary>
    /// 查找连续区域，并涂成目标颜色（非递归实现回溯）
    /// </summary>
    /// <returns>区域块个数</returns>
    // 测试：LC 695 or [LCR 105. 岛屿的最大面积](https://leetcode.cn/problems/ZL6zAn/)
    internal static int DfsFill(this int[][] mx, int i, int j, int color)
    {
#if DEBUG
        Debug.Assert(mx != null && mx.Length > 0 && mx[0] != null && mx[0].Length > 0 && i >= 0 && i < mx.Length && j >= 0 && j < mx[0].Length && mx[i][j] != color);
#endif
        int[] d = { 0, -1, 0, 1, 0 };
        Stack<(int, int, int)> stk = new();
        int orgColor = mx[i][j], nc = 1, x = i, y = j, z = 1;
        mx[i][j] = color;
        while (z < 5 || stk.Any())
        {
            if (z == 5)
            {
                (x, y, z) = stk.Pop();
                continue;
            }
            int nextX = x + d[z - 1], nextY = y + d[z];
            if (nextX >= 0 && nextX < mx.Length && nextY >= 0 && nextY < mx[0].Length)
            {
                if (mx[nextX][nextY] == orgColor)
                {
                    ++nc;
                    mx[nextX][nextY] = color;
                    stk.Push((x, y, z + 1));
                    (x, y, z) = (nextX, nextY, 1);
                    continue;
                }
            }
            ++z;
        }
        return nc;
    }
    #endregion 图论-矩阵

    #region 图论-树/二叉树
	/// <summary>
    /// 前序遍历（非递归）
    /// </summary>
	public static IEnumerable<TreeNode> PreOrder(this TreeNode root)
	{
		Stack<TreeNode> stk = new();
		TreeNode? cur = root;
		while (cur != null || stk.Any())
        {
            if (cur == null)
            {
                cur = stk.Pop();
            }
            else
            {
				yield return cur;
				if (cur.right != null)
					stk.Push(cur.right);
                cur = cur.left;
            }
        }
    }
	
	
    /// <summary>
    /// 中序遍历（非递归）
    /// </summary>
    public static IEnumerable<TreeNode> MidOrder(this TreeNode root)
    {
        Stack<TreeNode> stk = new();
        var cur = root;
        while (cur != null || stk.Any())
        {
            if (cur == null)
            {
                cur = stk.Pop();
                yield return cur;
                cur = cur.right;
            }
            else
            {
                stk.Push(cur);
                cur = cur.left;
            }
        }
    }
	
    /// <summary>
    /// 后序遍历（非递归）
    /// </summary>	
	public static IEnumerable<TreeNode> PostOrder(this TreeNode root)
	{
		// TODO 
		Stack<TreeNode> stk = new();
		TreeNode? cur = root;
		while (cur != null || stk.Any())
        {
            if (cur == null)
            {
                cur = stk.Pop();
            }
            else
            {
				yield return cur;
				if (cur.right != null)
					stk.Push(cur.right);
                cur = cur.left;
            }
        }
    }

    /// <summary>
    /// 无向树形图，节点编号0到n-1，边权数为n-1
    /// (带边权的请用WeightedTreeGraph）
    /// </summary>
    /// <param name="n">如果节点编号从1开始请传入n+1</param>
    /// <returns></returns>
    internal static List<int>[] TreeGraph(this int[][] edges, int n = 0)
    {
        if (n == 0) n = edges.Length + 1;
        var tg = Enumerable.Range(0, n).Select(_ => new List<int>()).ToArray();
        foreach (var ed in edges)
        {
            tg[ed[0]].Add(ed[1]);
            tg[ed[1]].Add(ed[0]);
        }
        return tg;
    }

    // 有边权的无向树
    internal static List<(int, int)>[] WeightedTreeGraph(this int[][] edges, int n = 0)
    {
        if (n == 0) n = edges.Length + 1;
        var tg = Enumerable.Range(0, n).Select(_ => new List<(int, int)>()).ToArray();
        foreach (var ed in edges)
        {
            tg[ed[0]].Add((ed[1], ed[2]));
            tg[ed[1]].Add((ed[0], ed[2]));
        }
        return tg;
    }

    /// <summary>
    /// 无向树的DFS（非递归模板）
    /// </summary>
    internal static void TreeDfs(this IList<int>[] tg)
    {
        Stack<(int, int)> dfsStack = new();
        int nodeIndex = 0, childIndex = 0;
        while (true)
        {
            if (childIndex == 0)
            {
                Debug.WriteLine("PreOrder In. current Node = " + nodeIndex + ", depth = " + dfsStack.Count);
            }

            // skip parent
            if (childIndex < tg[nodeIndex].Count && dfsStack.Any() && dfsStack.Peek().Item1 == tg[nodeIndex][childIndex]) ++childIndex;

            // recursion
            if (childIndex == tg[nodeIndex].Count)
            {
                Debug.WriteLine("PostOrder Out. current Node = " + nodeIndex);

                if (dfsStack.Any()) (nodeIndex, childIndex) = dfsStack.Pop();
                else break;
            }
            else
            {
                dfsStack.Push((nodeIndex, childIndex + 1));
                (nodeIndex, childIndex) = (tg[nodeIndex][childIndex], 0);
            }
        }
    }

    // 树形图递归（模板2）
    internal static void TreeDfs2(this IList<int>[] tg)
    {
        const int DFS_ROOT = 0;
        Stack<(int Node, int Child)> dfsStk = new();
        int i = DFS_ROOT, childIndex = 0;
        while (true)
        {
            if (childIndex == tg[i].Count)
            {
                int parent = dfsStk.Any() ? dfsStk.Peek().Node : -1;
                Debug.WriteLine("dfs out. currentIndex = s, parent = " + parent);

                if (dfsStk.Any())
                {
                    (i, childIndex) = dfsStk.Pop();
                    continue;
                }
                else break;
            }
            else if (childIndex == 0)
            {
                Debug.WriteLine("dfs in");
            }

            int nextIndex = tg[i][childIndex];

            if (dfsStk.Any() && dfsStk.Peek().Node == nextIndex)
            {
                ++childIndex;
                continue;
            }
            else
            {   // dfs next
                dfsStk.Push((i, childIndex + 1));
                (i, childIndex) = (nextIndex, 0);
            }
        }
    }

    // DEF: The diameter of a tree is the length of the longest path between any two nodes in the tree.
    internal static int TreeDiameter(this List<int>[] tg)
    {
        int n = tg.Length, dm = 1, dmi = 0;
        Span<int> a = stackalloc int[n];
        Queue<int> qu = new();
        for (int r = 0; r < 2; ++r)
        {
            if (r > 0) a.Clear();
            a[dmi] = 1;
            qu.Enqueue(dmi);
            while (qu.Any())
            {
                int i = qu.Dequeue();
                foreach (int j in tg[i])
                    if (a[j] == 0)
                    {
                        a[j] = a[i] + 1;
                        if (a[j] > dm)
                        {
                            dm = a[j];
                            dmi = j;
                        }
                        qu.Enqueue(j);
                    }
            }
        }
        return dm - 1;
    }
    #endregion

    #region 图论-最短路径
    /// <summary>
    /// 曼哈顿距离：d = |x1 - x2| + |y1 - y2|
    /// </summary>
    internal static int Dist(this (int x, int y) p1, (int x, int y) p2)
        => Math.Abs(p1.x - p2.x) + Math.Abs(p1.y - p2.y);
    /// <summary>
    /// 枚举所有边，支持去重
    /// </summary>
    internal static IEnumerable<(int, int, int)> EnumLengthEdges(this int[][] edges, bool isReverse = false)
    { 
        var el = isReverse ?
        edges.OrderBy(t => t[1]).ThenBy(t => t[0]).ThenBy(t => t[2]).Select(ed => (ed[1], ed[0], ed[2])) :
        edges.OrderBy(t => t[0]).ThenBy(t => t[1]).ThenBy(t => t[2]).Select(ed => (ed[0], ed[1], ed[2]));
        int s0 = -1, t0 = -1;
        foreach ((int s, int t, int l) in el)
        {
            if (s == s0 && t == t0) continue;
            else yield return (s, t, l);
            (s0, t0) = (s, t);
        }
    }

    /// <summary>
    /// 有向图，无边长
    /// </summary>
    internal static IDictionary<int, IList<int>> DirectedGraphNoLength(this int[][] edges)
    {
        IDictionary<int, IList<int>> dg = new Dictionary<int, IList<int>>();
        foreach (var edge in edges)
        {
            int a = edge[0], b = edge[1];
            if (!dg.ContainsKey(a)) dg[a] = new List<int>();
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
        foreach ((int sr, int dt, int le) in edges.EnumLengthEdges(isReverse))
            if (dg.TryGetValue(sr, out var li))
                li.Add((dt, le));
            else
                dg[sr] = new() { (dt, le) };
        return dg;
    }

    internal static Dictionary<int, List<(int, int)>> UndirectedGraphWithLength(this int[][] edges)
    {
        Dictionary<int, List<(int, int)>> ug = new();
        foreach ((int a, int b, int l) in edges.EnumLengthEdges())
        {
            if (!ug.ContainsKey(a)) ug[a] = new();
            ug[a].Add((b, l));
            if (!ug.ContainsKey(b)) ug[b] = new();
            ug[b].Add((a, l));
        }
        return ug;
    }

    // n确定时可以直接用数组
    internal static List<(int, int)>[] UndirectedGraphWithLength(this int[][] edges, int n)
    {
        var ug = Enumerable.Range(0, n).Select(_ => new List<(int, int)>()).ToArray();
        foreach ((int a, int b, int l) in edges.EnumLengthEdges())
        {
            ug[a].Add((b, l));
            ug[b].Add((a, l));
        }
        return ug;
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

    #region 图论-其他
    /// <summary>
    /// 构建无向图（双向图）
    /// </summary>
    internal static StarGraph<int, int> BuildGraph(this int[][] edges, bool isUg = false)
    {
        StarGraph<int, int> sg = new();
        foreach (var ed in edges)
        {
            int s = ed[0], t = ed[1], w = ed.Length > 2 ? ed[2] : 1;
            sg.AddEdge(s, t, w);
            if (isUg)
                sg.AddEdge(t, s, w);
        }
        return sg;
    }

    /// <summary>
    /// Hierholzer 算法 - 寻找欧拉路径
    /// </summary>
    /// <returns>留意输出的路径为反方向</returns>
    // 算法描述：https://leetcode.cn/problems/reconstruct-itinerary/solutions/389885/zhong-xin-an-pai-xing-cheng-by-leetcode-solution/
    // 模板题：P2097合法重新排列数对
    internal static IEnumerable<int> HierholzerDfs(this IDictionary<int, IList<int>> dg)
    {
        // determine start node
        Counter<int> ind = new();
        foreach (var ts in dg.Values)
            foreach (int t in ts)
                ++ind[t];
        int start = dg.Keys.FirstOrDefault(s => dg[s].Count == ind[s] + 1);
        if (start == 0 && !dg.ContainsKey(start))
            start = dg.Keys.First();

        // dfs using (answer keep in stack)
        Stack<int> euler = new();
        euler.Push(start);
        while (euler.Any())
        {
            int s = euler.Peek();
            if (dg.TryGetValue(s, out var t) && t.Any())
            {
                euler.Push(t.Last());
                t.RemoveAt(t.Count - 1);
            }
            else yield return euler.Pop();
        }
    }
    #endregion
}

/// <summary>
/// 基于链式前向星构建的图的封装类
/// </summary>
/// <typeparam name="K">支持节点类型为泛型</typeparam>
/// <typeparam name="V">支持边权值类型为泛型</typeparam>
public class StarGraph<K, V> 
    where K: notnull
    where V: IComparable
{
    readonly Dictionary<K, LinkEdge?> head = new();
    class LinkEdge
    {
        public readonly K To;
        public readonly V W;
        public readonly LinkEdge? Next;
        public LinkEdge(K t, V w, LinkEdge? n)
        {
            To = t;
            W = w;
            Next = n;
        }
    }

    public void AddEdge(K u, K v, V w)
    {
        head.TryAdd(u, null);
        head[u] = new(v, w, head[u]);
}

    public IEnumerable<K> Nexts(K u)
        => Edges(u).Select(t => t.Item1);

    public IEnumerable<(K, V)> Edges(K u)
    {
        for (var ed = head.ContainsKey(u) ? head[u] : null; ed != null; ed = ed.Next)
            yield return (ed.To, ed.W);
    }
}

/// <summary>
/// 字符串算法
/// </summary>
static class TextEX
{
    /// <summary>
    /// KMP关键算法-next映射
    /// </summary>
    public static int[] KmpBuildNext(this string s)
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

    /// <summary>
    /// 字符串匹配 - 在s中找到第一个p就返回
    /// </summary>
    public static int SearchKmp(this string s, string p)
    {
        int[] next = p.KmpBuildNext();
        int j = 0;
        for (int i = 0; i < s.Length; i++)
        {
            while (j >= 0 && s[i] != p[j])
            {
                if (j == 0)
                    j = -1;
                else
                    j = next[j - 1] + 1;
            }
            if (++j == p.Length)
                return i - j + 1;
        }
        return -1;
    }

    // https://oi-wiki.org/string/z-func/
    /// <summary>
    /// Z函数：返回一个数组z，其中z[s]表示s[s:]与s的最大公共前缀的长度
    /// </summary>
    // 范例题：P3303第一个几乎相等子字符串的下标
    internal static int[] ZFunction(this string s)
    {
        int n = s.Length;
        int[] z = new int[n];
        for (int i = 1, l = 0, r = 0; i < n; ++i)
        {
            if (i <= r && z[i - l] < r - i + 1)
            {
                z[i] = z[i - l];
            }
            else
            {
                z[i] = Math.Max(0, r - i + 1);
                while (i + z[i] < n && s[z[i]] == s[i + z[i]]) ++z[i];
            }
            if (i + z[i] - 1 > r)
            {
                l = i;
                r = i + z[i] - 1;
            }
        }
        return z;
    }

    /// <summary>
    /// 全域查找，返回数组dp，其中dp[s]为从i开始查找得到与p的最大匹配前缀的长度
    /// </summary>
    public static int[] SearchAllKmp(this string s, string p)
    {
        int[] dp = new int[s.Length], next = p.KmpBuildNext(), ps = p.ZFunction();

        // 滑动窗口：当前相等的最大子串为s[l..(l+j)]
        // 当出现要根据next移位时，l 移动到 r， 同时沿路维护dp
        int l = 0, j = 0;
        for (int i = 0; i < s.Length; ++i)
        {
            while (j >= 0 && (j == p.Length || s[i] != p[j]))
            {
                if (j == 0)
                {
                    j = -1;
                }
                else
                {
                    j = next[j - 1] + 1;
                }
            }
            int r = i - j, l0 = l;
            while (l < r)
            {
                if (++l < i)
                {
                    dp[l] = Math.Min(i - l, ps[l - l0]); 
                }
            }
            if (j >= 0) dp[l] = j + 1;
            ++j;
        }
        return dp;
    }

    /// <summary>
    /// 回文串高效算法 - Manacher(俗称马拉车): 返回每个字符对应的臂长（暂时只支持奇数长度的回文串）
    /// </summary>
    // 用法参见 P1960两个回文子字符串长度的最大乘积
    public static int[] Manacher(this string s)
    {
        int Expand(int i, int l = 1) => i - l < 0 || i + l >= s.Length || s[i - l] != s[i + l] ? l - 1 : Expand(i, l + 1);
        int[] arms = new int[s.Length];
        int i = 1;
        while (i < s.Length - 1)
        {
            arms[i] = Expand(i, arms[i] + 1);
            if (arms[i] == 0) ++i;
            else
            {
                for (int l = 1; l <= arms[i]; ++l)
                {
                    arms[i + l] = Math.Min(arms[i - l], arms[i] - l);
                    if (arms[i + l] + l >= arms[i])
                    {
                        i = i + l;
                        break;
                    }
                }
            }
        }
        return arms;
    }
}

/// <summary>
/// 仿Python的Counter
/// </summary>
public class Counter<T>
{
    readonly Dictionary<T, int> dic = new();
    public int Count => dic.Count;
    public int this[T key]
    {
        get => dic.TryGetValue(key, out var v) ? v : (dic[key] = 0);
        set
        {
            if (value == 0) dic.Remove(key);
            else dic[key] = value;
        }
    }
}

static class ReuseFunctions
{
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
    public virtual TreeNode? left { get; set; }
    public virtual TreeNode? right { get; set; }
    public TreeNode(int x)
    {
        val = x;
    }
    public TreeNode(int val = 0, TreeNode? left = null, TreeNode? right = null)
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
    readonly Func<V, V, bool> comp;
    readonly bool _keepKey = false;
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

    readonly List<K> _list = new();
    readonly Dictionary<K, (V val, int ind)> _dic = new();
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
    public int this[int index] => Find(index);
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
        TNode<E> u = te; // path[s];

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
                FixDoubleRed(u, v, path, i - 2); // s - 2 propagates upward
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

    readonly Func<E, E, int> comp;
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
/// 离散化
/// </summary>
class DiscreteNums<T> where T : IComparable
{
    readonly Dictionary<T, int> mp;
    readonly IList<T> orgValues;
    public DiscreteNums(IList<T> a)
    {
        orgValues = a;
        SortedSet<T> ss = new();
        foreach (var t in a) ss.Add(t);
        mp = new();
        int i = 0;
        foreach (var t in ss) mp[t] = i++;
    }
    public int Count => orgValues.Count;
    public int this[int index] => mp[orgValues[index]];
    public IEnumerable<int> EnumValues() => orgValues.Select(t => mp[t]);
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
        for (++i; i > 0; sum += arr[i], i &= i - 1); // s -= s & -s);
        return sum;
    }
    public int Get(int i)
    {
        int sum = arr[++i];
        for (int next = i - 1, end = i - (i & -i); next > end; sum -= arr[next], next -= next & -next) ;
        return sum;
    }
}

// 数值类型为long的树状数组
class FenwickLong
{
    readonly long[] _arr;
    public int Length => _arr.Length;
    internal long this[int index]
    {
        get => _arr[index - 1];
        set => _arr[index - 1] = value;
    }
    public FenwickLong(int n) => _arr = new long[n];
    public void Update(int i, long inc = 1L)
    {
        for (++i; i <= Length; this[i] += inc, i += i & -i) ;
    }
    public long Sum(int i)
    {
        long sum = 0;
        for (++i; i > 0; sum += this[i], i &= i - 1) ; 
        return sum;
    }
    public long Get(int i)
    {
        long sum = this[++i];
        for (int next = i - 1, end = i - (i & -i); next > end; sum -= this[next], next &= next - 1) ;
        return sum;
    }
}

/// <summary>
/// 树状数组，维护前缀最大值
/// </summary>
// 例题：力扣2286. 以组为单位订音乐会的门票
class MaxFenwick<T> where T : IComparable<T>
{
    readonly T[] _arr, a;
    public MaxFenwick(int n, T initValue)
    {
        _arr = new T[n];
        a = new T[n];
        Array.Fill(_arr, initValue);
        Array.Fill(a, initValue);
    }
    public int Length => _arr.Length;
    internal T this[int index]
    {
        get => _arr[index - 1];
        set => _arr[index - 1] = value;
    }

    /// <summary>
    /// 更新为一个更大的值
    /// </summary>
    public void Up(int i, T val)
    {
        if (val.CompareTo(a[i]) > 0)
            for (a[i++] = val; i <= Length; i += i & -i)
                if (val.CompareTo(this[i]) > 0)
                    this[i] = val;
                else 
                    break;
    }

    /// <summary>
    /// 更新为一个更小的值
    /// </summary>
    public void Down(int i, T val)
    {
        if (val.CompareTo(a[i]) < 0)
        {
            a[i] = val;
            Adjust(i + 1);
        }
    }

    /// <summary>
    /// 单点更新到较小值时，需要扫描全域看最大值是否有更新
    /// </summary>
    void Adjust(int index)
    {
        if (index > Length) return;
        T nextValue = a[index - 1];

        for (int i = index - 1, end = index & index - 1; i > end; i &= i - 1)
            if (this[i].CompareTo(nextValue) > 0)
                nextValue = this[i];

        if (nextValue.CompareTo(this[index]) < 0)
        {
            this[index] = nextValue;
            Adjust(index + (index & -index));
        }
    }

    public T Max(int i)
    {
        T mv = this[i + 1];
        for (i &= i + 1; i > 0; i &= i - 1)
            if (this[i].CompareTo(mv) > 0)
                mv = this[i];
        return mv;
    }

    public T Get(int i) => a[i];
}

/// <summary>
/// 线段树（位运算用）：每个节点只有0或1，支持区间翻转，以及统计区间内1的个数
/// </summary>
/// <remarks>
/// 操作：Flip - 将区间内所有的01翻转，并返回更新之后的Count
///       Update - 单点操作
/// </remarks>
class BitSegmentTree
{
    readonly int N;
    readonly (int, int)[] a;
    public int Count => a[0].Item2;
    public BitSegmentTree(int n)
    {
        N = n;
        int size = 1;
        while (size < n) size <<= 1;
        size <<= 1;
        a = new (int, int)[size];
    }

    public int Flip(int l, int r, int i = 0, int start = 0, int end = -1)
    {
        if (end == -1) end = N - 1;
        if (l == start && r == end)
        {
            (int b, int c) = a[i];
            b ^= 1;
            c = r - l + 1 - c;
            a[i] = (b, c);
            return c;
        }
        int m = start + end >> 1, lc = a[i << 1 | 1].Item2, rc = a[i + 1 << 1].Item2;
        if (l <= m) lc = Flip(l, Math.Min(r, m), i << 1 | 1, start, m);
        if (r > m) rc = Flip(Math.Max(m + 1, l), r, i + 1 << 1, m + 1, end);
        int t = lc + rc; if (a[i].Item1 == 1) t = end - start + 1 - t;
        a[i] = (a[i].Item1, t);
        return a[i].Item2;
    }

    public void Update(int i) => Flip(i, i);
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
    readonly BitArray ba;
    int map;
    public B4Array(int n) => ba = new(n << 1);
        // n > 16 ? new(n << 1) : null; // avoid set null
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

// BitSetTree - 20230620
// 用于快速判断某子集是否已被包含于bist中，例如存在1011，则check(1001)也为true
class BitSetTree
{
    readonly BitArray ba;
    public BitSetTree(int numBits) => ba = new(1 << numBits + 1);
    public bool this[int m]
    {
        get => m == 0 ? ba[0] : CheckBits(m);
        set
        {
            if (m == 0) ba[0] = value;
            else if (value) SetBits(m);
            else UnsetBits(m);
        }
    }

    bool CheckBits(int m, int i = 1) =>
        ba[i] &&
        (m == 1 ? ba[i << 1 | 1] :
        (m & 1) != 0 ? CheckBits(m >> 1, i << 1 | 1) :
        CheckBits(m >> 1, i << 1) || CheckBits(m >> 1, i << 1 | 1));

    void SetBits(int m, int i = 1)
    {
        ba[i] = true;
        if (m == 1)
            ba[i << 1 | 1] = true;
        else if ((m & 1) != 0)
            SetBits(m >> 1, i << 1 | 1);
        else
            SetBits(m >> 1, i << 1);
    }

    void UnsetBits(int m) { } // TODO
}

static class Common
{
    #region 计算几何
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
    #endregion

    #region 二分查找

    /// <summary>
    /// 查找排列序列中数值范围在[lo,hi]的区间
    /// </summary>
    internal static (int l, int r) BinaryRangeSearch<T>(this IList<T> list, T lo, T hi) where T: IComparable<T>
    {
        if (lo.CompareTo(hi) > 0 || list?.Any() != true || hi.CompareTo(list.First()) < 0 || lo.CompareTo(list.Last()) > 0)
            return (0, -1);
            
        int l1 = 0, r1 = list.Count - 1, l2 = 0, r2 = list.Count - 1;
        // lower bound, meanwhile restrict upper bound
        while (l1 < r1)
        {
            int mid = l1 + (r1 - l1 >> 1);
            if (list[mid].CompareTo(lo) < 0)
            {
                l1 = mid + 1;
            }
            else
            {
                r1 = mid;
                if (list[mid].CompareTo(hi) > 0)
                {
                    r2 = mid - 1;
                }
                else
                {
                    l2 = mid;
                }
            }
        }
        if (l1 - 1 > l2) l2 = l1 - 1;

        // upperbound
        while (l2 < r2)
        {
            int mid = l2 + (r2 - l2 + 1 >> 1);
            if (list[mid].CompareTo(hi) > 0)
            {
                r2 = mid - 1;
            }
            else
            {
                l2 = mid;
            }
        }

        return (l1, r2);

    }

    internal static int RangeCount(this (int l, int r) range) => range.r - range.l + 1;

    #endregion

    #region 字典更新
    // 通用的快速写法（将TKEY替换成真正的类型），小于则是val < 0
    //bool Update(Dictionary<TKEY, int> dp, TKEY key_set, int val)
    //    => (!dp.TryGetValue(key_set, out var v0) || val > v0) && (dp[key_set] = val) >= 0;

    public static bool UpdateBigger<K, V>(this Dictionary<K, V> d, K key, V val) where V: IComparable<V>
        => (!d.TryGetValue(key, out var v0) || val.CompareTo(v0) > 0) && (d[key] = val).CompareTo(val) == 0;

    public static bool UpdateSmaller<K, V>(this Dictionary<K, V> d, K key, V val) where V : IComparable<V>
        => (!d.TryGetValue(key, out var v0) || val.CompareTo(v0) < 0) && (d[key] = val).CompareTo(val) == 0;
    #endregion

    /// <summary>
    /// 调试用，位运算状态解压缩并输出所有位，例如 5 => { 0 2 }
    /// </summary>
    public static string ShowBits(this int m)
    {
        StringBuilder bu = new();
        bu.Append('{');
        for (int i = 0; i < 31; ++i)
            if ((1 << i & m) != 0)
                bu.Append(' ').Append(i);
        bu.Append(" }");
        return bu.ToString();
    }

    /// <summary>
    /// 从小到大枚举非负数序列的子序列和
    /// </summary>
    // 例题：P2386
    public static IEnumerable<long> OrderedSum(this IEnumerable<int> nums)
    {
        yield return 0L;

        var it = nums.GetEnumerator();
        List<int> a = new();
        PriorityQueue<(long, int, int), long> pq = new();
        if (it.MoveNext())
        {
            a.Add(it.Current);
            pq.Enqueue((a[0], 0, int.MaxValue), a[0]);
        }
        while (pq.Count > 0)
        {
            (long sm, int i, int limit) = pq.Dequeue();
            yield return sm;
            if (i + 1 == a.Count)
            {
                if (it.MoveNext()) a.Add(it.Current);
                else limit = i;
            }
            if (i + 1 <= limit)
            {
                long nxs = sm - a[i] + a[i + 1];
                pq.Enqueue((nxs, i + 1, limit), nxs);
            }
            if (i > 0) {
                long nxs = sm + a[0];
                pq.Enqueue((nxs, 0, i - 1), nxs);
            }
        }
    }

    // OrderedSum (数组确定版本）
    public static IEnumerable<long> OrderedSumOfList(this IList<int> a)
    {
        yield return 0L;
        if (!a.Any()) yield break;

        PriorityQueue<(long, int, int), long> pq = new();
        pq.Enqueue((a[0], 0, a.Count - 1), a[0]);
        while (pq.Count > 0)
        {
            (long sm, int i, int limit) = pq.Dequeue();
            yield return sm;
            if (i + 1 <= limit)
            {
                long nxs = sm - a[i] + a[i + 1];
                pq.Enqueue((nxs, i + 1, limit), nxs);
            }
            if (i > 0)
            {
                long nxs = sm + a[0];
                pq.Enqueue((nxs, 0, i - 1), nxs);
            }
        }
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
    public IList<Interval> Cut(Interval other)
    {
        if (other.Count <= 0) return new Interval[] { new() { start = start, end = end, val = val } }; 
        List<Interval> li = new(2);
        if (other.start > start) li.Add(new() { start = start, end = Math.Min(end, other.start - 1), val = val });
        if (other.end < end) li.Add(new() { start = Math.Max(start, other.end + 1), end = end, val = val });
        return li;
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

    // a ** b % MOD
	// 模板题：LC2961
    public static int Pow(this int x, int b)
    {
        if (MOD == 1 || x % MOD == 0 && b != 0) return 0;
        long a = x % MOD, r = 1L;
        for (; b > 0; r = (b & 1) != 0 ? r * a % MOD : r, a = a * a % MOD, b >>= 1) ;
        return (int)r;
    }

    // 返回全组合列表(C(m, n) mod 1e9+7 for All(m, n))
    public static int[][] AllCombinations(this int n)
    {
        int[][] ca = new int[n + 1][];
        for (int r = 0; r <= n; ++r)
        {
            ca[r] = new int[r + 1];
            for (int c = 0; c <= r; ++c)
                ca[r][c] = c == 0 || c == r ? 1 : (ca[r - 1][c - 1] + ca[r - 1][c]) % MOD;
        }
        return ca;
    }

    // 缓存所有的阶乘
    public static int[] AllFactorials(this int n)
    {
        int[] a = new int[n + 1];
        a[0] = 1;
        for (int m = 1; m <= n; ++m)
            a[m] = a[m - 1].Multi(m);
        return a;
    }
}

/* Divide: 除法：求 t = x / y (mod 1e9+7)
 * 例如 3 / 2 = 500000005 （= (3 + 1000000007) / 2)
 * 除法貌似只能暴力解
 */

/// <summary>
/// 常用排序算法
/// </summary>
static class SortEX
{
    // 冒泡排序
    internal static void BubbleSort<T>(this IList<T> a) where T : IComparable<T>
    {
        int bound, exchange = a.Count - 1;
        while (exchange != 0)
        {
            bound = exchange;
            exchange = 0;
            for (int i = 0; i < bound; i++)
                if (a[i].CompareTo(a[i + 1]) > 0)
                {
                    (a[i], a[i + 1]) = (a[i + 1], a[i]);
                    exchange = i;
                }
        }
    }

    // 选择排序
    internal static void SelectSort<T>(this IList<T> a) where T : IComparable<T>
    {
        for (int i = 0; i < a.Count - 1; i++)
        {
            int index = i;
            for (int j = i + 1; j < a.Count; j++)
                if (a[j].CompareTo(a[index]) < 0)
                    index = j;
            if (index != i)
                (a[i], a[index]) = (a[index], a[i]); //swap(a, s, index);
        }
    }

    // 插入排序
    internal static void InsertSort<T>(this IList<T> a) where T : IComparable<T>
    {
        for (int i = 1; i < a.Count; i++)
        {// 从第 2 个记录开始执行
            T t = a[i];                     // 将待插入元素暂存于变量 t 
            int j = i - 1;		
            for (; j >= 0 && t.CompareTo(a[j]) < 0; --j) // 搜索插入位置，注意不要越界
                a[j + 1] = a[j];			// 记录后移
            a[j + 1] = t;					// 完成 t 的插入操作
        }
    }

    // 归并排序
    internal static IList<T> MergeSort<T>(this IList<T> a, int left = 0, int right = -1) where T : IComparable<T>
    {
        if (right == -1) right = a.Count - 1;

        // 边界条件：没有元素或 1 个元素直接返回
        if (left > right) return Array.Empty<T>();
        if (left == right) return new T[] { a[left] };

        // 划分并求解子问题
        int mid = left + (right - left) / 2;
        IList<T> a1 = MergeSort(a, left, mid);
        IList<T> a2 = MergeSort(a, mid + 1, right);

        // 合并
        T[] c = new T[a1.Count + a2.Count];
        int i1 = 0, i2 = 0;
        while (i1 < a1.Count && i2 < a2.Count)
        {
            if (a1[i1].CompareTo(a2[i2]) <= 0)
            {
                c[i1 + i2] = a1[i1];
                ++i1;
            }
            else
            {
                c[i1 + i2] = a2[i2];
                ++i2;
            }
        }

        // 不要忘记对子数组的剩余部分进行合并

        while (i1 < a1.Count)
        {
            c[i1 + i2] = a1[i1];
            ++i1;
        }

        while (i2 < a2.Count)
        {
            c[i1 + i2] = a2[i2];
            ++i2;
        }

        return c;
    }

    // （快速排序核心算法）对 a[p..r] 进行划分操作
    static int Partition<T>(IList<T> a, int p, int r) where T : IComparable<T>
    {
        T pivot = a[p];   // 选定轴值
        int i = p, j = r;   // 初始化左右指针
        while (i < j)
        {
            while (i < j && pivot.CompareTo(a[j]) <= 0)
                j--;
            if (i < j)
            {
                (a[i], a[j]) = (a[j], a[i]); // swap(a, s, j);
                i++;
            }
            while (i < j && pivot.CompareTo(a[i]) >= 0)
                i++;
            if (i < j)
            {
                (a[i], a[j]) = (a[j], a[i]); // swap(a, s, j);
                j--;
            }
        }
        return i;
    }

    // 快速排序
    internal static void QuickSort<T>(this IList<T> a, int p = 0, int r = -1) where T: IComparable<T>
    {
        if (r == -1) r = a.Count - 1;
        if (p < r)
        {
            int q = Partition(a, p, r); // 划分，并返回轴值在数组中的位置
            QuickSort(a, p, q - 1);     // 递归求解左子数组
            QuickSort(a, q + 1, r);     // 递归求解右子数组
        }
    }

    // 拓扑排序
    internal static List<T> TopoSort<T>(this IList<IList<T>> ord)
    {
        Dictionary<T, List<T>> g = new();
        HashSet<T> s = new();
        Dictionary<T, int> cn = new();
        foreach (var p in ord)
        {
            var x = p[0];
            var y = p[1];
            if (!g.TryGetValue(x, out var li))
                g[x] = li = new();
            li.Add(y);
            if (cn.TryGetValue(y, out int c))
            {
                cn[y] = ++c;
            }
            else
            {
                cn[y] = c = 1;
            }
            if (c == 1) s.Remove(y);
            if (!cn.TryGetValue(x, out int xc) || xc == 0) s.Add(x);
        }
        List<T> a = new();
        while (s.Any())
        {
            var x = s.First();
            s.Remove(x);
            a.Add(x);
            if (g.TryGetValue(x, out var nt))
                foreach (var y in nt)
                    if (--cn[y] == 0)
                    {
                        s.Add(y);
                        cn.Remove(y);
                    }
        }
        if (cn.Any(kv => kv.Value > 0)) a.Clear();
        return a;
    }

    internal static List<int> TopoSort(this int[][] ord, int startInd, int nCount)
    {
        var a = ord.TopoSort();
        if (!a.Any() || a.Count == nCount) return a;
        else
        {
            HashSet<int> hs = new();
            for (int i = startInd; i < startInd + nCount; ++i)
                hs.Add(i);
            foreach (int j in a) hs.Remove(j);
            a.AddRange(hs);
            return a;
        }
    }
}