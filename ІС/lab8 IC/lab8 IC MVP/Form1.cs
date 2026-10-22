using System;
using System.Reflection.Emit;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace lab8_IC_MVP
{
    public partial class Form1 : Form, IPurchaseView
    {
        private readonly PurchasePresenter presenter;

        public Form1()
        {
            InitializeComponent();
            presenter = new PurchasePresenter(this);
        }

        public void DisplayResult(double total)
        {
            label4.Text = "Результат: " +
                          total.ToString("F2") + " грн";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (double.TryParse(textBox1.Text, out double price) &&
                double.TryParse(textBox2.Text, out double discount) &&
                double.TryParse(textBox3.Text, out double taxes) &&
                price >= 0 && discount >= 0 &&
                discount <= price && taxes >= 0)
            {
                presenter.CalculatePurchase(
                    price, discount, taxes);
            }
            else
            {
                MessageBox.Show(
                    "Перевірте правильність введених даних!");
            }
        }
    }
}
