namespace ConsoleCore1.contest;

// 完成情况较好，全部一次提交通过
// 143 / 3873	呱呱编程实验室 	17	0:45:00	0:02:15	0:18:45	0:29:03	0:45:00
internal class LC_WC354_20230716
{
    #region Problem A
    //class SolutionA :
    //    def sumOfSquares(self, nums: List[int]) -> int:
    //        return sum(x* x for i, x in enumerate(nums) if len(nums) % (i + 1) == 0)
    #endregion

    #region Problem B
    public int MaximumBeauty(int[] nums, int k)
    {
        Array.Sort(nums);
        int h = nums[0] - k;
        int maxn = 1;
        for (int i = 0, j = 0; j < nums.Length; ++j)
        {
            h = nums[j] - k;
            while (nums[i] + k < h) ++i;
            maxn = Math.Max(maxn, j - i + 1);
        }
        return maxn;
    }
    #endregion

    #region Problem C
    /*
    class SolutionC:
        def minimumIndex(self, nums: List[int]) -> int:
            n = len(nums)
            cn = Counter(nums)
            h, c = 0, 0
            for x, count in cn.most_common(1):
                h = x
                c = count
            ps = [0] * len(nums)
            n1, n2 = 0, c
            for i, x in enumerate(nums):
                if x == h:
                    n1 += 1
                    n2 -= 1
                if (n1 << 1) > i + 1 and (n2 << 1) > n - i - 1:
                    return i
            return -1
        * */
    #endregion

    #region Problem D

    // 字典树/AC自动机，拿1032改一下就好了
    readonly WordTree tree = new();
    readonly List<WordTree> list = new();

    public int LongestValidSubstring(string word, IList<string> forbidden)
    {
        foreach (var w in forbidden)
            tree.AddWord(w);
        int maxn = 0;
        for (int i = 0, j = 0; j <  word.Length; ++j)
        {
            int l = Query(word[j]);
            if (l > 0)
            {
                i = j - l + 2;
                j = i - 1;
                list.Clear();
            }
            maxn = Math.Max(maxn, j - i + 1);
        }
        return maxn;
    }

    public int Query(char letter)
    {
        int len = 0;
        for (int i = list.Count - 1; i >= 0; --i)
        {
            if (list[i].ContainsChar(letter))
            {
                list[i] = list[i][letter];
                if (list[i].Word != null) len = list[i].Word.Length;
            }
            else
            {
                list[i] = list[list.Count - 1];
                list.RemoveAt(list.Count - 1);
            }
        }
        if (tree.ContainsChar(letter))
        {
            list.Add(tree[letter]);
            if (list[^1].Word != null) len = list[^1].Word.Length;
        }
        return len;
    }
    #endregion

    #region Problem E
    public int SolveE(int x)
    {
        return x;
    }
    #endregion

    #region Run Test
    internal static int Run()
    {
        char p = 'D';
        LC_WC354_20230716 sln = new();

        return p switch
        {
            'A' => sln.RunTestA(),
            'B' => sln.RunTestB(),
            'C' => sln.RunTestC(),
            'D' => sln.RunTestD(),
            'E' => sln.RunTestE(),
            _ => -1
        };
    }

    int RunTestA()
    {
        return 0;
    }

    int RunTestB()
    {
        return 0;
    }

    int RunTestC()
    {
        return 0;
    }

    int RunTestD()
    {
        return 0;
    }

    int RunTestE()
    {
        return 0;
    }
    #endregion
}

