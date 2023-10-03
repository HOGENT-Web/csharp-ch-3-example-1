using Domain;
using System;
using System.Collections.Generic;

BankAccount account = new BankAccount("123-4567890-02");
Console.WriteLine($"AccountNumber: {account.AccountNumber} ");
Console.WriteLine($"Balance: {account.Balance} ");
account.Deposit(200M);
Console.WriteLine($"Balance after deposit of 200 euros: {account.Balance} ");
account.Withdraw(100);
Console.WriteLine($"Balance after withdraw of 100 euros: {account.Balance} ");
Console.WriteLine($"Number of transactions: {account.NumberOfTransactions}");
IEnumerable<Transaction> transactions = account.GetTransactions(null, null);


foreach (Transaction t in transactions)
{
    Console.WriteLine(t);
}

BankAccount savingsAccount = new SavingsAccount("123-4567890-02", 0.05M);
Console.WriteLine($"SavingsAccount : {savingsAccount}");
savingsAccount.Deposit(200M);
savingsAccount.Withdraw(100M);
Console.WriteLine($"Balance savingsaccount: {savingsAccount.Balance} ");
(savingsAccount as SavingsAccount).AddInterest();
Console.WriteLine($"Balance savingsaccount after interest: {savingsAccount.Balance} ");
Console.WriteLine(savingsAccount); // implicit call to ToString()
