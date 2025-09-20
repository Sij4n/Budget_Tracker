using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Globalization;

namespace BudgetTracker
{
    /// <summary>
    /// Main program class for the Budget Tracker console application
    /// </summary>
    class Program
    {
        private static readonly string DataFile = "budget.json";
        private static List<BudgetItem> budgetItems = new List<BudgetItem>();

        static void Main(string[] args)
        {
            Console.Title = "Budget Tracker";
            LoadData();
            ShowWelcome();

            bool running = true;
            while (running)
            {
                ShowMenu();
                string choice = Console.ReadLine()?.Trim();

                switch (choice)
                {
                    case "1":
                        AddIncome();
                        break;
                    case "2":
                        AddExpense();
                        break;
                    case "3":
                        ShowAllRecords();
                        break;
                    case "4":
                        ShowSummary();
                        break;
                    case "5":
                        SaveData();
                        Console.WriteLine("\n💾 Data saved successfully!");
                        Console.WriteLine("Thanks for using Budget Tracker. Goodbye! 👋");
                        running = false;
                        break;
                    default:
                        Console.WriteLine("\n❌ Invalid choice. Please select 1-5.");
                        break;
                }

                if (running)
                {
                    Console.WriteLine("\nPress any key to continue...");
                    Console.ReadKey();
                }
            }
        }

        /// <summary>
        /// Displays welcome message and current date
        /// </summary>
        private static void ShowWelcome()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("╔═══════════════════════════════════════╗");
            Console.WriteLine("║         💰 BUDGET TRACKER 💰         ║");
            Console.WriteLine("╚═══════════════════════════════════════╝");
            Console.ResetColor();
            Console.WriteLine($"Today's Date: {DateTime.Now:dddd, MMMM dd, yyyy}");
            Console.WriteLine($"Loaded {budgetItems.Count} existing records.\n");
        }

        /// <summary>
        /// Displays the main menu options
        /// </summary>
        private static void ShowMenu()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("═══════════════ MAIN MENU ═══════════════");
            Console.ResetColor();
            Console.WriteLine("1. 📈 Add Income");
            Console.WriteLine("2. 📉 Add Expense");
            Console.WriteLine("3. 📋 Show All Records");
            Console.WriteLine("4. 📊 Show Summary");
            Console.WriteLine("5. 🚪 Exit & Save");
            Console.WriteLine("═════════════════════════════════════════");
            Console.Write("Please choose an option (1-5): ");
        }

        /// <summary>
        /// Adds a new income entry
        /// </summary>
        private static void AddIncome()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("📈 ADD NEW INCOME");
            Console.ResetColor();
            Console.WriteLine("════════════════════");

            var item = CreateBudgetItem(BudgetType.Income);
            if (item != null)
            {
                budgetItems.Add(item);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"\n✅ Income added successfully!");
                Console.WriteLine($"   💰 ${item.Amount:F2} - {item.Description}");
                Console.ResetColor();
            }
        }

        /// <summary>
        /// Adds a new expense entry
        /// </summary>
        private static void AddExpense()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("📉 ADD NEW EXPENSE");
            Console.ResetColor();
            Console.WriteLine("═════════════════════");

            var item = CreateBudgetItem(BudgetType.Expense);
            if (item != null)
            {
                budgetItems.Add(item);
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"\n✅ Expense added successfully!");
                Console.WriteLine($"   💸 ${item.Amount:F2} - {item.Description}");
                Console.ResetColor();
            }
        }

        /// <summary>
        /// Creates a new budget item with user input
        /// </summary>
        /// <param name="type">The type of budget item (Income/Expense)</param>
        /// <returns>A new BudgetItem or null if creation failed</returns>
        private static BudgetItem CreateBudgetItem(BudgetType type)
        {
            try
            {
                // Get description
                Console.Write("Description: ");
                string description = Console.ReadLine()?.Trim();
                if (string.IsNullOrEmpty(description))
                {
                    Console.WriteLine("❌ Description cannot be empty.");
                    return null;
                }

                // Get amount
                Console.Write("Amount ($): ");
                if (!decimal.TryParse(Console.ReadLine(), NumberStyles.Currency | NumberStyles.Number,
                    CultureInfo.CurrentCulture, out decimal amount) || amount <= 0)
                {
                    Console.WriteLine("❌ Please enter a valid amount greater than 0.");
                    return null;
                }

                // Get date (optional - defaults to today)
                Console.Write($"Date (MM/dd/yyyy) [Press Enter for today]: ");
                string dateInput = Console.ReadLine()?.Trim();
                DateTime date = DateTime.Today;

                if (!string.IsNullOrEmpty(dateInput))
                {
                    if (!DateTime.TryParseExact(dateInput, "MM/dd/yyyy", CultureInfo.InvariantCulture,
                        DateTimeStyles.None, out date))
                    {
                        Console.WriteLine("❌ Invalid date format. Using today's date.");
                        date = DateTime.Today;
                    }
                }

                return new BudgetItem(type, description, amount, date);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Error creating budget item: {ex.Message}");
                return null;
            }
        }

        /// <summary>
        /// Displays all budget records in a formatted table
        /// </summary>
        private static void ShowAllRecords()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("📋 ALL BUDGET RECORDS");
            Console.ResetColor();
            Console.WriteLine("═════════════════════════════════════════════════════════════════");

            if (budgetItems.Count == 0)
            {
                Console.WriteLine("No records found. Start by adding some income or expenses!");
                return;
            }

            // Sort by date (newest first)
            var sortedItems = budgetItems.OrderByDescending(x => x.Date).ToList();

            Console.WriteLine($"{"Date",-12} {"Type",-8} {"Amount",-12} {"Description",-30}");
            Console.WriteLine("─────────────────────────────────────────────────────────────────");

            foreach (var item in sortedItems)
            {
                Console.ForegroundColor = item.Type == BudgetType.Income ? ConsoleColor.Green : ConsoleColor.Red;
                string typeIcon = item.Type == BudgetType.Income ? "📈" : "📉";
                Console.WriteLine($"{item.Date:MM/dd/yyyy}   {typeIcon} {item.Type,-6} ${item.Amount,8:F2}   {item.Description}");
                Console.ResetColor();
            }

            Console.WriteLine("─────────────────────────────────────────────────────────────────");
            Console.WriteLine($"Total Records: {budgetItems.Count}");
        }

        /// <summary>
        /// Shows financial summary including totals and balance
        /// </summary>
        private static void ShowSummary()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("📊 FINANCIAL SUMMARY");
            Console.ResetColor();
            Console.WriteLine("═══════════════════════");

            decimal totalIncome = budgetItems.Where(x => x.Type == BudgetType.Income).Sum(x => x.Amount);
            decimal totalExpenses = budgetItems.Where(x => x.Type == BudgetType.Expense).Sum(x => x.Amount);
            decimal balance = totalIncome - totalExpenses;

            int incomeCount = budgetItems.Count(x => x.Type == BudgetType.Income);
            int expenseCount = budgetItems.Count(x => x.Type == BudgetType.Expense);

            Console.WriteLine("┌─────────────────────────────────────┐");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"│ 📈 Total Income:     ${totalIncome,12:F2} │");
            Console.ResetColor();
            Console.WriteLine($"│    ({incomeCount} transactions)");

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"│ 📉 Total Expenses:   ${totalExpenses,12:F2} │");
            Console.ResetColor();
            Console.WriteLine($"│    ({expenseCount} transactions)");

            Console.WriteLine("├─────────────────────────────────────┤");

            if (balance >= 0)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"│ 💰 Net Balance:      ${balance,12:F2} │");
                Console.ResetColor();
                Console.WriteLine("│ 🎉 You're in the positive!         │");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"│ ⚠️  Net Balance:      ${balance,12:F2} │");
                Console.ResetColor();
                Console.WriteLine("│ 📢 Consider reducing expenses       │");
            }

            Console.WriteLine("└─────────────────────────────────────┘");

            // Show percentage breakdown if there are records
            if (budgetItems.Count > 0)
            {
                Console.WriteLine("\n📈 Breakdown:");
                if (totalIncome > 0)
                {
                    double expensePercentage = (double)(totalExpenses / totalIncome) * 100;
                    Console.WriteLine($"   Expenses are {expensePercentage:F1}% of your income");
                }
            }
        }

        /// <summary>
        /// Loads budget data from JSON file
        /// </summary>
        private static void LoadData()
        {
            try
            {
                if (File.Exists(DataFile))
                {
                    string jsonData = File.ReadAllText(DataFile);
                    if (!string.IsNullOrEmpty(jsonData))
                    {
                        budgetItems = JsonSerializer.Deserialize<List<BudgetItem>>(jsonData) ?? new List<BudgetItem>();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"⚠️  Warning: Could not load existing data. Starting fresh. ({ex.Message})");
                budgetItems = new List<BudgetItem>();
            }
        }

        /// <summary>
        /// Saves budget data to JSON file
        /// </summary>
        private static void SaveData()
        {
            try
            {
                var options = new JsonSerializerOptions
                {
                    WriteIndented = true
                };
                string jsonData = JsonSerializer.Serialize(budgetItems, options);
                File.WriteAllText(DataFile, jsonData);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Error saving data: {ex.Message}");
            }
        }
    }
}