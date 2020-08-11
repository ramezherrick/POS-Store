using System;
using System.Collections.Generic;
using System.Text;

namespace GC_MidtermProject
{
    abstract class Payment
    {

        public Payment() { }

        //method
        public abstract double ChangeBack();

        public virtual void Receipt (List<Product> shoppingList,double subTotal, double taxTotal)
        {
            Console.WriteLine("RECEIPT");
            Console.WriteLine($"Item\tName\t\tQTY\tUnit Price\tLine Total");


            for (int i =0;i<shoppingList.Count;i++)
            {
                string price = shoppingList[i].Price.ToString();
                double total = shoppingList[i].Quantity * shoppingList[i].Price;
                string lineTotal = total.ToString();

                Console.WriteLine($"{i+1}\t{string.Format("{0,-15}",shoppingList[i].Name)}\t{shoppingList[i].Quantity}\t${string.Format("{0,10}",price.ToString())}\t${lineTotal}");                
            }
            Console.WriteLine($"The Total = ${(subTotal + taxTotal):N2}");
        }

    }
}
