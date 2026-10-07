using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab1.lab2
{
    public class Sum_even
    {
        public int a;

        private int b;

        public Sum_even()
        {
            a = 0;
            b = 0;
        }
        public Sum_even(int a)
        {
            this.a = a;
            b = 0;
        }
        public Sum_even(int a, int b)
        {
            this.a = a;
            this.b = b;
        }
        private bool IsEven(int number)
        {
            return number % 2 == 0;
        }
        public int CalculateSum()
        {
            int sum = 0;

            for (int i = a; i <= b; i++)
            {
                if (IsEven(i))
                {
                    sum += i;
                }
            }

            return sum;
        }
    }
}