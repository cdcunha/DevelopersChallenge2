using System;
using System.Text.RegularExpressions;

namespace DevelopersChallenge2.Helper
{
    public static class DateTimeHelper
    {
        /// <summary>
        /// Convert the text from the field DTPOSTED to DateTime
        /// </summary>
        /// <param name="text">Value of the field DTPOSTED</param>
        /// <returns></returns>
        public static DateTime ConvertDtPostedToDateTime(string text)
        {
            DateTime result = new ();

            //Regular expression to get the datetime fields in text
            Regex regex = new(@"(\d{4})(\d{2})(\d{2})(\d{2})(\d{2})(\d{2})");
            MatchCollection matches = regex.Matches(text);

            foreach (Match match in matches)
            {
                if (match.Success)
                {
                    GroupCollection groups = match.Groups;
                    if (groups != null && groups.Count == 7)
                    {
                        //Get the 6 groups with the datetime fields values to concatenate in datetime format and convert
                        string aux = $"{groups[1]}/{groups[2]}/{groups[3]} {groups[4]}:{groups[5]}:{groups[6]}";
                        result = Convert.ToDateTime(aux);
                    }
                }
            }

            return result;
        }
    }
}
