using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace GC_MidtermProject
{
    class POSController
    {
        public List<Product> Products { get; set; }

        public POSController() { }

        public POSController(List<Product> products)
        {
            this.Products = products;
        }

        public void RunProgram()
        {
            while (true)
            {
                #region Primary Requirements
                List<Product> products = new List<Product>() { };

                StreamReader reader = new StreamReader("../../../ProductList.txt");  //reads product list in from txt file
                string line = reader.ReadLine();
                while (line != null)
                {
                    string[] productProperty = line.Split('|');
                    products.Add(new Product(productProperty[0], productProperty[1], productProperty[2], double.Parse(productProperty[3]), int.Parse(productProperty[4])));
                    line = reader.ReadLine();
                }
                reader.Close();

                Console.WriteLine("Main Menu");  //writes Main Menu to console
                Console.WriteLine("Press 0: Standard User (cashier)");
                Console.WriteLine("Press 1: Admin (add product)");
                Console.WriteLine("Press 2: Exit");

                int choice = -1;

                while (!int.TryParse(Console.ReadLine(), out choice))  //choose and validate menu option
                {
                    Console.WriteLine("Invalid Entry-Please enter 0 or 1 or 2");
                }

                if (choice == 0)  //user chooses cashier
                {
                    while (true)  //loops and validates if user would like to process another order
                    {
                        List<Product> shoppingCart = new List<Product>();

                        shoppingCart = ShoppingMenu(products);

                        CheckOut(shoppingCart);

                        Console.Write("Would you like to process another order? (y/n) ");
                        string cont = Console.ReadLine().ToLower();
                        while (cont != "n" && cont != "y")
                        {
                            Console.Write("Invalid entry. please input (y/n) ");
                            cont = Console.ReadLine().ToLower();
                        }
                        if (cont == "n")
                        {
                            Console.Clear();
                            break;
                        }
                        Console.Clear();
                    }
                }
                #endregion
                #region Extended Exercise

                else if (choice == 1) //user chooses admin mode
                {
                    while (true)
                    {
                        Console.WriteLine("New Item Entry");

                        Console.Write("Please enter a name (max 15 characters): ");  //prompts for item name entry and ensures 15 character length

                        string name = Console.ReadLine();

                        Console.Write("Please enter a description (max 15 characters): ");  //prompts for item description and ensures 15 character length

                        string description = Console.ReadLine();

                        Console.WriteLine("Please choose a category from the options below.");  //prompts for item category
                        Console.WriteLine($"1. Drink\n2. Food\n3. HardGood\n4. SoftGood");

                        string _category = "";
                        int category = -1;
                        while (true)
                        {
                            while (!int.TryParse(Console.ReadLine(), out category))
                            {
                                Console.WriteLine("Please enter a numerical value 1 - 4");
                            }
                            if (category == 1)
                            {
                                _category = "Drink   ";
                                break;
                            }
                            else if (category == 2)
                            {
                                _category = "Food    ";
                                break;
                            }
                            else if (category == 3)
                            {
                                _category = "HardGood";
                                break;
                            }
                            else if (category == 4)
                            {
                                _category = "SoftGood";
                                break;
                            }
                            else
                            {
                                Console.Write("Invalid Entry - Please try again ");
                            }
                        }


                        Console.Write("Please enter the price: ");  //prompts user for price and validates entry

                        double price = 0;

                        while (!double.TryParse(Console.ReadLine(), out price))
                        {
                            Console.WriteLine("Please enter a numerical value");
                        }

                        Console.Write($"How many of this item do you want to add to inventory? ");  //prompts user for inventory level
                        int Inventory = 0;
                        while (!int.TryParse(Console.ReadLine(), out Inventory))
                        {
                            Console.WriteLine("Please enter a numerical value");
                        }

                        
                        
                        //adds newly created product to the product list
                        products.Add(new Product(name, _category, description, price, Inventory));  

                        
                        //writes new product to the txt file
                        StreamWriter writer = new StreamWriter("../../../ProductList.txt");  
                        foreach (Product product in products)
                        {
                            writer.WriteLine($"{product.Name}|{product.Category}|{product.Description}|{product.Price}|{product.Inventory}");
                        }
                        writer.Close();

                        
                        
                        Console.WriteLine("Would you like to add another item (y/n)?");
                        string answer = Console.ReadLine().ToLower();

                        while (answer != "y" && answer != "n")
                        {
                            Console.Write("Invalid entry.  Please enter (y/n) ");
                            answer = Console.ReadLine().ToLower();
                        }

                        if (answer == "y")
                        {
                            continue;
                        }
                        else
                        {
                            break;
                        }

                    }
                }
                #endregion
                else if (choice == 2)
                {
                    Console.WriteLine("Thank you - Bye!");
                    break;
                }

                else
                {
                    Console.WriteLine("Invalid - Entry");
                }
            }
        }

        public static List<Product> ShoppingMenu(List<Product> inventory)
        {
            #region Primary Functionality

            int shoppingcartIndex = 0;
            List<Product> shoppingCart = new List<Product>() { };

            string input2 = "";


            while (true)
            {
                Console.WriteLine($"Item #\tName\t\tCategory\tDescription\tPrice\t\tInventory");

                for (int index = 0; index < inventory.Count; index++)  //displays all products available to purchase
                {
                    Console.Write($"{index + 1}\t");
                    inventory[index].PrintList();
                }

                Console.WriteLine("Which item would you like to add to your cart?");  //finds index in inventory of what product we want to purchase
                int itemselection = 0; //int.Parse(Console.ReadLine()) - 1;

                while (true) //validation for menu entry
                {
                    input2 = Console.ReadLine();
                    try
                    {
                        itemselection = int.Parse(input2);
                        if (itemselection > 0 && itemselection <= inventory.Count)
                        {
                            itemselection = itemselection - 1;
                            break;
                        }
                        else
                        {
                            Console.Write($"That is not a valid choice.  Please input a number between 1 and {inventory.Count} ");
                            continue;
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("That choice wasn't a number.  Please try again.");
                    }
                }
                #endregion

                #region Extra Functionality
                Console.WriteLine($"How many would {inventory[itemselection].Name.Trim()}(s) would you like?  We have {inventory[itemselection].Inventory} in stock.");  //asks user for how much of said product they'd like to buy


                int quantity = 0;
                
                //validation for quantity entry
                while (true) 
                {
                    input2 = Console.ReadLine();
                    try
                    {
                        quantity = int.Parse(input2);
                        if (quantity > inventory[itemselection].Inventory)
                        {
                            Console.WriteLine($"Not enough in stock - please enter a quantity less than {inventory[itemselection].Inventory}");
                            continue;
                        }
                    #region Extra validation
                        else if (quantity > 0)
                        {
                            inventory[itemselection].Inventory -= quantity;
                            break;
                        }

                        else
                        {
                            Console.WriteLine($"That is not a valid choice.  Please input a quantity greater than 0.");
                            continue;
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("That choice wasn't a number.  Please try again.");
                    }
                }
                #endregion

                shoppingCart.Add(inventory[itemselection]);  //adds selected product to shopping cart

                shoppingCart[shoppingcartIndex].Quantity = quantity;  //updates qty of product in shopping cart

                shoppingCart[shoppingcartIndex].PrintLineTotal();  //print a line summary for the item added

                shoppingcartIndex++;  //iterates shopping cart index



                Console.Write("Would you like to purchase another item? (y/n) ");
                string input = Console.ReadLine().ToLower();

                while (input != "n" && input != "y")
                {
                    Console.Write("Invalid response.  Please enter (y/n) ");
                    input = Console.ReadLine().ToLower();
                }

                if (input == "n")
                {
                    Console.Clear();
                    break;
                }
                Console.Clear();
            }

            //updates txt file with new inventory levels
            StreamWriter writer = new StreamWriter("../../../ProductList.txt");  
            foreach (Product product in inventory)
            {
                writer.WriteLine($"{product.Name}|{product.Category}|{product.Description}|{product.Price}|{product.Inventory}");
            }

            writer.Close();
            #endregion

            return shoppingCart;

        }

        public static void CheckOut(List<Product> shoppingList)
        {
            string[] checkoutOptions = { "Cash", "Credit", "Check" };
            double subTotal = 0;
            double taxRate = .06;
            double taxTotal = 0;

            foreach (Product product in shoppingList)  //adds each item from the shopping cart's price to the subtotal
            {
                subTotal += product.Price * product.Quantity;
            }

            taxTotal = subTotal * taxRate;

            for (int i = 0; i < shoppingList.Count; i++)  //prints a summary of items from the shopping cart along with totals
            {
                shoppingList[i].PrintLineTotal();
            }
            Console.WriteLine($"Subtotal = ${subTotal:N2}");
            Console.WriteLine($"Tax (6%) = ${taxTotal:N2}");
            Console.WriteLine($"Grand Total = ${(subTotal + taxTotal):N2}");

            Console.WriteLine("How would you like to pay?");  //prompts user for how to pay

            for (int i = 0; i < checkoutOptions.Length; i++)  //prints list of checkout options
            {
                Console.WriteLine($"{i + 1}) {checkoutOptions[i]}");
            }

            int paymentChoice = 0;
            while (true)
            {
                while (!int.TryParse(Console.ReadLine(), out paymentChoice))
                {
                    Console.WriteLine("Please enter a valid number choice ");
                }

                if (paymentChoice == 1)  //cash option
                {
                    Console.WriteLine("How much money are you giving us?");

                    double tenderedAmount = 0;

                    while (!double.TryParse(Console.ReadLine(), out tenderedAmount))
                    {
                        Console.WriteLine("Please enter a valid dollar amount");
                    }

                    Cash cashPayment = new Cash(tenderedAmount, (subTotal + taxTotal));
                    double change = cashPayment.ChangeBack();       //cash receipt

                    Console.WriteLine($"Thanks for shopping!");

                    Console.WriteLine("Please press any key to view your receipt.");
                    Console.ReadKey();
                    Console.Clear();

                    cashPayment.Receipt(shoppingList, subTotal, taxTotal);
                    break;
                }
                else if (paymentChoice == 2)  //credit card option
                {
                    CreditCard creditPayment = new CreditCard();
                    creditPayment.ChangeBack();
                    creditPayment.Receipt(shoppingList, subTotal, taxTotal);       //credit receipt
                    break;
                }
                else if (paymentChoice == 3) //check option
                {
                    Check check = new Check();
                    check.ChangeBack();
                    check.Receipt(shoppingList, subTotal, taxTotal);  //check receipt
                    break;
                }
                else
                {
                    Console.WriteLine("Invalid Entry - Please choose a number from the list.");
                }
            }
        }

    }
}

