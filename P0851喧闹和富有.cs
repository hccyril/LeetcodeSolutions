using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1
{
    // medium, 应用题（有拓扑排序的元素）,2021/12/15 Daily
    internal class P0851喧闹和富有
    {
        class PersonStruct
        {
            public int qp, qv;
            List<PersonStruct> next = new();
            public void Connect(PersonStruct p)
            {
                next.Add(p);
                p.Update(qp, qv);
            }
            void Update(int p, int v)
            {
                if (v < qv)
                {
                    qp = p; qv = v;
                    next.ForEach(ps => ps.Update(p, v));
                }
            }
        }
        public int[] LoudAndRich(int[][] richer, int[] quiet)
        {
            var plist = new PersonStruct[quiet.Length];
            for (int i = 0; i < plist.Length; i++)
                plist[i] = new PersonStruct { qp = i, qv = quiet[i] };
            foreach (var pr in richer)
            {
                PersonStruct pa = plist[pr[0]], pb = plist[pr[1]];
                pa.Connect(pb);
            }
            return plist.Select(p => p.qp).ToArray();
        }
    }
}
