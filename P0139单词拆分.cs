using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1
{
    // 动态规划
    class P0139单词拆分
    {
        class WordComp
        {
            public string word;
            public int index;
        }
        public bool WordBreak(string s, IList<string> wordDict)
        {
            List<string>[] dic = new List<string>[26];
            for (int i = 0; i < 26; ++i) dic[i] = new List<string>();
            foreach (var word in wordDict)
                dic[word[0] - 'a'].Add(word);

            bool[] dp = new bool[s.Length];
            List<WordComp> list = new List<WordComp>();
            for (int i = 0; i < s.Length; ++i)
            {
                if (i == 0 || dp[i - 1])
                {
                    list.AddRange(dic[s[i] - 'a'].Select(t => new WordComp { word = t, index = 0 }));
                }

                for (int j = list.Count - 1; j >= 0; --j)
                {
                    if (s[i] == list[j].word[list[j].index++])
                    {
                        if (list[j].index == list[j].word.Length)
                        {
                            dp[i] = true;
                            list.RemoveAt(j);
                        }
                    }
                    else
                    {
                        list.RemoveAt(j);
                    }
                }
            }

            return dp[s.Length - 1];
        }
    }
}
