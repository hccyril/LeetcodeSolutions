using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace ProblemTests
{
    // BFS - 超时

    [TestClass]
    public class BiC73_D
    {
        int[] cnts = new int[26];
        double[] rates = new double[1000000];
        double Rating(string s)
        {
            double r = 0;
            int n = 0, len = 0;
            Array.Fill(cnts, 0);
            for (int i = 0, j = s.Length - 1; i < j; ++i, --j)
            {
                ++len;
                if (cnts[s[i] - 'a']++ == 0) n++;
                if (cnts[s[i] - 'a'] == 0) n--;
                if (cnts[s[j] - 'a']-- == 0) n++;
                if (cnts[s[j] - 'a'] == 0) n--;
                if (n == 0)
                {
                    r += 1.0 / len;
                    len = 0;
                }
            }
            return r;
        }
        bool IsPa(string s)
        {
            for (int i = 0, j = s.Length - 1; i < j; ++i, --j)
                if (s[i] != s[j]) return false;
            return true;
        }
        string Swap(string s, int i) // exchange i & i + 1
        {
            if (i == 0) return string.Format("{0}{1}{2}", s[1], s[0], s.Substring(2));
            else if (i == s.Length - 2) return string.Format("{0}{1}{2}", s.Substring(0, s.Length - 2), s[s.Length - 1], s[s.Length - 2]);
            else return string.Format("{0}{1}{2}{3}", s.Substring(0, i), s[i + 1], s[i], s.Substring(i + 2));
        }
        public int MinMovesToMakePalindrome(string s)
        {
            if (s.Length <= 2) return 0;
            HashSet<string> hs = new();
            Queue<(string, int, double)> qu = new();
            double r = Rating(s);
            if (r > s.Length / 2 - 1 && IsPa(s)) return 0;
            rates[0] = r;
            hs.Add(s);

            qu.Enqueue((s, 0, r));
            while (qu.Any())
            {
                (string ns, int p, double ra) = qu.Dequeue();
                if (rates[p + 1] == 0) rates[p + 1] = rates[p];
                if (ra < rates[p] - 0.0001) continue;
                for (int i = 0; i < s.Length - 1; ++i)
                {
                    string st = Swap(ns, i); // 我！！！！啊 写成Swap(s, i)了！！！
                    if (hs.Add(st) && (r = Rating(st)) > rates[p + 1] - 0.0001)
                    {
                        rates[p + 1] = r;
                        if (r > s.Length / 2 - 1 && IsPa(st)) return p + 1;
                        qu.Enqueue((st, p + 1, r));
                    }
                }
            }
            return -1;
        }

        [TestMethod]
        public void Run()
        {
            string s = "skwhhaaunskegmdtutlgtteunmuuludii";
            //string s = "aabb";
            int x = MinMovesToMakePalindrome(s);
            Console.WriteLine("x=" + x);
            Assert.IsTrue(x > 0);
            //Assert.AreEqual(2, x);
        }
    }
}
