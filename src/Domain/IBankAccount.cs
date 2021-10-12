using System;
using System.Collections.Generic;

namespace Domain
{
    public interface IBankAccount
    {
        AccountNumber AccountNumber { get; }
        int AmountOfTransactions { get; }
        Money Balance { get; }

        void Deposit(Money amount);
        bool Equals(object obj);
        int GetHashCode();
        IEnumerable<Transaction> GetTransactions(DateTime? from, DateTime? till);
        string ToString();
        void Withdraw(Money amount);
    }
}