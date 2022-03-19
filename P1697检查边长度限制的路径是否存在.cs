using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1
{
    // hard, 周赛题WC220-D // 2022/3/12
    // rank: 2300
    // MST 最小生成树应用
    internal class P1697检查边长度限制的路径是否存在
    {
        public bool[] DistanceLimitedPathsExist(int n, int[][] edgeList, int[][] queries)
        {
            UnionFind uni = new(n);
            var it = queries.OrderBy(t => t[2]).GetEnumerator();
            var qry = it.MoveNext() ? it.Current : null;
            Dictionary<(int, int, int), bool> dic = new();
            foreach (var edge in edgeList.OrderBy(t => t[2]))
            {
                if (qry == null) break;
                int u = Math.Min(edge[0], edge[1]),
                    v = Math.Max(edge[0], edge[1]),
                    dist = edge[2];
                if (uni.Check(u, v)) continue;
                while (qry != null && qry[2] <= dist)
                {
                    dic[(qry[0], qry[1], qry[2])] = uni.Check(qry[0], qry[1]);
                    qry = it.MoveNext() ? it.Current : null;
                }
                uni.Union(u, v);
            }
            while (qry != null)
            {
                dic[(qry[0], qry[1], qry[2])] = uni.Check(qry[0], qry[1]);
                qry = it.MoveNext() ? it.Current : null;
            }
            return queries.Select(q => dic[(q[0], q[1], q[2])]).ToArray();
        }
    }
}
