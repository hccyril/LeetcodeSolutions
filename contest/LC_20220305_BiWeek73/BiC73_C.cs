using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProblemTests
{
    [TestClass]
    public class ContestBiweekC
    {
        class NodeStruct
        {
            public int Id;
            public HashSet<int> inList = new();
            public HashSet<int> outList = new();
            public HashSet<int> ans = new();
            public NodeStruct(int i) => Id = i;
        }
        public IList<IList<int>> GetAncestors(int n, int[][] edges)
        {
            NodeStruct[] arr = new NodeStruct[n];
            for (int i = 0; i < n; ++i)
                arr[i] = new NodeStruct(i);
            foreach (var ed in edges)
            {
                int a = ed[0], b = ed[1];
                arr[a].outList.Add(b);
                arr[b].inList.Add(a);
            }
            Queue<int> qu = new();
            foreach (var nd in arr.Where(a => !a.inList.Any()))
            {
                qu.Enqueue(nd.Id);
            }
            while (qu.Any())
            {
                NodeStruct nd = arr[qu.Dequeue()];
                foreach (var next in nd.outList)
                {
                    var nn = arr[next];
                    foreach (var ances in nd.ans)
                        nn.ans.Add(ances);
                    nn.ans.Add(nd.Id);
                    nn.inList.Remove(nd.Id);
                    if (!nn.inList.Any()) qu.Enqueue(nn.Id);
                }
            }
            return arr.Select(nd => nd.ans.OrderBy(n => n).ToList() as IList<int>).ToList();
        }
        [TestMethod]
        public void Run()
        {
            int i = 1;
            Assert.AreEqual(0, i);
        }
    }
}
