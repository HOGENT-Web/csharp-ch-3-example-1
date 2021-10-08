using Ardalis.GuardClauses;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Domain
{
    public class AccountNumber : ValueObject
    {
        public string Number { get; }
        private static readonly Regex _pattern = new(@"^(?<bankcode>\d{3})-(?<rekeningnr>\d{7})-(?<checksum>\d{2})$");

        public AccountNumber(string number)
        {
            if (!IsValidMod97(number))
                throw new ArgumentException("97 test of the bankaccount number failed", nameof(AccountNumber));
            
            Number = number;
        }

        public override string ToString()
        {
            return Number;
        }

        private bool IsValidMod97(string number)
        {
            Match match = _pattern.Match(number);
            if (!match.Success)
                throw new ArgumentException("Invalid Format");

            int getal = int.Parse(match.Groups["bankcode"].Value + match.Groups["rekeningnr"].Value);
            int checksum = int.Parse(match.Groups["checksum"].Value);
            return getal % 97 == checksum;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Number;
        }
    }
}
