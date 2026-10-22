namespace lab8_IC
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


        private void button1_Click(object sender, EventArgs e)
        {
            if (double.TryParse(textBox1.Text, out double price) &&
                double.TryParse(textBox2.Text, out double discount) &&
                double.TryParse(textBox3.Text, out double taxes) &&
                price >= 0 && discount >= 0 &&
                discount <= price && taxes >= 0)
            {
                PurchaseController controller = new PurchaseController();

                double total = controller.Calculate(price, discount, taxes);

                label4.Text = "Результат: " + total.ToString("F2") + " грн";
            }
            else
            {
                MessageBox.Show("Перевірте правильність введених даних!");
            }
        }

    }
}
