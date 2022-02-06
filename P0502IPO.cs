using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1
{
    class P0502IPO
    {
        class Project
        {
            public int profit;
            public int capital;
        }
        public int FindMaximizedCapital(int k, int w, int[] profits, int[] capital)
        {
            var projects = profits.Zip(capital, 
                (a, b) => new Project { profit = a, capital = b })
                .OrderBy(t => t.capital)
                .ToArray();
            Heap<Project> hp = new Heap<Project>((a, b) => a.profit > b.profit);
            int i = 0;
            for (; k > 0; --k)
            {
                while (i < projects.Length && projects[i].capital <= w)
                    hp.Push(projects[i++]);
                if (hp.Any())
                {
                    var doingProject = hp.Pop();
                    w += doingProject.profit;
                }
            }
            return w;
        }
    }
}
