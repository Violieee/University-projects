using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using lab1.lab2;

namespace lab1
{
    public partial class Form4 : Form
    {
        public Form4()
        {
            InitializeComponent();

            panel1.BringToFront();
        }

        private void завдання1ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            panel1.BringToFront();
        }

        private void завдання2ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            panel2.BringToFront();
        }
        private void завдання3ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            panel3.BringToFront();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int a = int.Parse(textBox1.Text);
            int b = int.Parse(textBox2.Text);
            int c = int.Parse(textBox3.Text);

            Cube_sum cube = new Cube_sum(a, b, c);

            int result = cube.Calculate();

            label5.Text = result.ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            int a = int.Parse(textBox4.Text);
            int b = int.Parse(textBox5.Text);

            Sum_even sum = new Sum_even(a, b);

            int result = sum.CalculateSum();

            label9.Text = result.ToString();
        }
        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            double R = double.Parse(textBox6.Text);
            double h = double.Parse(textBox7.Text);

            SphereSegment segment = new SphereSegment(R, h);

            double volume = segment.GetVolume();
            double baseRadius = segment.GetBaseRadius();
            double area = segment.GetCurvedSurfaceArea();

            label15.Text = volume.ToString("F2");
            label17.Text = baseRadius.ToString("F2");
            label16.Text = area.ToString("F2");
        }
        private void button6_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {
        }


    }
}