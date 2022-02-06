using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1
{
    // medium, 前缀树应用
    class P0211添加与搜索单词
    {
        class WordTree
        {
            public Dictionary<char, WordTree> next;
            public string word;
            //public HashSet<string> words;
            public WordTree this[char c]
            {
                get
                {
                    if (next == null) next = new Dictionary<char, WordTree>();
                    if (!next.ContainsKey(c)) next[c] = new WordTree();
                    return next[c];
                }
            }
            public bool ContainsChar(char c) => next?.ContainsKey(c) == true;
            public bool End() => next == null;

            public void BuildTree(string word, int i)
            {
                if (i >= word.Length)
                {
                    this.word = word;
                    //words.Add(word);
                    return;
                }
                this[word[i]].BuildTree(word, i + 1);
                //this['.'].BuildTree(word, i + 1);
            }

            public bool Find(string word, int i)
            {
                if (i >= word.Length)
                {
                    return !string.IsNullOrEmpty(this.word);
                }
                if (word[i] == '.' && next != null)
                {
                    foreach (var nextTree in next.Values)
                        if (nextTree.Find(word, i + 1)) return true;
                }
                else if (word[i] != '.' && ContainsChar(word[i]))
                    return this[word[i]].Find(word, i + 1);
                return false;
            }
        }

        public class WordDictionary
        {
            WordTree tree;

            public WordDictionary()
            {
                tree = new WordTree();
            }

            public void AddWord(string word)
            {
                tree.BuildTree(word, 0);
            }

            public bool Search(string word)
            {
                return tree.Find(word, 0);
            }
        }
    }
}
