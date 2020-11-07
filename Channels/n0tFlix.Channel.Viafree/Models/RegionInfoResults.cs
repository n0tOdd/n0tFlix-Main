using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace n0tFlix.Channel.Viafree.Models
{
    public static class RegionInfoResults
    {
        public static string URL = "https://viafree-content.mtg-api.com/viafree-content/v1/regionlookup";

        public class root
        {
            [JsonProperty("ip")]
            public string Ip { get; set; }

            [JsonProperty("countryCode")]
            public string CountryCode { get; set; }
        }

        /// <summary>
        /// returns the regionloojup results, dont need anything here
        /// </summary>
        /// <param name="UsePulicPath"></param>
        /// <param name="CountryCode"></param>
        /// <param name="Path"></param>
        /// <returns>Returns selected sportsclip by publicpath or slug </returns>
        public static async System.Threading.Tasks.Task<root> GetRoot(bool UsePulicPath = true, string CountryCode = "no", string Path = null)
        {
            System.Net.WebClient client = new System.Net.WebClient();

            string json = await client.DownloadStringTaskAsync(URL);
            if (string.IsNullOrEmpty(json))
            {
                return null;
            }
            root klar = JsonConvert.DeserializeObject<root>(json);
            return klar;
        }
    }
}