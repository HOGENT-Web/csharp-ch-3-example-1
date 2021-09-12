using System;

namespace Domain
{
    public class Transaction
    {
        #region Properties
        public decimal Amount { get; }
        public DateTime DateOfTrans { get; }
        public bool IsDeposit => TransactionType == TransactionType.Deposit;
        public bool IsWithdraw => TransactionType == TransactionType.Withdraw;
        public TransactionType TransactionType { get; }
        #endregion

        #region Constructors
        public Transaction(decimal amount, TransactionType type)
        {
            Amount = amount;
            TransactionType = type;
        }
        #endregion
    }
}
