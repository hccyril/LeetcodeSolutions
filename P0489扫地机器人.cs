using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleCore1
{
    /**
    房间（用格栅表示）中有一个扫地机器人。格栅中的每一个格子有空和障碍物两种可能。

    扫地机器人提供4个API，可以向前进，向左转或者向右转。每次转弯90度。

    当扫地机器人试图进入障碍物格子时，它的碰撞传感器会探测出障碍物，使它停留在原地。

    请利用提供的4个API编写让机器人清理整个房间的算法。

    interface Robot {
      // 若下一个方格为空，则返回true，并移动至该方格
      // 若下一个方格为障碍物，则返回false，并停留在原地
      boolean move();

      // 在调用turnLeft/turnRight后机器人会停留在原位置
      // 每次转弯90度
      void turnLeft();
      void turnRight();

      // 清理所在方格
      void clean();
    }
    示例:

    输入:
    room = [
      [1,1,1,1,1,0,1,1],
      [1,1,1,1,1,0,1,1],
      [1,0,1,1,1,1,1,1],
      [0,0,0,1,0,0,0,0],
      [1,1,1,1,1,1,1,1]
    ],
    row = 1,
    col = 3

    解析:
    房间格栅用0或1填充。0表示障碍物，1表示可以通过。
    机器人从row=1，col=3的初始位置出发。在左上角的一行以下，三列以右。
    注意:

    输入只用于初始化房间和机器人的位置。你需要“盲解”这个问题。换而言之，你必须在对房间和机器人位置一无所知的情况下，只使用4个给出的API解决问题。 
    扫地机器人的初始位置一定是空地。
    扫地机器人的初始方向向上。
    所有可抵达的格子都是相连的，亦即所有标记为1的格子机器人都可以抵达。
    可以假定格栅的四周都被墙包围。
     * */

    // hard, plus, 2022/2/27
    // 回溯
    internal class P0489扫地机器人
    {
        // This is the robot's control interface.
        // You should not implement it, or speculate about its implementation
        public interface Robot
        {
            // Returns true if the cell in front is open and robot moves into the cell.
            // Returns false if the cell in front is blocked and robot stays in the current cell.
            public bool Move();

            // Robot will stay in the same cell after calling turnLeft/turnRight.
            // Each turn will be 90 degrees.
            public void TurnLeft();
            public void TurnRight();

            // Clean the current cell.
            public void Clean();
        }

        // directions:
        //     0
        //  3     1
        //     2
        int x, y, d;
        HashSet<(int, int)> map = new();
        Robot robot;

        static IEnumerable<(int ni, int nj)> FourDir(int i, int j)
        {
            yield return (i - 1, j);
            yield return (i + 1, j);
            yield return (i, j - 1);
            yield return (i, j + 1);
        }

        int Direction(int nx, int ny)
        {
            if (ny < y) return 3;
            else if (nx < x) return 2;
            else if (ny > y) return 1;
            else return 0;
        }

        bool MoveTo(int nx, int ny)
        {
            int di = Direction(nx, ny);
            while (d > di)
            {
                robot.TurnLeft();
                --d;
            }
            while (d < di)
            {
                robot.TurnRight();
                ++d;
            }
            bool canMove = robot.Move();
            if (canMove)
            {
                x = nx;
                y = ny;
            }
            return canMove;
        }

        void Dfs(int from_x, int from_y)
        {
            robot.Clean();
            int x0 = x, y0 = y;
            foreach ((int nx, int ny) in FourDir(x0, y0))
                if (map.Add((nx, ny)) && MoveTo(nx, ny))
                    Dfs(x0, y0);
            if (x0 != from_x || y0 != from_y)
                MoveTo(from_x, from_y);
        }

        public void CleanRoom(Robot robot)
        {
            this.robot = robot;
            x = 0; y = 0; d = 0;
            map.Add((0, 0));
            Dfs(0, 0);
        }
    }
}
