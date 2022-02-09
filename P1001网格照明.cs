using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1
{
    internal class P1001网格照明
    {
        class LampStruct
        {
            public int i, j;
            public bool on;
            public LampStruct(int i, int j)
            {
                this.i = i; this.j = j;
                on = true;
            }
        }
        IEnumerable<(int ni, int nj)> NineDir(int n, int i, int j)
        {
            yield return (i, j);
            if (i > 0)
            {
                yield return (i - 1, j);
                if (j > 0) yield return (i - 1, j - 1);
                if (j < n - 1) yield return (i - 1, j + 1);
            }
            if (j > 0) yield return (i, j - 1);
            if (j < n - 1) yield return (i, j + 1);
            if (i < n - 1)
            {
                yield return (i + 1, j);
                if (j > 0) yield return (i + 1, j - 1);
                if (j < n - 1) yield return (i + 1, j + 1);
            }
        }
        int Add(Dictionary<int, int> dic, int key) => dic.ContainsKey(key) ? ++dic[key] : (dic[key] = 1);
        bool Check(Dictionary<int, int> dic, int key) => dic.ContainsKey(key) && dic[key] > 0;
        int Remove(Dictionary<int, int> dic, int key) => dic.ContainsKey(key) ? --dic[key] : -1;
        public int[] GridIllumination(int n, int[][] lamps, int[][] queries)
        {
            Dictionary<int, int> rdic = new(), cdic = new(), xldic = new(), xrdic = new();
            Dictionary<(int i, int j), List<LampStruct>> removeDic = new();
            foreach (var lp in lamps)
            {
                int i = lp[0], j = lp[1];
                Add(rdic, i);
                Add(cdic, j);
                Add(xldic, i - j);
                Add(xrdic, i + j);
                LampStruct ls = new(i, j);
                foreach ((int ni, int nj) in NineDir(n, i, j))
                {
                    if (!removeDic.ContainsKey((ni, nj)))
                        removeDic[(ni, nj)] = new();
                    removeDic[(ni, nj)].Add(ls);
                }
            }
            int[] ans = new int[queries.Length];
            foreach (int idx in Enumerable.Range(0, queries.Length))
            {
                int i = queries[idx][0], j = queries[idx][1];
                ans[idx] = Check(rdic, i) || Check(cdic, j) || Check(xldic, i - j) || Check(xrdic, i + j) ? 1 : 0;
                if (removeDic.ContainsKey((i, j)))
                    foreach (var lp in removeDic[(i, j)])
                        if (lp.on)
                        {
                            lp.on = false;
                            int li = lp.i, lj = lp.j;
                            Remove(rdic, li);
                            Remove(cdic, lj);
                            Remove(xldic, li - lj);
                            Remove(xrdic, li + lj);
                        }
            }
            return ans;
        }
    }
}
