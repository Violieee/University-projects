using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab1.class_lab1
{
    internal class Arrays2D
    {
        public bool error = false;

        private int[,] a;
        private int size;

        public int Size
        {
            get { return size; }
        }

        public int this[int i, int j]
        {
            get
            {
                if (i >= 0 && i < size &&
                    j >= 0 && j < size)
                {
                    return a[i, j];
                }
                else
                {
                    error = true;
                    return 0;
                }
            }

            set
            {
                if (i >= 0 && i < size &&
                    j >= 0 && j < size)
                {
                    a[i, j] = value;
                }
                else
                {
                    error = true;
                }
            }
        }

        public Arrays2D()
        {
            size = 5;
            a = new int[size, size];
        }

        public Arrays2D(int size)
        {
            this.size = size;
            a = new int[size, size];
        }

        public Arrays2D(int[,] mas)
        {
            a = mas;
            size = mas.GetLength(0);
        }

        public int FindMax()
        {
            int max = a[0, 0];

            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    if (a[i, j] > max)
                    {
                        max = a[i, j];
                    }
                }
            }

            return max;
        }

        public int FindMaxRow()
        {
            int max = a[0, 0];
            int maxRow = 0;
            int maxColumn = 0;

            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    if (a[i, j] > max)
                    {
                        max = a[i, j];
                        maxRow = i;
                        maxColumn = j;
                    }
                }
            }

            return maxRow;
        }

        public int FindMaxColumn()
        {
            int max = a[0, 0];
            int maxRow = 0;
            int maxColumn = 0;

            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    if (a[i, j] > max)
                    {
                        max = a[i, j];
                        maxRow = i;
                        maxColumn = j;
                    }
                }
            }

            return maxColumn;
        }

        public string MaxPosition()
        {
            int row = FindMaxRow();
            int column = FindMaxColumn();

            if (row == column)
            {
                return "На діагоналі";
            }
            else if (column > row)
            {
                return "Вище";
            }
            else
            {
                return "Нижче";
            }
        }

        public string GetElement(int i, int j)
        {
            if (i >= 0 && i < size &&
                j >= 0 && j < size)
            {
                return a[i, j].ToString();
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

            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    result += a[i, j] + " ";
                }

                result += Environment.NewLine;
            }

            return result;
        }
    }
}