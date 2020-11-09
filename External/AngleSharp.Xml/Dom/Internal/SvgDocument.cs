namespace AngleSharp.Svg.Dom
{
    using AngleSharp.Dom;
    using AngleSharp.Io;
    using AngleSharp.Text;
    using AngleSharp.Xml;
    using System;

    /// <summary>
    /// Represents a document node that contains only SVG nodes.
    /// </summary>
    sealed class SvgDocument : Document, ISvgDocument
    {
        #region Fields

        private readonly IElementFactory<Document, SvgElement> _factory;

        #endregion

        #region ctor

        internal SvgDocument(IBrowsingContext context, TextSource source)
            : base(context ?? BrowsingContext.New(), source)
        {
            ContentType = MimeTypeNames.Svg;
            _factory = Context.GetFactory<IElementFactory<Document, SvgElement>>();
        }

        internal SvgDocument(IBrowsingContext context = null)
            : this(context, new TextSource(String.Empty))
        {
        }

        #endregion

        #region Properties

        public override IElement DocumentElement => RootElement;

        public ISvgSvgElement RootElement => this.FindChild<ISvgSvgElement>();

        public override IEntityProvider Entities => Context.GetProvider<IEntityProvider>() ?? XmlEntityProvider.Resolver;

        #endregion

        #region Methods

        public override Element CreateElementFrom(String name, String prefix, NodeFlags flags = NodeFlags.None) => _factory.Create(this, name, prefix, flags);

        public override Node Clone(Document owner, Boolean deep)
        {
            var node = new SvgDocument(Context, new TextSource(Source.Text));
            CloneDocument(node, deep);
            return node;
        }

        #endregion

        #region Helpers

        protected override String GetTitle()
        {
            var title = RootElement.FindChild<ISvgTitleElement>();
            return title?.TextContent.CollapseAndStrip() ?? base.GetTitle();
        }

        protected override void SetTitle(String value)
        {
            ISvgElement title = RootElement.FindChild<ISvgTitleElement>();

            if (title == null)
            {
                title = _factory.Create(this, TagNames.Title);
                RootElement.AppendChild(title);
            }

            title.TextContent = value;
        }

        #endregion
    }
}
