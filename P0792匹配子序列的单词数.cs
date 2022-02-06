using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1
{
    /*
     *  Number of Matching Subsequences
https://leetcode-cn.com/problems/number-of-matching-subsequences/

Given a string s and an array of strings words, return the number of words[i] that is a subsequence of s.

A subsequence of a string is a new string generated from the original string with some characters (can be none) deleted without changing the relative order of the remaining characters.

For example, "ace" is a subsequence of "abcde".
 

Example 1:

Input: s = "abcde", words = ["a","bb","acd","ace"]
Output: 3
Explanation: There are three strings in words that are a subsequence of s: "a", "acd", "ace".
Example 2:

Input: s = "dsahjpjauf", words = ["ahjpjau","ja","ahbwzgqnuk","tnmlanowax"]
Output: 2
 

Constraints:

1 <= s.length <= 5 * 104
1 <= words.length <= 5000
1 <= words[i].length <= 50
s and words[i] consist of only lowercase English letters.
     * */
    class P0792匹配子序列的单词数
    {
        static int GetIndex(char c)
        {
            return c - 'a';
        }

        class WordCount
        {
            public WordCount(string word) => this.word = word;
            public string word;
            public int index = 0;
            public int CharIndex => GetIndex(word[index]);
        }
        class Counter
        {
            public int count = 0;
            List<WordCount>[] arr = new List<WordCount>[26];
            public Counter()
            {
                for (int i = 0; i < 26; ++i)
                    arr[i] = new List<WordCount>();
            }
            
            public void Add(string word)
            {
                WordCount wc = new WordCount(word);
                arr[wc.CharIndex].Add(wc);
            }

            public void CountChar(char ch)
            {
                var cInd = GetIndex(ch);
                var list = arr[cInd];
                for (int i = list.Count - 1; i >= 0; --i)
                {
                    var wc = list[i];
                    wc.index++;
                    if (wc.index >= wc.word.Length)
                    {
                        list.RemoveAt(i);
                        count++;
                    }
                    else if (wc.CharIndex != cInd)
                    {
                        list.RemoveAt(i);
                        arr[wc.CharIndex].Add(wc);
                    }
                }
            }
        }

        public int NumMatchingSubseq(string s, string[] words)
        {
            Counter c = new Counter();
            foreach (var word in words) c.Add(word);
            foreach (var ch in s) c.CountChar(ch);
            return c.count;
        }
    }
}
