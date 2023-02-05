using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1.contest
{
    internal class ContestTemplate
    {
        #region Problem A
        public int SolveA(int x)
        {
            return x;
        }
        #endregion

        #region Problem B
        public int SolveB(int x)
        {
            return x;
        }
        #endregion

        #region Problem C
        public int SolveC(int x)
        {
            return x;
        }
        #endregion

        #region Problem D
        public int SolveD(int x)
        {
            return x;
        }
        #endregion

        #region Problem E
        public int SolveE(int x)
        {
            return x;
        }
        #endregion

        #region Run Test
        internal static int Run()
        {
            char p = 'D';
            ContestTemplate sln = new();

            return p switch
            {
                'A' => sln.RunTestA(),
                'B' => sln.RunTestB(),
                'C' => sln.RunTestC(),
                'D' => sln.RunTestD(),
                'E' => sln.RunTestE(),
                _ => -1
            };
        }

        int RunTestA()
        {
            return 0;
        }

        int RunTestB()
        {
            return 0;
        }

        int RunTestC()
        {
            return 0;
        }

        int RunTestD()
        {
            return 0;
        }

        int RunTestE()
        {
            return 0;
        }
        #endregion
    }
}
