using MediaBrowser.Common.Plugins;
using System;
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
            string path = @"C:\Users\oddos\source\repos\n0tFlix2\n0tFlix-Main\Addons\n0tFlix.Addons.Subscriptions\bin\Release\netstandard2.1\n0tFlix.Addons.Subscriptions.dll";
            if (File.Exists(path))
            {
                var ss = typeof(IPlugin);
                Assembly assembly = Assembly.LoadFrom(path);
                Type t = assembly.GetTypes()
                    .Where(x => ss.IsAssignableFrom(x))
                    .FirstOrDefault();
                var mm = t.GetMethod("get_Name");
                var saa = mm.Invoke(null, null);
                if (t is IPluginAssembly assemblyPlugin)
                {
                }
            }
        }
    }
}