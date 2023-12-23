using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1
{
    // medium, 2023/9/12 Daily
    // 图论经典应用题
    internal class P1462课程表IV
    {
        public IList<bool> CheckIfPrerequisite(int numCourses, int[][] prerequisites, int[][] queries)
        {
            Dictionary<int, Dictionary<int, bool>> di = new();
            foreach (var qa in queries)
            {
                int a = qa[0], b = qa[1];
                di.TryAdd(a, new());
                di[a][b] = false;
            }

            // 创建图
            var dg = prerequisites.DirectedGraphNoLength();

            void Dfs(int s, int i, BitArray ba)
            {
                ba[i] = true;
                if (di[s].ContainsKey(i))
                    di[s][i] = true;
                if (dg.ContainsKey(i)) // !!注意：邻接表不一定带有所有顶点
                    foreach (int j in dg[i])
                        if (!ba[j])
                            Dfs(s, j, ba);
            }

            foreach (var s in di.Keys)
                Dfs(s, s, new(numCourses));

            return queries.Select(q => di[q[0]][q[1]]).ToArray();
        }
    }
}
