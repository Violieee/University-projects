using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab1.class_lab1
{
    internal class Tabul
    {
        public double[,] xy = new double[1000, 2];
        public int n = 0;
        private double f1(double x)
        {
            return Math.Exp(-2 * x) + Math.Pow(2 * Math.Pow(x, 4) + Math.Pow(x, 2) + 1, 1.0 / 7.0);
        }
        private double f2(double x)
        {
            return (Math.Pow(x, 3) + 2 * Math.Pow(x, 2) - 4 * x) / (Math.Pow(x, 5) * (1.0 / Math.Tan(2 * Math.Pow(x, 3))));
        }
        private double f3(double x)
        {
            return Math.Pow(Math.Cos(Math.Pow(x + 2, 3)), 2) / (2 * Math.Sqrt(Math.Pow(x, 3)) * Math.Sin(Math.Pow(x, 3)));
        }

        public void tab(double xn = 2.9, double xk = 17.48, double xh = 0.3, double a = 3) 
        {
            double x = xn, y;
            int i = 0;
            while (x <= xk)
            {
                if(x < 0)
                {
                    y = f1(x);
                }
                else if(0 <= x && x < a)
                {
                    y = f2(x);
                }
                else
                {
                    y = f3(x);
                }
                xy[i, 0] = x;
                xy[i, 1] = y;
                x += xh;
                i++;
            }
            n = i;
        }
    }
}
