using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1
{
    // hard, 2022/4/16
    internal class P1373二叉搜索子树的最大键值和
    {
        class TreeStruct
        {
            public int max, min, sum;
            public bool isBst;
        }
        Dictionary<TreeNode, TreeStruct> dic = new();
        public int MaxSumBST(TreeNode root) 
        {
            dic.TryAdd(root, new());
            var ts = dic[root];
            if (root.left == null && root.right == null)
            {
                ts.isBst = true;
                ts.max = ts.min = ts.sum = root.val;
                return Math.Max(root.val, 0); // 坑！最大和为负数时空树也算，和为0 
            }
            else if (root.right == null)
            {
                int sum = MaxSumBST(root.left);
                var left = dic[root.left];
                if (left.isBst && root.val > left.max)
                {
                    ts.isBst = true;
                    ts.sum = left.sum + root.val;
                    ts.min = left.min;
                    ts.max = root.val;
                    return Math.Max(sum, ts.sum);
                }
                return sum;
            }
            else if (root.left == null)
            {
                int sum = MaxSumBST(root.right);
                var right = dic[root.right];
                if (right.isBst && root.val < right.min)
                {
                    ts.isBst = true;
                    ts.sum = right.sum + root.val;
                    ts.max = right.max;
                    ts.min = root.val;
                    return Math.Max(sum, ts.sum);
                }
                return sum;
            }
            else
            {
                int sum = Math.Max(MaxSumBST(root.left), MaxSumBST(root.right));
                TreeStruct left = dic[root.left], right = dic[root.right];
                if (left.isBst && right.isBst && root.val > left.max && root.val < right.min)
                {
                    ts.isBst = true;
                    ts.sum = left.sum + root.val + right.sum;
                    ts.max = right.max;
                    ts.min = left.min;
                    return Math.Max(sum, ts.sum);
                }
                return sum;
            }

            //*DEBUG*/Console.WriteLine($"val={root.val} bst={ts.isBst} max={ts.max} min={ts.min} sum={ts.sum}");

        }
    }
}
