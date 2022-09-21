using System;
using System.Collections.Generic;
using Xunit;

namespace Domain.Tests;

public class BankAccountTest
{
    #region Fields
    private readonly BankAccount _bankAccount;
    private readonly string _accountNumber = "123-4567890-02";
    private readonly DateTime _yesterday = DateTime.Today.AddDays(-1);
    private readonly DateTime _tomorrow = DateTime.Today.AddDays(1);
    #endregion

    #region Set up
    public BankAccountTest()
    {
        _bankAccount = new BankAccount(_accountNumber);
    }
    #endregion

    #region Constructor tests
    [Fact]
    public void NewAccount_BalanceZero()
    {
        Assert.Equal(0, _bankAccount.Balance);
    }

    [Fact]
    public void NewAccount_HasZeroTransactions()
    {
        Assert.Equal(0, _bankAccount.NumberOfTransactions);
    }

    [Fact]
    public void NewAccount_SetsAccountsNumber()
    {
        Assert.Equal(_accountNumber, _bankAccount.AccountNumber);
    }

    [Fact]
    public void NewAccount_EmptyString_Fails()
    {
        Assert.Throws<ArgumentException>(() => new BankAccount(""));
    }

    [Fact]
    public void NewAccount_Null_Fails()
    {
        Assert.Throws<ArgumentNullException>(() => new BankAccount(null));
    }

    [Fact]
    public void NewAccount_TooLong_Fails()
    {
        Assert.Throws<ArgumentException>(() => new BankAccount("133-4567890-0333"));
    }

    [Fact]
    public void NewAccount_WrongFormat_Fails()
    {
        Assert.Throws<ArgumentException>(() => new BankAccount("063-1547563@60"));
    }

    [Fact]
    public void NewAccount_NoDivisionBy97_Fails()
    {
        Assert.Throws<ArgumentException>(() => new BankAccount("133-4567890-03"));
    }
    #endregion

    #region Deposit
    [Fact]
    public void Deposit_Amount_AddsTransaction()
    {
        _bankAccount.Deposit(100);
        Assert.Equal(1, _bankAccount.NumberOfTransactions);
        //Test of de toegevoegde transactie de juiste gegevens bevat
        Transaction t = new List<Transaction>(_bankAccount.GetTransactions(DateTime.Today, DateTime.Today))[0];
        Assert.Equal(100, t.Amount);
        Assert.Equal(TransactionType.Deposit, t.TransactionType);
    }

    [Theory]
    [InlineData(200)]
    [InlineData(500)]
    public void Deposit_AmountBiggerThanZero_ChangesBalance(decimal depositAmount)
    {
        _bankAccount.Deposit(depositAmount);
        Assert.Equal(depositAmount, _bankAccount.Balance);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-100)]
    public void Deposit_NegativeOrZeroAmount_Fails(decimal amount)
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => _bankAccount.Deposit(amount));
    }
    #endregion

    #region Withdraw
    [Fact]
    public void WithDraw_Amount_AddsTransaction()
    {
        _bankAccount.Withdraw(100);
        Assert.Equal(1, _bankAccount.NumberOfTransactions);
        //Test of de toegevoegde transactie de juiste gegevens bevat
        Transaction t = new List<Transaction>(_bankAccount.GetTransactions(DateTime.Today, DateTime.Today))[0];
        Assert.Equal(100, t.Amount);
        Assert.Equal(TransactionType.Withdraw, t.TransactionType);
    }

    [Theory]
    [InlineData(200, 110, 90)]
    [InlineData(200, 300, -100)]
    public void Withdraw_AmountBiggerThanZero_ChangesBalance(decimal depositAmount, decimal withdrawAmount, decimal expected)
    {
        _bankAccount.Deposit(depositAmount);
        _bankAccount.Withdraw(withdrawAmount);
        Assert.Equal(expected, _bankAccount.Balance);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-100)]
    public void Withdraw_NegativeOrZeroAmount_Fails(decimal amount)
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => _bankAccount.Withdraw(amount));
    }
    #endregion

    #region Equals
    [Fact]
    public void Equals_SameAccountNumber_ReturnsTrue()
    {
        BankAccount bankAccount2 = new BankAccount(_bankAccount.AccountNumber);
        Assert.True(_bankAccount.Equals(bankAccount2));
    }

    [Fact]
    public void Equals_DifferentAccountNumber_ReturnsFalse()
    {
        BankAccount bankAccount2 = new BankAccount("123-4567891-03");
        Assert.False(_bankAccount.Equals(bankAccount2));
    }
    #endregion

    #region GetTransactions
    [Fact]
    public void GetTransactions_All_ReturnsAllTransactions()
    {
        _bankAccount.Deposit(100);
        _bankAccount.Deposit(100);
        List<Transaction> tt = new List<Transaction>(_bankAccount.GetTransactions(null, null));
        Assert.Equal(2, tt.Count);
    }

    public static IEnumerable<object[]> TestData
    {
        get
        {
            DateTime yesterday = DateTime.Today.AddDays(-1);
            DateTime tomorrow = DateTime.Today.AddDays(1);

            yield return new object[] { null, null, 2 }; // All
            yield return new object[] { yesterday, tomorrow, 2 }; // Within a period that has transactions
            yield return new object[] { yesterday, yesterday, 0 }; // Within a period that has no transactions
            yield return new object[] { null, tomorrow, 2 }; // Before a date with transactions
            yield return new object[] { null, yesterday, 0 }; // Before a date without transactions
            yield return new object[] { yesterday, null, 2 }; // After a date with transactions
            yield return new object[] { tomorrow, null, 0 }; // After a date without transactions
        }
    }

    [Theory]
    [MemberData(nameof(TestData))]
    public void GetTransactions_ReturnsTransactions(DateTime? from, DateTime? till, int expected)
    {
        _bankAccount.Deposit(100);
        _bankAccount.Deposit(100);
        List<Transaction> tt = new List<Transaction>(_bankAccount.GetTransactions(from, till));
        Assert.Equal(expected, tt.Count);
    }
    #endregion
}
