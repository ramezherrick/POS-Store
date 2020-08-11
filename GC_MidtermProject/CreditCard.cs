using System;
using System.Collections.Generic;
using System.Text;

namespace GC_MidtermProject
{
    class CreditCard : Payment
    {
        //properties
        public double CreditCardNumber { get; set; }
        public DateTime ExpirationDate { get; set; }
        public int CVV { get; set; }

        //Constructor
        public CreditCard() { }
        public CreditCard(double CreditCardNumber, DateTime ExpirationDate, int CVV)
        {
            this.CreditCardNumber = CreditCardNumber;
            this.ExpirationDate = ExpirationDate;
            this.CVV = CVV;
        }

        //method
        public override double ChangeBack()
        {
            //Prompt for card number
            Console.Write("Please enter your 16-digit credit card number: ");

            while (true)
            {
                while (true)
                {

                    try
                    {
                        CreditCardNumber = double.Parse(Console.ReadLine());
                        break;
                    }
                    catch
                    {
                        Console.WriteLine("Please Enter a numerical value");
                        continue;
                    }
                }


                if (CreditCardNumber <= 9999999999999999 && CreditCardNumber > 999999999999999)
                {
                    //Prompt for expiration date
                    Console.Write("Please enter your credit card expiration date: ");
                    DateTime date;

                    while (!DateTime.TryParse(Console.ReadLine(), out date))
                    {
                        Console.WriteLine("Please enter a correct date: ");
                    }

                    Console.WriteLine("Please enter your 3-digit CVV: ");
                    int CVV = 0; 
                    

                    while (true)
                    {
                        while (!int.TryParse(Console.ReadLine(), out CVV))
                        {
                            Console.WriteLine("Invalid CVV number - Please try again!");
                        }
                        if (CVV < 1000 && CVV > 99)
                        {
                            Console.WriteLine("Payment Processing Please Wait");
                            Console.WriteLine("\nPayment Processed - Thank you!");
                            return -1;
                        }

                        else
                        {
                            Console.WriteLine("Invalid CVV.  Please try again: ");                          
                        }
                    }
                }
                else
                {
                    Console.Write("Invalid Credit Card number entered.  Please try again: ");
                    continue;
                }
            }
        }
        public override void Receipt(List<Product> shoppingList, double subTotal, double taxTotal)
        {
            base.Receipt(shoppingList, subTotal, taxTotal);
            Console.WriteLine("Payment Type: Credit Card");

            string lastFour = CreditCardNumber.ToString().Substring(12, 4);
            Console.WriteLine($"Credit card number: XXXX XXXX XXXX {lastFour}");

        }
    }
}
