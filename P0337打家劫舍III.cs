using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1
{
    // 递归，动态规划（记忆化回溯）
    class P0337打家劫舍III
    {
        // dp表示包含当前节点（有可能偷也有可能不偷）的子树所获得的最大收益
        Dictionary<TreeNode, int> dp = new Dictionary<TreeNode, int>();
        // dp0表示不偷当前节点的情况下能获得的最大收益
        Dictionary<TreeNode, int> dp0 = new Dictionary<TreeNode, int>();

        public int NoRob(TreeNode root)
        {
            if (root == null) return 0;
            if (dp0.ContainsKey(root)) return dp0[root];
            return dp0[root] = Rob(root.left) + Rob(root.right);
        }
        public int Rob(TreeNode root)
        {
            if (root == null) return 0;
            if (dp.ContainsKey(root)) return dp[root];
            return dp[root] = Math.Max(NoRob(root), root.val + NoRob(root.left) + NoRob(root.right));
        }
    }
}
