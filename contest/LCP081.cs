using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1.contest
{
    /// <summary>
    /// 线段树
    /// </summary>
    public class SegTree
    {
        readonly int N, K;
        readonly int[] a, ks;
        public SegTree(int n, int k)
        {
            N = n; K = k;
            ks = new int[k];
            a = new int[n << 2 | 1];
        }

        int UpdateTree(int l, int r, int k, int i, int val)
        {
            if (l == r) return a[k] = val;
            int m = l + r >> 1,
                lv = i <= m ? UpdateTree(l, m, k << 1, i, val) : a[k << 1],
                rv = i > m ? UpdateTree(m + 1, r, k << 1 | 1, i, val) : a[k << 1 | 1];
            return a[k] = lv & rv;
        }

        public void Update(int i, int val)
            => UpdateTree(0, N - 1, 1, i, val);

        /// <summary>
        /// 统计当前位，在a[]中的后缀有多少个连续的1，如果全都是1，返回-1
        /// </summary>
        int CountBit(int b, int l, int r, int k)
        {
            if ((a[k] & b) != 0) return -1;
            else if (l == r) return 0;

            int m = l + r >> 1,
                rc = CountBit(b, m + 1, r, k << 1 | 1);

            return rc >= 0 ? rc : r - m + CountBit(b, l, m, k << 1);
        }

        public void Build()
        {
            for (int i = 0; i < K; ++i)
                ks[i] = CountBit(1 << i, 0, N - 1, 1);
        }

        public void Modify(int i, int oriVal, int val)
        {
            Update(i, val);
            int changedBits = oriVal ^ val;
            for (int j = 0; j < K; ++j)
            {
                int b = 1 << j;
                if ((changedBits & b) != 0)
                    ks[j] = CountBit(b, 0, N - 1, 1);
            }

        }

        public int Calc(int x, int y)
        {
            int s = 0;
            for (int i = 0; i < K; ++i)
            {
                // arr全是1，结果为当前位翻转 x * N 次
                if (ks[i] < 0)
                {
                    s |= y & 1 << i;
                    if ((x & 1) != 0 && (N & 1) != 0)
                        s ^= 1 << i;
                }
                // 否则，结果为 1 翻转 ? 次 ?=后缀连续1的数量
                else
                {
                    if ((ks[i] & 1) == 0)
                        s |= 1 << i;
                }
            }
            return s;
        }
    }

    // 线段树，比赛时没时间看，后来看下感觉规律也不是太难找
    // 2023/5/10
    internal class LCP081
    {
        public int GetNandResult(int k, int[] arr, int[][] operations)
        {
            int n = arr.Length;
            SegTree st = new(n, k);
            for (int i = 0; i < n; ++i) st.Update(i, arr[i]);

            st.Build();

            int ans = 0;
            foreach (var opa in operations)
                if (opa[0] == 0)
                {
                    st.Modify(opa[1], arr[opa[1]], opa[2]);
                    arr[opa[1]] = opa[2];
                }
                else
                {
                    ans ^= st.Calc(opa[1], opa[2]);
                }

            return ans;
        }
    }
}
