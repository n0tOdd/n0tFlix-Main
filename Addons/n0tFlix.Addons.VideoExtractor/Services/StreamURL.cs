using MediaBrowser.Model.Services;
using n0tFlix.Addons.VideoExtractor.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace n0tFlix.Addons.VideoExtractor.Services
{
    public class StreamURL : IService
    {
        [Route("/VideoExtractor/StreamURL", "GET", Description = "Gets Streamlink / download link from a webpage")]
        public class GetJson : IReturn<string>
        {
            [ApiMember(Name = "Url", Description = "Gets a streamable link for the video in the page", IsRequired = true, DataType = "string",
              ParameterType = "query", Verb = "GET")]
            public string Url { get; set; }
        }

        public async Task<string> Get(GetJson getURL)
        {
            var res = await BaseExtractor.ExtractAsync(getURL.Url);
            return res.First().Videos.First().url;
        }
    }
}