using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1
{
    /*
     * 存在一个由 n 个节点组成的无向连通图，图中的节点按从 0 到 n - 1 编号。

给你一个数组 graph 表示这个图。其中，graph[i] 是一个列表，由所有与节点 i 直接相连的节点组成。

返回能够访问所有节点的最短路径的长度。你可以在任一节点开始和停止，也可以多次重访节点，并且可以重用边。

来源：力扣（LeetCode）
链接：https://leetcode-cn.com/problems/shortest-path-visiting-all-nodes
著作权归领扣网络所有。商业转载请联系官方授权，非商业转载请注明出处。

     * 分析：n只有12，每条边固定长度为1，用广度优先可解
     * */
    class P0847访问所有节点的最短路径
    {
        const int map = (1 << 12) - 1;
        int Next(int key, int next) => key & map | (1 << next) | (next << 12);
        int KeyNode(int key) => key >> 12;
        public int ShortestPathLength(int[][] graph)
        {
            int allNodesMap = (1 << graph.Length) - 1;
            Dictionary<int, int> dic = Enumerable.Range(0, graph.Length).ToDictionary(t => Next(0, t), _ => 0);
            Queue<int> qu = new Queue<int>(dic.Keys);
            while (qu.Any())
            {
                int key = qu.Dequeue(), path = dic[key];
                foreach (var next in graph[KeyNode(key)])
                {
                    int nextKey = Next(key, next);
                    if ((nextKey & allNodesMap) == allNodesMap) return path + 1;
                    else if (!dic.ContainsKey(nextKey))
                    {
                        dic[nextKey] = path + 1;
                        qu.Enqueue(nextKey);
                    }
                }
            }
            return 0; // 还真有一个空集的用例要返回0
        }
    }
}
