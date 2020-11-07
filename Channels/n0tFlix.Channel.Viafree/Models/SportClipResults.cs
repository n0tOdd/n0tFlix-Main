using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace n0tFlix.Channel.Viafree.Models
{
    public static class SportClipResults
    {
        public static string URL = "https://viafree-content.mtg-api.com/viafree-content/v1/{0}/sportClip/{1}";

        public class SportClipPoster
        {
            [JsonProperty("href")]
            public string Href { get; set; }

            [JsonProperty("method")]
            public string Method { get; set; }

            [JsonProperty("templated")]
            public bool Templated { get; set; }
        }

        public class PlayerPoster
        {
            [JsonProperty("href")]
            public string Href { get; set; }

            [JsonProperty("method")]
            public string Method { get; set; }

            [JsonProperty("templated")]
            public bool Templated { get; set; }
        }

        public class Images
        {
            [JsonProperty("sportClipPoster")]
            public SportClipPoster SportClipPoster { get; set; }

            [JsonProperty("playerPoster")]
            public PlayerPoster PlayerPoster { get; set; }
        }

        public class Synopsis
        {
            [JsonProperty("long")]
            public string Long { get; set; }

            [JsonProperty("short")]
            public string Short { get; set; }
        }

        public class Availability
        {
            [JsonProperty("start")]
            public string Start { get; set; }

            [JsonProperty("end")]
            public object End { get; set; }
        }

        public class Sport
        {
            [JsonProperty("name")]
            public string Name { get; set; }

            [JsonProperty("slug")]
            public string Slug { get; set; }
        }

        public class Competition
        {
            [JsonProperty("name")]
            public string Name { get; set; }

            [JsonProperty("slug")]
            public string Slug { get; set; }
        }

        public class SportTags
        {
            [JsonProperty("programType")]
            public object ProgramType { get; set; }

            [JsonProperty("sport")]
            public Sport Sport { get; set; }

            [JsonProperty("competition")]
            public Competition Competition { get; set; }

            [JsonProperty("contents")]
            public IList<string> Contents { get; set; }

            [JsonProperty("events")]
            public IList<object> Events { get; set; }
        }

        public class SportClip
        {
            [JsonProperty("preamble")]
            public object Preamble { get; set; }

            [JsonProperty("sportTags")]
            public SportTags SportTags { get; set; }

            [JsonProperty("text")]
            public string Text { get; set; }
        }

        public class Duration
        {
            [JsonProperty("milliseconds")]
            public int Milliseconds { get; set; }

            [JsonProperty("readable")]
            public string Readable { get; set; }
        }

        public class Video
        {
            [JsonProperty("mediaGuid")]
            public string MediaGuid { get; set; }

            [JsonProperty("duration")]
            public Duration Duration { get; set; }
        }

        public class Self
        {
            [JsonProperty("href")]
            public string Href { get; set; }

            [JsonProperty("method")]
            public string Method { get; set; }
        }

        public class Play
        {
            [JsonProperty("href")]
            public string Href { get; set; }

            [JsonProperty("publicPath")]
            public string PublicPath { get; set; }

            [JsonProperty("method")]
            public string Method { get; set; }

            [JsonProperty("templated")]
            public bool Templated { get; set; }
        }

        public class StreamLink
        {
            [JsonProperty("href")]
            public string Href { get; set; }
        }

        public class AdInfo
        {
            [JsonProperty("href")]
            public string Href { get; set; }

            [JsonProperty("method")]
            public string Method { get; set; }
        }

        public class CustomDimensions
        {
            [JsonProperty("href")]
            public string Href { get; set; }

            [JsonProperty("method")]
            public string Method { get; set; }
        }

        public class Tracking
        {
            [JsonProperty("href")]
            public string Href { get; set; }

            [JsonProperty("method")]
            public string Method { get; set; }
        }

        public class Links
        {
            [JsonProperty("self")]
            public Self Self { get; set; }

            [JsonProperty("play")]
            public Play Play { get; set; }

            [JsonProperty("streamLink")]
            public StreamLink StreamLink { get; set; }

            [JsonProperty("adInfo")]
            public AdInfo AdInfo { get; set; }

            [JsonProperty("customDimensions")]
            public CustomDimensions CustomDimensions { get; set; }

            [JsonProperty("tracking")]
            public Tracking Tracking { get; set; }

            [JsonProperty("partnerInfo")]
            public object PartnerInfo { get; set; }
        }

        public class Self2
        {
            [JsonProperty("href")]
            public string Href { get; set; }
        }

        public class Links2
        {
            [JsonProperty("self")]
            public Self2 Self { get; set; }
        }

        public class AdBlockerBlocker
        {
            [JsonProperty("enabled")]
            public bool Enabled { get; set; }
        }

        public class AdRequestKeyValues
        {
            [JsonProperty("_pbformat")]
            public string Pbformat { get; set; }

            [JsonProperty("_pbseason")]
            public string Pbseason { get; set; }
        }

        public class Freewheel
        {
            [JsonProperty("videoAssetId")]
            public string VideoAssetId { get; set; }

            [JsonProperty("serverUrl")]
            public string ServerUrl { get; set; }

            [JsonProperty("networkId")]
            public string NetworkId { get; set; }

            [JsonProperty("siteSectionNetworkId")]
            public string SiteSectionNetworkId { get; set; }

            [JsonProperty("siteSectionId")]
            public string SiteSectionId { get; set; }

            [JsonProperty("profileId")]
            public string ProfileId { get; set; }

            [JsonProperty("showPrerolls")]
            public bool ShowPrerolls { get; set; }

            [JsonProperty("showMidrolls")]
            public bool ShowMidrolls { get; set; }

            [JsonProperty("showPostrolls")]
            public bool ShowPostrolls { get; set; }

            [JsonProperty("showOverlays")]
            public bool ShowOverlays { get; set; }

            [JsonProperty("showPauseAds")]
            public bool ShowPauseAds { get; set; }

            [JsonProperty("midrollCuepoints")]
            public IList<object> MidrollCuepoints { get; set; }

            [JsonProperty("videoLiveDuration")]
            public int VideoLiveDuration { get; set; }

            [JsonProperty("adRequestKeyValues")]
            public AdRequestKeyValues AdRequestKeyValues { get; set; }
        }

        public class Embedded2
        {
            [JsonProperty("adBlockerBlocker")]
            public AdBlockerBlocker AdBlockerBlocker { get; set; }

            [JsonProperty("freewheel")]
            public Freewheel Freewheel { get; set; }
        }

        public class AdInfo2
        {
            [JsonProperty("type")]
            public string Type { get; set; }

            [JsonProperty("_links")]
            public Links2 Links { get; set; }

            [JsonProperty("_embedded")]
            public Embedded2 Embedded { get; set; }
        }

        public class Self3
        {
            [JsonProperty("href")]
            public string Href { get; set; }
        }

        public class Links3
        {
            [JsonProperty("self")]
            public Self3 Self { get; set; }
        }

        public class CustomDimensions2
        {
            [JsonProperty("title")]
            public string Title { get; set; }

            [JsonProperty("format")]
            public string Format { get; set; }

            [JsonProperty("category")]
            public string Category { get; set; }

            [JsonProperty("clipLabel")]
            public string ClipLabel { get; set; }

            [JsonProperty("videoLength")]
            public string VideoLength { get; set; }

            [JsonProperty("season")]
            public string Season { get; set; }

            [JsonProperty("episode")]
            public string Episode { get; set; }

            [JsonProperty("videoType")]
            public string VideoType { get; set; }

            [JsonProperty("productionType")]
            public string ProductionType { get; set; }

            [JsonProperty("channel")]
            public string Channel { get; set; }

            [JsonProperty("country")]
            public string Country { get; set; }

            [JsonProperty("owner")]
            public string Owner { get; set; }

            [JsonProperty("webExclusive")]
            public string WebExclusive { get; set; }

            [JsonProperty("loginRequired")]
            public string LoginRequired { get; set; }

            [JsonProperty("publishDate")]
            public string PublishDate { get; set; }

            [JsonProperty("rights")]
            public string Rights { get; set; }

            [JsonProperty("daysOnPlay")]
            public string DaysOnPlay { get; set; }

            [JsonProperty("adType")]
            public string AdType { get; set; }

            [JsonProperty("casting")]
            public string Casting { get; set; }

            [JsonProperty("assetId")]
            public string AssetId { get; set; }

            [JsonProperty("embedUrl")]
            public string EmbedUrl { get; set; }

            [JsonProperty("appVersion")]
            public string AppVersion { get; set; }

            [JsonProperty("platform")]
            public string Platform { get; set; }

            [JsonProperty("authenticated")]
            public string Authenticated { get; set; }

            [JsonProperty("gender")]
            public string Gender { get; set; }

            [JsonProperty("age")]
            public string Age { get; set; }

            [JsonProperty("city")]
            public string City { get; set; }

            [JsonProperty("cuepoints")]
            public string Cuepoints { get; set; }

            [JsonProperty("competition")]
            public string Competition { get; set; }

            [JsonProperty("subCategory")]
            public string SubCategory { get; set; }

            [JsonProperty("partnerName")]
            public string PartnerName { get; set; }

            [JsonProperty("theme")]
            public string Theme { get; set; }
        }

        public class GoogleAnalytics
        {
            [JsonProperty("customDimensions")]
            public CustomDimensions2 CustomDimensions { get; set; }
        }

        public class Video2
        {
            [JsonProperty("id")]
            public string Id { get; set; }

            [JsonProperty("category")]
            public string Category { get; set; }

            [JsonProperty("channel")]
            public string Channel { get; set; }

            [JsonProperty("format")]
            public string Format { get; set; }

            [JsonProperty("productionType")]
            public string ProductionType { get; set; }

            [JsonProperty("season")]
            public string Season { get; set; }

            [JsonProperty("subCategory")]
            public string SubCategory { get; set; }

            [JsonProperty("title")]
            public string Title { get; set; }

            [JsonProperty("videoType")]
            public string VideoType { get; set; }
        }

        public class Krux
        {
            [JsonProperty("video")]
            public Video2 Video { get; set; }
        }

        public class Gallup
        {
            [JsonProperty("siteId")]
            public string SiteId { get; set; }

            [JsonProperty("prefix")]
            public string Prefix { get; set; }

            [JsonProperty("trackAds")]
            public bool TrackAds { get; set; }

            [JsonProperty("stream")]
            public string Stream { get; set; }

            [JsonProperty("ct")]
            public string Ct { get; set; }

            [JsonProperty("cq")]
            public string Cq { get; set; }

            [JsonProperty("country")]
            public string Country { get; set; }
        }

        public class Data
        {
            [JsonProperty("mediaGuid")]
            public string MediaGuid { get; set; }

            [JsonProperty("productGuid")]
            public string ProductGuid { get; set; }

            [JsonProperty("seriesGuid")]
            public string SeriesGuid { get; set; }

            [JsonProperty("nextEpisodeProductGuid")]
            public string NextEpisodeProductGuid { get; set; }

            [JsonProperty("title")]
            public string Title { get; set; }

            [JsonProperty("duration")]
            public int Duration { get; set; }

            [JsonProperty("isLive")]
            public bool IsLive { get; set; }

            [JsonProperty("mainContentStart")]
            public int MainContentStart { get; set; }

            [JsonProperty("mainContentEnd")]
            public int MainContentEnd { get; set; }

            [JsonProperty("productType")]
            public string ProductType { get; set; }

            [JsonProperty("programType")]
            public int ProgramType { get; set; }

            [JsonProperty("sectionType")]
            public int SectionType { get; set; }

            [JsonProperty("deviceKey")]
            public string DeviceKey { get; set; }

            [JsonProperty("deviceId")]
            public string DeviceId { get; set; }

            [JsonProperty("isAnonymous")]
            public bool IsAnonymous { get; set; }

            [JsonProperty("userId")]
            public string UserId { get; set; }
        }

        public class ClientStream
        {
            [JsonProperty("base")]
            public string Base { get; set; }

            [JsonProperty("brand")]
            public string Brand { get; set; }

            [JsonProperty("mtgApiDomain")]
            public string MtgApiDomain { get; set; }

            [JsonProperty("data")]
            public Data Data { get; set; }
        }

        public class Embedded3
        {
            [JsonProperty("googleAnalytics")]
            public GoogleAnalytics GoogleAnalytics { get; set; }

            [JsonProperty("krux")]
            public Krux Krux { get; set; }

            [JsonProperty("gallup")]
            public Gallup Gallup { get; set; }

            [JsonProperty("clientStream")]
            public ClientStream ClientStream { get; set; }
        }

        public class Tracking2
        {
            [JsonProperty("type")]
            public string Type { get; set; }

            [JsonProperty("_links")]
            public Links3 Links { get; set; }

            [JsonProperty("_embedded")]
            public Embedded3 Embedded { get; set; }
        }

        public class Embedded
        {
            [JsonProperty("adInfo")]
            public AdInfo2 AdInfo { get; set; }

            [JsonProperty("tracking")]
            public Tracking2 Tracking { get; set; }
        }

        public class root
        {
            [JsonProperty("type")]
            public string Type { get; set; }

            [JsonProperty("guid")]
            public string Guid { get; set; }

            [JsonProperty("publicPath")]
            public string PublicPath { get; set; }

            [JsonProperty("slug")]
            public string Slug { get; set; }

            [JsonProperty("title")]
            public string Title { get; set; }

            [JsonProperty("images")]
            public Images Images { get; set; }

            [JsonProperty("synopsis")]
            public Synopsis Synopsis { get; set; }

            [JsonProperty("availability")]
            public Availability Availability { get; set; }

            [JsonProperty("sportClip")]
            public SportClip SportClip { get; set; }

            [JsonProperty("video")]
            public Video Video { get; set; }

            [JsonProperty("_links")]
            public Links Links { get; set; }

            [JsonProperty("_embedded")]
            public Embedded Embedded { get; set; }
        }

        /// <summary>
        /// Returns the selected spotsClip, here you need the slug identity of the clip, can use publicpath or not youre choise
        /// </summary>
        /// <param name="UsePulicPath"></param>
        /// <param name="CountryCode"></param>
        /// <param name="Path"></param>
        /// <returns>Returns selected sportsclip by publicpath or slug </returns>
        public static async System.Threading.Tasks.Task<root> GetRoot(bool UsePulicPath = true, string CountryCode = "no", string Path = "/sport/haaland-doblet-etter-ralekker-chip")
        {
            System.Net.WebClient client = new System.Net.WebClient();
            if (UsePulicPath)
            {
                string publicPathURL = "https://viafree-content.mtg-api.com/viafree-content/v1/{0}/path{1}";
                string json = await client.DownloadStringTaskAsync(String.Format(publicPathURL, CountryCode, Path));
                if (string.IsNullOrEmpty(json))
                {
                    return null;
                }
                root klar = JsonConvert.DeserializeObject<root>(json);
                return klar;
            }
            else
            {
                string json = await client.DownloadStringTaskAsync(String.Format(URL, CountryCode, Path));
                if (string.IsNullOrEmpty(json))
                {
                    return null;
                }
                root klar = JsonConvert.DeserializeObject<root>(json);
                return klar;
            }
        }
    }
}