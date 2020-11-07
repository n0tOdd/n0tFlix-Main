using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace n0tFlix.Channel.Viafree.Models
{
    public static class ChannelsResults
    {
        //https://viafree-content.mtg-api.com/viafree-content/v1/no/channels <= her får du resultat på alle, channelresult for spesifik
        public static string URL = "https://viafree-content.mtg-api.com/viafree-content/v1/{0}/channels";

        public class Self
        {
            public string href;
            public string method;
        }

        public class Links
        {
            public Self self;
        }

        public class Image
        {
            public string href;
            public string method;
            public bool templated;
        }

        public class Logo
        {
            public string href;
            public string method;
            public bool templated;
        }

        public class LogoDark
        {
            public string href;
            public string method;
            public bool templated;
        }

        public class Bug
        {
            public string href;
            public string method;
            public bool templated;
        }

        public class BugDark
        {
            public string href;
            public string method;
            public bool templated;
        }

        public class Images
        {
            public Image image;
            public Logo logo;
            public LogoDark logoDark;
            public Bug bug;
            public BugDark bugDark;
        }

        public class Channel2
        {
            public string href;
            public string publicPath;
            public string method;
            public bool templated;
        }

        public class Links2
        {
            public Channel2 channel;
        }

        public class Channel
        {
            public string type;
            public string guid;
            public string name;
            public string slug;
            public string country;
            public string description;
            public Images images;
            public Links2 _links;
        }

        public class Embedded
        {
            public IList<Channel> channels;
        }

        public class root
        {
            public string type;
            public Links _links;
            public Embedded _embedded;
        }

        /// <summary>
        /// Only country code needed here
        /// </summary>
        /// <param name="UsePulicPath"></param>
        /// <param name="CountryCode"></param>
        /// <param name="Path"></param>
        /// <returns>Returns all the channels on viafree so we can check what programs from each channel are awailable</returns>
        public static async Task<root> GetRoot(bool UsePulicPath = false, string CountryCode = "no", string Path = null)
        {
            WebClient client = new WebClient();

            string json = await client.DownloadStringTaskAsync(String.Format(URL, CountryCode));
            if (string.IsNullOrEmpty(json))
            {
                return null;
            }
            root klar = JsonConvert.DeserializeObject<root>(json);
            return klar;
        }
    }
}