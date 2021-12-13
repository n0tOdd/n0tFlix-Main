using MediaBrowser.Common.Plugins;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Reflection.Metadata;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace n0tFlix.Manifest.Manager
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            //  if (args.Length == 0)
            //    return;
            string template = @"C:\Users\ersoucy\source\repos\ersoucy\n0tFlix-Main\Manifest-Template.json";
            string manifest = File.ReadAllText(template);
            foreach (string file in Directory.GetFiles(@"C:\Users\ersoucy\source\repos\ersoucy\n0tFlix-Main\", "*.md5", SearchOption.AllDirectories))
            {
                if (file.Contains("Debug"))
                    continue;
                FileInfo fi = new FileInfo(file);
                string name = fi.Name.Replace(".md5", "");
                string md5 = File.ReadAllText(file).Replace(Environment.NewLine, "");

                manifest = manifest.Replace(name + "-MD5", md5);
            }

            File.WriteAllText(@"C:\Users\ersoucy\source\repos\ersoucy\n0tFlix-Main\Manifest.json", manifest);
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