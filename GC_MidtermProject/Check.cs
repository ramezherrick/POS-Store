using System;
using System.Collections.Generic;
using System.Text;

namespace GC_MidtermProject
{
    class Check : Payment
    {
        //Property
        int RoutingNumber { get; set; }
        double AccountNumber { get; set; }

        //Constructor

        public Check()
        {
            //this.RoutingNumber = RoutingNumber;
            //this.AccountNumber = AccountNumber;
        }

        //method
        public override double ChangeBack()
        {
            Console.Write("Please Enter your 7-digit routing number: ");

            while (true)
            {
                while (true)
                {

                    try
                    {
                        RoutingNumber = int.Parse(Console.ReadLine());
                        break;
                    }
                    catch
                    {
                        Console.WriteLine("Invalid entry - Please enter a numerical value");
                        continue;
                    }
                }
                if (RoutingNumber > 999999 && RoutingNumber <= 9999999)
                {
                    while (true)
                    {

                        while (true)
                        {
                            Console.Write("Please Enter your 8-digit account number: ");

                            try
                            {
                                AccountNumber = double.Parse(Console.ReadLine());
                                break;
                            }
                            catch
                            {
                                Console.WriteLine("Invalid entry - Please enter a numerical value");
                                continue;
                            }
                        }

                        if (AccountNumber > 9999999 && AccountNumber <= 99999999)
                        {

                            Console.WriteLine("\nPayment Processed - Thank you for using our service");
                            return -1;
                        }

                        else
                        {
                            Console.WriteLine("Invalid account number - Please try again");
                            continue;
                        }
                        
                    }

                }
                else
                {
                    Console.WriteLine("Invalid - routing number, Please try again");
                }
            }
            return -1;
        }
        public override void Receipt(List<Product> shoppingList, double subTotal, double taxTotal)
        {
            base.Receipt(shoppingList, subTotal, taxTotal);
            Console.WriteLine("Payment Type: Check");
        }

    }
}
