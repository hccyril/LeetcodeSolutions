using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1
{
    class P0095不同的二叉搜索树II
    {
        int N;
        int maps, changeFlags;
        IList<TreeNode> ans;
        TreeNode root;

        TreeNode CopyTree(TreeNode rn)
        {
            if (rn == null || rn.val <= 0) return null;
            return new TreeNode(rn.val, CopyTree(rn.left), CopyTree(rn.right));
        }

        // tn: null表示创建一个新的，否则查找该node,找到且位置正确设为i，否则保持-1返回
        TreeNode SearchNode(int i, TreeNode tn)
        {
            TreeNode rn = root;
            Stack<int> stk = new Stack<int>();
            while (rn != null && rn.val > 0)
            {
                stk.Push(rn.val);
                if (i < rn.val)
                {
                    if (rn.left == null) rn.left = new TreeNode(0);
                    rn = rn.left;
                }
                else
                {
                    if (rn.right == null) rn.right = new TreeNode(0);
                    rn = rn.right;
                }
            }
            if (tn == null)
            {
                rn.val = i;
                while (stk.Any()) changeFlags |= 1 << stk.Pop();
                return rn;
            }
            else if (rn.val == -1)
            {
                rn.val = i;
                return rn;
            }
            else return tn; // tn.val == -1 && rn没定位到tn
        }
        void Permute(int n)
        {
            if (n == 0)
            {
                ans.Add(CopyTree(root));
                return;
            }
            TreeNode tn = null;
            for (int i = 1; i <= N; ++i)
            {
                int bit = 1 << i;
                if ((maps & bit) == 0)
                {
                    tn = SearchNode(i, tn);
                    if (tn.val == i)
                    {
                        maps |= bit;
                        Permute(n - 1);
                        maps ^= bit;
                        if ((changeFlags & bit) != 0)
                        {
                            changeFlags ^= bit;
                            tn.val = -1;
                        }
                        else break;
                    }
                }
            }
            if (tn != null) tn.val = 0;
        }
        public IList<TreeNode> GenerateTrees(int n)
        {
            N = n;
            maps = 0;
            ans = new List<TreeNode>();
            root = new TreeNode(0);
            Permute(n);
            return ans;
        }

        public static void Run()
        {
            int input = 3;
            var output = new P0095不同的二叉搜索树II().GenerateTrees(input);
            Console.WriteLine(output.Count + "\r\n" + string.Join('\n', output));
        }
    }
}
