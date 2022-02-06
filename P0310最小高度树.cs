using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1
{
    // medium, 2021/12/16
    // 之前做过类似的题目，但找不回来了= =
    // 记忆化回溯+剪枝
    internal class P0310最小高度树
    {
        Dictionary<int, int> dic; // key: [node, parent], value: height -> 表示当前node在parent为父亲时其高度值
        int minHeight = 2147483647;
        List<int>[] chld;

        int GetHeight(int node, int parent)
        {
            int key = (node << 15) | parent;
            if (dic.ContainsKey(key)) return dic[key];
            int height = 0;
            foreach (var c in chld[node].Where(t => t != parent))
            {
                height = Math.Max(height, GetHeight(c, node) + 1);
                if (height > minHeight) break; // 剪枝- 当前已经比最小高度大，就不用再往下算了
            }
            return dic[key] = height;
        }

        public IList<int> FindMinHeightTrees(int n, int[][] edges)
        {
            dic = new();
            chld = new List<int>[n];
            for (int i = 0; i < n; i++) chld[i] = new List<int>();
            foreach (var ed in edges)
            {
                chld[ed[0]].Add(ed[1]);
                chld[ed[1]].Add(ed[0]);
            }
            IList<int> ans = new List<int>();
            for (int i = 0; i < n; ++i)
            {
                int height = 0;
                foreach (var c in chld[i])
                {
                    height = Math.Max(height, GetHeight(c, i) + 1);
                    if (height > minHeight) break;
                }
                if (height <= minHeight)
                {
                    if (height < minHeight)
                    {
                        minHeight = height;
                        ans.Clear();
                    }
                    ans.Add(i);
                }
            }
            return ans;
        }
    }
}
