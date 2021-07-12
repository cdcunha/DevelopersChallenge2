using System;
using System.Text.RegularExpressions;

namespace DevelopersChallenge2.Helper
{
    public static class DateTimeHelper
    {
        public static DateTime ConvertDtPostedToDateTime(string text)
        {
            DateTime result = new ();

            Regex regex = new(@"(\d{4})(\d{2})(\d{2})(\d{2})(\d{2})(\d{2})");
            MatchCollection matches = regex.Matches(text);

            foreach (Match match in matches)
            {
                if (match.Success)
                {
                    GroupCollection groups = match.Groups;
                    if (groups != null && groups.Count == 7)
                    {
                        string aux = $"{groups[1]}/{groups[2]}/{groups[3]} {groups[4]}:{groups[5]}:{groups[6]}";
                        result = Convert.ToDateTime(aux);
                    }
                }
            }

            return result;
        }
    }
}
