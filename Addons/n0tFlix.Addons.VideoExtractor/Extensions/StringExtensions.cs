using System;
using System.Collections.Generic;
using System.Text;

namespace n0tFlix.Addons.VideoExtractor.Extensions
{
    public static class StringExtensions
    {
        public static int TryToInt(this string text)
        {
            if (int.TryParse(text, out int i))
            {
                return i;
            }

            return -1;
        }

        public static long TryToLong(this string text)
        {
            if (long.TryParse(text, out long i))
            {
                return i;
            }
            return -1;
        }

        //public static string GetStringBetween_(this string text, string start, string end)
        //{
        //    var id1 = text.IndexOf(start, StringComparison.Ordinal);//0
        //    var id2 = text.Substring(id1).IndexOf(end, StringComparison.Ordinal)+id1;//12
        //    //lazim olan//sub()
        //    var indexStart = id1 + start.Length;//5
        //    var length = id2   - indexStart;//8
        //    return text.Substring(indexStart, length);
        //}
        public static string GetStringBetween(this string text, string start, string end)
        {
            var id1 = text.IndexOf(start, StringComparison.Ordinal);//0
            var txt = text.Substring(id1 + start.Length);
            var id2 = txt.IndexOf(end, StringComparison.Ordinal);//12
            txt = txt.Substring(0, id2);
            return txt;
        }

        public static string FromThisToEnd(this string text, string start)
        {
            var id1 = text.IndexOf(start, StringComparison.Ordinal);//0
            var indexStart = id1 + start.Length;//5
            return text.Substring(indexStart);
        }

        public static string RemoveSubString(this string text, string removeString)
        {
            var result = text;
            while (result.Contains(removeString))
            {
                result = result.Replace(removeString, string.Empty);
            }

            return result;
        }
    }
}