using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1
{
    // hard, 2022/7/8 US Daily
    // rank: 2056
    // DP
    internal class P1473粉刷房子III
    {
        public int MinCost(int[] houses, int[][] cost, int m, int n, int target)
        {
            Dictionary<(int nbr, int clr), int> dp0 = new(), dp;

            int Update(Dictionary<(int nbr, int clr), int> d, int nbr, int clr, int cst)
                => !d.ContainsKey((nbr, clr)) || cst < d[(nbr, clr)] ? d[(nbr, clr)] = cst : -1;

            if (houses[0] > 0)
                Update(dp0, 1, houses[0] - 1, 0);
            else
                for (int c = 0; c < cost[0].Length; ++c)
                    Update(dp0, 1, c, cost[0][c]);

            for (int i = 1; i < houses.Length; ++i)
            {
                dp = new();
                if (houses[i] > 0)
                    foreach (((int nb0, int cr0), int ct0) in dp0)
                        if (houses[i] - 1 == cr0) Update(dp, nb0, houses[i] - 1, ct0);
                        else Update(dp, nb0 + 1, houses[i] - 1, ct0);
                else
                    for (int c = 0; c < cost[i].Length; ++c)
                        foreach (((int nb0, int cr0), int ct0) in dp0)
                            if (c == cr0) Update(dp, nb0, c, ct0 + cost[i][c]);
                            else Update(dp, nb0 + 1, c, ct0 + cost[i][c]);
                dp0 = dp;
            }
            var rc = dp0.Keys.Where(t => t.nbr == target);
            return rc.Any() ? rc.Select(t => dp0[t]).Min() : -1;
        }
    }
}
