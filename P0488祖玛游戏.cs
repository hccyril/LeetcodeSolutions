using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1
{
    // Hard
    // NP-complete, 回溯或者BFS
    class P0488祖玛游戏
    {
        // 红色 'R'、黄色 'Y'、蓝色 'B'、绿色 'G' 或白色 'W'
        public int FindMinStep(string board, string hand)
        {
            if (string.IsNullOrEmpty(board)) return 0;
            Dictionary<string, int> dic = new Dictionary<string, int>();
            Queue<string> qu = new Queue<string>();
            string s0 = string.Format("{0}-{1}", board, string.Join("", hand.ToCharArray().OrderBy(t => t)));
            dic[s0] = 0;
            qu.Enqueue(s0);
            while (qu.Any())
            {
                string qs = qu.Dequeue();
                int step = dic[qs] + 1;
//#if DEBUG
//                Console.WriteLine(qs + ", " + step);
//#endif
                var split = qs.Split('-');
                var barr = split[0].ToCharArray();
                var harr = split[1].ToCharArray();
                var hc = ' ';
                for (int hi = 0; hi < harr.Length; ++hi)
                    if (harr[hi] != hc)
                    {
                        hc = harr[hi];
                        for (int i = 0; i <= barr.Length; ++i)
                        {
                            if (i < barr.Length && hc == barr[i]) continue; // 相同字符默认插到最后面
                            string s = Place(barr, i, harr, hi);
                            if (s == "") return step;
                            if (s != null && !dic.ContainsKey(s)) // s == null表示无解
                            {
                                dic[s] = step;
                                qu.Enqueue(s);
                            }
                        }
                    }
            }
            return -1;
        }

        // 在barr的位置i插入harr的hi的字符
        string Place(char[] barr, int bi, char[] harr, int hi)
        {
            int start = bi;
            char hc = harr[hi];
            int map = (1 << barr.Length) - 1, fullmap = map;
            // 判断是否有发生消除
            int combo = 2;
            while (combo > 0 && start > 0 && barr[start - 1] == hc)
            {
                start--;
                if (start > 0 && barr[start - 1] == barr[start])
                    start--;
                for (int ci = start + 1; ci < barr.Length; ++ci)
                    if ((map & (1 << ci)) != 0)
                        if (barr[ci] == barr[start]) combo++; else break;
                if (combo >= 3)
                {
                    for (int ri = start; ri < barr.Length; ++ri)
                        if ((map & (1 << ri)) != 0)
                            if (barr[ri] == barr[start])
                            {
                                map ^= 1 << ri;
                                hc = ri + 1 < barr.Length ? barr[ri + 1] : ' ';
                            }
                            else break;
                    combo = 1;
                }
                else combo = 0;
            }
            if (map == fullmap)
            {
                // 没有发生消除的情况
                if (harr.Length == 1) return null;
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < bi; ++i)
                    sb.Append(barr[i]);
                sb.Append(harr[hi]);
                for (int i = bi; i < barr.Length; ++i) 
                    sb.Append(barr[i]);
                sb.Append('-');
                for (int i = 0; i < harr.Length; ++i)
                    if (i != hi)
                        sb.Append(harr[i]);
                return sb.ToString();
            }
            else
            {
                // 有发生消除的情况
                if (map == 0) return ""; // 找到最终结果
                else if (harr.Length == 1) return null;
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < barr.Length; ++i)
                    if ((map & (1 << i)) != 0)
                        sb.Append(barr[i]);
                sb.Append('-');
                for (int i = 0; i < harr.Length; ++i)
                    if (i != hi)
                        sb.Append(harr[i]);
                return sb.ToString();
            }
        }

        internal static void Run()
        {
            var so = new P0488祖玛游戏();
            string b1 = "WRRBBW", h1 = "RB", // -1
                b2 = "WWRRBBWW", h2 = "WRBRW", // 2
                b3 = "G", h3 = "GGGGG", // 2
                b4 = "RBYYBBRRB", h4 = "YRBGB", // 3
                b5 = "RRYGGYYRRYGGYYRR", h5 = "GGBBB"; // 特殊：这个是有解的，一开始把一个B给中间的两个R隔开
            Console.WriteLine(so.FindMinStep(b1, h1)); // -1
            Console.WriteLine(so.FindMinStep(b2, h2)); // 2
            Console.WriteLine(so.FindMinStep(b3, h3)); // 2
            Console.WriteLine(so.FindMinStep(b4, h4)); // 3
            Console.WriteLine(so.FindMinStep(b5, h5)); // 5
        }
    }
}
