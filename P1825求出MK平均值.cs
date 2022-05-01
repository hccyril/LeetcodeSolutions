using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1
{
    // hard, WC236-D
    // rank: 2396
    // 很多个sortedset+1个Queue
    public class MKAverage
    {
        class MStruct : IComparable<MStruct>
        {
            public int i, n, t;
            public int CompareTo(MStruct other) => n == other.n ? i.CompareTo(other.i) : n.CompareTo(other.n);
        }

        int M, K, Sum = 0;
        int _i = 1;
        SortedSet<MStruct> left = new(), mid = new(), right = new();
        Queue<MStruct> qu = new();
        public MKAverage(int m, int k) => (M, K) = (m, k);

        public void AddElement(int num)
        {
            MStruct ms = new() { i = _i++, n = num, t = 0 };

            qu.Enqueue(ms);
            if (left.Count < K || num < left.Max.n)
            {
                left.Add(ms); ms = null;
                if (left.Count > K)
                {
                    ms = left.Max;
                    left.Remove(ms);
                }
            }
            if (ms != null && (right.Count < K || ms.n > right.Min.n))
            {
                ms.t = 2;
                right.Add(ms); ms = null;
                if (right.Count > K)
                {
                    ms = right.Min;
                    right.Remove(ms);
                }
            }
            if (ms != null)
            {
                ms.t = 1;
                mid.Add(ms);
                Sum += ms.n;
            }

            if (qu.Count > M)
            {
                ms = qu.Dequeue();
                if (ms.t == 0)
                {
                    left.Remove(ms);
                    ms = mid.Min;
                    ms.t = 0;
                    left.Add(ms);
                }
                else if (ms.t == 2)
                {
                    right.Remove(ms);
                    ms = mid.Max;
                    ms.t = 2;
                    right.Add(ms);
                }
                Sum -= ms.n;
                mid.Remove(ms);
            }
        }

        public int CalculateMKAverage() => qu.Count < M ? -1 : Sum / mid.Count;
    }

    internal class P1825求出MK平均值
    {
    }
}
