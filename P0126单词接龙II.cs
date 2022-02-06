using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1
{
    class P0126单词接龙II
    {
        class WordCount
        {
            public string word;
            public int length => word.Length;
            public IList<WordCount> prevList = new List<WordCount>();
            public int count = 0;
            public bool Compare(WordCount wo)
            {
                bool shouldEnqueue = count == 0;
                if (shouldEnqueue || count == wo.count + 1)
                {
                    int path = 0;
                    for (int i = 0; i < length; ++i)
                    {
                        if (word[i] != wo.word[i]) ++path;
                        if (path > 1) break;
                    }
                    if (path == 1)
                    {
                        count = wo.count + 1;
                        prevList.Add(wo);
                        return shouldEnqueue;
                    }
                }
                return false;
            }

            public IList<IList<string>> Output(IList<IList<string>> ans)
            {
                if (count == 0)
                {
                    IList<string> li = new List<string>();
                    li.Add(word);
                    ans.Add(li);
                    return ans;
                }

                foreach (var prev in prevList)
                    prev.Output(ans);

                foreach (var li in ans)
                    if (li.Count == count)
                        li.Add(word);

                return ans;
            }
        }

        // P126单词接龙I
        public int LadderLength(string beginWord, string endWord, IList<string> wordList)
        {
            IList<IList<string>> ans = new List<IList<string>>();
            List<WordCount> list = new List<WordCount>();
            WordCount end = null;
            foreach (var w in wordList)
            {
                var wo = new WordCount { word = w };
                list.Add(wo);
                if (w == endWord) end = wo;
            }
            if (end == null) return 0;

            Queue<WordCount> qu = new Queue<WordCount>();
            qu.Enqueue(new WordCount { word = beginWord });
            while (qu.Any())
            {
                var wo = qu.Dequeue();
                foreach (var wi in list)
                    if (wi.Compare(wo))
                    {
                        qu.Enqueue(wi);
                        if (wi == end) return wi.count + 1;
                    }
            }

            return 0;
        }

        // P127
        public IList<IList<string>> FindLadders(string beginWord, string endWord, IList<string> wordList)
        {
            IList<IList<string>> ans = new List<IList<string>>();
            List<WordCount> list = new List<WordCount>();
            WordCount end = null;
            int minCount = 0;
            foreach (var w in wordList)
            {
                var wo = new WordCount { word = w };
                list.Add(wo);
                if (w == endWord) end = wo;
            }
            if (end == null) return ans;

            Queue<WordCount> qu = new Queue<WordCount>();
            qu.Enqueue(new WordCount { word = beginWord });
            while (qu.Any())
            {
                var wo = qu.Dequeue();
                if (minCount > 0 && wo.count == minCount) break;
                foreach (var wi in list)
                    if (wi.Compare(wo))
                    {
                        qu.Enqueue(wi);
                        if (wi == end) minCount = wi.count;
                    }
            }

            if (end.count == 0) return ans;
            return end.Output(ans);
        }

        // 第一版出现了重复，用这个方法解决了
        IList<IList<string>> RemoveDup(IList<IList<string>> ans)
        {
            HashSet<string> hs = new HashSet<string>(0);
            for (int i = ans.Count - 1; i >= 0; --i)
            {
                string key = string.Join(',', ans[i]);
                if (hs.Contains(key))
                    ans.RemoveAt(i);
                else
                    hs.Add(key);
            }
            return ans;
        }
    }
}
