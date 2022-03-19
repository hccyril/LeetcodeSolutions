using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProblemTests
{
    // 比赛时没做出来

    [TestClass]
    public class ProblemD
    {
        public class HashHeap
        {
            Func<long, long, bool> comp;

            /// <summary>
            /// 构造方法用于指定是大顶堆还是小顶堆
            /// </summary>
            /// <param name="maxOrMin">true表示大顶堆，false表示小顶堆</param>
            public HashHeap(bool maxOrMin)
            {
                if (maxOrMin) comp = (a, b) => a > b;
                else comp = (a, b) => a < b;
            }

            public HashHeap(Func<long, long, bool> comp) => this.comp = comp;
            bool Compare(int i, int j) => comp(GetAt(i)[0], GetAt(j)[0]);

            long[] swap;
            long[] GetAt(int index) => _list[index - 1];
            List<long[]> _list = new List<long[]>(); // e[0]: 值; e[1]: 在list中对应的index; e[2]: key
            Dictionary<long, long[]> _dic = new Dictionary<long, long[]>();
            public bool Any() => Count > 0;
            public long Count => _list.Count;
            public bool ContainsKey(long key) => _dic.ContainsKey(key);

            public long Head => _list[0][0];
            public long HeadKey => _list[0][2];
            void Swap(int i, int j)
            {
                swap = _list[i - 1];
                _list[i - 1] = _list[j - 1];
                _list[i - 1][1] = i;
                _list[j - 1] = swap;
                _list[j - 1][1] = j;
            }

            public void Push(long key, long val)
            {
                long[] item = { val, 0, key };
                _dic.Add(key, item);
                _list.Add(item);
                SwapUp((int)(item[1] = Count));
            }

            /// <summary>
            /// 基于增量的更新，留意只支持向上swap
            /// </summary>
            public void Update(long key, long inc)
            {
                long[] p = _dic[key];
                p[0] += inc;
                SwapUp((int)p[1]);
            }
            public long this[long key]
            {
                get => _dic[key][0];
            }
            //public (long index, long val) Get(long key)
            //{
            //    long[] p = _dic[key];
            //    return (p[1], p[0]);
            //}
            //public void UpdateAt(long index, long val)
            //{
            //    _list[index - 1][0] = val;
            //    SwapUp(index);
            //}

            public (long key, long val) Pop()
            {
                var p = GetAt(1);
                Remove(p[2]);
                return (p[2], p[0]);
            }

            public void Remove(long key)
            {
                int index = (int)_dic[key][1];
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

                Swap(index, (int)Count);
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

        public long MinimumWeight(int n, int[][] edges, int src1, int src2, int dest)
        {
            Dictionary<int, SortedList<int, int>> g = new();
            Dictionary<int, int> dic = new();
            foreach (var ed in edges)
            {
                int s = ed[1], t = ed[0];
                if (!g.ContainsKey(s)) g[s] = new();
                if (!g[s].ContainsKey(t) || g[s][t] > ed[2]) g[s][t] = ed[2];
            }
            HashHeap hp = new(false);
            hp.Push(dest, 0);
            long t1 = -1L;
            int r1 = -1;
            while (hp.Any())
            {
                (long s, long len) = hp.Pop();
                if (r1 >= 0 && len > t1) break;
                if (r1 < 0)
                {
                    if (s == src1 || s == src2)
                    {
                        r1 = (int)s;
                    }
                }
                foreach ((int t, int p) in g[(int)s])
                {
                    if (hp.ContainsKey(t))
                    {
                        if (hp[t] > len + p)
                        {
                            hp.Update(t, len + p - hp[t]);
                            dic[t] = (int)s;
                        }
                    }
                    else
                    {
                        hp.Push(t, len + p);
                        dic[t] = (int)s;
                    }
                }
            }

            throw new NotImplementedException();

        }

        [TestMethod]
        public void Run()
        {
            int ans = 0;
            Assert.AreEqual(1, ans);
        }
    }

 
}
