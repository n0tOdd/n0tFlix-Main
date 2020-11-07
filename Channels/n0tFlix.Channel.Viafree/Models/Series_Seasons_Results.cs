using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace n0tFlix.Channel.Viafree.Models
{
    /// <summary>
    /// se her for en eksempel link
    /// https://viafree-content.mtg-api.com/viafree-content/v1/no/block/series_seasons/boksing
    /// bytt ut boxing med slugen til programmet
    /// </summary>
    public class Series_Seasons_Results
    {
        public static string URL = "https://viafree-content.mtg-api.com/viafree-content/v1/{0}/block/series_seasons/{1}";

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

        public class Season2
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
            [JsonProperty("season")]
            public Season2 Season { get; set; }
        }

        public class Season
        {
            [JsonProperty("guid")]
            public string Guid { get; set; }

            [JsonProperty("title")]
            public string Title { get; set; }

            [JsonProperty("tvSeasonNumber")]
            public int TvSeasonNumber { get; set; }

            [JsonProperty("publicPath")]
            public string PublicPath { get; set; }

            [JsonProperty("_links")]
            public Links2 Links { get; set; }
        }

        public class Embedded
        {
            [JsonProperty("seasons")]
            public IList<Season> Seasons { get; set; }
        }

        public class root
        {
            [JsonProperty("componentName")]
            public string ComponentName { get; set; }

            [JsonProperty("slug")]
            public string Slug { get; set; }

            [JsonProperty("lazyBlock")]
            public bool LazyBlock { get; set; }

            [JsonProperty("_links")]
            public Links Links { get; set; }

            [JsonProperty("dataType")]
            public string DataType { get; set; }

            [JsonProperty("_embedded")]
            public Embedded Embedded { get; set; }
        }

        /// <summary>
        /// Returns all the seasons in the show, no publicpath here so change the path variable to series guid or series name ala boksing/13970 same result for both
        /// </summary>
        /// <param name="UsePulicPath"></param>
        /// <param name="CountryCode"></param>
        /// <param name="Path"></param>
        /// <returns>Returns the seasons for selected show </returns>
        public static async Task<root> GetRoot(bool UsePulicPath = false, string CountryCode = "no", string Path = "13970")
        {
            System.Net.WebClient client = new System.Net.WebClient();

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