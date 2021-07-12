namespace DevelopersChallenge2.Entities.Enums
{
    public enum TypeTransaction { Unknown, Debit, Credit }

    public static class StringToTypeTransaction
    {
        const string _debitValue = "DEBIT";
        const string _creditValue = "CREDIT";

        public static TypeTransaction ConvertStringToType(string text)
        {
            switch (text)
            {
                case _debitValue: return TypeTransaction.Debit;
                case _creditValue: return TypeTransaction.Credit;
                default: return TypeTransaction.Unknown;
            }
        }
    }
}
