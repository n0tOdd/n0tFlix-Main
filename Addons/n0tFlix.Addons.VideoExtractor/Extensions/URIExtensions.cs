using n0tFlix.Addons.VideoExtractor.Utilities;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Web;

namespace n0tFlix.Addons.VideoExtractor.Extensions
{
    public static class URIExtensions
    {
        public static Uri ApplyParameters(this Uri uri, IDictionary<string, string> parameters)
        {
            Utils.ArgumentNotNull(uri, nameof(uri));

            if (parameters == null || !parameters.Any())
            {
                return uri;
            }

            var newParameters = new Dictionary<string, string>();
            NameValueCollection existingParameters = HttpUtility.ParseQueryString(uri.Query);
            foreach (string key in existingParameters.AllKeys)
            {
                newParameters.Add(key, existingParameters[key]);
            }
            foreach (KeyValuePair<string, string> parameter in parameters)
            {
                newParameters.Add(parameter.Key, HttpUtility.UrlEncode(parameter.Value));
            }

            var queryString = string.Join("&", newParameters.Select((parameter) => $"{parameter.Key}={parameter.Value}"));
            var query = string.IsNullOrEmpty(queryString) ? null : queryString;

            var uriBuilder = new UriBuilder(uri)
            {
                Query = query
            };

            return uriBuilder.Uri;
        }
    }
}