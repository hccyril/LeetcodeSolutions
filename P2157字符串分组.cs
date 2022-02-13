using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1
{
    // hard, 2022/2/11 并查集，位运算
    // 跟839基本一样的解法
    internal class P2157字符串分组
    {
        const string 相关题1 = nameof(P0839相似字符串组);

        // ver2 并查集+哈希表 -> 怎么还更慢了= =||| （105406ms）
        // ver3 简化了哈希表 -> 188ms!
        IEnumerable<int> MakeKeySet(string s)
        {
            int map = s.Aggregate(0, (m, c) => m |= 1 << c - 'a');
            yield return map;
            foreach (var c in s) yield return map ^ 1 << c - 'a';
        }
        public int[] GroupStrings(string[] words)
        {
            UnionFind uni = new(words.Length);

            Dictionary<int, int> dic = new(); // key->gp // 再进化（ver3）
            //Dictionary<int, HashSet<int>> gset = new();
            
            foreach (int i in Enumerable.Range(0, words.Length))
            {

                // ver3 代码
                foreach (int key in MakeKeySet(words[i]))
                {
                    if (dic.ContainsKey(key)) uni.Union(dic[key], i);
                    else dic[key] = i;
                }

                // ver2 代码
                //int joinGp = -1;
                //foreach (int gp in gset.Keys)
                //{
                //    var hs = gset[gp];
                //    if (keySet.Any(k => hs.Contains(k)))
                //    {
                //        if (joinGp >= 0)
                //        {
                //            uni.Union(joinGp, gp);
                //            var joinSet = gset[joinGp];
                //            foreach (int k in hs) joinSet.Add(k);
                //            delGps.Add(gp);
                //        }
                //        else
                //        {
                //            joinGp = gp;
                //            uni.Union(joinGp, i);
                //            foreach (int k in keySet) hs.Add(k);
                //        }
                //    }
                //}
                //if (joinGp < 0)
                //{
                //    HashSet<int> hs = new();
                //    foreach (int k in keySet) hs.Add(k);
                //    gset[i] = hs;
                //}
                //foreach (int delGp in delGps) gset.Remove(delGp);
            }

            var gps = Enumerable.Range(0, words.Length).GroupBy(i => uni.Find(i));
            return new int[] { gps.Count(), gps.Select(g => g.Count()).Max() };
        }

        // ver1 并查集（跟839一样）—— TLE
        string[] words;
        int[] keys;

        bool IsConnected(int i, int j)
        {
            return Math.Abs(words[i].Length - words[j].Length) <= 1 && IsKeysConnected(keys[i], keys[j]);
        }

        int MakeKey(string s) => s.Aggregate(0, (m, c) => m |= 1 << c - 'a');

        //const int filter = (1 << 26) - 1;
        bool IsKeysConnected(int k1, int k2)
        {
            //int n1 = k1 >> 26, n2 = k2 >> 26,
            //    m1 = k1 & filter, m2 = k2 & filter;
            int n = k1 ^ k2;
            // 判断异或结果是否最多只有2个1
            if (n == 0) return true;
            n = n & (n - 1);
            if (n == 0) return true;
            n = n & (n - 1);
            return n == 0;
        }

        public int[] GroupStrings_ver1(string[] words)
        {
            this.words = words;
            keys = words.Select(s => MakeKey(s)).ToArray();

            UnionFind uni = new(words.Length);
            for (int i = 0; i < words.Length - 1; ++i)
                for (int j = 0; j < words.Length; ++j)
                    if (!uni.Check(i, j) && IsConnected(i, j))
                        uni.Union(i, j);
            var gps = Enumerable.Range(0, words.Length).GroupBy(i => uni.Find(i));
            return new int[] { gps.Count(), gps.Select(g => g.Count()).Max() };
        }

        internal static void Run()
        {
            //string[] input = { "a", "ab", "abc" };
            // ver1: TLE 18219ms -> 17109ms
            string[] input = Common.ReadInput<string[]>(2157); // ret= 10624 4144
            var sln = new P2157字符串分组();
            var ret = sln.GroupStrings(input);
            Console.WriteLine("ret= {0} {1}", ret[0], ret[1]);
        }
    }
}
