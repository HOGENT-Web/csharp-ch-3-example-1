using Ardalis.GuardClauses;
using System;
using System.Collections.Generic;

namespace Domain
{
    public class Money : ValueObject
    {
        public decimal Value { get; }
        public Money(decimal value)
        {
            Value = Guard.Against.Negative(value, nameof(value));
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Math.Round(Value, 2);
        }

        public override string ToString() => $"{Value:C}";
    }
}
