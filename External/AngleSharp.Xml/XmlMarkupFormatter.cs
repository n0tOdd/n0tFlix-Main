namespace AngleSharp.Xml
{
    using AngleSharp.Dom;
    using AngleSharp.Text;
    using System;

    /// <summary>
    /// Represents the standard XML markup formatter.
    /// </summary>
    public class XmlMarkupFormatter : IMarkupFormatter
    {
        #region Instance

        /// <summary>
        /// An instance of the XmlMarkupFormatter.
        /// </summary>
        public static readonly IMarkupFormatter Instance = new XmlMarkupFormatter();

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets if empty elements should always be considered self-closing.
        /// Otherwise, requires the element to have a self-closing flag.
        /// </summary>
        public Boolean IsAlwaysSelfClosing { get; set; }

        #endregion

        #region Methods

        /// <inheritdoc />
        public virtual String CloseTag(IElement element, Boolean selfClosing)
        {
            var prefix = element.Prefix;
            var name = element.LocalName;
            var tag = !String.IsNullOrEmpty(prefix) ? String.Concat(prefix, ":", name) : name;
            var closed = selfClosing || IsAlwaysSelfClosing && !element.HasChildNodes;
            return closed ? String.Empty : String.Concat("</", tag, ">");
        }

        /// <inheritdoc />
        public virtual String Comment(IComment comment) => String.Concat("<!--", comment.Data, "-->");

        /// <inheritdoc />
        public virtual String Doctype(IDocumentType doctype)
        {
            var publicId = doctype.PublicIdentifier;
            var systemId = doctype.SystemIdentifier;
            var noExternalId = String.IsNullOrEmpty(publicId) && String.IsNullOrEmpty(systemId);
            var externalId = noExternalId ? String.Empty : " " + (String.IsNullOrEmpty(publicId) ?
                String.Concat("SYSTEM \"", systemId, "\"") :
                String.Concat("PUBLIC \"", publicId, "\" \"", systemId, "\""));
            return String.Concat("<!DOCTYPE ", doctype.Name, externalId, ">");
        }

        /// <inheritdoc />
        public virtual String OpenTag(IElement element, Boolean selfClosing)
        {
            var prefix = element.Prefix;
            var temp = StringBuilderPool.Obtain();
            temp.Append(Symbols.LessThan);

            if (!String.IsNullOrEmpty(prefix))
            {
                temp.Append(prefix).Append(Symbols.Colon);
            }

            temp.Append(element.LocalName);

            foreach (var attribute in element.Attributes)
            {
                temp.Append(" ").Append(Attribute(attribute));
            }

            if (selfClosing || (IsAlwaysSelfClosing && !element.HasChildNodes))
            {
                temp.Append(" /");
            }

            temp.Append(Symbols.GreaterThan);
            return temp.ToPool();
        }

        /// <inheritdoc />
        public virtual String Processing(IProcessingInstruction processing)
        {
            var value = String.Concat(processing.Target, " ", processing.Data);
            return String.Concat("<?", value, "?>");
        }

        /// <inheritdoc />
        public virtual String LiteralText(ICharacterData text) => text.Data;

        /// <inheritdoc />
        public virtual String Text(ICharacterData text)
        {
            var content = text.Data;
            return EscapeText(content);
        }

        private String Attribute(IAttr attribute)
        {
            var value = attribute.Value;
            var temp = StringBuilderPool.Obtain();
            temp.Append(attribute.Name);
            temp.Append(Symbols.Equality).Append(Symbols.DoubleQuote);

            for (var i = 0; i < value.Length; i++)
            {
                switch (value[i])
                {
                    case Symbols.Ampersand: temp.Append("&amp;"); break;
                    case Symbols.LessThan: temp.Append("&lt;"); break;
                    case Symbols.DoubleQuote: temp.Append("&quot;"); break;
                    default: temp.Append(value[i]); break;
                }
            }

            return temp.Append(Symbols.DoubleQuote).ToPool();
        }

        #endregion

        #region Helpers

        /// <summary>
        /// Escapes the given text by replacing special characters with their
        /// XML entity (only applies to ampersands and opening angle brackets).
        /// </summary>
        /// <param name="content">The string to alter.</param>
        /// <returns>The altered string.</returns>
        public static String EscapeText(String content)
        {
            var temp = StringBuilderPool.Obtain();

            for (var i = 0; i < content.Length; i++)
            {
                switch (content[i])
                {
                    case Symbols.Ampersand: temp.Append("&amp;"); break;
                    case Symbols.LessThan: temp.Append("&lt;"); break;
                    default: temp.Append(content[i]); break;
                }
            }

            return temp.ToPool();
        }

        #endregion
    }
}
