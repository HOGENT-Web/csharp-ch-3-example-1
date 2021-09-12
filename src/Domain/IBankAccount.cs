using System;
using System.Collections.Generic;

namespace Domain
{
    public interface IBankAccount
    {
        #region Properties
        public string AccountNumber { get; }
        public decimal Balance { get; }
        public int NumberOfTransactions { get; }
        #endregion

        #region Methods
        void Deposit(decimal amount);
        void Withdraw(decimal amount);
        IEnumerable<Transaction> GetTransactions(DateTime? from, DateTime? till);
        #endregion
    }
}
