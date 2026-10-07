using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab1.lab2
{
    public class Cube_sum
    {
        public int a;
        public int c;

        private int b;

        public Cube_sum()
        {
            a = 0;
            b = 0;
            c = 0;
        }
        public Cube_sum(int a)
        {
            this.a = a;
            b = 0;
            c = 0;
        }
        public Cube_sum(int a, int b, int c)
        {
            this.a = a;
            this.b = b;
            this.c = c;
        }
        public bool HasEvenNumber()
        {
            return a % 2 == 0 || b % 2 == 0 || c % 2 == 0;
        }

        private int Cube(int number)
        {
            return number * number * number;
        }

        public int Calculate()
        {
            if (HasEvenNumber())
            {
                int sum = a + b + c;
                return Cube(sum);
            }
            else
            {
                return Cube(a) + Cube(b) + Cube(c);
            }
        }
    }
}