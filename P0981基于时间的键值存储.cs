using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1
{
    // medium, data structure // 2022/10/06 US Daily (redo)
    // SortedSet but timeout: gc=59837 sc=60163 Total Time: 125828ms
    internal class P0981_TimeMap
    {
        // ver2 才发现输入是按递增顺序的
        Dictionary<string, List<(int, string)>> di = new();

        public void Set(string key, string value, int timestamp)
        {
            if (!di.ContainsKey(key)) di[key] = new();
            di[key].Add((timestamp, value));
        }

        public string Get(string key, int timestamp)
        {
            if (di.ContainsKey(key))
            {
                int i = ~di[key].BinarySearch((timestamp + 1, ""));
                if (i > 0) return di[key][i - 1].Item2;
            }
            return "";
        }

        // ver1 sortedset 超时
        //Dictionary<string, SortedSet<(int, string)>> di = new();

        //public void Set(string key, string value, int timestamp)
        //{
        //    if (!di.ContainsKey(key)) di[key] = new();
        //    di[key].Add((timestamp, value));
        //}

        //public string Get(string key, int timestamp)
        //{
        //    if (di.ContainsKey(key))
        //    {
        //        //DEBUG*/ int t = Environment.TickCount;
        //        var r = di[key].GetViewBetween((0, ""), (timestamp + 1, ""));
        //        //DEBUG*/ total += Environment.TickCount - t;
        //        if (r.Any())
        //            return r.Max.Item2;
        //    }
        //    return "";
        //}

        internal int total = 0;
    }

    internal class P0981基于时间的键值存储
    {
        internal static void Run()
        {
            var input = Common.ReadInput<InputStruct>(981);
            int gc = 0, sc = 0;
            var tm = new P0981_TimeMap();
            //Console.WriteLine("ttest" + input.o.Length + " " + input.s.Length + " " + input.s[4][1]);
            for (int i = 1; i < input.o.Length; ++i)
            {
                var op = input.o[i];
                if (op == "get")
                {
                    //Console.WriteLine("{0}", input.s[i][1]);
                    tm.Get((string)input.s[i][0], (int)(long)input.s[i][1]);
                    ++gc;
                }
                else // if (op == "set")
                {
                    //Console.WriteLine("{0}", input.s[i][2]);
                    tm.Set((string)input.s[i][0], (string)input.s[i][1], (int)(long)input.s[i][2]);
                    ++sc;
                }
            }
            Console.WriteLine("gc={0} sc={1} total time={2}", gc, sc, tm.total);

        }

        class InputStruct
        {
            public string[] o { get; set; }
            public object[][] s { get; set; }
        }
    }
}
