using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace n0tFlix.Channel.Viafree.Models
{
    public class CategoriesResult
    {
        //https://viafree-content.mtg-api.com/viafree-content/v1/no/categories?guid= <= her får du all uten guid, setter du på guid får du spesifikk info
        //

        public static string URL = "https://viafree-content.mtg-api.com/viafree-content/v1/{0}/categories?guid={1}";

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

        public class Image
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
            [JsonProperty("image")]
            public Image Image { get; set; }
        }

        public class Category
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
            [JsonProperty("category")]
            public Category Category { get; set; }
        }

        public class Categories
        {
            [JsonProperty("type")]
            public string Type { get; set; }

            [JsonProperty("guid")]
            public string Guid { get; set; }

            [JsonProperty("name")]
            public string Name { get; set; }

            [JsonProperty("slug")]
            public string Slug { get; set; }

            [JsonProperty("country")]
            public string Country { get; set; }

            [JsonProperty("images")]
            public Images Images { get; set; }

            [JsonProperty("_links")]
            public Links2 Links { get; set; }
        }

        public class Embedded
        {
            [JsonProperty("categories")]
            public Categories Categories { get; set; }
        }

        public class root
        {
            [JsonProperty("type")]
            public string Type { get; set; }

            [JsonProperty("_links")]
            public Links Links { get; set; }

            [JsonProperty("_embedded")]
            public Embedded Embedded { get; set; }
        }

        public static async Task<root> GetRoot(bool UsePulicPath = true, string CountryCode = "no", string Path = "programmer")
        {
            WebClient client = new WebClient();
            if (UsePulicPath)
            {
                string json = await client.DownloadStringTaskAsync(String.Format(URL, CountryCode, Path));
                if (string.IsNullOrEmpty(json))
                {
                    return null;
                }
                root klar = JsonConvert.DeserializeObject<root>(json);
                return klar;
            }
            else
            {
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
}