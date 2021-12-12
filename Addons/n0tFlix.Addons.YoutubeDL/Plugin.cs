using MediaBrowser.Common.Configuration;
using MediaBrowser.Common.Plugins;
using MediaBrowser.Model.Plugins;
using MediaBrowser.Model.Serialization;
using n0tFlix.Addons.YoutubeDL.Configuration;
using System;
using System.Collections.Generic;

namespace n0tFlix.Addons.YoutubeDL
{
    public class Plugin : BasePlugin<PluginConfiguration>
    {
        public static Plugin Instance { get; set; }

        private readonly Guid _id = new Guid("e84adafb-7ad0-43c9-92b6-2dfbd54d1342");
        public override Guid Id => _id;

        public override string Name => GetPluginInfo().Name;
        public override string Description => base.Description;

        public override PluginInfo GetPluginInfo()
        {
            PluginInfo pluginInfo = new PluginInfo("YoutubeDL API", new Version("1.0.0.0"), "", _id, true)
            {
                ConfigurationFileName = base.ConfigurationFileName,
            };
            return pluginInfo;
        }

        public Plugin(IApplicationPaths applicationPaths, IXmlSerializer xmlSerializer) : base(applicationPaths,
            xmlSerializer)
        {
            //Todo add path dataen som youtubedl skal save i
            Instance = this;
        }
    }
}