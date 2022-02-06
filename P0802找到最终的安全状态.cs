using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1
{
    class P0802找到最终的安全状态
    {
        int[] ans;
        List<int>[] g;

        int Safe(int i)
        {
            if (ans[i] == -1)
            {
                ans[i] = -2;
                int target = -1;
                foreach (var j in g[i])
                {
                    int safeJ = Safe(j);
                    if (safeJ < 0) return ans[i] = safeJ;

                    // 第一次提交审错题，以为所有路径必须到达同一个终点才算safe
                    //if (target == -1)
                    //    target = safeJ;
                    //else if (target != safeJ)
                    //    return ans[i] = -2;
                }
                ans[i] = target == -1 ? i : target;
            }
            return ans[i];
        }

        public IList<int> EventualSafeNodes(int[][] graph)
        {
            ans = new int[graph.Length];
            for (int i = 0; i < ans.Length; ++i) ans[i] = -1;
            g = new List<int>[graph.Length];
            for (int i = 0; i < g.Length; ++i) g[i] = new List<int>();
            for (int i = 0; i < graph.Length; ++i)
                foreach (var j in graph[i])
                    g[i].Add(j);

            IList<int> ansList = new List<int>();
            foreach (var i in Enumerable.Range(0, ans.Length))
                if (Safe(i) >= 0)
                    ansList.Add(i);

            return ansList;
        }
    }
}
