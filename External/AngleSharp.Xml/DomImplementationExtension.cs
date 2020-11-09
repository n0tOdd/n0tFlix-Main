namespace AngleSharp.Xml
{
    using AngleSharp.Attributes;
    using AngleSharp.Dom;
    using AngleSharp.Xml.Dom;
    using System;

    /// <summary>
    /// Extensions for the DomImplementation interface.
    /// </summary>
    [DomExposed("DOMImplementation")]
    public static class DomImplementationExtension
    {
        /// <summary>
        /// Creates and returns an XMLDocument.
        /// </summary>
        /// <param name="impl">The implementation instance to extend.</param>
        /// <param name="namespaceUri">
        /// The namespace URI of the document to be created, or null if the
        /// document doesn't belong to one.
        /// </param>
        /// <param name="qualifiedName">
        /// The qualified name, that is an optional prefix and colon plus the
        /// local root element name, of the document to be created.
        /// </param>
        /// <param name="doctype">
        /// DocumentType of the document to be created. It defaults to null.
        /// </param>
        /// <returns>A new document.</returns>
        [DomName("createDocument")]
        public static IXmlDocument CreateDocument(this IImplementation impl, String namespaceUri = null, String qualifiedName = null, IDocumentType doctype = null)
        {
            var document = new XmlDocument();
            var ownerRef = impl.CreateDocumentType("xml", String.Empty, String.Empty);

            if (doctype != null)
            {
                document.AppendChild(doctype);
            }

            if (!String.IsNullOrEmpty(qualifiedName))
            {
                var element = document.CreateElement(namespaceUri, qualifiedName);

                if (element != null)
                {
                    document.AppendChild(element);
                }
            }

            document.BaseUrl = ownerRef.BaseUrl;
            return document;
        }
    }
}
