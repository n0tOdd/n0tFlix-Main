using n0tFlix.Addons.VideoExtractor.Interfaces;
using n0tFlix.Addons.VideoExtractor.Models;
using n0tFlix.Addons.VideoExtractor.Nettwork;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace n0tFlix.Addons.VideoExtractor.Base
{
    public static class BaseExtractor
    {
        public static async System.Threading.Tasks.Task<List<DownloadInfo>> ExtractAsync(string url)
        {
            var type = typeof(IExtractor);
            string PluginPath = Path.Combine(Directory.GetCurrentDirectory(), "Plugins");
            List<Type> types = new List<Type>();

            #region Load outside plugins

            if (Directory.Exists(PluginPath))
            {
                foreach (string file in Directory.GetFiles(PluginPath, "*.dll"))
                {
                    Assembly plugin = Assembly.LoadFrom(file);
                    types.AddRange(plugin.GetTypes().Where(s => type.IsAssignableFrom(s)));
                }
            }

            #endregion Load outside plugins

            #region Load internal extractors

            types.AddRange(AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(s => s.GetTypes())
                .Where(p => type.IsAssignableFrom(p)));

            #endregion Load internal extractors

            #region Loop all extractors to find correct one

            foreach (var extractor in types)
            {
                if (extractor == null || extractor.IsInterface)
                    continue;
                var ex = (IExtractor)Activator.CreateInstance(extractor);
                if (ex.CheckURL(url))
                {
                    if (await ex.Login(BaseSettings.Id, BaseSettings.Pw))
                    {
                        List<DownloadInfo> info = await ex.Extract(url).ConfigureAwait(false);
                        n0tWebClient client = new n0tWebClient();
                        client.httpClient.DefaultRequestHeaders.Add("referer", url);
                        var resp = await client.httpClient.GetAsync(info.First().Videos.First().url);
                        return info;
                    }
                }
            }

            #endregion Loop all extractors to find correct one

            Console.WriteLine("Sorry but we could not find a supported extractor for this url " + url);
            return null;
        }
    }
}