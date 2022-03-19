using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1
{
    // hard, 第 90 场周赛第4题。// 2022/3/9
    // rank: 2260 
    // 堆排序
    internal class P0857雇佣K名工人的最低成本
    {
        public double MincostToHireWorkers(int[] quality, int[] wage, int k)
        {
            double minCost = 0.0;
            Heap<int> hp = new((a, b) => a > b);
            double pay = 0.0;
            int sumQuality = 0;
            foreach ((int qua, double wpq) in quality.Zip(wage, (q, w) => (q, (double)w / q)).OrderBy(t => t.Item2))
            {
                if (hp.Count == k)
                {
                    if (qua >= hp.Head) continue;
                    sumQuality -= hp.Pop();
                    sumQuality += qua;
                    hp.Push(qua);
                    pay = wpq;
                    minCost = Math.Min(minCost, pay * sumQuality);
                }
                else
                {
                    hp.Push(qua);
                    sumQuality += qua;
                    pay = wpq;
                    if (hp.Count == k)
                        minCost = pay * sumQuality;
                }
            }
            return minCost;
        }
    }
}
