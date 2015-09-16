using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Dev4s.WebClient
{
    public static class Filters
    {
        //string regex
        private const string RegexUrlAddress = "(?:<h2><a href=')(?<Address>.*)(?:'>)(?<Name>.*)(?:</a></h2>)";
        private const string RegexNoShops = "<h4>Nie znaleziono żadnych sklepów!</h4>";

        public static bool ShopsExists(string text)
        {
            return !new Regex(RegexNoShops).IsMatch(text);
        }

        public static IEnumerable<string> GetShopUrlAddresses(string text)
        {
            var tempAddr = new HashSet<string>();
            var regexResult = new Regex(RegexUrlAddress).Matches(text);

            foreach (Match res in regexResult)
            {
                tempAddr.Add(res.Groups["Address"].Value.Replace("opinie", "sklep"));
            }

            return tempAddr;
        }

        public static ShopInfo GetShopInfo(string text)
        {
            return null;
        }
    }
}