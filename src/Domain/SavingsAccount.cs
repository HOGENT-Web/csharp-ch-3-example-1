using System;

namespace Domain;

public class SavingsAccount : BankAccount
{
    #region
    protected const decimal WithdrawCost = 0.25M;
    #endregion

    #region Properties
    public decimal InterestRate { get; } 
    #endregion

    #region Constructors
    public SavingsAccount(string accountNumber, decimal intrestRate): base(accountNumber)
    {
        InterestRate = intrestRate;
    }
    #endregion

    #region Methods
    public void AddInterest()
    {
        Deposit(Balance * InterestRate);
    }

    public override void Withdraw(decimal amount)
    {
        if (Balance - amount - WithdrawCost < 0)
            throw new InvalidOperationException("Not sufficient funds for this withdrawal");

        base.Withdraw(amount);
        base.Withdraw(WithdrawCost);
    }
    #endregion
}
