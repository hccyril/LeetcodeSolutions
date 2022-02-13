using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1
{
    /*
     * 684. Redundant Connection
Medium
https://leetcode.com/problems/redundant-connection/
In this problem, a tree is an undirected graph that is connected and has no cycles.

You are given a graph that started as a tree with n nodes labeled from 1 to n, with one additional edge added. The added edge has two different vertices chosen from 1 to n, and was not an edge that already existed. The graph is represented as an array edges of length n where edges[i] = [ai, bi] indicates that there is an edge between nodes ai and bi in the graph.

Return an edge that can be removed so that the resulting graph is a tree of n nodes. If there are multiple answers, return the answer that occurs last in the input.
     * */
    // medium, 最小生成树MST
    // also 《剑指 Offer II 118. 多余的边》
    // ver1: 2021/6/26
    // ver2: 2022/2/10 并查集
    class P0684冗余连接
    {
        // ver2: 并查集
        public int[] FindRedundantConnection(int[][] edges)
        {
            UnionFind uni = new(edges.Length);
            foreach (var ed in edges)
            {
                int a = ed[0] - 1, b = ed[1] - 1;
                if (uni.Check(a, b)) return ed;
                uni.Union(a, b);
            }
            return edges.Last();
        }

        public int[] FindRedundantConnection_ver1(int[][] edges)
        {
            // 标记节点是否被连接
            bool[] nc = new bool[edges.Length + 1];
            // 标记边是否包括在集合（即不属于冗余边）
            bool[] ec = new bool[edges.Length];
            nc[edges[0][0]] = true;
            for (int count = 1; count < edges.Length; ++count)
                for (int i = 0; i < edges.Length; ++i)
                    if (!ec[i])
                    {
                        int ai = edges[i][0], bi = edges[i][1];
                        if (nc[ai] ^ nc[bi])
                        {
                            ec[i] = nc[ai] = nc[bi] = true;
                            break;
                        }
                    }

            for (int i = 0; i < edges.Length; ++i)
                if (!ec[i])
                    return edges[i];

            return null;
        }
    }
}
