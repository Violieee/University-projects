using lab8_IC_MVP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab8_IC_MVP
{
    internal class Purchase : IPurchaseModel
    {
        private double price;
        private double discount;
        private double taxes;

        public Purchase(double price, double discount, double taxes)
        {
            this.price = price;
            this.discount = discount;
            this.taxes = taxes;
        }

        public double CalculateTotal()
        {
            return price - discount + taxes;
        }
    }
}
