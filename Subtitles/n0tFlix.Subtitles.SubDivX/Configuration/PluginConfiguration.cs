
using MediaBrowser.Model.Plugins;

namespace n0tFlix.Subtitles.SubDivX.Configuration
{
    public class PluginConfiguration : BasePluginConfiguration
    {
        public bool UseOriginalTitle { get; set; } = false;
        public bool ShowTitleInResult { get; set; } = true;
        public bool ShowUploaderInResult { get; set; } = true;
    }
}
