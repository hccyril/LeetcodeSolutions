using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProblemTests
{
    // ����ʱû������

    [TestClass]
    public class ProblemD
    {
        public class HashHeap
        {
            Func<long, long, bool> comp;

            /// <summary>
            /// ���췽������ָ���Ǵ󶥶ѻ���С����
            /// </summary>
            /// <param name="maxOrMin">true��ʾ�󶥶ѣ�false��ʾС����</param>
            public HashHeap(bool maxOrMin)
            {
                if (maxOrMin) comp = (a, b) => a > b;
                else comp = (a, b) => a < b;
            }

            public HashHeap(Func<long, long, bool> comp) => this.comp = comp;
            bool Compare(int i, int j) => comp(GetAt(i)[0], GetAt(j)[0]);

            long[] swap;
            long[] GetAt(int index) => _list[index - 1];
            List<long[]> _list = new List<long[]>(); // e[0]: ֵ; e[1]: ��list�ж�Ӧ��index; e[2]: key
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
            /// ���������ĸ��£�����ֻ֧������swap
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

                return i != index; // i���û�иı�˵��û��swap up�������swap up�Ͳ���Ҫswap down��
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
