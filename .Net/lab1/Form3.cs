using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using lab1.class_lab1;

namespace lab1
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form form = Application.OpenForms[0];
            form.Show();
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox26.Clear();

            TextBox[] textBoxes =
            {
                textBox1, textBox2, textBox3, textBox4, textBox5,
                textBox6, textBox7, textBox8, textBox9, textBox10,
                textBox11, textBox12, textBox13, textBox14, textBox15,
                textBox16, textBox17, textBox18, textBox19, textBox20,
                textBox21, textBox22, textBox23, textBox24, textBox25
            };

            Arrays2D array = new Arrays2D();

            int k = 0;

            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    if (!int.TryParse(textBoxes[k].Text, out int value))
                    {
                        textBox26.Text = "Введіть числа";
                        textBoxes[k].Focus();
                        return;
                    }

                    array[i, j] = value;
                    k++;
                }
            }

            int max = array.FindMax();
            int row = array.FindMaxRow();
            int column = array.FindMaxColumn();

            textBox26.Text =
                $"{max}; [{row + 1},{column + 1}]; {array.MaxPosition()}";
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
