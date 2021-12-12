using Microsoft.AspNetCore.Mvc;
using n0tFlix.Addons.VideoExtractor.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace n0tFlix.Addons.VideoExtractor.Services
{
    [ApiController]
    public class StreamURL : ControllerBase
    {
        [Route("/VideoExtractor/StreamURL/{url}")]
        public async Task<string> Get(string url)
        {
            var res = await BaseExtractor.ExtractAsync(url);
            return res.First().Videos.First().url;
        }
    }
}