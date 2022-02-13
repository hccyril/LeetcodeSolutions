using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1
{
    /*
    请判断原始的序列 org 是否可以从序列集 seqs 中唯一地 重建 。

    序列 org 是 1 到 n 整数的排列，其中 1 ≤ n ≤ 104。重建 是指在序列集 seqs 中构建最短的公共超序列，即  seqs 中的任意序列都是该最短序列的子序列。

    示例 1：
    输入: org = [1,2,3], seqs = [[1,2],[1,3]]
    输出: false
    解释：[1,2,3] 不是可以被重建的唯一的序列，因为 [1,3,2] 也是一个合法的序列。

    示例 2：
    输入: org = [1,2,3], seqs = [[1,2]]
    输出: false
    解释：可以重建的序列只有 [1,2]。

    示例 3：
    输入: org = [1,2,3], seqs = [[1,2],[1,3],[2,3]]
    输出: true
    解释：序列 [1,2], [1,3] 和 [2,3] 可以被唯一地重建为原始的序列 [1,2,3]。
    
    示例 4：
    输入: org = [4,1,5,2,6,3], seqs = [[5,2,6,3],[4,1,5,2]]
    输出: true

    提示：
    1 <= n <= 10^4
    org 是数字 1 到 n 的一个排列
    1 <= segs[i].length <= 10^5
    seqs[i][j] 是 32 位有符号整数
 
    来源：力扣（LeetCode）
    链接：https://leetcode-cn.com/problems/ur2n8P
    著作权归领扣网络所有。商业转载请联系官方授权，非商业转载请注明出处。
     */

    // medium, plus, 2022/2/10
    // 拓扑排序
    internal class P0444序列重建
    {
        public class PreStruct
        {
            public int Count { get; private set; } = 0;
            public bool this[int idx] => ba[idx - 1];
            BitArray ba;
            public void Set(int n)
            {
                --n;
                if (!ba[n])
                {
                    ba[n] = true;
                    ++Count;
                }
            }
            public void UnSet(int n)
            {
                --n;
                if (ba[n])
                {
                    ba[n] = false;
                    --Count;
                }
            }
            public PreStruct(int n) => ba = new(n);
        }
        public bool SequenceReconstruction(int[] org, IList<IList<int>> seqs)
        {
            if (org.Length == 1 && org[0] == 1 && seqs.Count == 0) return false; // 坑死人的弱智用例
            int n = org.Length, count = n; // count: 前置节点为0的节点数量
            PreStruct[] parr = new PreStruct[n + 1];
            HashSet<int>[] harr = new HashSet<int>[n + 1];
            for (int i = 1; i <= n; ++i)
            {
                parr[i] = new(n);
                harr[i] = new();
            }
            foreach (var sq in seqs)
            {
                int a = -1;
                foreach (int b in sq)
                {
                    if (b < 1 || b > n) return false;
                    if (a > 0)
                    {
                        if (parr[a][b]) return false;
                        if (parr[b].Count == 0) --count;
                        parr[b].Set(a);
                        harr[a].Add(b);
                    }
                    a = b;
                }
            }
            for (int i = 1; i <= n; ++i)
            {
                int o = org[i - 1];
                if (count != i || parr[o].Count != 0) return false;
                foreach (int next in harr[o])
                {
                    parr[next].UnSet(o);
                    if (parr[next].Count == 0) ++count;
                }
            }
            return true;
        }
    }
}
