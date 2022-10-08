using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Domain;

public class BankAccount : IBankAccount
{
    #region Fields
    private string _accountNumber;
    private readonly List<Transaction> _transactions = new();
    #endregion

    #region Properties
    public string AccountNumber {
        get
        {
            return _accountNumber;
        }
        private set
        {
            Regex regex = new Regex(@"^(?<bankcode>\d{3})-(?<rekeningnr>\d{7})-(?<checksum>\d{2})$");
            Match match = regex.Match(value);
            if (!match.Success)
                throw new ArgumentException("Bankaccount number format is not correct", nameof(AccountNumber));
            int getal = int.Parse(match.Groups["bankcode"].Value + match.Groups["rekeningnr"].Value);
            int checksum = int.Parse(match.Groups["checksum"].Value);
            if (getal % 97 != checksum)
                throw new ArgumentException("97 test of the bankaccount number failed", nameof(AccountNumber));
            _accountNumber = value;
        }
    }
    public decimal Balance { get; private set; }
    public int NumberOfTransactions => _transactions.Count;
    public event Action<string, Transaction> OnTransactionAdded;
    #endregion

    #region Constructors
    public BankAccount(string accountNumber)
    {
        AccountNumber = accountNumber;
        Balance = decimal.Zero;
    }
    #endregion

    #region Methods
    private void AddTransaction(Transaction transaction)
    {
        _transactions.Add(transaction);
        OnTransactionAdded?.Invoke(this.AccountNumber, transaction);
    }
    public void Deposit(decimal amount)
    {
        AddTransaction(new Transaction(amount, TransactionType.Deposit));
        Balance += amount;
    }

    public virtual void Withdraw(decimal amount)
    {
        AddTransaction(new Transaction(amount, TransactionType.Withdraw));
        Balance -= amount;
    }

    public IEnumerable<Transaction> GetTransactions(DateTime? from, DateTime? till)
    {
        if (from == null && till == null) return _transactions;
        if (from == null) from = DateTime.MinValue;
        if (till == null) till = DateTime.MaxValue;

        // This code can be written more concise with LINQ (see chapter 4)
        IList<Transaction> filtered = new List<Transaction>();

        foreach (var transaction in _transactions)
            if (transaction.DateOfTrans >= from && transaction.DateOfTrans <= till)
                filtered.Add(transaction);

        return filtered;
    }

    public override string ToString()
    {
        return $"{AccountNumber} - {Balance:F2}";
    }

    public override int GetHashCode()
    {
        return AccountNumber?.GetHashCode() ?? 0;
    }

    public override bool Equals(object obj)
    {
        if (obj == null || !(obj is BankAccount))
            return false;

        BankAccount account = (BankAccount) obj;
        return AccountNumber == account.AccountNumber;
    }
    #endregion
}
