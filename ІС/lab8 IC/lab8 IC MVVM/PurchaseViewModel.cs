using lab8_IC_MVVM;
using System;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Lab8_IC_MVVM
{
    internal class PurchaseViewModel : INotifyPropertyChanged
    {
        private double price;
        private double discount;
        private double taxes;

        public double Price
        {
            get { return price; }
            set
            {
                price = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(Total));
            }
        }

        public double Discount
        {
            get { return discount; }
            set
            {
                discount = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(Total));
            }
        }

        public double Taxes
        {
            get { return taxes; }
            set
            {
                taxes = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(Total));
            }
        }

        public double Total
        {
            get
            {
                Purchase purchase =
                    new Purchase(Price, Discount, Taxes);

                return purchase.CalculateTotal();
            }
        }

        public event PropertyChangedEventHandler?
            PropertyChanged;

        private void OnPropertyChanged(
            [CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(
                this,
                new PropertyChangedEventArgs(propertyName));
        }
    }
}
