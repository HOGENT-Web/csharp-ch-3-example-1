using Ardalis.GuardClauses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Transaction : ValueObject
    {
        public Money Amount { get; }
        public DateTime DateOfTrans { get; }
        public bool IsDeposit => TransactionType == TransactionType.Deposit;
        public bool IsWithdraw => TransactionType == TransactionType.Withdraw;
        public TransactionType TransactionType { get; }

        public Transaction(Money amount, TransactionType type)
        {
            Amount = Guard.Against.Null(amount, nameof(amount));
            DateOfTrans = DateTime.UtcNow;
            TransactionType = type;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Amount;
            yield return DateOfTrans.Date;
            yield return TransactionType;
        }
    }
}
