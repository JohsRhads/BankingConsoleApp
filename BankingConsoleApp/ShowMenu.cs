using System;
using System.Collections.Generic;
using System.Text;

namespace BankingConsoleApp
{
    internal class ShowMenu
    {
        private BankManager bankManager;

        public ShowMenu()
        {
            bankManager = new BankManager();
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
            Console.Write("Enter account holder name: ");
            string name = Console.ReadLine();
            Console.Write("Enter account type (Savings/Checking): ");
            string type = Console.ReadLine();
            Console.Write("Enter initial deposit: ");
            double initialDeposit = Convert.ToDouble(Console.ReadLine());

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
            Console.Write("Enter deposit amount: ");
            double amount = Convert.ToDouble(Console.ReadLine());

            bankManager.Deposit(accountNumber, amount);
        }

        private void Withdraw()
        {
            Console.Write("Enter account number: ");
            string accountNumber = Console.ReadLine();
            Console.Write("Enter withdrawal amount: ");
            double amount = Convert.ToDouble(Console.ReadLine());

            bankManager.Withdraw(accountNumber, amount);
        }

        private void Transfer()
        {
            Console.Write("Enter sender account number: ");
            string fromAccount = Console.ReadLine();
            Console.Write("Enter receiver account number: ");
            string toAccount = Console.ReadLine();
            Console.Write("Enter transfer amount: ");
            double amount = Convert.ToDouble(Console.ReadLine());

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