using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace n0tFlix.Channel.Viafree.Models
{
    public class CategoryResults
    {
        //https://viafree-content.mtg-api.com/viafree-content/v1/no/path/programmer/underholdning?lazy=false&testVariant=default
        //bytt ut underholdning med categorien du vil ha serier ffor
        //https://viafree-content.mtg-api.com/viafree-content/v1/no/page/category/BYTT <= her bytter du BYTT med category navnet

        public static string URL = "https://viafree-content.mtg-api.com/viafree-content/v1/{0}/page/category/{1}";

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

        public class Image
        {
            public string href;
            public string method;
            public bool templated;
        }

        public class Images
        {
            public Image image;
        }

        public class Category2
        {
            public string href;
            public string publicPath;
            public string method;
            public bool templated;
        }

        public class Links3
        {
            public Category2 category;
        }

        public class Category
        {
            public string type;
            public string guid;
            public string name;
            public string slug;
            public string country;
            public Images images;
            public Links3 _links;
        }

        public class MediaFeed
        {
            public string title;
            public int maxSize;
            public int displayCount;
        }

        public class Synopsis
        {
            [JsonProperty("long")]
            public string Long;

            [JsonProperty("short")]
            public string Short;
        }

        public class Availability
        {
            public string start;
            public string end;
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

        public class Hero
        {
            public string href;
            public string method;
            public bool templated;
        }

        public class Images2
        {
            public PlayerPoster playerPoster;
            public Landscape landscape;
            public Hero hero;
        }

        public class Tag
        {
            public string guid;
            public string title;
            public string value;
        }

        public class Channel
        {
            public string guid;
        }

        public class LatestVideo
        {
            public string id;
            public string title;
            public string publishedAt;
            public int? episodeNumber;
            public int seasonNumber;
        }

        public class Series
        {
            public int numberOfSeasons;
            public string featureBoxPromo;
            public string contextualLabel;
            public LatestVideo latestVideo;
        }

        public class Category3
        {
            public string guid;
            public string title;
            public string value;
        }

        public class Subcategory
        {
            public string guid;
            public string title;
            public string value;
        }

        public class Self3
        {
            public string href;
            public string method;
        }

        public class Series2
        {
            public string href;
            public string publicPath;
            public string method;
            public bool templated;
        }

        public class CustomDimensions
        {
            public string href;
            public string method;
        }

        public class Links4
        {
            public Self3 self;
            public Series2 series;
            public CustomDimensions customDimensions;
        }

        public class Program
        {
            public string type;
            public string guid;
            public string slug;
            public string publicPath;
            public string title;
            public Synopsis synopsis;
            public object parentalRating;
            public Availability availability;
            public Images2 images;
            public IList<string> flags;
            public IList<Tag> tags;
            public IList<Channel> channels;
            public string productKey;
            public int? popularityScore;
            public Series series;
            public IList<Category3> categories;
            public IList<Subcategory> subcategories;
            public string combinedTitle;
            public Links4 _links;
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
            public Category category;
            public MediaFeed mediaFeed;
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

        public static async Task<root> GetRoot(bool UsePulicPath = true, string CountryCode = "no", string Path = "dokumentar")
        {
            WebClient client = new WebClient();

            if (UsePulicPath)
            {
                string json = await client.DownloadStringTaskAsync(String.Format("https://viafree-content.mtg-api.com/viafree-content/v1/{0}/path/category/{1} ", CountryCode, Path));
                if (string.IsNullOrEmpty(json))
                {
                    return null;
                }
                root klar = JsonConvert.DeserializeObject<root>(json);
                return klar;
            }
            else
            {
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