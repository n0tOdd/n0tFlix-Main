using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace n0tFlix.Channel.Pornhub.Models
{
    public class CategoryResults
    {
        public class Category
        {
            [JsonProperty("id")]
            public string Id { get; set; }

            [JsonProperty("category")]
            public string category { get; set; }
        }

        public class root
        {
            [JsonProperty("categories")]
            public IList<Category> Categories { get; set; }
        }
    }
}