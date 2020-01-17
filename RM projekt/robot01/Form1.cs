using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace robot01
{
    public partial class Form1 : Form
    {
        public enum Method { QUADRATICAL, CONE, COMB };
        Thing thing1; 
        Thing Target; 
        Thing Person1; 
        Thing Person2;
        Thing Person3;
        Thing Person4;
        Robot robot; 

        static int xt = 100;
        static int yt = 305;
        private int[] robotCoords = { 400, 100 };
        private int[] thing1Coords = { 270, 270 };
        int[] TargetCoords = { xt, yt };

        private static Random rand1 = new Random();
        private static Random rand2 = new Random();
        private static Random rand3 = new Random();
        private static Random rand4 = new Random();
        private static Random rand5 = new Random();
        private static Random rand6 = new Random();
        private static Random rand7 = new Random();
        private static Random rand8 = new Random();
        private static int r1x = rand1.Next(150, 250);
        private static int r1y = rand1.Next(230, 260);
        private static int r2x = rand1.Next(101, 120);
        private static int r2y = rand1.Next(230, 380);
        private static int r3x = rand1.Next(200, 300);
        private static int r3y = rand1.Next(200, 280);
        private static int r4x = rand1.Next(200, 300);
        private static int r4y = rand1.Next(100, 380);

        private int[] Person1Coords= { r1x, r1y };
        private int[] Person2Coords= { r2x, r2y };
        private int[] Person3Coords= { r3x, r3y };
        private int[] Person4Coords= { r4x, r4y };

		
        private int robotR = 10;
        private int TargetR = 10;
        private int thing1R = 25;
        private int thing2R = 35;

        private int RobotSpeed = 1;

		private Boolean enable = false; 
        private Boolean hold = false; 
        private double ex;
        private double ey;
        private double ka =0.5;
        private double fx;
        private double fy;
        double N=5000000;
        double pmin=35;
        double p;
        double f1x; 
        double f1y; 
        double f2x;
        double f2y;
        double f3x;
        double f3y;
        double f4x;
        double f4y;
        double f5x;
        double f5y;
        double Fx;
        double Fy;
        double kb= 150;
        double norma;
        double wsk;
        public Method method = Method.QUADRATICAL;


        public Form1()
        {
            InitializeComponent();
            timer1.Stop();
            CreateElements();
            label3.Text = $"Robot pos: x={robot.Center()[0]}, y={robot.Center()[1]}\nTarget pos: tx={thing1.Center()[0]}, ty={thing1.Center()[1]}";
			
			ex = TargetCoords[0] - robotCoords[0];
            ey = TargetCoords[1] - robotCoords[1];
            fx = ka * ex;
            fy = ka * ey;
            if (fx > 1)
                fx = 1;
            else if (fx < 1 && fx > -1)
                fx = 0;
            else
                fx = -1;

            if (fy > 1)
                fy = 1;
            else if (fy < 1 && fy > -1)
                fy = 0;
            else
                fy = -1;
        }
        double Obstaclex(double distx, double disty,double fx_)
        {
            p = Math.Sqrt(Math.Pow((robot.x - distx), 2)+ Math.Pow((robot.y - disty), 2)) ;
                
            if (p > pmin)
            {
                fx_ = 0;
            }else
            {
                fx_ = -((N) / Math.Pow(p, 2)) * ((1 / p) - (1 / pmin)) * ((distx - robot.x) / p);
            }
            return (fx_);
        }

        double Obstacley(double distx, double disty, double fy_)
        {
            p = Math.Sqrt(Math.Pow((robot.x - distx), 2) + Math.Pow((robot.y - disty), 2));

            if (p > pmin)
            {
                fy_ = 0;
            }
            else
            {
                fy_ = -((N) / Math.Pow(p, 2)) * ((1 / p) - (1 / pmin)) * ((disty - robot.y) / p);
            }
            return (fy_);
        }

		
        private void CreateElements()
        {
            thing1 = new Thing(thing1Coords[0], thing1Coords[1], thing2R, thing2R, Color.Blue);
            thing1.SetStartPoint();
            Target = new Thing(TargetCoords[0], TargetCoords[1], TargetR, TargetR, Color.Red);
            robot = new Robot(robotCoords[0], robotCoords[1], robotR, robotR, Color.Black);
            Person1 = new Thing(Person1Coords[0], Person1Coords[1], thing1R, thing1R, Color.Blue);
            Person1.SetStartPoint();
            Person2 = new Thing(Person2Coords[0], Person2Coords[1], thing1R, thing1R, Color.Green);
            Person2.SetStartPoint();
            Person3 = new Thing(Person3Coords[0], Person3Coords[1], thing1R, thing1R, Color.Gray);
            Person3.SetStartPoint();
            Person4 = new Thing(Person4Coords[0], Person4Coords[1], thing1R, thing1R, Color.Red);
            Person4.SetStartPoint();

        }

		
        private void PaintElemnets(object sender, PaintEventArgs e)
        {
            Draw(sender, e, thing1);
            Draw(sender, e, Person1);
            Draw(sender, e, Person2);
            Draw(sender, e, Person3);
            Draw(sender, e, Person4);
            Draw(sender, e, Target);
            Draw(sender, e, robot);
        }
        private void Draw(object sender, PaintEventArgs e, Thing thing)
        {
            e.Graphics.FillEllipse(new SolidBrush(thing.color), thing.x, thing.y, thing.baund_x, thing.baund_y);   
        }

		
        private void timer1_Tick(object sender, EventArgs e)
        {
            Invalidate();
            label3.Text = $"Robot pos: x={robot.Center()[0]}, y={robot.Center()[1]}\nTarget pos: tx={thing1.Center()[0]}, ty={thing1.Center()[1]}";
			label7.Text = $"{method}";
            robot.CheckCollision(thing1);
            robot.CheckCollision(Person1);
            robot.CheckCollision(Person2);
            robot.CheckCollision(Person3);
            robot.CheckCollision(Person4);
			robot.CheckTarget(Target, label4);
            
            Person1.MargedMove(40, 40);
            Person2.BasicMoveHorizontal(50);
            Person3.BasicMoveVertical(50);
            Person4.ComplicatedMove(60, 70);
            if (robot.Collision) timer1.Stop();
            if (robot.GetDistance(Target) <= (robot.baund_x / 2 + Target.baund_x / 2)/ 2)
            {
                timer1.Stop(); 
            }
			
            //QUADRATICAL
            //start paraboli
            if (method == Method.QUADRATICAL)
            {
                ex = (TargetCoords[0] - robot.x);
                ey = (TargetCoords[1] - robot.y);
                fx = ka * ex;
                fy = ka * ey;
                f1x = Obstaclex(thing1.x, thing1.y, f1x);
                f1y = Obstacley(thing1.x, thing1.y, f1y);
                f2x = Obstaclex(Person1.x, Person1.y, f2x);
                f2y = Obstacley(Person1.x, Person1.y, f2y);
                f3x = Obstaclex(Person2.x, Person2.y, f2x);
                f3y = Obstacley(Person2.x, Person2.y, f2y);
                f4x = Obstaclex(Person3.x, Person3.y, f2x);
                f4y = Obstacley(Person3.x, Person3.y, f2y);
                f5x = Obstaclex(Person4.x, Person4.y, f2x);
                f5y = Obstacley(Person4.x, Person4.y, f2y);
                Fx = f1x + f2x + f3x + f4x + f5x + fx;
                Fy = f1y + f2y + f3y + f4y + f5y + fy;
                label5.Text = "Wartości gradientu: X: " + Fx.ToString();
                label6.Text = "Wartości gradientu: Y: " + Fy.ToString();
                if (Fx > 1)
                    Fx = 1;
                else if (Fx < 1 && Fx > -1)
                    Fx = 0;
                else
                    Fx = -1;

                if (Fy > 1)
                    Fy = 1;
                else if (Fy < 1 && Fy > -1)
                    Fy = 0;
                else
                    Fy = -1;
                robot.Move(Convert.ToInt32(Fx), Convert.ToInt32(Fy), RobotSpeed);
				WriteDirection();
			}
			//koniec paraboli

			//CONE
			//start stożkowego
			if (method == Method.CONE)
            {
                norma = Math.Sqrt(Math.Pow((TargetCoords[0] - robot.x), 2) + Math.Pow((TargetCoords[1] - robot.y), 2));
                ex = (TargetCoords[0] - robot.x);
                ey = (TargetCoords[1] - robot.y);
                fx = (kb * ex) / norma;
                fy = (kb * ey) / norma;

                f1x = Obstaclex(thing1.x, thing1.y, f1x);
                f1y = Obstacley(thing1.x, thing1.y, f1y);
                f2x = Obstaclex(Person1.x, Person1.y, f2x);
                f2y = Obstacley(Person1.x, Person1.y, f2y);
                f3x = Obstaclex(Person2.x, Person2.y, f2x);
                f3y = Obstacley(Person2.x, Person2.y, f2y);
                f4x = Obstaclex(Person3.x, Person3.y, f2x);
                f4y = Obstacley(Person3.x, Person3.y, f2y);
                f5x = Obstaclex(Person4.x, Person4.y, f2x);
                f5y = Obstacley(Person4.x, Person4.y, f2y);
                Fx = f1x + f2x + f3x + f4x + f5x + fx;
                Fy = f1y + f2y + f3y + f4y + f5y + fy;
                label5.Text = "Wartości gradientu: X: " + Fx.ToString();
                label6.Text = "Wartości gradientu: Y: " + Fy.ToString();
                if (Fx > 1)
                    Fx = 1;
                else if (Fx < 1 && Fx > -1)
                    Fx = 0;
                else
                    Fx = -1;

                if (Fy > 1)
                    Fy = 1;
                else if (Fy < 1 && Fy > -1)
                    Fy = 0;
                else
                    Fy = -1;
                robot.Move(Convert.ToInt32(Fx), Convert.ToInt32(Fy), RobotSpeed);
				WriteDirection();
			}
			//koniec stożkowego
			if (method == Method.COMB)
            {
                wsk = kb / ka;
                norma = Math.Sqrt(Math.Pow((TargetCoords[0] - robot.x), 2) + Math.Pow((TargetCoords[1] - robot.y), 2));
                if (norma <= wsk)
                {
                    ex = (TargetCoords[0] - robot.x);
                    ey = (TargetCoords[1] - robot.y);
                    fx = ka * ex;
                    fy = ka * ey;
                }
                else
                {
                    ex = (TargetCoords[0] - robot.x);
                    ey = (TargetCoords[1] - robot.y);
                    fx = (kb * ex) / norma;
                    fy = (kb * ey) / norma;
                }

                f1x = Obstaclex(thing1.x, thing1.y, f1x);
                f1y = Obstacley(thing1.x, thing1.y, f1y);
                f2x = Obstaclex(Person1.x, Person1.y, f2x);
                f2y = Obstacley(Person1.x, Person1.y, f2y);
                f3x = Obstaclex(Person2.x, Person2.y, f3x);
                f3y = Obstacley(Person2.x, Person2.y, f3y);
                f4x = Obstaclex(Person3.x, Person3.y, f4x);
                f4y = Obstacley(Person3.x, Person3.y, f4y);
                f5x = Obstaclex(Person4.x, Person4.y, f2x);
                f5y = Obstacley(Person4.x, Person4.y, f2y);
                Fx = f1x + f2x + f3x + f4x + f5x + fx;
                Fy = f1y + f2y + f3y + f4y + f5y + fy;
                label5.Text = "Wartości gradientu: X: " + Fx.ToString();
                label6.Text = "Wartości gradientu: Y: " + Fy.ToString();
                if (Fx > 1)
                    Fx = 1;
                else if (Fx < 1 && Fx > -1)
                    Fx = 0;
                else
                    Fx = -1;

                if (Fy > 1)
                    Fy = 1;
                else if (Fy < 1 && Fy > -1)
                    Fy = 0;
                else
                    Fy = -1;
                robot.Move(Convert.ToInt32(Fx), Convert.ToInt32(Fy), RobotSpeed);
				WriteDirection();
			}
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            PaintElemnets(sender, e);            
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e) 
        {
			//____Poruszanie strzałkami do debugowania________
            if (e.KeyCode == Keys.Up && enable)
            {
                robot.Move(0, -1, RobotSpeed);
                label1.Text = "Up";
            }
            if (e.KeyCode == Keys.Down && enable)
            {
                robot.Move(0, 1, RobotSpeed);
                label1.Text = "down";
            }
            if (e.KeyCode == Keys.Left && enable)
            {
                robot.Move(-1, 0, RobotSpeed);
                label1.Text = "Left";
            }
            if (e.KeyCode == Keys.Right && enable)
            {
                robot.Move(1, 0, RobotSpeed);
                label1.Text = "Right";
            }
			//__________________________________
            if (e.KeyCode == Keys.P) 
            {
                enable = false;
                timer1.Stop();
            }
            if (e.KeyCode == Keys.Enter) 
            {
                enable = true;
                timer1.Start();
            }
            if (e.KeyCode == Keys.Tab)
            {
                if(!hold)
                {
                    hold = !hold;
                    RobotSpeed *= 10;
                }
                else if(hold)
                {
                    hold = !hold;
                    RobotSpeed/=10;
                }
            }
            if (e.KeyCode == Keys.Escape) 
            {
                timer1.Stop();
                enable = false;
                label1.Text = "";
                robot.x = robotCoords[0];
                robot.y = robotCoords[1];
                thing1.x = thing1Coords[0];
                thing1.y = thing1Coords[1];
                Person1.x = Person1Coords[0];
                Person1.y = Person1Coords[1];
                Person2.x = Person2Coords[0];
                Person2.y = Person2Coords[1];
                Person3.x = Person3Coords[0];
                Person3.y = Person3Coords[1];
                Person4.x = Person4Coords[0];
                Person4.y = Person4Coords[1];
                label4.Font = new Font(label4.Font, FontStyle.Regular);
                robot.Collision = false;
            }
            if (e.KeyCode == Keys.Space) 
            {
                if (method == Method.QUADRATICAL)
                    method = Method.CONE;
                else if (method == Method.CONE)
                    method = Method.COMB;
                else if (method == Method.COMB)
                    method = Method.QUADRATICAL;

                label7.Text = $"{method}";
            }
            if (e.KeyCode == Keys.R)
            {
                if (!enable)
                {
                    r1x = rand1.Next(150, 250);
                    r1y = rand1.Next(140, 260);
                    r2x = rand1.Next(200, 380);
                    r2y = rand1.Next(230, 380);
                    r3x = rand1.Next(300, 400);
                    r3y = rand1.Next(300, 380);
                    r4x = rand1.Next(200, 300);
                    r4y = rand1.Next(100, 380);
                    Person1.x = r1x;
                    Person1.y = r1y;
                    Person2.x = r2x;
                    Person2.y = r2y;
                    Person3.x = r3x;
                    Person3.y = r3y;
                    Person4.x = r4x;
                    Person4.y = r4y;
                    Person1.SetStartPoint();
                    Person2.SetStartPoint();
                    Person3.SetStartPoint();
                    Person4.SetStartPoint();

                }
            }
        }

		private void WriteDirection()
		{
			if (Fx > 0)
			{
				label1.Text = "Right";
			}
			else
			{
				label1.Text = "Left";
			}
			if (Fy > 0)
			{
				label9.Text = "Down";
			}
			else
			{
				label9.Text = "Up";
			}
		}

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void Form1_Activated(object sender, EventArgs e)
        {

        }

		
    }
}