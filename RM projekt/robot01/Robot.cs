using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace robot01
{
    class Robot : Thing 
    {
		
        public Robot(int x, int y, int baund_x, int baund_y, Color color) : base(x, y, baund_x, baund_y, color)
        {
        }
		
        public Boolean Collision = false;

		
        public void CheckCollision(Thing thing)
        {
            int distance = GetDistance(thing);

            if (distance <= (baund_x/2 + thing.baund_x/2))
            {
                Collision = true;
            }
        }
		
        public bool GetCollison()
        {
            return Collision;
        }

		
        public void CheckTarget(Thing thing, Label label)
        {
            int distance = GetDistance(thing);
            if (distance <= (baund_x/2 + thing.baund_x/2)/2)
            {
                label.Text = "Subject: END";
                label.Font = new Font(label.Font, FontStyle.Bold);
            }
            else
            {
                label.Text = "Subject: Trying to go to target";
            }
        }
		
        public int GetDistance(Thing thing)
        {
            int[] center = Center();
            int[] center2 = thing.Center();
                int distance = (center[0] - center2[0])* (center[0] - center2[0]) + (center[1] - center2[1]) * (center[1] - center2[1]);
            double d = Math.Sqrt(Convert.ToDouble(distance));
            distance = Convert.ToInt32(d);

            return distance;
        }

    }
}
