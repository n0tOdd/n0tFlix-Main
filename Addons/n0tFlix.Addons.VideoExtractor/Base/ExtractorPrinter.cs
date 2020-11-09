using n0tFlix.Addons.VideoExtractor.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace n0tFlix.Addons.VideoExtractor.Base
{
    public static class ExtractorPrinter
    {
        public static void PrintExtractors()
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

            Console.WriteLine("Awailable extractors");
            foreach (var extractor in types)
            {
                if (extractor == null || extractor.IsInterface)
                    continue;
                var ex = (IExtractor)Activator.CreateInstance(extractor);
                Console.WriteLine(ex.Name + ":");
                Console.WriteLine(ex.Description);
            }

            #endregion Loop all extractors to find correct one
        }
    }
}