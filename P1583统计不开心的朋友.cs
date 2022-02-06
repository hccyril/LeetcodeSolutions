using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1
{
    // medium, 没什么技巧就是模拟 // TODO 2021/8/14 Daily (七夕节每日一题）
    class P1583统计不开心的朋友
    {
        public int UnhappyFriends(int n, int[][] preferences, int[][] pairs)
        {
            throw new NotImplementedException();
        }

        // 未做，以下为官方题解
        // 但是算法很简单，就是直接模拟而已
        public int UnhappyFriends_Official(int n, int[][] preferences, int[][] pairs)
        {
            int[,] order = new int[n, n];
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n - 1; j++)
                {
                    order[i, preferences[i][j]] = j;
                }
            }
            int[] match = new int[n];
            foreach (int[] pair in pairs)
            {
                int person0 = pair[0], person1 = pair[1];
                match[person0] = person1;
                match[person1] = person0;
            }
            int unhappyCount = 0;
            for (int x = 0; x < n; x++)
            {
                int y = match[x];
                int index = order[x, y];
                for (int i = 0; i < index; i++)
                {
                    int u = preferences[x][i];
                    int v = match[u];
                    if (order[u, x] < order[u, v])
                    {
                        unhappyCount++;
                        break;
                    }
                }
            }
            return unhappyCount;
        }
    }
}
