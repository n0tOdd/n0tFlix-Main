using System.Xml.Serialization;

namespace n0tFlix.Channel.TWiT
{
    /// <remarks/>
    [XmlType(AnonymousType = true)]
    [XmlRoot(Namespace = "", IsNullable = false)]
    public partial class rss
    {
        /// <remarks/>
        public rssChannel channel { get; set; }

        /// <remarks/>
        [XmlAttribute()]
        public decimal version { get; set; }
    }

    /// <remarks/>
    [XmlType(AnonymousType = true)]
    public partial class rssChannel
    {
        /// <remarks/>
        public string title { get; set; }

        /// <remarks/>
        public string link { get; set; }

        /// <remarks/>
        public string generator { get; set; }

        /// <remarks/>
        public string docs { get; set; }

        /// <remarks/>
        public string language { get; set; }

        /// <remarks/>
        public string copyright { get; set; }

        /// <remarks/>
        [XmlElement(Namespace = "http://backend.userland.com/creativeCommonsRssModule")]
        public string license { get; set; }

        /// <remarks/>
        public string managingEditor { get; set; }

        /// <remarks/>
        public string webMaster { get; set; }

        /// <remarks/>
        public ushort ttl { get; set; }

        /// <remarks/>
        [XmlElement(Namespace = "http://purl.org/rss/1.0/modules/syndication/")]
        public string updatePeriod { get; set; }

        /// <remarks/>
        [XmlElement(Namespace = "http://purl.org/rss/1.0/modules/syndication/")]
        public byte updateFrequency { get; set; }

        /// <remarks/>
        public string lastBuildDate { get; set; }

        /// <remarks/>
        public string pubDate { get; set; }

        /// <remarks/>
        public string category { get; set; }

        /// <remarks/>
        public rssChannelImage image { get; set; }

        /// <remarks/>
        [XmlElement("link", Namespace = "http://www.w3.org/2005/Atom")]
        public link link1 { get; set; }

        /// <remarks/>
        [XmlElement(Namespace = "http://www.itunes.com/dtds/podcast-1.0.dtd")]
        public string author { get; set; }

        /// <remarks/>
        [XmlElement(Namespace = "http://www.itunes.com/dtds/podcast-1.0.dtd")]
        public string subtitle { get; set; }

        /// <remarks/>
        [XmlElement(Namespace = "http://www.itunes.com/dtds/podcast-1.0.dtd")]
        public string summary { get; set; }

        /// <remarks/>
        public string description { get; set; }

        /// <remarks/>
        [XmlElement(Namespace = "http://www.itunes.com/dtds/podcast-1.0.dtd")]
        public string keywords { get; set; }

        /// <remarks/>
        [XmlElement(Namespace = "http://www.itunes.com/dtds/podcast-1.0.dtd")]
        public string @explicit { get; set; }

        /// <remarks/>
        [XmlElement(Namespace = "http://www.itunes.com/dtds/podcast-1.0.dtd")]
        public owner owner { get; set; }

        /// <remarks/>
        [XmlElement(Namespace = "http://www.itunes.com/dtds/podcast-1.0.dtd")]
        public string block { get; set; }

        /// <remarks/>
        [XmlElement("image", Namespace = "http://www.itunes.com/dtds/podcast-1.0.dtd")]
        public image image1 { get; set; }

        /// <remarks/>
        [XmlElement("category", Namespace = "http://www.itunes.com/dtds/podcast-1.0.dtd")]
        public category[] category1 { get; set; }

        /// <remarks/>
        [XmlElement("item")]
        public rssChannelItem[] item { get; set; }
    }

    /// <remarks/>
    [XmlType(AnonymousType = true)]
    public partial class rssChannelImage
    {
        /// <remarks/>
        public string title { get; set; }

        /// <remarks/>
        public string url { get; set; }

        /// <remarks/>
        public string link { get; set; }

        /// <remarks/>
        public byte width { get; set; }

        /// <remarks/>
        public byte height { get; set; }
    }

    /// <remarks/>
    [XmlType(AnonymousType = true, Namespace = "http://www.w3.org/2005/Atom")]
    [XmlRoot(Namespace = "http://www.w3.org/2005/Atom", IsNullable = false)]
    public partial class link
    {
        /// <remarks/>
        [XmlAttribute()]
        public string href { get; set; }

        /// <remarks/>
        [XmlAttribute()]
        public string type { get; set; }

        /// <remarks/>
        [XmlAttribute()]
        public string rel { get; set; }
    }

    /// <remarks/>
    [XmlType(AnonymousType = true, Namespace = "http://www.itunes.com/dtds/podcast-1.0.dtd")]
    [XmlRoot(Namespace = "http://www.itunes.com/dtds/podcast-1.0.dtd", IsNullable = false)]
    public partial class owner
    {
        /// <remarks/>
        public string name { get; set; }

        /// <remarks/>
        public string email { get; set; }
    }

    /// <remarks/>
    [XmlType(AnonymousType = true, Namespace = "http://www.itunes.com/dtds/podcast-1.0.dtd")]
    [XmlRoot(Namespace = "http://www.itunes.com/dtds/podcast-1.0.dtd", IsNullable = false)]
    public partial class image
    {
        /// <remarks/>
        [XmlAttribute()]
        public string href { get; set; }
    }

    /// <remarks/>
    [XmlType(AnonymousType = true, Namespace = "http://www.itunes.com/dtds/podcast-1.0.dtd")]
    [XmlRoot(Namespace = "http://www.itunes.com/dtds/podcast-1.0.dtd", IsNullable = false)]
    public partial class category
    {
        /// <remarks/>
        [XmlElement("category")]
        public categoryCategory category1 { get; set; }

        /// <remarks/>
        [XmlAttribute()]
        public string text { get; set; }
    }

    /// <remarks/>
    [XmlType(AnonymousType = true, Namespace = "http://www.itunes.com/dtds/podcast-1.0.dtd")]
    public partial class categoryCategory
    {
        /// <remarks/>
        [XmlAttribute()]
        public string text { get; set; }
    }

    /// <remarks/>
    [XmlType(AnonymousType = true)]
    public partial class rssChannelItem
    {
        /// <remarks/>
        public string title { get; set; }

        /// <remarks/>
        public string pubDate { get; set; }

        /// <remarks/>
        public string comments { get; set; }

        /// <remarks/>
        [XmlElement(Namespace = "http://www.itunes.com/dtds/podcast-1.0.dtd")]
        public string author { get; set; }

        /// <remarks/>
        [XmlElement("author")]
        public string author1 { get; set; }

        /// <remarks/>
        public string category { get; set; }

        /// <remarks/>
        public string description { get; set; }

        /// <remarks/>
        public rssChannelItemGuid guid { get; set; }

        /// <remarks/>
        [XmlElement(Namespace = "http://www.itunes.com/dtds/podcast-1.0.dtd")]
        public string subtitle { get; set; }

        /// <remarks/>
        [XmlElement(Namespace = "http://www.itunes.com/dtds/podcast-1.0.dtd")]
        public string summary { get; set; }

        /// <remarks/>
        [XmlElement(Namespace = "http://www.itunes.com/dtds/podcast-1.0.dtd")]
        public string @explicit { get; set; }

        /// <remarks/>
        [XmlElement(Namespace = "http://www.itunes.com/dtds/podcast-1.0.dtd")]
        public string duration { get; set; }

        /// <remarks/>
        public string link { get; set; }

        /// <remarks/>
        public rssChannelItemEnclosure enclosure { get; set; }

        /// <remarks/>
        [XmlElement(Namespace = "http://search.yahoo.com/mrss/")]
        public content content { get; set; }
    }

    /// <remarks/>
    [XmlType(AnonymousType = true)]
    public partial class rssChannelItemGuid
    {
        /// <remarks/>
        [XmlAttribute()]
        public bool isPermaLink { get; set; }

        /// <remarks/>
        [XmlText()]
        public string Value { get; set; }
    }

    /// <remarks/>
    [XmlType(AnonymousType = true)]
    public partial class rssChannelItemEnclosure
    {
        /// <remarks/>
        [XmlAttribute()]
        public string url { get; set; }

        /// <remarks/>
        [XmlAttribute()]
        public uint length { get; set; }

        /// <remarks/>
        [XmlAttribute()]
        public string type { get; set; }
    }

    /// <remarks/>
    [XmlType(AnonymousType = true, Namespace = "http://search.yahoo.com/mrss/")]
    [XmlRoot(Namespace = "http://search.yahoo.com/mrss/", IsNullable = false)]
    public partial class content
    {
        /// <remarks/>
        [XmlElement("credit")]
        public contentCredit[] credit { get; set; }

        /// <remarks/>
        [XmlElement("rating")]
        public contentRating[] rating { get; set; }

        /// <remarks/>
        [XmlAttribute()]
        public string url { get; set; }

        /// <remarks/>
        [XmlAttribute()]
        public uint fileSize { get; set; }

        /// <remarks/>
        [XmlAttribute()]
        public string type { get; set; }

        /// <remarks/>
        [XmlAttribute()]
        public string medium { get; set; }
    }

    /// <remarks/>
    [XmlType(AnonymousType = true, Namespace = "http://search.yahoo.com/mrss/")]
    public partial class contentCredit
    {
        /// <remarks/>
        [XmlAttribute()]
        public string role { get; set; }

        /// <remarks/>
        [XmlText()]
        public string Value { get; set; }
    }

    /// <remarks/>
    [XmlType(AnonymousType = true, Namespace = "http://search.yahoo.com/mrss/")]
    public partial class contentRating
    {
        /// <remarks/>
        [XmlAttribute()]
        public string scheme { get; set; }

        /// <remarks/>
        [XmlText()]
        public string Value { get; set; }
    }

}