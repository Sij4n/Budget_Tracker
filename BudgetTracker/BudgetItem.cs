using System;
using System.Text.Json.Serialization;

namespace BudgetTracker
{
    /// <summary>
    /// Enumeration for budget item types
    /// </summary>
    public enum BudgetType
    {
        Income,
        Expense
    }

    /// <summary>
    /// Represents a single budget item (income or expense)
    /// </summary>
    public class BudgetItem
    {
        /// <summary>
        /// Gets or sets the type of budget item (Income or Expense)
        /// </summary>
        public BudgetType Type { get; set; }

        /// <summary>
        /// Gets or sets the description of the budget item
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the monetary amount
        /// </summary>
        public decimal Amount { get; set; }

        /// <summary>
        /// Gets or sets the date when this budget item occurred
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Gets the unique identifier for this budget item
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Default constructor for JSON serialization
        /// </summary>
        public BudgetItem()
        {
            Id = Guid.NewGuid();
            Date = DateTime.Today;
            Description = string.Empty;
        }

        /// <summary>
        /// Creates a new budget item with specified parameters
        /// </summary>
        /// <param name="type">The type of budget item (Income or Expense)</param>
        /// <param name="description">Description of the transaction</param>
        /// <param name="amount">The monetary amount</param>
        /// <param name="date">The date of the transaction</param>
        public BudgetItem(BudgetType type, string description, decimal amount, DateTime date)
        {
            Id = Guid.NewGuid();
            Type = type;
            Description = description ?? throw new ArgumentNullException(nameof(description));
            Amount = amount > 0 ? amount : throw new ArgumentException("Amount must be greater than zero", nameof(amount));
            Date = date;
        }

        /// <summary>
        /// Creates a new budget item with today's date
        /// </summary>
        /// <param name="type">The type of budget item (Income or Expense)</param>
        /// <param name="description">Description of the transaction</param>
        /// <param name="amount">The monetary amount</param>
        public BudgetItem(BudgetType type, string description, decimal amount)
            : this(type, description, amount, DateTime.Today)
        {
        }

        /// <summary>
        /// Returns a string representation of the budget item
        /// </summary>
        /// <returns>Formatted string with item details</returns>
        public override string ToString()
        {
            string typeIcon = Type == BudgetType.Income ? "📈" : "📉";
            return $"{Date:MM/dd/yyyy} - {typeIcon} {Type}: ${Amount:F2} ({Description})";
        }

        /// <summary>
        /// Determines whether two budget items are equal based on their properties
        /// </summary>
        /// <param name="obj">The object to compare with</param>
        /// <returns>True if equal, false otherwise</returns>
        public override bool Equals(object? obj)
        {
            if (obj is BudgetItem other)
            {
                return Id == other.Id;
            }
            return false;
        }

        /// <summary>
        /// Gets the hash code for this budget item
        /// </summary>
        /// <returns>Hash code based on the ID</returns>
        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        /// <summary>
        /// Validates if the budget item has valid data
        /// </summary>
        /// <returns>True if valid, false otherwise</returns>
        public bool IsValid()
        {
            return !string.IsNullOrWhiteSpace(Description) &&
                   Amount > 0 &&
                   Date != default(DateTime);
        }

        /// <summary>
        /// Gets a formatted amount string with appropriate color coding hints
        /// </summary>
        /// <returns>Formatted amount string</returns>
        public string GetFormattedAmount()
        {
            string symbol = Type == BudgetType.Income ? "+" : "-";
            return $"{symbol}${Amount:F2}";
        }
    }
}