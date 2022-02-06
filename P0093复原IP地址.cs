using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1
{
    class P0093复原IP地址
    {
        IList<string> ans;

        int IsIp(string s)
        {
            if (s.Length > 1 && s.StartsWith('0')) return -1;
            int n = int.Parse(s);
            return n < 256 ? n : -1;
        }

        void Rec(string s, int i, List<int> list)
        {
            if (list.Count == 4)
            {
                ans.Add(string.Join('.', list));
                return;
            }
            for (int j = list.Count == 3 ? s.Length : i + 1; j <= s.Length - 3 + list.Count && j <= i + 3; ++j)
            {
                int n = IsIp(s.Substring(i, j - i));
                if (n >= 0)
                {
                    list.Add(n);
                    Rec(s, j, list);
                    list.RemoveAt(list.Count - 1);
                }
            }
        }

        public IList<string> RestoreIpAddresses(string s)
        {
            ans = new List<string>();
            if (s.Length > 12) return ans;
            Rec(s, 0, new List<int>());
            return ans;
        }

        public static void Run()
        {
            Console.WriteLine(string.Join('\n', new P0093复原IP地址().RestoreIpAddresses("0000")));
        }
    }
}
