using System;
using System.Collections.Generic;
using n0tFlix.Subtitles.TheSubDB.Configuration;
using MediaBrowser.Common.Configuration;
using MediaBrowser.Common.Plugins;
using MediaBrowser.Model.Plugins;
using MediaBrowser.Model.Serialization;
using System.Linq;

namespace n0tFlix.Subtitles.TheSubDB
{
    public class Plugin : BasePlugin<PluginConfiguration>
    {
        /// <summary>
        /// Gets the plugin instance.
        /// </summary>
        public static Plugin Instance { get; private set; }

        #region Configuration Variables for the plugin, remember to update the version on upgrades

        /// <summary>
        /// The name of youre plugin, we are gonna use the same variable all over so you just need to edit it this once ;)
        /// </summary>
        public override string Name => GetType().Namespace.Split(".").Last(); //Splits the namespace and uses the last part as a plugin name

        /// <summary>
        /// The Description of youre plugin, goin to be used by the manifestmanager later to keep the repository clrean
        /// </summary>
        public override string Description => "A plugin that grabs subtitles from TheSubDB";

        /// <summary>
        /// Just added so we can share where more is to be found :P
        /// </summary>
        public string HomePageURL => "https://n0tprojects.com";

        /// <summary>
        /// The id of our plugin rememmber DONT run multiple plugins with same guid, its just trouble in the end
        /// use new-guid in powershell for å fresh value
        /// </summary>
        public override Guid Id => Guid.Parse("f13d3969-2f3a-4dfe-b95e-4036f9f69f2c");

        /// <summary>
        /// Only way i found to keep the Version value managed, if anybody finds a better way please tell me
        /// </summary>
        /// <returns></returns>
        public override PluginInfo GetPluginInfo()
        {
            return new PluginInfo(this.Name, this.Version, this.Description, this.Id, this.CanUninstall)
            {
                ConfigurationFileName = this.Name + "-V" + Version.ToString(), //Configuration filename, using this as a themeplate incase we trow in a new one on updates and dont wanna override the old one at the first dropoff
            };
        }

        /// <summary>
        /// This one is to get youre html files for the config page or who know,
        /// </summary>
        /// <returns>returns some PluginPageInfo, maybe this can hack the channel interface?</returns>
        public IEnumerable<PluginPageInfo> GetPages()
        {
            List<PluginPageInfo> pluginPageInfos = new List<PluginPageInfo>();
            pluginPageInfos.Add(new PluginPageInfo()
            {
                Name = this.Name,
                DisplayName = this.Name,
                MenuSection = "n0tFlix",//<== create my own part of the meny, slowly im taking over all the jellyfin and netflix is born :P
                EmbeddedResourcePath = "",//todo add this bro, its suposed to be here
                EnableInMainMenu = false, //<== this makes it show up on youre dashboard menu, i was hoping public menu
                MenuIcon = "",
            });
            return pluginPageInfos;
        }

        #endregion Configuration Variables for the plugin, remember to update the version on upgrades

        public Plugin(IApplicationPaths applicationPaths, IXmlSerializer xmlSerializer)
                     : base(applicationPaths, xmlSerializer)
        {
            Instance = this;
        }

        public override void UpdateConfiguration(BasePluginConfiguration configuration)
        {
            base.UpdateConfiguration(configuration);
        }
    }
}