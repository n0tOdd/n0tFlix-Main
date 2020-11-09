namespace AngleSharp.Xml.Dom
{
    using AngleSharp.Dom;
    using AngleSharp.Text;
    using AngleSharp.Xml.Parser;
    using System;

    /// <summary>
    /// The object representation of an XMLElement.
    /// </summary>
    sealed class XmlElement : Element
    {        
        #region ctor

        public XmlElement(Document owner, String name, String prefix = null, String namespaceUri = null, NodeFlags flags = NodeFlags.None)
            : base(owner, name, prefix, namespaceUri, flags)
        {
        }

        #endregion

        #region Properties

        internal String IdAttribute
        {
            get;
            set;
        }

        #endregion

        #region Methods

        /// <inheritdoc />
        public override IElement ParseSubtree(String html)
        {
            var context = ((IElement)this).Owner?.Context;
            var source = new TextSource(html);
            var document = new XmlDocument(context, source);
            var parser = new XmlDomBuilder(document);
            var options = new XmlParserOptions
            {
                IsSuppressingErrors = true,
            };
            return parser.ParseFragment(options, this).DocumentElement;
        }

        /// <inheritdoc />
        public override Node Clone(Document owner, Boolean deep)
        {
            var node = new XmlElement(owner, LocalName, Prefix);
            CloneElement(node, owner, deep);
            node.IdAttribute = IdAttribute;
            return node;
        }

        #endregion
    }
}
