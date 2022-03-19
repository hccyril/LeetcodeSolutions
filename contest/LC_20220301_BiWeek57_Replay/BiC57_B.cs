using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProblemTests
{
    [TestClass]
    public class BiC57_B
    {
        public int SmallestChair(int[][] times, int targetFriend)
        {
            ++targetFriend;
            List<(int t, int id)> list = new();
            int i = 0;
            foreach (var tp in times)
            {
                ++i;
                list.Add((tp[0], i));
                list.Add((tp[1], -i));
            }
            int[] seats = new int[times.Length + 1];
            SortedSet<int> sort = new();
            foreach (var seat in Enumerable.Range(0, times.Length))
                sort.Add(seat);
            foreach ((int t, int id) in list.OrderBy(t => t.t).ThenBy(t => t.id))
            {
                if (id < 0) sort.Add(seats[-id]);
                else
                {
                    if (id == targetFriend) return sort.Min;
                    seats[id] = sort.Min;
                    sort.Remove(sort.Min);
                }
            }
            return -1;
        }

        [TestMethod]
        public void Run()
        {
            int i = 1;
            Assert.AreEqual(0, i);
        }
    }
}
