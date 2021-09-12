using System;
using System.Collections.Generic;

namespace Domain
{
    public class BankAccount : IBankAccount
    {
        #region Fields
        private readonly IList<Transaction> _transactions;
        #endregion

        #region Properties
        public string AccountNumber { get; }
        public decimal Balance { get; private set; }
        public int NumberOfTransactions => _transactions.Count;
        #endregion

        #region Constructors
        public BankAccount(string accountNumber)
        {
            _transactions = new List<Transaction>();
            AccountNumber = accountNumber;
            Balance = decimal.Zero;
        }
        #endregion

        #region Methods
        public void Deposit(decimal amount)
        {
            _transactions.Add(new Transaction(amount, TransactionType.Deposit));
            Balance += amount;
        }

        public virtual void Withdraw(decimal amount)
        {
            _transactions.Add(new Transaction(amount, TransactionType.Withdraw));
            Balance -= amount;
        }

        public IEnumerable<Transaction> GetTransactions(DateTime? from, DateTime? till)
        {
            if (from == null && till == null) return _transactions;
            if (from == null) from = DateTime.MinValue;
            if (till == null) till = DateTime.MaxValue;

            // This code can be written more concise with LINQ (see chapter 4)
            IList<Transaction> filtered = new List<Transaction>();

            foreach (var transaction in _transactions)
                if (transaction.DateOfTrans >= from && transaction.DateOfTrans <= till)
                    filtered.Add(transaction);

            return filtered;
        }

        public override string ToString()
        {
            return $"{AccountNumber} - {Balance:F2}";
        }

        public override int GetHashCode()
        {
            return AccountNumber?.GetHashCode() ?? 0;
        }

        public override bool Equals(object obj)
        {
            if (obj == null || !(obj is BankAccount))
                return false;

            BankAccount account = (BankAccount) obj;
            return AccountNumber == account.AccountNumber;
        }
        #endregion
    }
}
