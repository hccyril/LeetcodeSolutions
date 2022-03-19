using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1
{
    // hard, rank: 2251 // 2022/3/4
    // BFS -> simplified dijkstra
    internal class P1125最小的必要团队
    {
        public int[] SmallestSufficientTeam(string[] req_skills, IList<IList<string>> people)
        {
            string AddSkill(string s, int k) => s == "" ? k.ToString() : $"{s},{k}";
            int[] Output(string s) => s.Split(',').Select(t => int.Parse(t)).ToArray();

            Dictionary<string, int> skillDic = new();
            for (int i = 0; i < req_skills.Length; i++)
                skillDic[req_skills[i]] = 1 << i;
            int allMap = (1 << req_skills.Length) - 1;
            var ps = people.Select(sl => sl.Aggregate(0, (sum, s) => sum |= skillDic[s])).ToArray();
            BitArray ba = new(allMap + 1);
            Queue<(int, string)> qu = new();
            qu.Enqueue((0, "")); ba[0] = true;
            while (qu.Any())
            {
                (int k, string s) = qu.Dequeue();
                foreach (int i in Enumerable.Range(0, ps.Length))
                {
                    int sk = k | ps[i];
                    if (!ba[sk])
                    {
                        if (sk == allMap) return Output(AddSkill(s, i));
                        ba[sk] = true;
                        qu.Enqueue((sk, AddSkill(s, i)));
                    }
                }
            }
            return Array.Empty<int>();
        }
    }
}
