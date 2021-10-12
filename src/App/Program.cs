using Domain;
using System;

var an = new AccountNumber("123-4567890-02");
var ba = new BankAccount(an);
ba.Deposit(new Money(100));
Console.WriteLine(ba);


var sa = new SavingAccount(an, 0.05M);
sa.Deposit(new Money(100));
sa.Withdraw(new Money(50));

Console.WriteLine(sa.Balance);
