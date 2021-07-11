using System;
using System.Text.RegularExpressions;

namespace DevelopersChallenge2.Helper
{
    public class OfxHelper
    {
        public void OfxToDataSet(string ofxData)
        {
            Regex regex = new(@"<STMTTRN>[<\w\s\>\.\-\[\]\:]*<\/STMTTRN>");
            MatchCollection matches = regex.Matches(ofxData);

            foreach (Match match in matches)
            {
                GroupCollection groups = match.Groups;
                Console.WriteLine("'{0}' repeated at positions {1} and {2}",
                                  groups["word"].Value,
                                  groups[0].Index,
                                  groups[1].Index);
            }
        }
    }
}
