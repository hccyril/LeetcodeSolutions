using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using ConsoleCore1;

namespace ConsoleCore1.contest
{
    // 2023/4/30
    // 第4题很简单，但前面三题难度有所增加
    // 第3题做了40分钟，WA，总结：有传送门的是有向，无传送的为无向，这里理解错了
    internal class LC_WC0343_20230430
    {
        #region Problem A
        // 在力扣周赛里算复杂的第一题
        // 赛后总结：封装了ToBitArray方法
        public int IsWinner(int[] player1, int[] player2)
        {
            int n = player1.Length;
            BitArray b1 = n.ToBitArray(i => player1[i] == 10),
                b2 = n.ToBitArray(i => player2[i] == 10);
            //bool[] b1 = player1.Select(t => t == 10).ToArray(),
            //        b2 = player2.Select(t => t == 10).ToArray();
            for (int i = 0; i < n; ++i)
            {
                if (i > 0 && b1[i - 1] || i > 1 && b1[i - 2]) player1[i] <<= 1;
                if (i > 0 && b2[i - 1] || i > 1 && b2[i - 2]) player2[i] <<= 1;
            }
            int s1 = player1.Sum(), s2 = player2.Sum();
            if (s1 > s2) return 1;
            else if (s1 < s2) return 2;
            else return 0;
        }
        #endregion

        #region Problem B
        // B - 比赛后补做
        // 超简单，但比赛时先做C没做出来，B看都没看T_T
        public int FirstCompleteIndex(int[] arr, int[][] mat)
        {
            int m = mat.Length, n = mat[0].Length;
            Dictionary<int, (int, int)> di = new();
            for (int i = 0; i < m; ++i)
                for (int j = 0; j < n; ++j)
                    di[mat[i][j]] = (i, j);
            int[] rowCnt = new int[m], colCnt = new int[n];

            for (int i = 0; i < arr.Length; ++i)
            {
                (int r, int c) = di[arr[i]];
                if (++rowCnt[r] == n || ++colCnt[c] == m)
                    return i;
            }

            return -1;
        }
        #endregion

        #region Problem C

        // WA: 理解错，传送门是有向的，现场处理曼哈顿距离是无向的时候就把这个忘记了
        // 待完成：可优化地方：只需要维护传送门的终点，不需要维护起点
        // 修改后AC的代码：



        void AddEdge(Dictionary<(int, int), int> edi, int s, int t, int dist)
        {
            if (s != t)
            {
                //if (s > t) (s, t) = (t, s); -> !Wrong!
                if (!edi.ContainsKey((s, t)) || edi[(s, t)] > dist)
                    edi[(s, t)] = dist;
            }
        }

        int AddPoint((int x, int y) p, List<(int, int)> li, Dictionary<(int, int), int> di, Dictionary<(int, int), int> edi)
        {
            if (!di.ContainsKey(p))
            {
                int id = di[p] = li.Count;
                li.Add(p);
                for (int i = 0; i < li.Count - 1; ++i)
                {
                    var p0 = li[i];
                    var d = p0.Dist(p);
                    AddEdge(edi, i, id, d);
                    AddEdge(edi, id, i, d);
                }
                return id;
            }
            return di[p];
        }

        public int MinimumCost(int[] start, int[] target, int[][] specialRoads)
        {
            if (start[0] == target[0] && start[1] == target[1]) return 0;
            List<(int, int)> li = new();
            Dictionary<(int, int), int> di = new();
            (int x, int y) = (start[0], start[1]);
            di[(x, y)] = 0; li.Add((x, y));
            (x, y) = (target[0], target[1]);
            di[(x, y)] = 1; li.Add((x, y));
            Dictionary<(int, int), int> edi = new();
            edi[(0, 1)] = li[0].Dist(li[1]);
            //int[] a = { 0, 1, Dist(li[0], li[1]) }; edges.Add(a);
            foreach (int[] sr in specialRoads)
            {
                (int, int) p1 = (sr[0], sr[1]), p2 = (sr[2], sr[3]);
                int i1 = AddPoint(p1, li, di, edi);
                int i2 = AddPoint(p2, li, di, edi);
                AddEdge(edi, i1, i2, sr[4]);
            }

            var edges = edi.Select(kv => new int[] { kv.Key.Item1, kv.Key.Item2, kv.Value }).ToArray();
            Console.WriteLine("Debug");
            foreach (var ed in edges)
            {
                Console.WriteLine(string.Join(" ", ed));
            }
            return (int)edges.Dijkstra(0, 1);
        }

        // bakup 2023/4/30 12:00
#if 比赛现场提交的代码
public class Solution {
            int Dist((int x, int y) p1, (int x, int y) p2)
        {
            return Math.Abs(p1.x - p2.x) + Math.Abs(p1.y - p2.y);
        }

        void AddEdge(Dictionary<(int, int), int> edi, int s, int t, int dist)
        {
            if (s != t)
            {
                if (s > t) (s, t) = (t, s);
                if (!edi.ContainsKey((s, t)) || edi[(s, t)] != dist)
                    edi[(s, t)] = dist;
            }
        }

        int AddPoint((int x, int y) p, List<(int, int)> li, Dictionary<(int, int), int> di, Dictionary<(int, int), int> edi)
        {
            if (!di.ContainsKey(p))
            {
                int id = di[p] = li.Count;
                li.Add(p);
                for (int i = 0; i < li.Count - 1; ++i)
                {
                    var p0 = li[i];
                    AddEdge(edi, i, id, Dist(p0, p));
                }
                return id;
            }
            return di[p];
        }
        public int MinimumCost(int[] start, int[] target, int[][] specialRoads)
        {
            if (start[0] == target[0] && start[1] == target[1]) return 0;
            List<(int, int)> li = new();
            Dictionary<(int, int), int> di = new();
            (int x, int y) = (start[0], start[1]);
            di[(x, y)] = 0; li.Add((x, y));
            (x, y) = (target[0], target[1]);
            di[(x, y)] = 1; li.Add((x, y));
            Dictionary<(int, int), int> edi = new();
            edi[(0, 1)] = Dist(li[0], li[1]);
            //int[] a = { 0, 1, Dist(li[0], li[1]) }; edges.Add(a);
            foreach (int[] sr in specialRoads)
            {
                (int, int) p1 = (sr[0], sr[1]), p2 = (sr[2], sr[3]);
                int i1 = AddPoint(p1, li, di, edi);
                int i2 = AddPoint(p2, li, di, edi);
                AddEdge(edi, i1, i2, sr[4]);
            }
            foreach (int[] sr in specialRoads)
            {
                int i1 = di[(sr[0], sr[1])];
                int i2 =  di[(sr[2], sr[3])];
                AddEdge(edi, i1, i2, sr[4]);
            }

            var edges = edi.Select(kv => new int[] { kv.Key.Item1, kv.Key.Item2, kv.Value }).ToArray();
            Console.WriteLine("Debug");
            foreach (var ed in edges) 
                Console.WriteLine(string.Join(" ", ed));
            return (int)edges.Dijkstra(0, 1);
        }
}

internal static class EX {
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
        internal static long Dijkstra(this int[][] edges, int src, int dest)
        {
            var dg = edges.UndirectedGraphWithLength();
            SHeap<int, long> hp = new((a, b) => a < b);
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
           internal static IEnumerable<(int, int, int)> EnumLengthEdges(this int[][] edges, bool isReverse = false)
            => isReverse ?
            edges.OrderBy(t => t[1]).ThenBy(t => t[0]).ThenBy(t => t[2]).Select(ed => (ed[1], ed[0], ed[2])) :
            edges.OrderBy(t => t[0]).ThenBy(t => t[1]).ThenBy(t => t[2]).Select(ed => (ed[0], ed[1], ed[2]));
 
}

   public class SHeap<K, V>
    {
        Func<V, V, bool> comp;
        public bool KeepKey { private get; set; } = false;
        public SHeap(Func<V, V, bool> comp) => this.comp = comp;
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
            if (!KeepKey) _dic.Remove(key);
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

#endif
        #endregion

        #region Problem D
        int N = 0;
        char Kc = 'a';

        string Dfs(List<char> a, int i)
        {
            if (i >= N) return string.Join("", a);
            for (char c = a[i]; c <= Kc; c++)
            {
                a[i] = c;
                if (i > 0 && a[i] == a[i - 1]) continue;
                if (i > 1 && a[i] == a[i - 2]) continue;
                string r = Dfs(a, i + 1);
                if (!string.IsNullOrEmpty(r)) return r;
                else if (i < N - 1) a[i + 1] = 'a';
            }
            return "";
        }
        public string SmallestBeautifulString(string s, int k)
        {
            N = s.Length; Kc = (char)('a' + k - 1);
            var ca = s.ToList();
            ca[N - 1]++;
            return Dfs(ca, 0);
        }

        public int SolveD(int x)
        {
            string s = "abcz";
            int k =
            26;
            Console.WriteLine("ans=" + SmallestBeautifulString(s, k));
            return x;
        }
#endregion

#region Problem E
        public int SolveE(int x)
        {
            return x;
        }
#endregion

#region Run Test
        internal static int Run()
        {
            char p = 'C';
            LC_WC0343_20230430 sln = new();

            return p switch
            {
                'A' => sln.RunTestA(),
                'B' => sln.RunTestB(),
                'C' => sln.RunTestC(),
                'D' => sln.RunTestD(),
                'E' => sln.RunTestE(),
                _ => -1
            };
        }

        int RunTestA()
        {
            return 0;
        }

        int RunTestB()
        {
            return 0;
        }

        int RunTestC()
        {
            /* WA: My: 7 Expected: 8
            [1,1]
            [10,4]
            [[4,2,1,1,3],[1,2,7,4,4],[10,3,6,1,2],[6,1,1,2,3]]
            */
            int[] s = { 1, 1 }, t = { 10, 4 };
            int[][] sr = "[[4,2,1,1,3],[1,2,7,4,4],[10,3,6,1,2],[6,1,1,2,3]]"
                .ToTestInput<int[][]>();

            int a = MinimumCost(s, t, sr);
            Console.WriteLine("ans=" + a);
            return a;
        }

        int RunTestD()
        {
            return SolveD(4);
        }

        int RunTestE()
        {
            return 0;
        }
#endregion
    }
}
