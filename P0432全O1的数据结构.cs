using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1
{
    // 432. 全 O(1) 的数据结构
    // hard
    internal class P0432全O1的数据结构
    {
        class TestStruct
        {
            public string[] commands { get; set; }
            public string[][] args { get; set; }
        }
        internal static void Run()
        {
            var sln = new AllOne();
            var ti = Common.ReadInput<TestStruct>(432);
            for (int i =0; i < ti.commands.Length; ++i)
            {
                string cmd = ti.commands[i], arg = ti.args[i]?.Any() == true ? ti.args[i][0] : "";
                if (cmd == "inc") sln.Inc(arg);
                else if (cmd == "dec") sln.Dec(arg);
                else if (cmd == "getMaxKey") Console.Write(sln.GetMaxKey() + " ");
                else if (cmd == "getMinKey") Console.Write(sln.GetMinKey() + " ");
            }
            Console.WriteLine();
        }
    }
    // ver2 采用链表
    class AllOne
    {
        class AllOneNode
        {
            public int cnt = 1;
            public readonly HashSet<string> hs = new();
        }
        readonly LinkedList<AllOneNode> list = new();
        readonly Dictionary<string, LinkedListNode<AllOneNode>> dic = new();
        public AllOne()
        {
        }

        public void Inc(string key)
        {
            if (dic.ContainsKey(key))
            {
                var ln = dic[key];

                // add +1
                if (ln.Next?.Value?.cnt == ln.Value.cnt + 1)
                {
                    ln.Next.Value.hs.Add(key);
                }
                else
                {
                    AllOneNode an = new();
                    an.cnt = ln.Value.cnt + 1;
                    an.hs.Add(key);
                    list.AddAfter(ln, an);
                }
                dic[key] = ln.Next;

                // remove current
                ln.Value.hs.Remove(key);
                if (!ln.Value.hs.Any())
                    list.Remove(ln);
            }
            else
            {
                if (list.First?.Value?.cnt != 1)
                {
                    AllOneNode node = new();
                    list.AddFirst(node);
                }
                list.First.Value.hs.Add(key);
                dic[key] = list.First;
            }
        }

        public void Dec(string key)
        {
            var ln = dic[key];
            ln.Value.hs.Remove(key);

            if (ln.Previous?.Value?.cnt == ln.Value.cnt - 1)
            {
                ln.Previous.Value.hs.Add(key);
            }
            else if (ln.Value.cnt > 1)
            {
                AllOneNode an = new();
                an.cnt = ln.Value.cnt - 1;
                an.hs.Add(key);
                list.AddBefore(ln, an);
            }
            
            if (ln.Previous == null) dic.Remove(key); // 低级错误！！找了半天才发现漏了这句
            else dic[key] = ln.Previous;

            if (!ln.Value.hs.Any()) list.Remove(ln);
        }

        public string GetMaxKey() => list.Last?.Value?.hs?.FirstOrDefault() ?? "";

        public string GetMinKey() => list.First?.Value?.hs?.FirstOrDefault() ?? "";
    }
    // ver1 采用数组，结果被坑了
    // 例：添加一个a,10个b,5个c,然后删除那个a，此时min从a变成了c
#if VERSION_01_WA
    class AllOne
    {
        const int MAX = 50001;
        HashSet<string>[] cnts;
        Dictionary<string, int> dic = new();
        int max = 0, min = 0;
        public AllOne()
        {
            cnts = new HashSet<string>[MAX];
            for (int i = 0; i < MAX; ++i) cnts[i] = new();
        }

        public void Inc(string key)
        {
            int count = dic.ContainsKey(key) ? ++dic[key] : dic[key] = 1;
            if (count > max) max = count;
            cnts[count - 1].Remove(key);
            if (count == 1 && min > 1) min = 1;
            else if (!cnts[count - 1].Any() && count - 1 == min) min = count;
            cnts[count].Add(key);
        }

        public void Dec(string key)
        {
            int count = --dic[key];
            if (dic[key] == 0) dic.Remove(key);
            cnts[count + 1].Remove(key);
            if (!cnts[count + 1].Any() && count + 1 == max) max = count;
            if (count > 0 && count < min) min = count;
            else if (count == 0 && min == 1 && !cnts[1].Any()) min = 0;
            if (count > 0) cnts[count].Add(key);
        }

        public string GetMaxKey() => cnts[max].FirstOrDefault() ?? "";

        public string GetMinKey() => cnts[min].FirstOrDefault() ?? "";
    }
#endif
}
