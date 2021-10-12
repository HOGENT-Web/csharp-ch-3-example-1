using Ardalis.GuardClauses;

namespace Domain
{
    public class SavingAccount : BankAccount
    {
        protected const decimal WithdrawCost = 0.25M;
        public decimal InterestRate { get; }
        public SavingAccount(AccountNumber accountNumber, decimal interestRate)
            :base(accountNumber)
        {
            InterestRate = Guard.Against.Negative(interestRate,nameof(interestRate));
        }

        public void AddInterest()
        {
            Deposit(new Money(Balance.Value * InterestRate));
        }

        public override void Withdraw(Money amount)
        {
            base.Withdraw(new Money(amount.Value + WithdrawCost));
        }
    }
}

