using System;

namespace Domain;

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
        if (amount <= 0)
            throw new ArgumentOutOfRangeException(nameof(amount), "Amount must be greater than zero");

        DateOfTrans = DateTime.Today;
        Amount = amount;
        TransactionType = type;
    }
    #endregion

    #region Methods
    public override string ToString()
    {
        return $"Transaction: {DateOfTrans} - {Amount} - {TransactionType}";
    }
    #endregion
}
