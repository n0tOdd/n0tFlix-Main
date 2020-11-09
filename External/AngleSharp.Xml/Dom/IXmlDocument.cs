namespace AngleSharp.Xml.Dom
{
    using AngleSharp.Attributes;
    using AngleSharp.Dom;
    using System;

    /// <summary>
    /// The interface represent an XML document.
    /// </summary>
    [DomName("XMLDocument")]
    public interface IXmlDocument : IDocument
    {
        /// <summary>
        /// Gets if the document is actually valid.
        /// </summary>
        Boolean IsValid { get; }
    }
}
