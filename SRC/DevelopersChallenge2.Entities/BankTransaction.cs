using DevelopersChallenge2.Entities.Enums;
using System;
using System.Collections.Generic;

namespace DevelopersChallenge2.Entities
{
    public class BankTransaction
    {
        public TypeTransaction Type { get; set; }
        public DateTime Posted { get; set; }
        public decimal Amount { get; set; }
        public string Memo { get; set; }
    }

    sealed class BankTransactionEqualityComparer : IEqualityComparer<BankTransaction>
    {
        public bool Equals(BankTransaction x, BankTransaction y)
        {
            if (x == null && y == null)
                return true;
            else if (x == null || y == null)
                return false;
            else 
                return x.Amount == y.Amount &&
                    x.Memo == y.Memo &&
                    x.Posted == y.Posted &&
                    x.Type == y.Type;
        }

        public int GetHashCode(BankTransaction obj)
        {
            return obj.Amount.GetHashCode() ^ obj.Memo.GetHashCode() ^ obj.Posted.GetHashCode() ^ obj.Type.GetHashCode();
        }
    }
}
