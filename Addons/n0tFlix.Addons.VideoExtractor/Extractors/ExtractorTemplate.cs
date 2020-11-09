using n0tFlix.Addons.VideoExtractor.Interfaces;
using n0tFlix.Addons.VideoExtractor.Models;
using n0tFlix.Addons.VideoExtractor.Nettwork;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace n0tFlix.Addons.VideoExtractor.Extractors
{
    /// <summary>
    /// This class is just a empty class with the basic function and stuff for a extractor
    /// made to give you a easy copy paste when you want to start to work on a new extraction engine
    /// </summary>
    public class ExtractorTemplate : IExtractor
    {
        public string Name => "Name of extractor";

        public string Description => "A short description about this extractor with supported pages and stuff";

        //The baseurl, the extractor will only run if the url we want to extract from contains this string,
        //this should probably be changed to a regex that can match more than one string
        private string BaseURL = "someurl.com";

        /// <summary>
        /// A webclient with a premade httpclient and all the basic headers and shit needed to make the servers belive we are a browser
        /// also have Anglesharp implemented for simple parsing of wepages
        /// </summary>
        private n0tWebClient client = new n0tWebClient();

        public bool CheckURL(string url)
            => url.Contains(BaseURL, StringComparison.OrdinalIgnoreCase);

        public Task<List<DownloadInfo>> Extract(string url)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> Login(string id, string pw)
        {
            //  await client.OpenDocument(LoginURL); //Here we use the anglesharp browser just because why not
            //Some code to login anf make sure we are logged in

            return true;
        }
    }
}