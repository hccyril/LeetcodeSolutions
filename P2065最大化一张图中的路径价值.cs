using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1;

// hard, 2024/7/1 Daily
// rank: 2178
// 想了很久，才发现暴力求解可行（10<=time, maxtime <= 100，也就是深度最多10，每个节点最多4个分支，所以计算复杂度=4**10=1M）
internal class P2065最大化一张图中的路径价值
{
    public int MaximalPathQuality(int[] values, int[][] edges, int maxTime)
    {
        int n = values.Length, maxscore = 0;
        var ug = edges.UndirectedGraphWithLength(n);
        var vis = new int[n];

        void Dfs(int i, int time, int score)
        {
            if (i == 0) maxscore = Math.Max(maxscore, score);
            foreach ((int j, int t) in ug[i])
            {
                if (time + t <= maxTime) 
                {
                    if (++vis[j] == 1) score += values[j];
                    Dfs(j, time + t, score);
                    if (--vis[j] == 0) score -= values[j];
                }
            }
        }

        Dfs(0, 0, values[0]);
        return maxscore;
    }
}
