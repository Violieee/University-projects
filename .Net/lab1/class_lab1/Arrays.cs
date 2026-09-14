using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace lab1.class_lab1
{
    internal class Arrays
    {
        public bool error = false;
        private int[] a;
        private int length;

        public int Length
        {
            get { return length; }
        }

        public int this[int i]
        {
            get
            {
                if (i >= 0 && i < length)
                {
                    return a[i];
                }
                else
                {
                    error = true;
                    return 0;
                }
            }

            set
            {
                if (i >= 0 && i < length &&
                    value >= -100 && value <= 100)
                {
                    a[i] = value;
                }
                else
                {
                    error = true;
                }
            }
        }

        public Arrays()
        {
            length = 5;
            a = new int[length];
        }

        public Arrays(int[] mas)
        {
            a = mas;
            length = mas.Length;
        }    

        public Arrays(int size)
        {
            length = size;
            a = new int[length];
        }

        public int FindFirstNegativeHour()
        {
            for (int i = 0; i < length; i++)
            {
                if (a[i] < 0)
                {
                    return 8 + i;
                }
            }

            return -1;
        }

        public bool CheckDecreasing()
        {
            for (int i = 1; i < length; i++)
            {
                if (a[i] >= a[i - 1])
                {
                    return false;
                }
            }

            return true;
        }

        public int FirstNegativeHour
        {
            get
            {
                return FindFirstNegativeHour();
            }
        }

        public string GetElement(int index)
        {
            if (index >= 0 && index < length)
            {
                return a[index].ToString();
            }
            else
            {
                error = true;
                return "За межами масиву";
            }
        }

        public string GetArray()
        {
            string result = "";

            for (int i = 0; i < length; i++)
            {
                result += a[i] + " ";
            }

            return result;
        }
    }
}