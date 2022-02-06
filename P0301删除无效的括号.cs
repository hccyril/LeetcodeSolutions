using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1
{
    // 真 困难题 really hard
    // 回溯
    class P0301删除无效的括号
    {
        HashSet<string> ansList;
        static readonly char[] P_CHARS = { ')', 'a', '(' };
        IEnumerable<int> Range(string s, int direct = 1) =>
            direct > 0 ? Enumerable.Range(0, s.Length) : Enumerable.Range(0, s.Length).Reverse();
        IEnumerable<int> EnumChar(string s, int map, int direct = 1) =>
            Range(s, direct).Where(i => ((1 << i) & map) != 0);
        void AddAnswer(string s, int map) =>
            ansList.Add(string.Join("", EnumChar(s, map).Select(i => s[i])));
        int PaValid(string s, int map)
        {
            int n = 0;
            foreach (var i in EnumChar(s, map))
                if (s[i] == '(') n++;
                else if (s[i] == ')' && n-- <= 0) 
                    return 1; // 右括号多了
            return n > 0 ? -1 : 0; // 如果 n > 0 表示左括号多了
        }
        public IEnumerable<int> Parse(string s, int map, int direct = 1)
        {
            char left = P_CHARS[1 + direct], right = P_CHARS[1 - direct], lastChar = 'a';
            int n = 0;
            List<int> rl = new List<int>(); // 右括号的索引
            foreach (var i in EnumChar(s, map, direct))
            {
                if (s[i] == left) n++;
                else if (s[i] == right)
                {
                    // 添加右括号，注意连续右括号时，删任意一个效果一样的，例如))) -> ))
                    if (s[i] == lastChar) rl[rl.Count - 1] = i;
                    else rl.Add(i);
                    // 当右边括号比左边多了一个时，必须删掉其中一个
                    if (n-- <= 0)
                        return rl.Select(t => map ^ (1 << t));
                }
                lastChar = s[i];
            }
            return new int[0];
        }
        HashSet<int> processedMaps;
        void Dfs(string s, int map)
        {
            if (!processedMaps.Add(map)) return;
            int check = PaValid(s, map);
            if (check == 0) AddAnswer(s, map);
            else foreach (var next in Parse(s, map, check)) Dfs(s, next);
        }
        public IList<string> RemoveInvalidParentheses(string s)
        {
            ansList = new HashSet<string>();
            processedMaps = new HashSet<int>();
            Dfs(s, (1 << s.Length) - 1);
            return ansList.ToList();
        }
    }
    /* Test Cases
        "()())()"
        ")("
        ")a("
        ")()("
        "(((k()(("
     * */
    /* 评论内容
还是相当难的一道题目，建议先把哈希表、位运算、回溯等基本技能掌握，以及先完成本站内其他括号匹配的题目，再来挑战这道题

先说两个前提条件：

1. 不用左右括号都考虑，因为数据是对称的，从左往右扫描一次之后再用同样算法从右往左扫描一次就可以了，因此只需要考虑需要删除右括号的一种情况
2. 看到有评论说因为求删除最少的括号所以要用BFS，这个是不必要的，等下可以看到要删除多少个括号是很容易算出来的，类似贪心法，而真正需要分支搜索的只有删除括号后的每个不同解，因此这是一题典型的回溯

下面来说下算法和优化的代码细节：

1. 从头开始扫描，数左括号和右括号的数量，当当前第一次右括号比左括号多的时候，可以明显看出此时除了删除一个右括号以外没有任何方法能使当前的子串括号匹配，此时便可以删除一个右括号，然后回溯进一步搜索
2. 不难证明上一步中删除任意一个右括号都是合法的，例如((a)a)a)，可以得到三个解((aa)a)， ((a)aa)， ((a)a)a
3. 当右括号是连续排列的时候，例如)))，删除任何一个得到的结果都是一样的))，这里可以进行剪枝优化
4. 字符串最长只有25位，可以用一个整型表示它的子串，对应位的1表示保留，0表示删除，这样极大地优化了内存空间，只有在输出结果的时候我们才把字符串构建出来
5. 以上都做完之后仍然会有重复解，因此除了中间结果要哈希，最终结果也要

最后是完整代码，因为一些地方用LINQ顺手因此还是用了最熟悉的C#

```C#
public class Solution {
    HashSet<string> ansList;
    static readonly char[] P_CHARS = { ')', 'a', '(' };
    IEnumerable<int> Range(string s, int direct = 1) =>
        direct > 0 ? Enumerable.Range(0, s.Length) : Enumerable.Range(0, s.Length).Reverse();
    IEnumerable<int> EnumChar(string s, int map, int direct = 1) =>
        Range(s, direct).Where(i => ((1 << i) & map) != 0);
    void AddAnswer(string s, int map) =>
        ansList.Add(string.Join("", EnumChar(s, map).Select(i => s[i])));
    int PaValid(string s, int map)
    {
        int n = 0;
        foreach (var i in EnumChar(s, map))
            if (s[i] == '(') n++;
            else if (s[i] == ')' && n-- <= 0) 
                return 1; // 右括号多了
        return n > 0 ? -1 : 0; // 如果 n > 0 表示左括号多了
    }
    public IEnumerable<int> Parse(string s, int map, int direct = 1)
    {
        char left = P_CHARS[1 + direct], right = P_CHARS[1 - direct], lastChar = 'a';
        int n = 0;
        List<int> rl = new List<int>(); // 右括号的索引
        foreach (var i in EnumChar(s, map, direct))
        {
            if (s[i] == left) n++;
            else if (s[i] == right)
            {
                // 添加右括号，注意连续右括号时，删任意一个效果一样的，例如))) -> ))
                if (s[i] == lastChar) rl[rl.Count - 1] = i;
                else rl.Add(i);
                // 当右边括号比左边多了一个时，必须删掉其中一个
                if (n-- <= 0)
                    return rl.Select(t => map ^ (1 << t));
            }
            lastChar = s[i];
        }
        return new int[0];
    }
    HashSet<int> processedMaps;
    void Dfs(string s, int map)
    {
        if (!processedMaps.Add(map)) return;
        int check = PaValid(s, map);
        if (check == 0) AddAnswer(s, map);
        else foreach (var next in Parse(s, map, check)) Dfs(s, next);
    }
    public IList<string> RemoveInvalidParentheses(string s)
    {
        ansList = new HashSet<string>();
        processedMaps = new HashSet<int>();
        Dfs(s, (1 << s.Length) - 1);
        return ansList.ToList();
    }
}
```

     * */
}
