using Ardalis.GuardClauses;
using System.Collections.Generic;

namespace Domain
{
    public class BankAccount
    {
        public AccountNumber AccountNumber { get;}
        public Money Balance { get; private set; }

        private ICollection<Transaction> _transactions = new List<Transaction>();

        public BankAccount(AccountNumber accountNumber)
        {
            AccountNumber = accountNumber;
        }

        public void Deposit(Money amount)
        {
            Balance = new Money(Balance.Value + amount.Value);
        }

        public void Withdraw(Money amount)
        {
            Balance = new Money(Balance.Value - amount.Value);
        }
    }
}

