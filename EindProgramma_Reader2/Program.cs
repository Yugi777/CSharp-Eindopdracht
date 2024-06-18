using System.Threading.Channels;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Collections.Generic;


namespace PhoneSharp
{
    class Program
    {
        public static List<Phones> listofphones = new List<Phones>();

        public static void Main(string[] args)
        {
            // string username = args[0];
            // string password = args[1];

            Console.WriteLine("Hi, welcome to SharpPhone. What's your name?");
            string name = Console.ReadLine();
            
            switch (SharpPhone.LogIn())
            {
                case SharpPhone.Pass.Ok:
                    Console.WriteLine("Welcome to SharpPhone {0}", name);
                    SharpPhone.ShowMenu();
                    break;
                case SharpPhone.Pass.NotOk:
                    Console.WriteLine("Incorrect Password!");
                    break;
            }
        }

        public static class SharpPhone
        {
            public enum Pass
            {
                Ok,
                NotOk,
                Unknown,
            }

            public static Pass LogIn()
            {
                string correct = "PHONESHARP";
                int Try = 0;
                int maxTry = 3;
                while (Try <= maxTry)
                {
                    if (Try >= 1)
                    {
                        Console.WriteLine("Incorrect you used {0} of {1} tries.", Try, maxTry);
                    }

                    if (Try == maxTry)
                    {
                        Console.WriteLine("WARNING!! Last try!");
                    }

                    Console.WriteLine("What's the password?");
                    string passInput = Console.ReadLine();
                    if (correct == passInput)
                    {
                        Phones.BigList();
                        return Pass.Ok;
                    }
                    else
                    {
                        Try++;
                    }
                }

                return Pass.Unknown;
            }

            public static void ShowMenu()
            {
                Console.WriteLine("\nWhat can i do for you? " +
                                  "\n1. Phone Summary's" +
                                  "\n2. Summary of our Stock" +
                                  "\n3. Mutate Stock" +
                                  "\n4. Statistics" +
                                  "\n5. Add smartphone" +
                                  "\n8. Show menu" +
                                  "\n9. Exit");
                ConsoleKeyInfo inputKey = new ConsoleKeyInfo();

                bool loop = true;
                while (loop)
                {
                    inputKey = Console.ReadKey();
                    switch (inputKey.KeyChar.ToString())
                    {
                        case "1":
                            ShowModel();
                            break;
                        case "2":
                            ShowStock();
                            break;
                        case "3":
                            MutateStock();
                            break;
                        case "4":
                            ShowStats();
                            break;
                        case "5":
                            AddPhone();
                            break;
                        case "8":
                            ShowMenu();
                            break;
                        case "9":
                            Console.WriteLine("\nProgram will exit now.");
                            loop = false;
                            break;
                    }
                }
            }

            public static void ShowModel()
            {
                Console.WriteLine("\nOkay, here are all our models:");
                foreach (Phones phones in Phones.PhoneList)
                {
                    Console.WriteLine($"ID: {phones.id}\nBrand: {phones.brand}" +
                                      $"\nModel: {phones.model}" +
                                      $"\nMemory in MB: {phones.memory}" +
                                      $"\nPrice: {phones.price}");
                }
            }

            public static void ShowStock()
            {
                Console.WriteLine("\nSure, here is our supply of all our phones:");
                foreach (Phones phones in Phones.PhoneList)
                {
                    Console.WriteLine($"ID: {phones.id}\nStock: {phones.stock}");
                }
            }

            public static void MutateStock()
            {
                int idInput = 0;
                bool idCheck = false;
                int mutate = 0;

                Console.WriteLine("\nWhat ID would you like to mutate?");
                try
                {
                    idInput = Int32.Parse(Console.ReadLine());
                }
                catch (FormatException)
                {
                    Console.WriteLine("Input incorrect.");
                    MutateStock();
                }

                foreach (Phones phone in Phones.PhoneList)
                {
                    if (phone.id == idInput)
                    {
                        idCheck = true;
                        break;
                    }
                }
                if (idCheck)
                { }
                else
                {
                    Console.WriteLine("This ID is not in the system, try again!");
                    MutateStock();
                }
                Console.WriteLine("Input the mutation.");
                try
                {
                    mutate = Int32.Parse(Console.ReadLine());
                }
                catch (FormatException)
                {
                    Console.WriteLine("Input is incorrect, please try again.");
                    MutateStock();
                }

                foreach (Phones phones in Phones.PhoneList)
                {
                    if (phones.stock + mutate < 0)
                    {
                        Console.WriteLine("Stock can't go negative, please try again.");
                        MutateStock();
                        return;
                    }
                }
                
                foreach (Phones phones in Phones.PhoneList)
                {
                    if (phones.id == idInput)
                    {
                        phones.stock += mutate;
                        Console.WriteLine("Mutation Succesfull, press 2 to see stock.");
                        break;
                    }
                }
            }

            public static void ShowStats()
            {
                Console.WriteLine("\nHere you can find the statistics of all our products:" +
                                  "\n1. Total stock of all our phones." +
                                  "\n2. Total value of all our phones" +
                                  "\n3. Average price of all our phones" +
                                  "\n4. Best value for money in terms of storage ");
                ConsoleKeyInfo inputKey = new ConsoleKeyInfo();
                inputKey = Console.ReadKey();
                switch (inputKey.KeyChar.ToString())
                {
                    case "1":
                        int totalStock = Phones.PhoneList.Sum(item => item.stock);
                        Console.WriteLine($"\nThe total stock of our phones is {totalStock}");
                        break;
                    case "2":
                        double totalValue = Phones.PhoneList.Sum(item => item.price * item.stock);
                        Console.WriteLine($"\nThe total value of our phones are {totalValue}");
                        break;
                    case "3":
                        double averagePrice = Phones.PhoneList.Average(item => item.price);
                        Console.WriteLine($"\nThe average price of our phones are {averagePrice}");
                        break;
                    case "4":
                        var bestValue = Phones.PhoneList.MaxBy(item => item.memory  / item.price);
                        Console.WriteLine($"\nThe best value for your money in terms of storage is the {bestValue.brand} {bestValue.model}");
                        break;
                }
            }

            public static void AddPhone()
            {
                static int phoneId()
                {
                    return Phones.PhoneList.Max(item => item.id + 1);
                }
                
                Phones NewPhone = new Phones();
                Console.WriteLine("\nFirst we need some data for the phone:" +
                                  "\nPlease enter the brand:");
                string brandInput = Console.ReadLine();
                Console.WriteLine("Please enter the model:");
                string modelInput = Console.ReadLine();
                Console.WriteLine("Please enter the memory of the phone:");
                int memoryInput = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("Please enter the price of the phone");
                double priceInput = Convert.ToDouble(Console.ReadLine());
                NewPhone.id = phoneId();
                NewPhone.brand = brandInput;
                NewPhone.model = modelInput;
                NewPhone.memory = memoryInput;
                NewPhone.price = priceInput;
                NewPhone.stock = 0;
                Phones.PhoneList.Add(NewPhone);
                Console.WriteLine("New phone added succesfully, press 1 to see all our models. ");
            }
        }
    }
}