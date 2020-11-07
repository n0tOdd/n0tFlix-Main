using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace n0tFlix.Helpers.Common.Json
{
    public static class JsonExtractors
    {
        /// <summary>
        /// Checks for json parts in the source strings and returns all matches in a list to be sortet later
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="Source"></param>
        /// <returns></returns>
        public static async Task<List<string>> GetJsonWithRegex(string Source)
        {
            Regex regex = new Regex(@"\{(.|\s)*\}", RegexOptions.Compiled, TimeSpan.FromSeconds(30));
            if (!regex.IsMatch(Source))
            {
                return default;
            }
            MatchCollection matchCollection = regex.Matches(Source);
            List<string> Matches = new List<string>();
            foreach (Match match in matchCollection)
            {
                if (match.Success)
                {
                    Matches.Add(match.Value);
                }
            }
            return Matches;
        }

        /// <summary>
        /// Checks for json objects in the original string and tries to return a Deserialized object for us
        /// </summary>
        /// <param name="mixedString"></param>
        /// <returns></returns>
        public static object ExtractJsonObject(string mixedString)
        {
            for (var i = mixedString.IndexOf('{'); i > -1; i = mixedString.IndexOf('{', i + 1))
            {
                for (var j = mixedString.LastIndexOf('}'); j > -1; j = mixedString.LastIndexOf("}", j - 1))
                {
                    var jsonProbe = mixedString.Substring(i, j - i + 1);
                    try
                    {
                        return JsonConvert.DeserializeObject(jsonProbe);
                    }
                    catch
                    {
                    }
                }
            }
            return null;
        }
    }
}