using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1
{
    class P0113路径总和II
    {
        IList<IList<int>> ansList = new List<IList<int>>();
        List<int> list = new List<int>();

        public IList<IList<int>> PathSum(TreeNode root, int targetSum)
        {
            if (root == null) return ansList; // WA: 测试数据有空集 []
            if (root.left == null && root.right == null && targetSum == root.val)
            {
                IList<int> ans = new List<int>(list);
                ans.Add(root.val);
                ansList.Add(ans);
            }
            else // if (root.val <= /*>=*/ targetSum) // 对于第一个测试数据，判断条件反了 // WA: val和sum可以为负数
            {
                targetSum -= root.val;
                list.Add(root.val);
                if (root.left != null) PathSum(root.left, targetSum);
                if (root.right != null) PathSum(root.right, targetSum);
                list.RemoveAt(list.Count - 1);
            }
            return ansList;
        }
    }
}
