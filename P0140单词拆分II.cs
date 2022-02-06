using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1
{
    class P0140单词拆分II
    {
        class WordComp
        {
            public string word;
            public int index;
        }

        public IList<string> WordBreak(string s, IList<string> wordDict)
        {
            List<string>[] dic = new List<string>[26];
            for (int i = 0; i < 26; ++i) dic[i] = new List<string>();
            foreach (var word in wordDict)
                dic[word[0] - 'a'].Add(word);

            List<string>[] dp = new List<string>[s.Length];
            List<WordComp> list = new List<WordComp>();
            for (int i = 0; i < s.Length; ++i)
            {
                if (i == 0 || dp[i - 1]?.Any() == true)
                {
                    list.AddRange(dic[s[i] - 'a'].Select(t => new WordComp { word = t, index = 0 }));
                }

                for (int j = list.Count - 1; j >= 0; --j)
                {
                    if (s[i] == list[j].word[list[j].index++])
                    {
                        if (list[j].index == list[j].word.Length)
                        {
                            if (dp[i] == null) dp[i] = new List<string>();
                            dp[i].Add(list[j].word);
                            list.RemoveAt(j);
                        }
                    }
                    else
                    {
                        list.RemoveAt(j);
                    }
                }
            }

            IList<string> ans = new List<string>();
            Dfs(ans, dp, s.Length - 1, new List<string>());
            return ans;
        }

        void Dfs(IList<string> ans, List<string>[] dp, int i, IList<string> list)
        {
            if (i == -1)
            {
                ans.Add(string.Join(' ', list));
                return;
            }
            foreach (var word in dp[i] ?? new List<string>())
            {
                list.Insert(0, word);
                Dfs(ans, dp, i - word.Length, list);
                list.RemoveAt(0);
            }
        }
    }
}
