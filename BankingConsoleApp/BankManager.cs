using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace BankingConsoleApp
{
    public class BankManager
    {
        public List<Account> Accounts = new List<Account>();
        private readonly string _filePath = "banking.json";


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

                Console.WriteLine($"Data loaded. {Accounts.Count} student(s) found.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Load failed: {ex.Message}. Starting fresh.");
                Accounts = new List<Account>();
            }
        }

    }
}
