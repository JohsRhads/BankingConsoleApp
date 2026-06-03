using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace BankingConsoleApp
{
    public class BankManager
    {
        public List<Account> Accounts = new List<Account>();
        private readonly string _filePath = "accounts.json";


        public BankManager()
        {
            LoadFromFile();
        }

        public void SaveToFile()
        {
            try
            {
                string json = JsonSerializer.Serialize(Accounts);
                File.WriteAllText(_filePath, json);
                Console.WriteLine("Data saved successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Save failed: {ex.Message}");
            }
        }

        // ==========================================
        // LOAD FROM FILE
        // ==========================================
        public void LoadFromFile()
        {
            if (!File.Exists(_filePath))
            {
                Console.WriteLine("No save file found. Starting fresh.");
                return;
            }

            try
            {
                string json = File.ReadAllText(_filePath);
                var result = JsonSerializer.Deserialize<List<Account>>(json);

                if (result != null)
                {
                    Accounts = result;
                }
                else
                {
                    Accounts = new List<Account>();
                }

                Console.WriteLine($"Data loaded. {Accounts.Count} account(s) found.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Load failed: {ex.Message}. Starting fresh.");
                Accounts = new List<Account>();
            }
        }
        public void CreateAccount(string name, string type, double initialDeposit)
        {
            Account newAccount = new Account(name, type, initialDeposit);

            Accounts.Add(newAccount);

            SaveToFile();
            Console.WriteLine($"Account successfully created! Account Number: {newAccount.AccountNumber}");
        }

        public void ViewAllAccounts()
        {
            if (Accounts == null || Accounts.Count == 0)
            {
                Console.WriteLine("══════════════════════════════════════════════════════════════════════");
                Console.WriteLine("                         NO ACCOUNTS FOUND");
                Console.WriteLine("══════════════════════════════════════════════════════════════════════");
                return;
            }

            Console.WriteLine("══════════════════════════════════════════════════════════════════════");
            Console.WriteLine("                          ACCOUNT LIST");
            // Column alignments: AccountNumber (15 spaces), Name (20 spaces), Type (12 spaces), Balance (15 spaces)
            Console.WriteLine($"{"Account No.",-15}{"Name",-20}{"Type",-12}{"Balance",-15}");
            Console.WriteLine("──────────────────────────────────────────────────────────────────────");

            foreach (var account in Accounts)
            {
                Console.WriteLine(
                    $"{account.AccountNumber,-15}" +
                    $"{account.Name,-20}" +
                    $"{account.Type,-12}" +
                    $"{account.Balance,-15:C}" // ':C' formats the double/decimal directly to local currency (e.g., $1,000.00)
                );
            }

            Console.WriteLine("══════════════════════════════════════════════════════════════════════");
        }
        public void SearchAccount(string accountNumber)
        {
            // Search for the account by AccountNumber instead of Name
            var account = Accounts.Find(a => a.AccountNumber.Equals(accountNumber, StringComparison.OrdinalIgnoreCase));

            // If no matching account number is found
            if (account == null)
            {
                Console.WriteLine("══════════════════════════════════════════════════════════════════════");
                Console.WriteLine($"            ACCOUNT NUMBER '{accountNumber.ToUpper()}' NOT FOUND");
                Console.WriteLine("══════════════════════════════════════════════════════════════════════");
                return;
            }

            // Header structure
            Console.WriteLine("══════════════════════════════════════════════════════════════════════");
            Console.WriteLine("                         SEARCH RESULT");
            Console.WriteLine("══════════════════════════════════════════════════════════════════════");
            Console.WriteLine($"{"Account No.",-15}{"Name",-20}{"Type",-12}{"Balance",-15}");
            Console.WriteLine("──────────────────────────────────────────────────────────────────────");

            // Print the specific account details
            Console.WriteLine(
                $"{account.AccountNumber,-15}" +
                $"{account.Name,-20}" +
                $"{account.Type,-12}" +
                $"{account.Balance,-15:C}"
            );

            Console.WriteLine("══════════════════════════════════════════════════════════════════════");
        }
        public void Deposit(string accountNumber ,double amount)
        {
            var findaccount = Accounts.Find(a => a.AccountNumber.Equals(accountNumber,StringComparison.OrdinalIgnoreCase));

            if (findaccount == null)
            {
                Console.WriteLine("══════════════════════════════════════════════════════════════════════");
                Console.WriteLine($"       ACCOUNT NUMBER '{accountNumber.ToUpper()}' NOT FOUND");
                Console.WriteLine("══════════════════════════════════════════════════════════════════════");
                return;
            }
            if(amount <= 0)
            {
                Console.WriteLine("Amount must be positive.");
                return ;
            }

            findaccount.Balance += amount;
            SaveToFile();
            Console.WriteLine($"Deposit successful! New balance: {findaccount.Balance:C}");

        }
        public void Withdraw(string accountNumber ,double amount)
        {
            var findaccount = Accounts.Find(a => a.AccountNumber.Equals(accountNumber, StringComparison.OrdinalIgnoreCase));

            if (findaccount == null)
            {
                Console.WriteLine("══════════════════════════════════════════════════════════════════════");
                Console.WriteLine($"       ACCOUNT NUMBER '{accountNumber.ToUpper()}' NOT FOUND");
                Console.WriteLine("══════════════════════════════════════════════════════════════════════");
                return;
            }
            if (amount <= 0)
            {
                Console.WriteLine("Amount must be positive.");
                return;
            }
            if(amount > findaccount.Balance)
            {
                Console.WriteLine("Insufficient funds.");
                return;
            }

            findaccount.Balance -= amount;
            SaveToFile();
            Console.WriteLine($"Withdrawal successful! New balance: ₱{findaccount.Balance}");
        }
        public void Transfer(string fromAccountNumber, string toAccountNumber, double amount)
        {
            var findsenderaccount = Accounts.Find(a => a.AccountNumber.Equals(fromAccountNumber, StringComparison.OrdinalIgnoreCase));
            if (findsenderaccount == null)
            {
                Console.WriteLine("══════════════════════════════════════════════════════════════════════");
                Console.WriteLine($"      SENDER ACCOUNT NUMBER '{fromAccountNumber.ToUpper()}' NOT FOUND");
                Console.WriteLine("══════════════════════════════════════════════════════════════════════");
                return;
            }

            var findrecieveraccount = Accounts.Find(a => a.AccountNumber.Equals(toAccountNumber, StringComparison.OrdinalIgnoreCase));
            if (findrecieveraccount == null)
            {
                Console.WriteLine("══════════════════════════════════════════════════════════════════════");
                Console.WriteLine($"    RECEIVER ACCOUNT NUMBER '{toAccountNumber.ToUpper()}' NOT FOUND");
                Console.WriteLine("══════════════════════════════════════════════════════════════════════");
                return;
            }

            if(findrecieveraccount ==  findsenderaccount)
            {
                Console.WriteLine("Cannot transfer to the same account.");
                return;
            }
            if(amount  <= 0)
            {
                Console.WriteLine("Amount must be positive.");
                return ;
            }
            if(amount  > findsenderaccount.Balance)
            {
                Console.WriteLine("Insufficient funds.");
                return;
            }
            findsenderaccount.Balance -= amount;
            findrecieveraccount.Balance += amount;
            SaveToFile();
            Console.WriteLine("Transfer successful!");
        }
        public void CalculateInterest(string accountNumber) 
        {
            var findaccountNumber = Accounts.Find(a => a.AccountNumber.Equals(accountNumber, StringComparison.OrdinalIgnoreCase));

            if(findaccountNumber == null)
            {
                Console.WriteLine("Account not found.");
                return;
            }

            if(findaccountNumber.Type == "Checking")
            {
                Console.WriteLine("Interest only applies to Savings accounts.");
                return;
            }
            if(findaccountNumber.Type == "Savings")
            {
                double interest = findaccountNumber.Balance * 0.04;
                Console.WriteLine($"Account: {findaccountNumber.AccountNumber}");
                Console.WriteLine($"Balance: {findaccountNumber.Balance:C}");
                Console.WriteLine($"Interest (4%): {interest:C}");
            }
        }
        public void CloseAccount(string accountNumber)
        {
            var removeAccountCount = Accounts.RemoveAll(a => a.AccountNumber.Equals(accountNumber, StringComparison.OrdinalIgnoreCase));
            if(removeAccountCount == 0)
            {
                Console.WriteLine("══════════════════════════════════════════════════════════════════════");
                Console.WriteLine($"     ACCOUNT NUMBER '{accountNumber.ToUpper()}' NOT FOUND");
                Console.WriteLine("══════════════════════════════════════════════════════════════════════");
                return;
            }
            if(removeAccountCount > 0)
            {
                Console.WriteLine("Account closed successfully.");
                SaveToFile(); 

            }
        }
    }
}
