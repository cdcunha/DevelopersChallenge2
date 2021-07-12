namespace DevelopersChallenge2.Entities.Enums
{
    public enum TypeTransaction 
    { 
        Invalid,  
        Debit, 
        Credit 
    }

    public static class StringToTypeTransaction
    {
        const string _debitValue = "DEBIT";
        const string _creditValue = "CREDIT";

        /// <summary>
        /// If the value of the field TRNTYPE is: DEBIT, will be returned the enum Debit
        /// If the value of the field TRNTYPE is: CREDIT, will be returned the enum Credit
        /// If don't find the value DEBIT or CREDIT of the field TRNTYPE, will be returned the enum Invalid
        /// </summary>
        /// <param name="text">Value of the field TRNTYPE</param>
        /// <returns></returns>
        public static TypeTransaction ConvertStringToType(string text)
        {
            switch (text)
            { 
                case _debitValue: return TypeTransaction.Debit;
                case _creditValue: return TypeTransaction.Credit;
                default: return TypeTransaction.Invalid;
            }
        }
    }
}
