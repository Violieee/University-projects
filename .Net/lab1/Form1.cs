using lab1.class_lab1;

namespace lab1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void chart1_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Tabul tabul = new Tabul();

            double xn, xk, xh, a;

            xn = Convert.ToDouble(textBox1.Text);
            xk = Convert.ToDouble(textBox2.Text);
            xh = Convert.ToDouble(textBox3.Text);
            a = Convert.ToDouble(textBox4.Text);

            chart1.Series[0].Points.Clear();
            dataGridView1.Rows.Clear();

            tabul.tab(xn, xk, xh, a);

            for (int i = 0; i < tabul.n; i++)
            {
                dataGridView1.Rows.Add(
                    Math.Round(tabul.xy[i, 0], 2).ToString(),
                    Math.Round(tabul.xy[i, 1], 3).ToString()
                );

                chart1.Series[0].Points.AddXY(
                    tabul.xy[i, 0],
                    tabul.xy[i, 1]
                );
            }
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Form2 f = new Form2();
            f.Show();
            this.Hide();
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            Form3 f = new Form3();
            f.Show();
            this.Hide();
        }
    }
}
