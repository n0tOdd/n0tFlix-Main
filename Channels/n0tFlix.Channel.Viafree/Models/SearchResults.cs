using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace n0tFlix.Channel.Viafree.Models
{
    public class SearchResults
    {
        //https://viafree-content.mtg-api.com/viafree-content/v1/no/search/SearchWord <== husk å bytte SearchWord

        public static string URL = "https://viafree-content.mtg-api.com/viafree-content/v1/{0}/search/{1}";

        public class Self
        {
            [JsonProperty("href")]
            public string Href { get; set; }

            [JsonProperty("method")]
            public string Method { get; set; }
        }

        public class Links
        {
            [JsonProperty("self")]
            public Self Self { get; set; }
        }

        public class Category
        {
            [JsonProperty("guid")]
            public string Guid { get; set; }

            [JsonProperty("title")]
            public string Title { get; set; }

            [JsonProperty("value")]
            public string Value { get; set; }
        }

        public class Tag
        {
            [JsonProperty("guid")]
            public string Guid { get; set; }

            [JsonProperty("title")]
            public string Title { get; set; }

            [JsonProperty("value")]
            public string Value { get; set; }
        }

        public class LatestVideo
        {
            [JsonProperty("id")]
            public string Id { get; set; }

            [JsonProperty("title")]
            public string Title { get; set; }

            [JsonProperty("publishedAt")]
            public string PublishedAt { get; set; }

            [JsonProperty("episodeNumber")]
            public int EpisodeNumber { get; set; }

            [JsonProperty("seasonNumber")]
            public int SeasonNumber { get; set; }
        }

        public class Series
        {
            [JsonProperty("numberOfSeasons")]
            public int NumberOfSeasons { get; set; }

            [JsonProperty("featureBoxPromo")]
            public string FeatureBoxPromo { get; set; }

            [JsonProperty("contextualLabel")]
            public string ContextualLabel { get; set; }

            [JsonProperty("latestVideo")]
            public LatestVideo LatestVideo { get; set; }
        }

        public class PlayerPoster
        {
            [JsonProperty("href")]
            public string Href { get; set; }

            [JsonProperty("method")]
            public string Method { get; set; }

            [JsonProperty("templated")]
            public bool Templated { get; set; }
        }

        public class Landscape
        {
            [JsonProperty("href")]
            public string Href { get; set; }

            [JsonProperty("method")]
            public string Method { get; set; }

            [JsonProperty("templated")]
            public bool Templated { get; set; }
        }

        public class Images
        {
            [JsonProperty("playerPoster")]
            public PlayerPoster PlayerPoster { get; set; }

            [JsonProperty("landscape")]
            public Landscape Landscape { get; set; }
        }

        public class Series2
        {
            [JsonProperty("href")]
            public string Href { get; set; }

            [JsonProperty("publicPath")]
            public string PublicPath { get; set; }

            [JsonProperty("method")]
            public string Method { get; set; }

            [JsonProperty("templated")]
            public bool Templated { get; set; }
        }

        public class Links2
        {
            [JsonProperty("series")]
            public Series2 Series { get; set; }
        }

        public class Program
        {
            [JsonProperty("type")]
            public string Type { get; set; }

            [JsonProperty("searchType")]
            public string SearchType { get; set; }

            [JsonProperty("guid")]
            public string Guid { get; set; }

            [JsonProperty("title")]
            public string Title { get; set; }

            [JsonProperty("slug")]
            public string Slug { get; set; }

            [JsonProperty("categories")]
            public IList<Category> Categories { get; set; }

            [JsonProperty("tags")]
            public IList<Tag> Tags { get; set; }

            [JsonProperty("numberOfSeasons")]
            public int NumberOfSeasons { get; set; }

            [JsonProperty("series")]
            public Series Series { get; set; }

            [JsonProperty("images")]
            public Images Images { get; set; }

            [JsonProperty("flags")]
            public IList<string> Flags { get; set; }

            [JsonProperty("publicPath")]
            public string PublicPath { get; set; }

            [JsonProperty("_links")]
            public Links2 Links { get; set; }
        }

        public class Embedded
        {
            [JsonProperty("programs")]
            public IList<Program> Programs { get; set; }
        }

        public class root
        {
            [JsonProperty("_links")]
            public Links Links { get; set; }

            [JsonProperty("_embedded")]
            public Embedded Embedded { get; set; }
        }

        /// <summary>
        /// Search the api for shows, no public path in use here change Path variable to youre searchwor
        ///
        /// </summary>
        /// <param name="UsePulicPath"></param>
        /// <param name="CountryCode"></param>
        /// <param name="Path"></param>
        /// <returns>Returns the result of youre search </returns>
        public static async Task<root> GetRoot(bool UsePulicPath = false, string CountryCode = "no", string Path = "Alex")
        {
            WebClient client = new WebClient();

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