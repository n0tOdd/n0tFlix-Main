using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace n0tFlix.Helpers.Common.Web
{
    public static class n0tHttpClient
    {
        private static System.Net.Http.HttpClient _HttpClient;
        private static HttpMessageHandler httpClientHandler;
        private static CookieContainer cookieContainer;

        static n0tHttpClient()
        {
            cookieContainer = new CookieContainer();

            httpClientHandler = new HttpClientHandler()
            {
                AllowAutoRedirect = true,
                CookieContainer = cookieContainer,
                AutomaticDecompression = DecompressionMethods.GZip,
                MaxAutomaticRedirections = 5,
                UseCookies = true,
            };
            _HttpClient = new System.Net.Http.HttpClient(httpClientHandler) { Timeout = TimeSpan.FromSeconds(10) };
        }

        /// <summary>
        /// Downloads the response from the url as a string
        /// uses headers and all the extra stuff to try and fake a browsaer request
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static async Task<string> GetResponseStringAsync(string url)
        {
            using (var request = new HttpRequestMessage(HttpMethod.Get, new Uri(url)))
            {
                request.Headers.TryAddWithoutValidation("Accept", "text/html,application/xhtml+xml,application/xml");
                request.Headers.TryAddWithoutValidation("Accept-Encoding", "gzip, deflate");
                request.Headers.TryAddWithoutValidation("User-Agent", "Mozilla/5.0 (Windows NT 6.2; WOW64; rv:19.0) Gecko/20100101 Firefox/19.0");
                request.Headers.TryAddWithoutValidation("Accept-Charset", "ISO-8859-1");
                Random r = new Random(DateTime.Now.Millisecond);
                request.Headers.TryAddWithoutValidation("X-Forwarded-For", r.Next(0, 255) + "." + r.Next(0, 255) + "." + r.Next(0, 255) + "." + r.Next(0, 255));

                using (var response = await _HttpClient.SendAsync(request).ConfigureAwait(false))
                {
                    response.EnsureSuccessStatusCode();
                    return await response.Content.ReadAsStringAsync();
                    using (var responseStream = await response.Content.ReadAsStreamAsync().ConfigureAwait(false))
                    using (var decompressedStream = new GZipStream(responseStream, CompressionMode.Decompress))
                    using (var streamReader = new StreamReader(decompressedStream))
                    {
                        return await streamReader.ReadToEndAsync().ConfigureAwait(false);
                    }
                }
            }
        }

        /// <summary>
        /// Downloads the response from the url as a string
        /// uses headers and all the extra stuff to try and fake a browsaer request
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static async Task<Stream> GetResponseStreamAsync(string url)
        {
            using (var request = new HttpRequestMessage(HttpMethod.Get, new Uri(url)))
            {
                request.Headers.TryAddWithoutValidation("Accept", "text/html,application/xhtml+xml,application/xml");
                request.Headers.TryAddWithoutValidation("Accept-Encoding", "gzip, deflate");
                request.Headers.TryAddWithoutValidation("User-Agent", "Mozilla/5.0 (Windows NT 6.2; WOW64; rv:19.0) Gecko/20100101 Firefox/19.0");
                request.Headers.TryAddWithoutValidation("Accept-Charset", "ISO-8859-1");
                Random r = new Random(DateTime.Now.Millisecond);
                request.Headers.TryAddWithoutValidation("X-Forwarded-For", r.Next(0, 255) + "." + r.Next(0, 255) + "." + r.Next(0, 255) + "." + r.Next(0, 255));

                using (var response = await _HttpClient.SendAsync(request).ConfigureAwait(false))
                {
                    response.EnsureSuccessStatusCode();
                    using (var responseStream = await response.Content.ReadAsStreamAsync().ConfigureAwait(false))
                    using (var decompressedStream = new GZipStream(responseStream, CompressionMode.Decompress))
                    {
                        return decompressedStream.BaseStream;
                    }
                }
            }
        }
    }
}