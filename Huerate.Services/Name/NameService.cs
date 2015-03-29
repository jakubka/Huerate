/*
Huerate - Mobile System for Customer Feedback Collection
Master Thesis at BUT FIT (http://www.fit.vutbr.cz), 2013
Available at http://huerate.cz

Author: Bc. Jakub Kadlubiec, xkadlu00@stud.fit.vutbr.cz or jakub.kadlubiec@gmail.com
*/

using System.Globalization;
using System.Linq;
using System.Text;

namespace Huerate.Services.Names
{
    internal class NameService : INameService
    {
        public string ToCodeName(string displayName)
        {
            displayName = ReplaceDiacritics(displayName);

            return new string(displayName.Where(char.IsLetterOrDigit).Select(char.ToLower).ToArray());
        }


        private string ReplaceDiacritics(string input)
        {
            string stFormD = input.Normalize(NormalizationForm.FormD);
            StringBuilder sb = new StringBuilder();

            for (int ich = 0; ich < stFormD.Length; ich++)
            {
                UnicodeCategory uc = CharUnicodeInfo.GetUnicodeCategory(stFormD[ich]);
                if (uc != UnicodeCategory.NonSpacingMark)
                {
                    sb.Append(stFormD[ich]);
                }
            }

            return (sb.ToString().Normalize(NormalizationForm.FormC));
        }
    }
}