using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace n0tFlix.Channel.Viafree.Models
{
    public static class StreamResults
    {
        //https://viafree.mtg-api.com/stream-links/viafree/web/no/clear-media-guids/10004091/streams
        public static string URL = "https://viafree.mtg-api.com/stream-links/viafree/web/{0}/clear-media-guids/{1}/streams";

        public class Self
        {
            public string href;
            public string method;
            public string returnType;
        }

        public class Links
        {
            public Self self;
        }

        public class Stream
        {
            public string href;
            public string method;
        }

        public class Links2
        {
            public Stream stream;
        }

        public class PrioritizedStream
        {
            public Links2 links;
        }

        public class Link
        {
            public string href;
            public string method;
        }

        public class Data
        {
            [JsonProperty("default")]
            public bool Default;

            public int languageCode;
            public string language;
            public string format;
            public bool sdh;
        }

        public class Subtitle
        {
            public Link link;
            public Data data;
        }

        public class Embedded
        {
            public IList<PrioritizedStream> prioritizedStreams;
            public IList<Subtitle> subtitles;
        }

        public class Data2
        {
            public string streamStartTime;
            public string streamEndTime;
        }

        public class root
        {
            public Links links;
            public Embedded embedded;
            public Data2 data;
        }

        /// <summary>
        /// Returns the info about stream link, no public path here, you can use the direct link to the stream json data or the media-guid variable for the video
        /// </summary>
        /// <param name="UsePulicPath"></param>
        /// <param name="CountryCode"></param>
        /// <param name="Path"></param>
        /// <returns>Returns the start page in all its glory </returns>
        public static async System.Threading.Tasks.Task<root> GetRoot(bool UsePulicPath = false, string CountryCode = "no", string Path = "976615")
        {
            System.Net.WebClient client = new System.Net.WebClient();
            if (Path.Contains("http"))
            {
                string json = await client.DownloadStringTaskAsync(Path);
                if (string.IsNullOrEmpty(json))
                {
                    return null;
                }
                root klar = JsonConvert.DeserializeObject<root>(json);
                return klar;
            }
            else
            {
                string publicPathURL = "https://viafree-content.mtg-api.com/viafree-content/v1/{0}/path{1}";
                string json = await client.DownloadStringTaskAsync(String.Format(URL, CountryCode, Path));
                if (string.IsNullOrEmpty(json))
                {
                    return null;
                }
                root klar = JsonConvert.DeserializeObject<root>(json);
                return klar;
            }
        }
    }
}