using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1
{
    // hard, 2022/4/9 Daily
    // 这题有毒，做了几遍都错
    internal class P0780到达终点
    {
        // ver final: AC
        public bool ReachingPoints(int sx, int sy, int tx, int ty)
        {
            if (tx == sx && ty == sy) return true;
            if (tx < sx || ty < sy) return false;
            if (tx == ty) return false;

            if (tx < ty)
            {
                int target = Math.Max(ty % tx, sy);
                if (ty == target || (ty - target) % tx != 0) return false;
                return ReachingPoints(sx, sy, tx, target);
            }
            else
            {
                int target = Math.Max(tx % ty, sx);
                if (tx == target || (tx - target) % ty != 0) return false;
                return ReachingPoints(sx, sy, target, ty);
            }
        }

        // ver x: WA
        //public bool ReachingPoints(int sx, int sy, int tx, int ty)
        //{
        //    if (tx < sx || ty < sy) return false;
        //    if (tx == sx && ty == sy) return true;
        //    if (tx == ty) return false;

        //    if (tx < ty)
        //    {
        //        int target = Math.Max(ty % tx, sy);
        //        if ((ty - target) % tx != 0) return false;
        //        return ReachingPoints(sx, sy, tx, target);
        //    }
        //    else
        //    {
        //        int target = Math.Max(tx % ty, sx);
        //        if ((tx - target) % ty != 0) return false;
        //        return ReachingPoints(sx, sy, target, ty);
        //    }
        //}

        internal static void Run()
        {
            var sln = new P0780到达终点();
            Console.WriteLine(sln.ReachingPoints(1, 5, 19, 5));
        }
    }
}
