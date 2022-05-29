using System;
using System.Drawing;
using System.Windows.Forms;
using static System.Math;
using System.Drawing.Drawing2D;

namespace UFO
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            Paint += new PaintEventHandler(draw_gr);
        }
        static int factorial(int x)
        {
            int k = 0;
            if (x <= 0)
                return 1;
            k = x * factorial(x - 1);
            return k;
        }

        double sin(double x, int y)
        {
            double result = 0;
            for (int i = 1; i < y + 1; i++)
            {
                result += Math.Pow(-1, i - 1) * Math.Pow(x, 2 * i - 1) / factorial(2 * i - 1);
            }
            return result;
        }

        double cos(double x, int y)
        {
            double result = 0;
            for (int i = 1; i < y + 1; i++)
            {
                result += Math.Pow(-1, i - 1) * Math.Pow(x, 2 * i - 2) / factorial(2 * i - 2);
            }
            return result;
        }
        double arctan(double x, int y)
        {
            double result = 0;
            if (-1 <= x && x <= 1)
            {
                for (int i = 1; i < y + 1; i++)
                {
                    result += Math.Pow(-1, i - 1) * Math.Pow(x, 2 * i - 1) / (2 * i - 1);
                }
            }
            else
            {
                if (x >= 1)
                {
                    result += PI / 2;
                    for (int i = 0; i < y; i++)
                    {
                        result -= Pow(-1, i) / ((2 * i + 1) * Pow(x, 2 * i + 1));
                    }
                }
                else
                {
                    result -= PI / 2;
                    for (int i = 0; i < y; i++)
                    {
                        result -= Pow(-1, i) / ((2 * i + 1) * Pow(x, 2 * i + 1));
                    }
                }
            }
            return result;
        }
        void draw_gr(object sender, PaintEventArgs e)
        {
            double x1 = 30;
            double y1 = 40;
            double x2 = 400;
            double y2 = 100;
            double step = 1;

            double m = 200;
            double n = 10;

            int y = 2;

            Graphics g = e.Graphics;
            Pen pen = new Pen(Color.Green, 2);

            GraphicsState gs;

            gs = g.Save();

            double error = Abs(x1 - x2) + Abs(y1 - y2);
            double angle = arctan((y2 - y1) / (x1 - x2), y);
            while (true)
            {
                y1 -= step * sin(angle, y);
                x1 += step * cos(angle, y);

                if (Abs(x1 - x2) + Abs(y1 - y2) > error)
                {
                    g.DrawString("Accuracy: " + error.ToString(), new Font("Times New Roman", 12), new SolidBrush(Color.Black), 300, 10);
                    break;
                }
                else
                {
                    error = Abs(x1 - x2) + Abs(y1 - y2);
                }
            }
            g.Restore(gs);

            g.DrawLine(new Pen(Color.Black, 2), 100, 400, 100, 0);
            g.DrawLine(new Pen(Color.Black, 2), 100, 400, 600, 400);

            for (int i = 0; i < 11; i++)
            {
                g.DrawLine(new Pen(Color.Black, 2), 90, 400 - (i + 1) * 50, 110, 400 - (i + 1) * 50);
                g.DrawLine(new Pen(Color.Black, 2), 50 + (i + 1) * 50, 390, 50 + (i + 1) * 50, 410);
            }

            g.DrawEllipse(new Pen(Color.Black, 2), 200, 10, 2, 2);

            for (int j = 1; j < 6; j++)
            {
                y = j;

                x1 = 30;
                y1 = 40;
                error = Abs(x1 - x2) + Abs(y1 - y2);
                angle = arctan((y2 - y1) / (x1 - x2), y);
                while (true)
                {
                    y1 -= step * sin(angle, y);
                    x1 += step * cos(angle, y);
                    if (Abs(x1 - x2) + Abs(y1 - y2) > error)
                    {
                        if (j != 1)
                        {
                            g.DrawLine(pen, (float)m, (float)n, 100 + 100 * j, 400 - (float)error * 100);
                            m = 100 + 100 * j;
                            n = 400 - error * 100;
                            g.DrawEllipse(new Pen(Color.Black, 3), 100 + 100 * j, 400 - (float)error * 100, 2, 2);
                        }
                        break;
                    }
                    else
                    {
                        error = Abs(x1 - x2) + Abs(y1 - y2);
                    }
                }
            }
            gs = g.Save();
            g.Restore(gs);
        }
    }
}