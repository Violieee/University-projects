using Lab8_IC_MVP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab8_IC_MVP
{
    internal class PurchasePresenter
    {
        private readonly IPurchaseView view;

        public PurchasePresenter(IPurchaseView view)
        {
            this.view = view;
        }

        public void CalculatePurchase(
            double price,
            double discount,
            double taxes)
        {
            IPurchaseModel model =
                new Purchase(price, discount, taxes);

            double total = model.CalculateTotal();

            view.DisplayResult(total);
        }
    }
}
