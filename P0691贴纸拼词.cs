using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1
{
    // hard, 2022/5/14 Daily
    // BFS
    internal class P0691贴纸拼词
    {
        public int MinStickers(string[] stickers, string target)
        {
            target = string.Join("", target.OrderBy(c => c));
            int full = (1 << target.Length) - 1;

            int Add(int map, string s)
            {
                foreach (var c in s)
                    for (int i = 0; i < target.Length; ++i)
                        if (((1 << i) & map) == 0 && target[i] == c)
                        {
                            map ^= 1 << i;
                            break;
                        }
                return map;
            }

            // ver1出现out of memory，需要加一个set
            HashSet<int> hs = new();
            hs.Add(0);

            Queue<(int, int)> qu = new();
            qu.Enqueue((0, 0));
            while (qu.Any())
            {
                (int map, int cnt) = qu.Dequeue();
                foreach (var s in stickers)
                {
                    int rm = Add(map, s);
                    if (rm != map && hs.Add(rm))
                    {
                        if (rm == full) return cnt + 1;
                        qu.Enqueue((rm, cnt + 1));
                    }
                }
            }
            return -1;
        }

        // test case: 
        /*
["and","pound","force","human","fair","back","sign","course","sight","world","close","saw","best","fill","late","silent","open","noon","seat","cell","take","between","it","hundred","hat","until","either","play","triangle","stay","separate","season","tool","direct","part","student","path","ear","grow","ago","main","was","rule","element","thing","place","common","led","support","mean"]
"quietchord"
         * */
    }
}
