using MediaBrowser.Common.Plugins;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Reflection.Metadata;

namespace n0tFlix.Manifest.Manager
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            //  if (args.Length == 0)
            //    return;

            var manifest = JsonConvert.DeserializeObject<root>("{{" + File.ReadAllText(@"C:\Users\oddos\source\repos\n0tFlix2\n0tFlix-Main\Manifest.json") + "}}");
        }
    }

    public class Version
    {
        [JsonProperty("version")]
        public string version { get; set; }

        [JsonProperty("changelog")]
        public string Changelog { get; set; }

        [JsonProperty("targetAbi")]
        public string TargetAbi { get; set; }

        [JsonProperty("sourceUrl")]
        public string SourceUrl { get; set; }

        [JsonProperty("checksum")]
        public string Checksum { get; set; }

        [JsonProperty("timestamp")]
        public string Timestamp { get; set; }
    }

    public class root
    {
        [JsonProperty("guid")]
        public string Guid { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("overview")]
        public string Overview { get; set; }

        [JsonProperty("owner")]
        public string Owner { get; set; }

        [JsonProperty("category")]
        public string Category { get; set; }

        [JsonProperty("versions")]
        public IList<Version> Versions { get; set; }
    }

    public class main
    {
        public List<root> Main { get; set; }
    }
}