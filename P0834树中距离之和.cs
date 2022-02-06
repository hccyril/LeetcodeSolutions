using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1
{
    // hard, DP
    class P0834树中距离之和
    {
        List<int>[] leaves;
        Dictionary<int, int[]> dic = new Dictionary<int, int[]>();

        // 当r为父节点时子节点的路径和及节点个数
        int[] SumNode(int r, int c)
        {
            int key = (r << 16) | c;
            if (dic.ContainsKey(key)) return dic[key];
            int sum = 0, count = 1;
            foreach (var cc in leaves[c])
                if (cc != r)
                {
                    var ret = SumNode(c, cc);
                    sum += ret[0] + ret[1];
                    count += ret[1];
                }
            return dic[key] = new int[] { sum, count };
        }
        int SumTree(int r)
        {
            int sum = 0;
            foreach (var c in leaves[r])
            {
                int[] ret = SumNode(r, c);
                sum += ret[0] + ret[1];
            }
            return sum;
        }
        public int[] SumOfDistancesInTree(int n, int[][] edges)
        {
            leaves = new List<int>[n];
            for (int i = 0; i < n; ++i) leaves[i] = new List<int>();
            foreach (var e in edges)
            {
                int a = e[0], b = e[1];
                leaves[a].Add(b);
                leaves[b].Add(a);
            }
            int[] ans = new int[n];
            for (int i = 0; i < n; ++i)
                ans[i] = SumTree(i);
            return ans;
        }
    }
}
