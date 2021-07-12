using DevelopersChallenge2.Entities.Enums;
using System;
using System.Collections.Generic;

namespace DevelopersChallenge2.Entities
{
    /// <summary>
    /// Bank Transaction
    /// </summary>
    public class BankTransaction
    {
        /// <summary>
        /// Transaction's Type: Debit or Credit are the valid values
        /// </summary>
        public TypeTransaction Type { get; set; }

        /// <summary>
        /// Transaction's Posted Date
        /// </summary>
        public DateTime Posted { get; set; }

        /// <summary>
        /// Transaction's Amount
        /// </summary>
        public decimal Amount { get; set; }

        /// <summary>
        /// Transaction's Memo
        /// </summary>
        public string Memo { get; set; }
    }

    /// <summary>
    /// Class responsable to make the comparations between two BankTransaction objects
    /// </summary>
    public class BankTransactionEqualityComparer : IEqualityComparer<BankTransaction>
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
