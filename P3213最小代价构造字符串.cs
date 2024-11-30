using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1;

// hard, 2024/7/7 WC405-D
// AC自动机

public static class AcAuto
{

    static TrieNode root;
    static TrieNode temp;

    public static void Build(IEnumerable<string> words)
    {
        root = new();
        foreach (string word in words)
        {
            TrieNode cur = root;
            foreach (char ch in word)
            {
                cur = cur.GetChild(ch - 'a');
            }
            cur.Words.Add(word);
        }
        root.Fail = root; 
        Queue<TrieNode> q = new();
        for (int i = 0; i < 26; i++)
        {
            if (root.HasChild(i))
            {
                root[i].Fail = root;
                q.Enqueue(root[i]);
            }
            else
            {
                root[i] = root;
            }
        }
        while (q.Count > 0)
        {
            TrieNode node = q.Dequeue();
            if (node.Fail != node && node.Fail.IsEnd)
            {
                node.Words.AddRange(node.Fail.Words);
            }
            for (int i = 0; i < 26; i++)
            {
                if (node.HasChild(i))
                {
                    node[i].Fail = node.Fail[i];
                    q.Enqueue(node[i]);
                }
                else
                {
                    node[i] = node.Fail[i];
                }
            }
        }

        temp = root;
    }

    public static IList<string> Query(char letter)
    {
        temp = temp[letter - 'a'];
        return temp.Words;
    }
}

class TrieNode
{
    TrieNode[] children = Array.Empty<TrieNode>();
    public bool IsEnd => Words.Any();
    public TrieNode Fail { get; set; }
    public List<string> Words { get; } = new();

    public bool HasChild(int index) => children.Any() && children[index] != null;
    public TrieNode GetChild(int index)
    {
        if (this[index] == null) this[index] = new();
        return this[index];
    }
    public TrieNode this[int index]
    {
        get
        {
            if (children.Length == 0)
            {
                children = new TrieNode[26];
            }
            return children[index];
        }
        set
        {
            if (children.Length == 0)
            {
                children = new TrieNode[26];
            }
            children[index] = value;
        }
    }
}

internal class P3213最小代价构造字符串
{
    public int MinimumCost(string target, string[] words, int[] costs)
    {
        var di = new Dictionary<string, int>();
        int m = words.Length;
        for (int i = 0; i < m; ++i)
        {
            string w = words[i]; int c = costs[i];
            if (!di.TryAdd(w, c))
                di[w] = Math.Min(c, di[w]);
        }
        AcAuto.Build(di.Keys);
        Span<int> dp = stackalloc int[target.Length + 1];
        dp.Fill(999999999); dp[0] = 0;
        for (int i = 1; i <= target.Length; ++i)
        {
            var x = target[i - 1];
            foreach (var w in AcAuto.Query(x))
            {
                int c = di[w];
                dp[i] = Math.Min(dp[i], dp[i - w.Length] + c);
            }
        }
        return dp[^1] >= 999999999 ? -1 : dp[^1];
    }

    internal static void Run()
    {
        var sln = new P3213最小代价构造字符串();
        string target = "abcdef";
        string[] words = { "abdef", "abc", "d", "def", "ef" };
        int[] costs = { 100, 1, 1, 10, 5 };
        Console.WriteLine(sln.MinimumCost(target, words, costs));
    }
}
