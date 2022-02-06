using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1
{
    class P0881救生艇
    {
        public int NumRescueBoats(int[] people, int limit)
        {
            Array.Sort(people);
            int count = 0;
            int i = 0, j = people.Length - 1;
            while (i <= j)
            {
                if (j > i && people[i] + people[j] <= limit)
                {
                    count++;
                    j--;
                    i++;
                }
                else
                {
                    count++;
                    j--;
                }
            }
            return count;
        }
    }
}
