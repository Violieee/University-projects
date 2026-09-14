using System;
using System.Windows.Forms;
using lab1.class_lab1;

namespace lab1
{
    public partial class Form2 : Form
    {
        public Form2()
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
            TextBox[] textBoxes =
            {
                textBox1,
                textBox2,
                textBox3,
                textBox4,
                textBox5,
                textBox6,
                textBox7,
                textBox8,
                textBox9,
                textBox10,
                textBox11,
                textBox12,
                textBox13
            };

            Arrays temperatures = new Arrays(13);

            for (int i = 0; i < textBoxes.Length; i++)
            {
                if (!int.TryParse(textBoxes[i].Text, out int value))
                {
                    textBox14.Text = "Введіть числа.";
                    return;
                }

                if (value < -100 || value > 100)
                {
                    textBox14.Text = "Від -100 до 100.";
                    return;
                }

                temperatures[i] = value;
            }

            if (!temperatures.CheckDecreasing())
            {
                textBox14.Text = "Не знижується.";
                return;
            }

            int hour = temperatures.FindFirstNegativeHour();

            if (hour != -1)
            {
                textBox14.Text = $"{hour}:00";
            }
            else
            {
                textBox14.Text =
                    "Не від'ємна.";
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }
    }
}