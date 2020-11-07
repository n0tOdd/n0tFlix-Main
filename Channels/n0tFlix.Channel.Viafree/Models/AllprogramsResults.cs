using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace n0tFlix.Channel.Viafree.Models
{
    public class AllprogramsResults
    {
        //https://viafree-content.mtg-api.com/viafree-content/v1/no/block/allPrograms?device=web
        //https://viafree-content.mtg-api.com/viafree-content/v1/no/page/allPrograms?device= <= denne er best på info
        public static string URL = "https://viafree-content.mtg-api.com/viafree-content/v1/{0}/path/{1}";

        public class Meta
        {
            public string title;
            public string description;
            public string image;
            public string siteName;
            public string siteUrl;
            public string route;
            public string publicPath;
            public string canonicalUrl;
        }

        public class AbTesting
        {
            public string defaultVariant;
            public IList<string> availableVariants;
        }

        public class Self
        {
            public string href;
            public string method;
        }

        public class Links
        {
            public Self self;
        }

        public class Self2
        {
            public string href;
            public string method;
        }

        public class Links2
        {
            public Self2 self;
        }

        public class Channel
        {
            public string guid;
            public string name;
        }

        public class Category
        {
            public string guid;
            public string name;
        }

        public class AllPrograms
        {
            public IList<Channel> channels;
            public IList<Category> categories;
        }

        public class Channel2
        {
            public string guid;
        }

        public class Category2
        {
            public string guid;
            public string value;
        }

        public class Series
        {
            public int numberOfSeasons;
        }

        public class PlayerPoster
        {
            public string href;
            public string method;
            public bool templated;
        }

        public class Landscape
        {
            public string href;
            public string method;
            public bool templated;
        }

        public class Images
        {
            public PlayerPoster playerPoster;
            public Landscape landscape;
        }

        public class Series2
        {
            public string href;
            public string publicPath;
            public string method;
            public bool templated;
        }

        public class Links3
        {
            public Series2 series;
        }

        public class Program
        {
            public string type;
            public string guid;
            public string title;
            public string slug;
            public IList<Channel2> channels;
            public IList<object> tags;
            public IList<Category2> categories;
            public int numberOfSeasons;
            public Series series;
            public Images images;
            public IList<string> flags;
            public string publicPath;
            public Links3 _links;
        }

        public class Embedded2
        {
            public IList<Program> programs;
        }

        public class ViafreeBlock
        {
            public string componentName;
            public string slug;
            public bool lazyBlock;
            public Links2 _links;
            public string dataType;
            public AllPrograms allPrograms;
            public Embedded2 _embedded;
        }

        public class Embedded
        {
            public IList<ViafreeBlock> viafreeBlocks;
        }

        public class root
        {
            public string type;
            public Meta meta;
            public IList<AbTesting> abTesting;
            public Links _links;
            public Embedded _embedded;
        }

        public static async Task<root> GetRoot(bool UsePulicPath = true, string CountryCode = "no", string Path = "programmer")
        {
            WebClient client = new WebClient();
            if (UsePulicPath)
            {
                string json = await client.DownloadStringTaskAsync(new Uri(String.Format(URL, CountryCode, Path)));
                if (string.IsNullOrEmpty(json))
                {
                    return null;
                }
                root klar = JsonConvert.DeserializeObject<root>(json);
                return klar;
            }
            else
            {
                string json = await client.DownloadStringTaskAsync(String.Format("https://viafree-content.mtg-api.com/viafree-content/v1/{0}/page/allPrograms?device=", CountryCode));
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