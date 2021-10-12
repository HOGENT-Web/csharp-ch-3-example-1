using System;
using System.Collections.Generic;
using System.Linq;

namespace Domain
{

    public class BankAccount
    {
        private ICollection<Transaction> _transactions = new List<Transaction>();
        
        public AccountNumber AccountNumber { get;}
        public Money Balance { get; private set; }

        public int AmountOfTransactions => _transactions.Count;

        public BankAccount(AccountNumber accountNumber)
        {
            AccountNumber = accountNumber;
            Balance = new Money(0);
        }

        public void Deposit(Money amount)
        {
            Balance = new Money(Balance.Value + amount.Value);
            _transactions.Add(new Transaction(amount, TransactionType.Deposit));

        }

        public virtual void Withdraw(Money amount)
        {
            Balance = new Money(Balance.Value - amount.Value);
            _transactions.Add(new Transaction(amount, TransactionType.Withdraw));
        }

        public IEnumerable<Transaction> GetTransactions(DateTime? from, DateTime? till)
        {
            if (from is null && till is null)
                return _transactions;

            if (from is null)
                from = DateTime.MinValue;

            if (till is null)
                till = DateTime.MaxValue;

            return _transactions.Where(x => x.DateOfTrans >= from && x.DateOfTrans <= till);
        }

        public override string ToString() => $"{AccountNumber} - {Balance}";

        public override bool Equals(object obj)
        {
            if (obj is null)
                return false;

            if (obj.GetType() != this.GetType())
                return false;

            var other = (BankAccount)obj;

            return other.AccountNumber == AccountNumber;
        }

        public override int GetHashCode()
        {
            return AccountNumber.GetHashCode();
        }
    }
}

