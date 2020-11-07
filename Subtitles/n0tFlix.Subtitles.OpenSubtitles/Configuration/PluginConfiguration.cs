using MediaBrowser.Model.Plugins;

namespace n0tFlix.Subtitles.OpenSubtitles.Configuration
{
    public class PluginConfiguration : BasePluginConfiguration
    {
        public string Username { get; set; } = string.Empty;

        public string Password { get; set; } = string.Empty;
    }
}
