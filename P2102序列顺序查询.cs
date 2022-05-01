using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1
{
    // hard, BiC67-D // 2022/4/9
    // rank: 2159
    // 红黑树
    internal class P2102序列顺序查询
    {
        TreeSet<(string name, int score)> ts = new((a, b) => a.score == b.score ? a.name.CompareTo(b.name) : b.score.CompareTo(a.score));
        int seq = 0;
        public void Add(string name, int score) => ts.Add((name, score));
        public string Get() => ts[seq++].name;

        internal static void Run()
        {
            var sln = new P2102序列顺序查询();
            sln.Add("a", 2);
            sln.Add("b", 3);
            Console.WriteLine(sln.Get());
        }
    }
}
