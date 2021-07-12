using DevelopersChallenge2.Entities;
using DevelopersChallenge2.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text.RegularExpressions;

namespace DevelopersChallenge2.Helper
{
    public static class OfxHelper
    {
        /// <summary>
        /// Get all transaction in the OFX text and creates a list of BankTransaction
        /// </summary>
        /// <param name="ofxData">Value of the OFX file</param>
        /// <returns></returns>
        public static List<BankTransaction> OfxToBankTransactions(string ofxData)
        {
            List<BankTransaction> bankTransactions = new();

            //Necessary replace the character / to \ before the regex. It'll be replace to the original in the method ConvertTextToBankTransaction
            string sb_trim = Regex.Replace(ofxData, @"(\d{2})(\/)(\d{2})", 
                m => $"{m.Groups[1].Value}{m.Groups[2].Value.Replace("/", "\\")}{m.Groups[3].Value}");

            //Pattern to find all transactions inside the TAG STMTTRN
            Regex regex = new(@"<STMTTRN>([\s\w\<\>\[\]\-\:\.\\]*)<\/STMTTRN>");
            MatchCollection matches = regex.Matches(sb_trim);

            foreach (Match match in matches)
            {
                if (match.Success)
                {
                    GroupCollection groups = match.Groups;
                    if (groups != null)
                    {
                        var bankTransaction = ConvertTextToBankTransaction(groups[1].Value);
                        if (bankTransaction != null && bankTransaction.Type != TypeTransaction.Invalid)
                        {
                            bankTransactions.Add(bankTransaction);
                        }
                    }
                }
                match.NextMatch();
            }

            return bankTransactions;
        }

        /// <summary>
        /// Convert the text from the TAG STMTTRN to an object BankTransaction
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        private static BankTransaction ConvertTextToBankTransaction(string text)
        {
            #region Fields of STMTTRN
            const string trnType = "TRNTYPE";
            const string dtPosted = "DTPOSTED";
            const string trnAmt = "TRNAMT";
            const string memo = "MEMO";
            #endregion

            BankTransaction bankTransaction = new();

            //Patter to find the Field's Name and Value
            Regex regex = new(@"<(\w*)>([\w\s\d\-\.\[\]:\\]+)\r\n");
            MatchCollection matches = regex.Matches(text);

            foreach (Match match in matches)
            {
                if (match.Success)
                {
                    GroupCollection groups = match.Groups;
                    if (groups != null && groups.Count == 3)
                    {
                        switch (groups[1].Value)
                        {
                            case trnType:
                                {
                                    bankTransaction.Type = StringToTypeTransaction.ConvertStringToType(groups[2].Value);
                                    break;
                                }
                            case dtPosted:
                                {
                                    bankTransaction.Posted = DateTimeHelper.ConvertDtPostedToDateTime(groups[2].Value);
                                    break;
                                }
                            case trnAmt:
                                {
                                    bankTransaction.Amount = Convert.ToDecimal(groups[2].Value, new System.Globalization.CultureInfo("en-US"));
                                    break;
                                }
                            case memo:
                                {
                                    //Replace to the original character /, replaced previously
                                    bankTransaction.Memo = groups[2].Value.Trim().Replace("\\", "/");
                                    break;
                                }
                        }
                    }
                }
            }

            return bankTransaction;
        }
    }
}
