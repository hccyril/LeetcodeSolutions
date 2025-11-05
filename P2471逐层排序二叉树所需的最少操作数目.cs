using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1;

internal class P2471逐层排序二叉树所需的最少操作数目
{
    public int MinimumOperations(TreeNode root)
    {
        List<TreeNode> li = new() { root };
        int ans = 0;
        while (li.Any())
        {
            List<TreeNode> cl = new();
            foreach (TreeNode tn in li)
            {
                if (tn.left != null) cl.Add(tn.left);
                if (tn.right != null) cl.Add(tn.right);
            }

            // sort
            var di = Enumerable.Range(0, li.Count).ToDictionary(i => li[i].val, i => i);
            var a = li.Select(t => t.val).OrderBy(v => v).ToArray();
            for (int i = 0; i < li.Count; ++i)
            {
                int j = i, v = li[i].val;
                while (v != a[j])
                {
                    ++ans;
                    li[j].val = li[j = di[a[j]]].val;
                }
                if (li[j].val != v) li[j].val = v; // WA: 一开始漏了这句
            }
            li = cl;
        }
        return ans;
    }

    internal static void Run()
    {
        string s = "[1,4,3,7,6,8,5,null,null,null,null,9,null,10]";
        var root = TreeNode.FromInput(s);
        Console.WriteLine("{0} \n ans={1}", nameof(P2471逐层排序二叉树所需的最少操作数目),
            new P2471逐层排序二叉树所需的最少操作数目().MinimumOperations(root));
    }
}
