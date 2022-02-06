using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1
{
    // hard, 应用题（也许是贪心）// TODO 2021/09/29 Daily
    class P0517超级洗衣机
    {
        public int FindMinMoves(int[] machines)
        {
            throw new NotImplementedException();
        }

        public int FindMinMoves_Official(int[] machines)
        {
            var sum = machines.Sum();
            if (sum % machines.Length != 0)
                return -1;

            int fill = sum / machines.Length;
            int diff = 0;

            int result = 0;
            foreach (var machine in machines)
            {
                diff += machine - fill;
                result = Math.Max(result, Math.Abs(diff));
                result = Math.Max(result, machine - fill);
            }

            return result;
        }
    }
}
