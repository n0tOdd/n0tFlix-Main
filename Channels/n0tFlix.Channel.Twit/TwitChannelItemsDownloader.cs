using System;
using System.Threading;
using System.Threading.Tasks;
using MediaBrowser.Common.Net;
using MediaBrowser.Model.Serialization;
using System.IO;
using System.Text;
using Microsoft.Extensions.Logging;
using System.Net.Http;

namespace n0tFlix.Channel.TWiT
{
    public class TwitChannelItemsDownloader
    {
        private ILogger<TwitChannel> _logger;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IXmlSerializer _xmlSerializer;

        public TwitChannelItemsDownloader(ILogger<TwitChannel> logManager, IXmlSerializer xmlSerializer, IHttpClientFactory httpClientFactory)
        {
            _logger = logManager;
            _xmlSerializer = xmlSerializer;
            _httpClientFactory = httpClientFactory;
        }

        public async Task<rss> GetStreamList(String queryUrl, int offset, CancellationToken cancellationToken)
        {
            using (var xml = await _httpClientFactory.CreateClient().GetStreamAsync(queryUrl, cancellationToken))
            {
                _logger.LogInformation("Reading TWiT response with StreamReader");

                using (var reader = new StreamReader(xml))
                {
                    var str = reader.ReadToEnd();

                    _logger.LogInformation("Deserializing TwiT response");

                    rss result = _xmlSerializer.DeserializeFromBytes(typeof(rss), Encoding.UTF8.GetBytes(str)) as rss;
                    _logger.LogInformation(result.channel.category);
                    _logger.LogInformation("Deserialized TwiT response");
                    return result;
                }
            }
        }
    }
}