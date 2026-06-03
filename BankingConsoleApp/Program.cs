using System;
namespace BankingConsoleApp
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            try
            {
                BankManager manager = new BankManager();
                ShowMenu menu = new ShowMenu(manager);
                menu.Run();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unexpected error: {ex.Message}");
                Console.WriteLine("Press any key to exit...");
                Console.ReadKey();
            }
        }
    }
    
}