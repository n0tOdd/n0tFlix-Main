using n0tFlix.Addons.VideoExtractor.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace n0tFlix.Addons.VideoExtractor.Nettwork
{
    public class SimpleConsoleHTTPLogger : IHTTPLogger
    {
        private const string OnRequestFormat = "\n{0} {1} [{2}] {3}";

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1303")]
        public void OnRequest(IRequest request)
        {
            Utilities.Utils.ArgumentNotNull(request, nameof(request));

            string? parameters = null;
            if (request.Parameters != null)
            {
                parameters = string.Join(",", request.Parameters?.Select(kv => kv.Key + "=" + kv.Value).ToArray());
            }

            Console.WriteLine(OnRequestFormat, request.Method, request.Endpoint, parameters, request.Body);
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Globalization", "CA1303")]
        public void OnResponse(IResponse response)
        {
            Utilities.Utils.ArgumentNotNull(response, nameof(response));
            string? body = response.Body?.ToString().Replace("\n", "", StringComparison.InvariantCulture);

            body = body?.Substring(0, Math.Min(50, body?.Length ?? 0));
            Console.WriteLine("--> {0} {1} {2}\n", response.StatusCode, response.ContentType, body);
        }
    }
}