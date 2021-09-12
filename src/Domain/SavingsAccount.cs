namespace Domain
{
    public class SavingsAccount : BankAccount
    {
        #region
        protected const decimal WithdrawCost = 0.25M;
        #endregion

        #region Properties
        public decimal InterestRate { get; } 
        #endregion

        #region Constructors
        public SavingsAccount(string accountNumber, decimal intrestRate):
            base(accountNumber)
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
            Withdraw(amount);
            Withdraw(WithdrawCost);
        }
        #endregion
    }
}
