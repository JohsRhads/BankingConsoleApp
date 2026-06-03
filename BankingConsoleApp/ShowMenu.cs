using System;
using System.Collections.Generic;
using System.Text;

namespace BankingConsoleApp
{
    internal class ShowMenu
    {
        private BankManager bankManager;

        // Default constructor
        public ShowMenu()
        {
            bankManager = new BankManager();
        }

        // Constructor that accepts BankManager
        public ShowMenu(BankManager manager)
        {
            bankManager = manager;
        }

        public void Run()
        {
            bool running = true;

            while (running)
            {
                Console.WriteLine("\n════════════════════════════════");
                Console.WriteLine("         METROBANK SYSTEM");
                Console.WriteLine("════════════════════════════════");
                Console.WriteLine("1. Create Account");
                Console.WriteLine("2. View All Accounts");
                Console.WriteLine("3. Search Account");
                Console.WriteLine("4. Deposit Money");
                Console.WriteLine("5. Withdraw Money");
                Console.WriteLine("6. Transfer Money");
                Console.WriteLine("7. Calculate Interest");
                Console.WriteLine("8. Close Account");
                Console.WriteLine("9. Exit");
                Console.WriteLine("════════════════════════════════");
                Console.Write("Enter your choice: ");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        CreateAccount();
                        break;
                    case "2":
                        bankManager.ViewAllAccounts();
                        break;
                    case "3":
                        SearchAccount();
                        break;
                    case "4":
                        Deposit();
                        break;
                    case "5":
                        Withdraw();
                        break;
                    case "6":
                        Transfer();
                        break;
                    case "7":
                        CalculateInterest();
                        break;
                    case "8":
                        CloseAccount();
                        break;
                    case "9":
                        running = false;
                        Console.WriteLine("Thank you for using MetroBank System. Goodbye!");
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            }
        }

        private void CreateAccount()
        {
            // Validate name
            string name;
            do
            {
                Console.Write("Enter account holder name: ");
                name = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(name))
                {
                    Console.WriteLine("Name cannot be empty. Please try again.");
                }
            } while (string.IsNullOrWhiteSpace(name));

            // Validate type
            string type;
            do
            {
                Console.Write("Enter account type (Savings/Checking): ");
                type = Console.ReadLine();

                if (!type.Equals("Savings", StringComparison.OrdinalIgnoreCase) &&
                    !type.Equals("Checking", StringComparison.OrdinalIgnoreCase))
                {
                    Console.WriteLine("Invalid account type. Must be 'Savings' or 'Checking'. Please try again.");
                }
            } while (!type.Equals("Savings", StringComparison.OrdinalIgnoreCase) &&
                     !type.Equals("Checking", StringComparison.OrdinalIgnoreCase));

            // Validate initial deposit with TryParse
            double initialDeposit;
            bool validDeposit = false;
            do
            {
                Console.Write("Enter initial deposit: ");
                string input = Console.ReadLine();

                if (double.TryParse(input, out initialDeposit) && initialDeposit > 0)
                {
                    validDeposit = true;
                }
                else
                {
                    Console.WriteLine("Invalid amount. Please enter a valid positive number.");
                }
            } while (!validDeposit);

            bankManager.CreateAccount(name, type, initialDeposit);
        }

        private void SearchAccount()
        {
            Console.Write("Enter account number to search: ");
            string accountNumber = Console.ReadLine();
            bankManager.SearchAccount(accountNumber);
        }

        private void Deposit()
        {
            Console.Write("Enter account number: ");
            string accountNumber = Console.ReadLine();

            double amount;
            bool validAmount = false;
            do
            {
                Console.Write("Enter deposit amount: ");
                string input = Console.ReadLine();

                if (double.TryParse(input, out amount) && amount > 0)
                {
                    validAmount = true;
                }
                else
                {
                    Console.WriteLine("Invalid amount. Please enter a valid positive number.");
                }
            } while (!validAmount);

            bankManager.Deposit(accountNumber, amount);
        }

        private void Withdraw()
        {
            Console.Write("Enter account number: ");
            string accountNumber = Console.ReadLine();

            double amount;
            bool validAmount = false;
            do
            {
                Console.Write("Enter withdrawal amount: ");
                string input = Console.ReadLine();

                if (double.TryParse(input, out amount) && amount > 0)
                {
                    validAmount = true;
                }
                else
                {
                    Console.WriteLine("Invalid amount. Please enter a valid positive number.");
                }
            } while (!validAmount);

            bankManager.Withdraw(accountNumber, amount);
        }

        private void Transfer()
        {
            Console.Write("Enter sender account number: ");
            string fromAccount = Console.ReadLine();
            Console.Write("Enter receiver account number: ");
            string toAccount = Console.ReadLine();

            double amount;
            bool validAmount = false;
            do
            {
                Console.Write("Enter transfer amount: ");
                string input = Console.ReadLine();

                if (double.TryParse(input, out amount) && amount > 0)
                {
                    validAmount = true;
                }
                else
                {
                    Console.WriteLine("Invalid amount. Please enter a valid positive number.");
                }
            } while (!validAmount);

            bankManager.Transfer(fromAccount, toAccount, amount);
        }

        private void CalculateInterest()
        {
            Console.Write("Enter account number: ");
            string accountNumber = Console.ReadLine();
            bankManager.CalculateInterest(accountNumber);
        }

        private void CloseAccount()
        {
            Console.Write("Enter account number to close: ");
            string accountNumber = Console.ReadLine();
            bankManager.CloseAccount(accountNumber);
        }
    }
}