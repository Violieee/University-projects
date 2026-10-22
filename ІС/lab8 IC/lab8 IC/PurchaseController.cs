using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab8_IC
{
    internal class PurchaseController
    {
        public double Calculate(double price, double discount, double taxes)
        {
            Purchase purchase = new Purchase(price, discount, taxes);

            return purchase.CalculateTotal();
        }
    }
}

