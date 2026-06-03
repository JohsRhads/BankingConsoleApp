using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;

namespace BankingConsoleApp
{

    public class Account
    {

        private string name = string.Empty;
        private string type;
        private double balance;
        private static int nextcounNumber = 1001;
        public string AccountNumber;

        public DateTime DateCreated {  get; set; }
        public string Name
        {
            get => name;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    Console.WriteLine("Account name cannot be empty or whitespace.");
                else
                    name = value.Trim();
            }
        }

        public string Type
        {
            get => type;
            set
            {
                string formalizedType = value.Trim();
                if (formalizedType.Equals("Savings", StringComparison.OrdinalIgnoreCase) ||
                    formalizedType.Equals("Checking", StringComparison.OrdinalIgnoreCase))  
                {
                    // Formats correctly as "Savings" or "Checking" regardless of user input casing
                    type = char.ToUpper(formalizedType[0]) + formalizedType.Substring(1).ToLower();
                }
                else
                {
                    Console.WriteLine("Account type must be either 'Savings' or 'Checking'.");
                }
            }
        }

        public double Balance
        {
            get => balance;
            set
            {
                if (value >= 0)
                    balance = value;
                else
                    Console.WriteLine("Balance cannot go below 0.");
            }
        }
        // Parameterless constructor for JSON
        public Account() { }
        public  Account(string name, string type, double initialDeposit)
        {   
            Name = name;
            Type = type;
            DateCreated = DateTime.Now;
            AccountNumber = $"ACC-{nextcounNumber}";
            nextcounNumber++;
            Balance = initialDeposit;

            if (Type.Equals("Savings", StringComparison.OrdinalIgnoreCase) && initialDeposit < 500)
            {
                Console.WriteLine("Savings account requires minimum ₱500 deposit.");
                Balance = 0;
            }
            else if (Type.Equals("Checking", StringComparison.OrdinalIgnoreCase) && initialDeposit < 1000)
            {
                Console.WriteLine("Checking account requires minimum ₱1000 deposit.");
                Balance = 0;
            }
            else
            {
                Balance = initialDeposit;
            }
        }
    }
}
