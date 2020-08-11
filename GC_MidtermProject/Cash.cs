using System;
using System.Collections.Generic;
using System.Text;

namespace GC_MidtermProject
{
    class Cash:Payment
    {
        //Properties
        double TenderedAmount { get; set; }
        double TotalPrice { get; set; }

        //constructor
        public Cash() { }
        public Cash(double TenderedAmount, double TotalPrice)
        {
            this.TenderedAmount = TenderedAmount;
            this.TotalPrice = TotalPrice;
        }

        //Method
        public override double ChangeBack()
        {
            while (TenderedAmount < TotalPrice)
            {
                Console.WriteLine($"That is not enough money.  You are short by ${(TotalPrice - TenderedAmount):N2}.");
                Console.WriteLine($"Please give us an amount equal to ${TotalPrice:N2} or higher.");
                TenderedAmount = double.Parse(Console.ReadLine());
            }
            
            double ChangeBack = (TenderedAmount - TotalPrice);
            
            return ChangeBack;
        }
        public override void Receipt(List<Product> shoppingList, double subTotal, double taxTotal)
        {
            base.Receipt(shoppingList,subTotal,taxTotal);
            Console.WriteLine("Payment Type: Cash");
            Console.WriteLine($"Tendered Amount: ${TenderedAmount:N2}");
            Console.WriteLine($"Your change: ${ChangeBack():N2}");

        }
    }
}
