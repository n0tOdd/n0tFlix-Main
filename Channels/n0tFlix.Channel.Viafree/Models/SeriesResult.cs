using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace n0tFlix.Channel.Viafree.Models
{
    public class SeriesResult
    {
        //https://viafree-content.mtg-api.com/viafree-content/v1/no/page/series/border-security-americas-front-line/~?device=web

        public static string URL = "https://viafree-content.mtg-api.com/viafree-content/v1/{0}/page/series/{1}?device=web";

        public class Meta
        {
            [JsonProperty("title")]
            public string Title { get; set; }

            [JsonProperty("description")]
            public string Description { get; set; }

            [JsonProperty("image")]
            public string Image { get; set; }

            [JsonProperty("siteName")]
            public string SiteName { get; set; }

            [JsonProperty("siteUrl")]
            public string SiteUrl { get; set; }

            [JsonProperty("route")]
            public string Route { get; set; }

            [JsonProperty("publicPath")]
            public string PublicPath { get; set; }

            [JsonProperty("canonicalUrl")]
            public string CanonicalUrl { get; set; }
        }

        public class AbTesting
        {
            [JsonProperty("defaultVariant")]
            public string DefaultVariant { get; set; }

            [JsonProperty("availableVariants")]
            public IList<string> AvailableVariants { get; set; }
        }

        public class Self
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
        }

        public class Self2
        {
            [JsonProperty("href")]
            public string Href { get; set; }

            [JsonProperty("method")]
            public string Method { get; set; }
        }

        public class NextEpisodeToWatch
        {
            [JsonProperty("href")]
            public string Href { get; set; }

            [JsonProperty("templated")]
            public bool Templated { get; set; }

            [JsonProperty("userContent")]
            public bool UserContent { get; set; }

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

        public class AdInfo
        {
            [JsonProperty("href")]
            public string Href { get; set; }

            [JsonProperty("method")]
            public string Method { get; set; }
        }

        public class Image
        {
            [JsonProperty("href")]
            public string Href { get; set; }

            [JsonProperty("method")]
            public string Method { get; set; }

            [JsonProperty("templated")]
            public bool Templated { get; set; }
        }

        public class Season
        {
            [JsonProperty("href")]
            public string Href { get; set; }

            [JsonProperty("templated")]
            public bool Templated { get; set; }

            [JsonProperty("method")]
            public string Method { get; set; }
        }

        public class Content
        {
            [JsonProperty("href")]
            public string Href { get; set; }

            [JsonProperty("method")]
            public string Method { get; set; }
        }

        public class Links2
        {
            [JsonProperty("self")]
            public Self2 Self { get; set; }

            [JsonProperty("nextEpisodeToWatch")]
            public NextEpisodeToWatch NextEpisodeToWatch { get; set; }

            [JsonProperty("customDimensions")]
            public CustomDimensions CustomDimensions { get; set; }

            [JsonProperty("adInfo")]
            public AdInfo AdInfo { get; set; }

            [JsonProperty("image")]
            public Image Image { get; set; }

            [JsonProperty("season")]
            public Season Season { get; set; }

            [JsonProperty("content")]
            public Content Content { get; set; }

            [JsonProperty("series")]
            public Series2 Series { get; set; }
        }

        public class Season3
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

        public class Links3
        {
            [JsonProperty("season")]
            public Season3 Season { get; set; }
        }

        public class Season2
        {
            [JsonProperty("guid")]
            public string Guid { get; set; }

            [JsonProperty("title")]
            public string Title { get; set; }

            [JsonProperty("tvSeasonNumber")]
            public int TvSeasonNumber { get; set; }

            [JsonProperty("publicPath")]
            public string PublicPath { get; set; }

            [JsonProperty("_links")]
            public Links3 Links { get; set; }
        }

        public class SeriesHeader
        {
            [JsonProperty("guid")]
            public string Guid { get; set; }

            [JsonProperty("channelId")]
            public string ChannelId { get; set; }

            [JsonProperty("title")]
            public string Title { get; set; }

            [JsonProperty("slug")]
            public string Slug { get; set; }

            [JsonProperty("socialLinks")]
            public IList<object> SocialLinks { get; set; }

            [JsonProperty("contextualLabel")]
            public string ContextualLabel { get; set; }

            [JsonProperty("flag")]
            public object Flag { get; set; }

            [JsonProperty("seasonId")]
            public string SeasonId { get; set; }

            [JsonProperty("summary")]
            public string Summary { get; set; }

            [JsonProperty("seasons")]
            public IList<Season2> Seasons { get; set; }
        }

        public class Self3
        {
            [JsonProperty("href")]
            public string Href { get; set; }

            [JsonProperty("method")]
            public string Method { get; set; }
        }

        public class Links4
        {
            [JsonProperty("self")]
            public Self3 Self { get; set; }
        }

        public class AdRequestKeyValues
        {
            [JsonProperty("_pbformat")]
            public string Pbformat { get; set; }

            [JsonProperty("_pbseason")]
            public string Pbseason { get; set; }

            [JsonProperty("testPreroll")]
            public string TestPreroll { get; set; }
        }

        public class Freewheel
        {
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

            [JsonProperty("adRequestKeyValues")]
            public AdRequestKeyValues AdRequestKeyValues { get; set; }
        }

        public class Embedded3
        {
            [JsonProperty("freewheel")]
            public Freewheel Freewheel { get; set; }
        }

        public class AdInfo2
        {
            [JsonProperty("type")]
            public string Type { get; set; }

            [JsonProperty("_links")]
            public Links4 Links { get; set; }

            [JsonProperty("_embedded")]
            public Embedded3 Embedded { get; set; }
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
            public string End { get; set; }
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

        public class Landscape
        {
            [JsonProperty("href")]
            public string Href { get; set; }

            [JsonProperty("method")]
            public string Method { get; set; }

            [JsonProperty("templated")]
            public bool Templated { get; set; }
        }

        public class SeasonImage
        {
            [JsonProperty("href")]
            public string Href { get; set; }

            [JsonProperty("method")]
            public string Method { get; set; }

            [JsonProperty("templated")]
            public bool Templated { get; set; }
        }

        public class Hero
        {
            [JsonProperty("href")]
            public string Href { get; set; }

            [JsonProperty("method")]
            public string Method { get; set; }

            [JsonProperty("templated")]
            public bool Templated { get; set; }
        }

        public class Boxart
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
            [JsonProperty("playerPoster")]
            public PlayerPoster PlayerPoster { get; set; }

            [JsonProperty("landscape")]
            public Landscape Landscape { get; set; }

            [JsonProperty("seasonImage")]
            public SeasonImage SeasonImage { get; set; }

            [JsonProperty("hero")]
            public Hero Hero { get; set; }

            [JsonProperty("boxart")]
            public Boxart Boxart { get; set; }
        }

        public class Tag
        {
            [JsonProperty("guid")]
            public string Guid { get; set; }

            [JsonProperty("title")]
            public string Title { get; set; }

            [JsonProperty("value")]
            public string Value { get; set; }
        }

        public class Channel
        {
            [JsonProperty("guid")]
            public string Guid { get; set; }
        }

        public class Category
        {
            [JsonProperty("guid")]
            public string Guid { get; set; }

            [JsonProperty("title")]
            public string Title { get; set; }

            [JsonProperty("value")]
            public string Value { get; set; }
        }

        public class Subcategory
        {
            [JsonProperty("guid")]
            public string Guid { get; set; }

            [JsonProperty("title")]
            public string Title { get; set; }

            [JsonProperty("value")]
            public string Value { get; set; }
        }

        public class Episode
        {
            [JsonProperty("seriesId")]
            public string SeriesId { get; set; }

            [JsonProperty("seriesTitle")]
            public string SeriesTitle { get; set; }

            [JsonProperty("episodeNumber")]
            public int EpisodeNumber { get; set; }

            [JsonProperty("seasonNumber")]
            public int SeasonNumber { get; set; }

            [JsonProperty("extraMaterialsCount")]
            public int ExtraMaterialsCount { get; set; }
        }

        public class Subtitles
        {
        }

        public class Duration
        {
            [JsonProperty("milliseconds")]
            public int Milliseconds { get; set; }

            [JsonProperty("readable")]
            public string Readable { get; set; }
        }

        public class ImageLandscape
        {
            [JsonProperty("href")]
            public string Href { get; set; }

            [JsonProperty("method")]
            public string Method { get; set; }

            [JsonProperty("templated")]
            public bool Templated { get; set; }
        }

        public class ImagePortrait
        {
            [JsonProperty("href")]
            public string Href { get; set; }

            [JsonProperty("method")]
            public string Method { get; set; }

            [JsonProperty("templated")]
            public bool Templated { get; set; }
        }

        public class LoginRequired
        {
            [JsonProperty("upsellText")]
            public string UpsellText { get; set; }

            [JsonProperty("imageLandscape")]
            public ImageLandscape ImageLandscape { get; set; }

            [JsonProperty("imagePortrait")]
            public ImagePortrait ImagePortrait { get; set; }
        }

        public class Video
        {
            [JsonProperty("duration")]
            public Duration Duration { get; set; }

            [JsonProperty("mediaGuid")]
            public string MediaGuid { get; set; }

            [JsonProperty("loginRequired")]
            public LoginRequired LoginRequired { get; set; }
        }

        public class Broadcast
        {
            [JsonProperty("airAt")]
            public string AirAt { get; set; }

            [JsonProperty("endAt")]
            public string EndAt { get; set; }
        }

        public class Self4
        {
            [JsonProperty("href")]
            public string Href { get; set; }

            [JsonProperty("method")]
            public string Method { get; set; }
        }

        public class Series
        {
            [JsonProperty("href")]
            public string Href { get; set; }

            [JsonProperty("publicPath")]
            public string PublicPath { get; set; }

            [JsonProperty("method")]
            public string Method { get; set; }

            [JsonProperty("templated")]
            public bool Templated { get; set; }

            [JsonProperty("numberOfSeasons")]
            public int NumberOfSeasons { get; set; }

            [JsonProperty("featureBoxPromo")]
            public string FeatureBoxPromo { get; set; }

            [JsonProperty("contextualLabel")]
            public string ContextualLabel { get; set; }

            [JsonProperty("latestVideo")]
            public LatestVideo LatestVideo { get; set; }
        }

        public class Season4
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

            [JsonProperty("method")]
            public string Method { get; set; }
        }

        public class AdInfo3
        {
            [JsonProperty("href")]
            public string Href { get; set; }

            [JsonProperty("method")]
            public string Method { get; set; }
        }

        public class CustomDimensions2
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

        public class StreamProgress
        {
            [JsonProperty("href")]
            public string Href { get; set; }

            [JsonProperty("templated")]
            public bool Templated { get; set; }

            [JsonProperty("method")]
            public string Method { get; set; }
        }

        public class Links5
        {
            [JsonProperty("self")]
            public Self4 Self { get; set; }

            [JsonProperty("series")]
            public Series Series { get; set; }

            [JsonProperty("season")]
            public Season4 Season { get; set; }

            [JsonProperty("play")]
            public Play Play { get; set; }

            [JsonProperty("streamLink")]
            public StreamLink StreamLink { get; set; }

            [JsonProperty("adInfo")]
            public AdInfo3 AdInfo { get; set; }

            [JsonProperty("customDimensions")]
            public CustomDimensions2 CustomDimensions { get; set; }

            [JsonProperty("tracking")]
            public Tracking Tracking { get; set; }

            [JsonProperty("partnerInfo")]
            public object PartnerInfo { get; set; }

            [JsonProperty("streamProgress")]
            public StreamProgress StreamProgress { get; set; }
        }

        public class LatestVideo
        {
            [JsonProperty("id")]
            public string Id { get; set; }

            [JsonProperty("title")]
            public string Title { get; set; }

            [JsonProperty("publishedAt")]
            public string PublishedAt { get; set; }

            [JsonProperty("episodeNumber")]
            public int? EpisodeNumber { get; set; }

            [JsonProperty("seasonNumber")]
            public int? SeasonNumber { get; set; }
        }

        public class Series2
        {
            [JsonProperty("numberOfSeasons")]
            public int NumberOfSeasons { get; set; }

            [JsonProperty("featureBoxPromo")]
            public string FeatureBoxPromo { get; set; }

            [JsonProperty("contextualLabel")]
            public string ContextualLabel { get; set; }

            [JsonProperty("latestVideo")]
            public LatestVideo LatestVideo { get; set; }

            [JsonProperty("href")]
            public string Href { get; set; }

            [JsonProperty("publicPath")]
            public string PublicPath { get; set; }

            [JsonProperty("method")]
            public string Method { get; set; }

            [JsonProperty("templated")]
            public bool Templated { get; set; }
        }

        public class Program
        {
            [JsonProperty("type")]
            public string Type { get; set; }

            [JsonProperty("guid")]
            public string Guid { get; set; }

            [JsonProperty("slug")]
            public string Slug { get; set; }

            [JsonProperty("publicPath")]
            public string PublicPath { get; set; }

            [JsonProperty("title")]
            public string Title { get; set; }

            [JsonProperty("synopsis")]
            public Synopsis Synopsis { get; set; }

            [JsonProperty("parentalRating")]
            public object ParentalRating { get; set; }

            [JsonProperty("availability")]
            public Availability Availability { get; set; }

            [JsonProperty("images")]
            public Images Images { get; set; }

            [JsonProperty("flags")]
            public IList<string> Flags { get; set; }

            [JsonProperty("tags")]
            public IList<Tag> Tags { get; set; }

            [JsonProperty("channels")]
            public IList<Channel> Channels { get; set; }

            [JsonProperty("productKey")]
            public string ProductKey { get; set; }

            [JsonProperty("popularityScore")]
            public object PopularityScore { get; set; }

            [JsonProperty("categories")]
            public IList<Category> Categories { get; set; }

            [JsonProperty("subcategories")]
            public IList<Subcategory> Subcategories { get; set; }

            [JsonProperty("episode")]
            public Episode Episode { get; set; }

            [JsonProperty("subtitles")]
            public Subtitles Subtitles { get; set; }

            [JsonProperty("video")]
            public Video Video { get; set; }

            [JsonProperty("broadcast")]
            public Broadcast Broadcast { get; set; }

            [JsonProperty("combinedTitle")]
            public string CombinedTitle { get; set; }

            [JsonProperty("_links")]
            public Links5 Links { get; set; }

            [JsonProperty("series")]
            public Series2 Series { get; set; }
        }

        public class Season6
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

        public class Links6
        {
            [JsonProperty("season")]
            public Season6 Season { get; set; }
        }

        public class Season5
        {
            [JsonProperty("guid")]
            public string Guid { get; set; }

            [JsonProperty("title")]
            public string Title { get; set; }

            [JsonProperty("tvSeasonNumber")]
            public int TvSeasonNumber { get; set; }

            [JsonProperty("publicPath")]
            public string PublicPath { get; set; }

            [JsonProperty("_links")]
            public Links6 Links { get; set; }
        }

        public class Self5
        {
            [JsonProperty("href")]
            public string Href { get; set; }

            [JsonProperty("method")]
            public string Method { get; set; }
        }

        public class StreamProgress2
        {
            [JsonProperty("href")]
            public string Href { get; set; }

            [JsonProperty("templated")]
            public bool Templated { get; set; }

            [JsonProperty("method")]
            public string Method { get; set; }
        }

        public class Links7
        {
            [JsonProperty("self")]
            public Self5 Self { get; set; }

            [JsonProperty("streamProgress")]
            public IList<StreamProgress2> StreamProgress { get; set; }
        }

        public class MediaFeed
        {
            [JsonProperty("title")]
            public string Title { get; set; }

            [JsonProperty("displayCount")]
            public int DisplayCount { get; set; }

            [JsonProperty("maxSize")]
            public int MaxSize { get; set; }
        }

        public class Synopsis2
        {
            [JsonProperty("long")]
            public string Long { get; set; }

            [JsonProperty("short")]
            public string Short { get; set; }
        }

        public class Availability2
        {
            [JsonProperty("start")]
            public string Start { get; set; }

            [JsonProperty("end")]
            public object End { get; set; }
        }

        public class PlayerPoster2
        {
            [JsonProperty("href")]
            public string Href { get; set; }

            [JsonProperty("method")]
            public string Method { get; set; }

            [JsonProperty("templated")]
            public bool Templated { get; set; }
        }

        public class Landscape2
        {
            [JsonProperty("href")]
            public string Href { get; set; }

            [JsonProperty("method")]
            public string Method { get; set; }

            [JsonProperty("templated")]
            public bool Templated { get; set; }
        }

        public class SeasonImage2
        {
            [JsonProperty("href")]
            public string Href { get; set; }

            [JsonProperty("method")]
            public string Method { get; set; }

            [JsonProperty("templated")]
            public bool Templated { get; set; }
        }

        public class Images2
        {
            [JsonProperty("playerPoster")]
            public PlayerPoster2 PlayerPoster { get; set; }

            [JsonProperty("landscape")]
            public Landscape2 Landscape { get; set; }

            [JsonProperty("seasonImage")]
            public SeasonImage2 SeasonImage { get; set; }
        }

        public class Tag2
        {
            [JsonProperty("guid")]
            public string Guid { get; set; }

            [JsonProperty("title")]
            public string Title { get; set; }

            [JsonProperty("value")]
            public string Value { get; set; }
        }

        public class Channel2
        {
            [JsonProperty("guid")]
            public string Guid { get; set; }
        }

        public class Category2
        {
            [JsonProperty("guid")]
            public string Guid { get; set; }

            [JsonProperty("title")]
            public string Title { get; set; }

            [JsonProperty("value")]
            public string Value { get; set; }
        }

        public class Episode2
        {
            [JsonProperty("seriesId")]
            public string SeriesId { get; set; }

            [JsonProperty("seriesTitle")]
            public string SeriesTitle { get; set; }

            [JsonProperty("episodeNumber")]
            public int EpisodeNumber { get; set; }

            [JsonProperty("seasonNumber")]
            public int SeasonNumber { get; set; }

            [JsonProperty("extraMaterialsCount")]
            public int ExtraMaterialsCount { get; set; }
        }

        public class Subtitles2
        {
        }

        public class Duration2
        {
            [JsonProperty("milliseconds")]
            public int Milliseconds { get; set; }

            [JsonProperty("readable")]
            public string Readable { get; set; }
        }

        public class ImageLandscape2
        {
            [JsonProperty("href")]
            public string Href { get; set; }

            [JsonProperty("method")]
            public string Method { get; set; }

            [JsonProperty("templated")]
            public bool Templated { get; set; }
        }

        public class ImagePortrait2
        {
            [JsonProperty("href")]
            public string Href { get; set; }

            [JsonProperty("method")]
            public string Method { get; set; }

            [JsonProperty("templated")]
            public bool Templated { get; set; }
        }

        public class LoginRequired2
        {
            [JsonProperty("upsellText")]
            public string UpsellText { get; set; }

            [JsonProperty("imageLandscape")]
            public ImageLandscape2 ImageLandscape { get; set; }

            [JsonProperty("imagePortrait")]
            public ImagePortrait2 ImagePortrait { get; set; }
        }

        public class Video2
        {
            [JsonProperty("duration")]
            public Duration2 Duration { get; set; }

            [JsonProperty("mediaGuid")]
            public string MediaGuid { get; set; }

            [JsonProperty("loginRequired")]
            public LoginRequired2 LoginRequired { get; set; }
        }

        public class Broadcast2
        {
            [JsonProperty("airAt")]
            public string AirAt { get; set; }

            [JsonProperty("endAt")]
            public string EndAt { get; set; }
        }

        public class Self6
        {
            [JsonProperty("href")]
            public string Href { get; set; }

            [JsonProperty("method")]
            public string Method { get; set; }
        }

        public class Series3
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

        public class Season7
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

        public class Play2
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

        public class StreamLink2
        {
            [JsonProperty("href")]
            public string Href { get; set; }

            [JsonProperty("method")]
            public string Method { get; set; }
        }

        public class AdInfo4
        {
            [JsonProperty("href")]
            public string Href { get; set; }

            [JsonProperty("method")]
            public string Method { get; set; }
        }

        public class CustomDimensions3
        {
            [JsonProperty("href")]
            public string Href { get; set; }

            [JsonProperty("method")]
            public string Method { get; set; }
        }

        public class Tracking2
        {
            [JsonProperty("href")]
            public string Href { get; set; }

            [JsonProperty("method")]
            public string Method { get; set; }
        }

        public class StreamProgress3
        {
            [JsonProperty("href")]
            public string Href { get; set; }

            [JsonProperty("templated")]
            public bool Templated { get; set; }

            [JsonProperty("method")]
            public string Method { get; set; }
        }

        public class Links8
        {
            [JsonProperty("self")]
            public Self6 Self { get; set; }

            [JsonProperty("series")]
            public Series3 Series { get; set; }

            [JsonProperty("season")]
            public Season7 Season { get; set; }

            [JsonProperty("play")]
            public Play2 Play { get; set; }

            [JsonProperty("streamLink")]
            public StreamLink2 StreamLink { get; set; }

            [JsonProperty("adInfo")]
            public AdInfo4 AdInfo { get; set; }

            [JsonProperty("customDimensions")]
            public CustomDimensions3 CustomDimensions { get; set; }

            [JsonProperty("tracking")]
            public Tracking2 Tracking { get; set; }

            [JsonProperty("partnerInfo")]
            public object PartnerInfo { get; set; }

            [JsonProperty("streamProgress")]
            public StreamProgress3 StreamProgress { get; set; }
        }

        public class Program2
        {
            [JsonProperty("type")]
            public string Type { get; set; }

            [JsonProperty("guid")]
            public string Guid { get; set; }

            [JsonProperty("slug")]
            public string Slug { get; set; }

            [JsonProperty("publicPath")]
            public string PublicPath { get; set; }

            [JsonProperty("title")]
            public string Title { get; set; }

            [JsonProperty("synopsis")]
            public Synopsis2 Synopsis { get; set; }

            [JsonProperty("parentalRating")]
            public object ParentalRating { get; set; }

            [JsonProperty("availability")]
            public Availability2 Availability { get; set; }

            [JsonProperty("images")]
            public Images2 Images { get; set; }

            [JsonProperty("flags")]
            public IList<string> Flags { get; set; }

            [JsonProperty("tags")]
            public IList<Tag2> Tags { get; set; }

            [JsonProperty("channels")]
            public IList<Channel2> Channels { get; set; }

            [JsonProperty("productKey")]
            public string ProductKey { get; set; }

            [JsonProperty("popularityScore")]
            public object PopularityScore { get; set; }

            [JsonProperty("categories")]
            public IList<Category2> Categories { get; set; }

            [JsonProperty("subcategories")]
            public IList<Subcategories> Subcategories { get; set; }

            [JsonProperty("episode")]
            public Episode2 Episode { get; set; }

            [JsonProperty("subtitles")]
            public Subtitles2 Subtitles { get; set; }

            [JsonProperty("video")]
            public Video2 Video { get; set; }

            [JsonProperty("broadcast")]
            public Broadcast2 Broadcast { get; set; }

            [JsonProperty("combinedTitle")]
            public string CombinedTitle { get; set; }

            [JsonProperty("_links")]
            public Links8 Links { get; set; }
        }

        public class Subcategories
        {
            [JsonProperty("guid")]
            public string Guid { get; set; }

            [JsonProperty("title")]
            public string Title { get; set; }

            [JsonProperty("value")]
            public string Value { get; set; }
        }

        public class Embedded4
        {
            [JsonProperty("programs")]
            public IList<Program2> Programs { get; set; }
        }

        public class Block
        {
            [JsonProperty("componentName")]
            public string ComponentName { get; set; }

            [JsonProperty("slug")]
            public string Slug { get; set; }

            [JsonProperty("lazyBlock")]
            public bool LazyBlock { get; set; }

            [JsonProperty("_links")]
            public Links7 Links { get; set; }

            [JsonProperty("dataType")]
            public string DataType { get; set; }

            [JsonProperty("mediaFeed")]
            public MediaFeed MediaFeed { get; set; }

            [JsonProperty("_embedded")]
            public Embedded4 Embedded { get; set; }
        }

        public class Embedded2
        {
            [JsonProperty("adInfo")]
            public AdInfo2 AdInfo { get; set; }

            [JsonProperty("programs")]
            public IList<Program> Programs { get; set; }

            [JsonProperty("seasons")]
            public IList<Season5> Seasons { get; set; }

            [JsonProperty("blocks")]
            public IList<Block> Blocks { get; set; }

            [JsonProperty("channelTabs")]
            public object ChannelTabs { get; set; }
        }

        public class Advertisement
        {
            [JsonProperty("adType")]
            public string AdType { get; set; }
        }

        public class Multi
        {
            [JsonProperty("isTab")]
            public bool IsTab { get; set; }
        }

        public class MediaFeed2
        {
            [JsonProperty("title")]
            public string Title { get; set; }

            [JsonProperty("displayCount")]
            public int DisplayCount { get; set; }

            [JsonProperty("maxSize")]
            public int MaxSize { get; set; }
        }

        public class ViafreeBlock
        {
            [JsonProperty("componentName")]
            public string ComponentName { get; set; }

            [JsonProperty("slug")]
            public string Slug { get; set; }

            [JsonProperty("lazyBlock")]
            public bool LazyBlock { get; set; }

            [JsonProperty("_links")]
            public Links2 Links { get; set; }

            [JsonProperty("dataType")]
            public string DataType { get; set; }

            [JsonProperty("seriesHeader")]
            public SeriesHeader SeriesHeader { get; set; }

            [JsonProperty("_embedded")]
            public Embedded2 Embedded { get; set; }

            [JsonProperty("advertisement")]
            public Advertisement Advertisement { get; set; }

            [JsonProperty("multi")]
            public Multi Multi { get; set; }

            [JsonProperty("mediaFeed")]
            public MediaFeed2 MediaFeed { get; set; }
        }

        public class Embedded
        {
            [JsonProperty("viafreeBlocks")]
            public IList<ViafreeBlock> ViafreeBlocks { get; set; }

            [JsonProperty("programs")]
            public IList<Program> Programs { get; set; }
        }

        public class root
        {
            [JsonProperty("type")]
            public string Type { get; set; }

            [JsonProperty("meta")]
            public Meta Meta { get; set; }

            [JsonProperty("abTesting")]
            public IList<AbTesting> AbTesting { get; set; }

            [JsonProperty("_links")]
            public Links Links { get; set; }

            [JsonProperty("_embedded")]
            public Embedded Embedded { get; set; }
        }

        /// <summary>
        /// Get the page with info about the series
        /// use public path if you want else we use a link like this where you need to set Path to the series name
        /// https://viafree-content.mtg-api.com/viafree-content/v1/no/page/series/alexia-vs-verden
        /// </summary>
        /// <param name="UsePulicPath"></param>
        /// <param name="CountryCode"></param>
        /// <param name="Path"></param>
        /// <returns>Returns all the latest episodes </returns>
        public static async Task<root> GetRoot(ILogger logger, bool UsePulicPath = false, string CountryCode = "no", string Path = "/programmer/dokumentar/alexia-vs-verden")
        {
            logger.LogInformation("Grabbing series by " + Path);
            WebClient client = new WebClient();
            if (UsePulicPath)
            {
                logger.LogInformation("Grabbing publicpath as by " + Path);
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
                logger.LogInformation("Grabbing series by id url at " + String.Format(URL, CountryCode, Path));
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