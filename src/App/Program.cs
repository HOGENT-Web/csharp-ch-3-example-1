using Domain;
using System;

var an = new AccountNumber("123-4567890-02");
var ba = new BankAccount(an);
Console.WriteLine(ba.AccountNumber);
