using MediaBrowser.Common.Configuration;
using MediaBrowser.Common.Plugins;
using MediaBrowser.Model.Drawing;
using MediaBrowser.Model.Plugins;
using MediaBrowser.Model.Serialization;
using n0tFlix.Addons.Subscriptions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;

namespace n0tFlix.Addons.Subscriptions
{
    public class Plugin : BasePlugin<PluginConfiguration>, IHasWebPages
    {
        public static Plugin Instance { get; set; }
        public ImageFormat ThumbImageFormat => ImageFormat.Png;

        private readonly Guid _id = new Guid("fd876e40-9413-4fbc-acee-94bfce03734f");
        public override Guid Id => _id;

        public override string Name => GetPluginInfo().Name;

        public override PluginInfo GetPluginInfo()
        {
            PluginInfo pluginInfo = new PluginInfo("Subscriptions", new Version("1.0.0.0"), "", _id, true)
            {
                ConfigurationFileName = base.ConfigurationFileName,
            };
            return base.GetPluginInfo();
        }

        public Stream GetThumbImage()
        {
            var type = GetType();
            return type.Assembly.GetManifestResourceStream(type.Namespace + "Images.subscribe.png");
        }

        public Plugin(IApplicationPaths applicationPaths, IXmlSerializer xmlSerializer) : base(applicationPaths, xmlSerializer)
        {
            Instance = this;
        }

        public IEnumerable<PluginPageInfo> GetPages() => new[]
        {
            new PluginPageInfo
            {
                Name = "SubscriptionPage",
                EmbeddedResourcePath = GetType().Namespace + ".Configuration.SubscriptionPage.html"
            },
            new PluginPageInfo
            {
                Name = "SubscriptionPageJS",
                EmbeddedResourcePath = GetType().Namespace + ".Configuration.SubscriptionPage.js"
            }
        };
    }
}