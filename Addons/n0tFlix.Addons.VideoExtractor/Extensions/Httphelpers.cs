using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;

namespace n0tFlix.Addons.VideoExtractor.Extensions
{
    public static class Httphelpers
    {
        public static byte[] ReadToEnd(this Stream stream)
        {
            var buffer = new byte[16 * 1024];
            using (var ms = new MemoryStream())
            {
                int read;
                while ((read = stream.Read(buffer, 0, buffer.Length)) > 0)
                {
                    ms.Write(buffer, 0, read);
                }
                return ms.ToArray();
            }
        }

        public static string HtmlDecode(string value)
        {
#if PORTABLE
                return WebUtility.HtmlDecode(value);
#else
            return System.Web.HttpUtility.HtmlDecode(value);
#endif
        }

        public static IDictionary<string, string> ParseQueryString(string s)
        {
            // remove anything other than query string from url
            if (s.Contains("?"))
            {
                s = s.Substring(s.IndexOf('?') + 1);
            }

            return Regex.Split(s, "&").Select(vp => Regex.Split(vp, "=")).ToDictionary(strings => strings[0],
                strings => strings.Length == 2 ? UrlDecode(strings[1]) : string.Empty);
        }

        public static string ReplaceQueryStringParameter(string currentPageUrl, string paramToReplace, string newValue)
        {
            var query = ParseQueryString(currentPageUrl);

            query[paramToReplace] = newValue;

            var resultQuery = new StringBuilder();
            var isFirst = true;

            foreach (var pair in query)
            {
                if (!isFirst)
                {
                    resultQuery.Append("&");
                }
                resultQuery.Append(pair.Key);
                resultQuery.Append("=");
                resultQuery.Append(pair.Value);

                isFirst = false;
            }
            var uriBuilder = new UriBuilder(currentPageUrl)
            {
                Query = resultQuery.ToString()
            };
            return uriBuilder.ToString();
        }

        public static string UrlDecode(string url)
        {
#if PORTABLE
                return WebUtility.UrlDecode(url);
#else
            return System.Web.HttpUtility.UrlDecode(url);
#endif
        }

        private static string ReadStreamFromResponse(WebResponse response)
        {
            using (var responseStream = response.GetResponseStream())
            {
                using (var sr = new StreamReader(responseStream ?? throw new InvalidOperationException()))
                {
                    return sr.ReadToEnd();
                }
            }
        }
    }
}