using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace robot01
{
    class Thing
    {
        public int x, y, baund_x, baund_y; 
        public int StartX, StartY; 
        public bool direction = true;
        public bool hor = false;
        public bool ver = true;
        public Color color;

		
        public Thing(int x, int y, int baund_x, int baund_y, Color color)
        {
            this.x = x;
            this.y = y;
            this.baund_x = baund_x;
            this.baund_y = baund_y;
            this.color = color;
        }

        public Thing(int x, int y, int baund_x, int baund_y)
        {
            this.x = x;
            this.y = y;
            this.baund_x = baund_x;
            this.baund_y = baund_y;
        }

		
        public void Move(int directory_x, int directory_y, int speed)
        {
            x += directory_x*speed;
            y += directory_y*speed;
        }

		
        public int[] Center()
        {
            int[] center = { 0, 0 }; //x,y
            center[0] = x + baund_x/2;
            center[1] = y + baund_y/2;
            return center;
        }
		
        public void SetStartPoint()
        {
            StartX = x;
            StartY = y;
        }

		
        public void RandomMove()
		{
            Random rand = new Random(); 
            int vectorX = rand.Next(-5, 6);
            int vectorY = rand.Next(-5, 6);
            int[] center = Center();

            Move(1, 0, vectorX);
            Move(0, 1, vectorY);
            if (center[0] < 0) x = 0;
            if (center[1] < 0) y = 0;
            if (center[0] > 500) x = 500;
            if (center[1] > 500) y = 500;
        }
		
        public void RandomMove2(bool directionX) 
        {
            Random rand = new Random();
            int magnitude = rand.Next(0, 1000);
            double angle = (magnitude * 2 * 3.14) * 180 / 3.14;
            double sinAngle = 10 * Math.Sin(angle);
            int SinAngle = Convert.ToInt32(sinAngle);
            if (directionX)
                Move(1, 0, SinAngle);
            else
                Move(0, 1, SinAngle);
        }

		
        public void BasicMoveHorizontal(int distance)
        {
            if(direction)
            {
                Move(0, 1, 1);
            }
            else if (!direction)
            {
                Move(0, -1, 1);
            }
            if (y >= StartY + distance || y <= StartY - distance)
                direction = !direction;
        }
		
        public void BasicMoveVertical(int distance)
        {
            if (direction)
            {
                Move(1, 0, 1);
            }
            else if (!direction)
            {
                Move(-1, 0, 1);
            }
            if (x >= StartX + distance || x <= StartX - distance)
                direction = !direction;
        }
		
        public void MargedMove(int distancex, int distancey)
        {
            BasicMoveHorizontal(distancey);
            BasicMoveVertical(distancey);
        }

		
		public void ComplicatedMove(int distancex, int distancey)
        {
            if(ver && direction)
            {
                Move(1, 0, 1);
            }
            if(ver && !direction)
            {
                Move(-1, 0, 1);
            }
            if(hor && direction)
            {
                Move(0, 1, 1);
            }
            if(hor && !direction)
            {
                Move(0, -1, 1);
            }
            if (x >= StartX + distancex)
            {
                direction = true;
                ver = false;
                hor = true;
            }
            if(y >= StartY + distancey)
            {
                direction = false;
                ver = true;
                hor = false;
            }
            if(x <= StartX - distancex)
            {
                direction = false;
                ver = false;
                hor = true;
            }
            if (y <= StartY - distancey && x <= StartX)
            {
                direction = true;
                ver = true;
                hor = false;
            }
        }
    }

}
