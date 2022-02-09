using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1
{
    // medium, 2022/2/7
    // same：《剑指 Offer II 111. 计算除法》
    // 最短路径Floyd算法
    internal class P0399除法求值
    {
        public double[] CalcEquation(IList<IList<string>> equations, double[] values, IList<IList<string>> queries)
        {
            int n = 0;
            Dictionary<string, int> dic = new();
            foreach (var list in equations)
                foreach (var node in list)
                    if (!dic.ContainsKey(node))
                        dic.Add(node, n++);
            n = dic.Count;
            double[,] rl = new double[n, n];
            for (int i = 0; i < n; ++i)
                for (int j = 0; j < n; ++j)
                    rl[i, j] = i == j ? 1.0 : -1.0;
            foreach (int i in Enumerable.Range(0, equations.Count))
            {
                int a = dic[equations[i][0]], b = dic[equations[i][1]];
                double val = values[i];
                rl[a, b] = val; rl[b, a] = 1.0 / val;
            }
            // Floyd
            for (int k = 0; k < n; ++k)
                for (int i = 0; i < n; ++i)
                    for (int j = 0; j < n; ++j)
                        if (i != k && k != j && rl[i, k] >= 0 && rl[k, j] >= 0 && rl[i, j] < 0)
                            rl[j, i] = 1.0 / (rl[i, j] = rl[i, k] * rl[k, j]);
            // result
            double[] ans = new double[queries.Count];
            foreach (int i in Enumerable.Range(0, queries.Count))
            {
                int a, b;
                if (dic.TryGetValue(queries[i][0], out a) && dic.TryGetValue(queries[i][1], out b))
                    ans[i] = rl[a, b];
                else
                    ans[i] = -1.0;
            }
            return ans;
        }
    }
}
