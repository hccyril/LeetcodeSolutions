using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1
{
    class P0124二叉树中的最大路径和
    {
        int max;
        int Path(TreeNode root)
        {
            int l = root.left == null ? -300000000 : Path(root.left);
            int r = root.right == null ? -300000000 : Path(root.right);
            int sum = (l > 0 ? l : 0) + (r > 0 ? r : 0) + root.val;
            if (sum > max) max = sum;
            return Math.Max(l > 0 ? l : 0, r > 0 ? r : 0) + root.val;
        }
        public int MaxPathSum(TreeNode root)
        {
            if (root == null) return 0;
            max = -300000000;
            Path(root);
            return max;
        }
    }
}
