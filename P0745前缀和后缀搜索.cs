using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1
{
    // hard, 前缀树 -> 前后缀树
    // 写了两个小时，各种老毛病，nullreference, keynotfound...代码能力要重回巅峰还需时日
    // 2022/1/28 提交通过，但2022/7/14每日一题重新提交时却又runtime error
    public class WordFilter
    {
        public WordFilter(string[] words)
        {
            int i = 0;
            foreach (var word in words)
                AddWord(word, i++);
        }

        public int F(string prefix, string suffix)
        {
            WordFilter tree = this;
            List<WordFilter> list = null;
            for (int i = 0, j = suffix.Length - 1; i < prefix.Length || j >= 0; ++i, --j)
            {
                char pre = ' ', post = ' ';
                if (i < prefix.Length) pre = prefix[i];
                if (j >= 0) post = suffix[j];
                if (pre != ' ' && post != ' ')
                {
                    tree = tree.TryGet(pre, post);
                    if (tree == null) return -1;
                }
                else if (pre != ' ')
                {
                    if (list == null) list = new() { tree };
                    tree = null;
                    List<WordFilter> listNew = new();
                    foreach (var tf in list)
                        if (tf.ContainsPreChar(pre))
                            foreach (var t in tf.PreNext(pre))
                                listNew.Add(t);
                    list = listNew;
                }
                else if (post != ' ')
                {
                    if (list == null) list = new() { tree };
                    tree = null;
                    List<WordFilter> listNew = new();
                    foreach (var tf in list)
                        if (tf.ContainsPostChar(post))
                            foreach (var t in tf.PostNext(post))
                                listNew.Add(t);
                    list = listNew;
                }
            }
            if (tree != null)
                list = new() { tree };
            int maxIndex = -1;
            foreach (var tf in list)
                maxIndex = Math.Max(maxIndex, tf.FindMaxIndex());
            return maxIndex;
        }

        Dictionary<int, WordFilter> next;
        Dictionary<char, HashSet<int>> preList;
        Dictionary<char, HashSet<int>> postList;
        static int Key(char a, char b) => (a - 'a' << 6) | (b - 'a');
        public string Word { get; private set; }
        public int Index { get; private set; }
        public WordFilter() { }
        public WordFilter this[char pre, char post]
        {
            get
            {
                if (next == null)
                {
                    next = new();
                    preList = new();
                    postList = new();
                }
                int key = Key(pre, post);
                if (!next.ContainsKey(key)) next[key] = new();
                if (!preList.ContainsKey(pre)) preList[pre] = new();
                preList[pre].Add(key);
                if (!postList.ContainsKey(post)) postList[post] = new();
                postList[post].Add(key);
                return next[key];
            }
        }
        public WordFilter TryGet(char pre, char post)
        {
            int key = Key(pre, post);
            if (next == null || !next.ContainsKey(key)) return null; // 2022/7/14 原来少了next == null判断
            return next[key];
        }
        public IEnumerable<WordFilter> PreNext(char pre)
            => from key in preList[pre]
               select next[key];
        public IEnumerable<WordFilter> PostNext(char post)
            => from key in postList[post]
               select next[key];
        public bool ContainsPreChar(char c) => preList != null && preList.ContainsKey(c);
        public bool ContainsPostChar(char c) => postList != null && postList.ContainsKey(c);
        //public bool ContainsWord(string s, int i)
        //    => ContainsChar(s[i]) && (i == s.Length - 1 && next[s[i]].Word != null || ContainsWord(s, i + 1));
        public bool End() => next == null;
        public void AddWord(string word, int index)
        {
            WordFilter tree = this;
            for (int i = 0, j = word.Length - 1; j >= 0; ++i, --j)
                tree = tree[word[i], word[j]];
            tree.Word = word; tree.Index = index;
        }
        public int FindMaxIndex()
        {
            int i = -1;
            if (Word != null) i = Index;
            if (next != null)
                foreach (var nt in next.Values)
                    i = Math.Max(i, nt.FindMaxIndex());
            return i;
        }
    }

    internal class P0745前缀和后缀搜索
    {
        internal static void Run()
        {
            // 2022/7/14
            var input = Common.ReadInput<string[][]>(745);
            WordFilter wf = null;
            var actions = input[0];
            int i = 1;
            foreach (var act in actions)
            {
                var args = input[i++];
                if (act == "WordFilter")
                {
                    wf = new(args);
                }
                else
                {
                    wf.F(args[0], args[1]);
                }
            }
            Console.WriteLine("Done");

//            string[] input = { "apple" };
//            WordFilter wt = new(input);
//            Console.WriteLine(wt.F("a", "e"));

//            // compile error
//            var actions = new string[] { "WordFilter", "f", "f", "f", "f", "f", "f", "f", "f", "f", "f" };
//            var args =
//new string[][] {
//new string[] {"cabaabaaaa","ccbcababac","bacaabccba","bcbbcbacaa","abcaccbcaa","accabaccaa","cabcbbbcca","ababccabcb","caccbbcbab","bccbacbcba"},new string[] {"bccbacbcba","a"},new string[] {"ab","abcaccbcaa"},new string[] {"a","aa"},new string[] {"cabaaba","abaaaa"},new string[] {"cacc","accbbcbab"},new string[] {"ccbcab","bac"},new string[] {"bac","cba"},new string[] {"ac","accabaccaa"},new string[] {"bcbb","aa"},new string[] {"ccbca","cbcababac"}};
//            for (int i = 0; i < actions.Length; ++i)
//            {
//                if (actions[i] == "WordFilter")
//                    wt = new(args[i]);
//                else
//                    Console.WriteLine(wt.F(args[i][0], args[i][1]));
//            }
        }
    }
}
