# 💰 Budget Tracker

A simple, user-friendly console application for tracking personal income and expenses, built with C# .NET.

![Budget Tracker](https://img.shields.io/badge/.NET-6.0-512BD4?style=for-the-badge&logo=dotnet)
![Language](https://img.shields.io/badge/C%23-239120?style=for-the-badge&logo=c-sharp&logoColor=white)
![License](https://img.shields.io/badge/License-MIT-green.svg?style=for-the-badge)

## 📋 Features

- **📈 Income Tracking**: Add and categorize your income sources
- **📉 Expense Management**: Record and monitor your spending
- **💾 Data Persistence**: Automatic save/load functionality using JSON storage
- **📊 Financial Summary**: View total income, expenses, and net balance
- **📋 Detailed Records**: See all transactions in a clean, formatted table
- **🎨 User-Friendly Interface**: Intuitive console UI with color-coded feedback
- **⚡ Input Validation**: Robust error handling for all user inputs
- **📅 Date Support**: Custom dates or automatic current date assignment

## 🚀 Quick Start

### Prerequisites

- [.NET 6.0 SDK](https://dotnet.microsoft.com/download/dotnet/6.0) or later

### Installation & Running

1. **Clone the repository:**
   ```bash
   git clone https://github.com/yourusername/BudgetTracker.git
   cd BudgetTracker
   ```

2. **Run the application:**
   ```bash
   dotnet run
   ```

3. **Build the project (optional):**
   ```bash
   dotnet build
   ```

4. **Create a release build:**
   ```bash
   dotnet publish -c Release -o publish
   ```

## 🎮 Usage

When you run the application, you'll see a main menu with the following options:

```
═══════════════ MAIN MENU ═══════════════
1. 📈 Add Income
2. 📉 Add Expense
3. 📋 Show All Records
4. 📊 Show Summary
5. 🚪 Exit & Save
═════════════════════════════════════════
```

### Adding Transactions

1. **Add Income**: Select option 1, enter description, amount, and optional date
2. **Add Expense**: Select option 2, enter description, amount, and optional date

### Viewing Data

3. **Show All Records**: Displays all transactions in chronological order
4. **Show Summary**: Shows total income, expenses, balance, and spending analysis

### Data Storage

- Data is automatically saved to `budget.json` when you exit
- The file is created in the same directory as the executable
- Data loads automatically when you restart the application

## 📁 Project Structure

```
BudgetTracker/
├── Program.cs          # Main application logic and UI
├── BudgetItem.cs       # Data model and business logic
├── budget.json         # Data storage (created at runtime)
├── README.md           # Project documentation
└── BudgetTracker.csproj # Project configuration (auto-generated)
```

## 🏗️ Architecture

### Object-Oriented Design

- **`BudgetItem` Class**: Encapsulates transaction data with validation
- **`BudgetType` Enum**: Defines transaction types (Income/Expense)
- **Separation of Concerns**: UI logic separated from data management

### Key Design Patterns

- **Data Transfer Object**: `BudgetItem` serves as a clean data container
- **Repository Pattern**: JSON file acts as a simple data repository
- **Input Validation**: Comprehensive error handling throughout

## 📊 Sample Output

### Financial Summary View
```
📊 FINANCIAL SUMMARY
═══════════════════════
┌─────────────────────────────────────┐
│ 📈 Total Income:         $3,500.00 │
│    (5 transactions)                 │
│ 📉 Total Expenses:       $2,100.00 │
│    (8 transactions)                 │
├─────────────────────────────────────┤
│ 💰 Net Balance:          $1,400.00 │
│ 🎉 You're in the positive!         │
└─────────────────────────────────────┘

📈 Breakdown:
   Expenses are 60.0% of your income
```

## 🔧 Technical Details

### Technologies Used

- **C# 10.0**: Modern C# features and syntax
- **System.Text.Json**: For data serialization/deserialization
- **Console Application**: Cross-platform command-line interface

### Data Validation

- Amount validation (must be positive numbers)
- Date format validation (MM/dd/yyyy)
- Description validation (non-empty strings)
- Error handling for file I/O operations

## 🚀 Future Improvements

- [ ] **Categories**: Add expense/income categories
- [ ] **Search & Filter**: Filter by date range, type, or amount
- [ ] **Export Features**: CSV export functionality
- [ ] **Budget Goals**: Set monthly/yearly budget limits
- [ ] **Charts**: ASCII charts for visual data representation
- [ ] **Multi-Currency**: Support for different currencies
- [ ] **Recurring Transactions**: Automated recurring entries
- [ ] **Data Import**: Import from bank statements or other formats
- [ ] **Backup System**: Cloud backup integration
- [ ] **Reports**: Monthly/yearly financial reports

## 🤝 Contributing

1. Fork the project
2. Create your feature branch (`git checkout -b feature/AmazingFeature`)
3. Commit your changes (`git commit -m 'Add some AmazingFeature'`)
4. Push to the branch (`git push origin feature/AmazingFeature`)
5. Open a Pull Request

## 📜 License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

## 📞 Support

If you encounter any problems or have suggestions:

- Open an issue on GitHub
- Check existing issues for solutions
- Feel free to contribute improvements!

## 🙏 Acknowledgments

- Built with love for the developer community
- Inspired by the need for simple, effective budget tracking
- Thanks to the .NET community for excellent documentation

---

**Happy Budget Tracking! 💰**
