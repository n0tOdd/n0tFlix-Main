namespace AngleSharp
{
    using AngleSharp.Dom;
    using AngleSharp.Io;
    using AngleSharp.Svg.Dom;
    using AngleSharp.Xml.Dom;
    using AngleSharp.Xml.Parser;
    using System;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// XML related extensions for the configuration.
    /// </summary>
    public static class XmlConfigurationExtensions
    {
        /// <summary>
        /// Adds XML capabilities to the configuration.
        /// </summary>
        /// <param name="configuration">The configuration to extend.</param>
        /// <returns>The new configuration.</returns>
        public static IConfiguration WithXml(this IConfiguration configuration)
        {
            var documentFactory = configuration.Services.OfType<DefaultDocumentFactory>().FirstOrDefault();

            if (documentFactory != null)
            {
                documentFactory.Unregister(MimeTypeNames.Xml);
                documentFactory.Unregister(MimeTypeNames.ApplicationXml);
                documentFactory.Unregister(MimeTypeNames.Svg);
                documentFactory.Register(MimeTypeNames.Xml, LoadXmlAsync);
                documentFactory.Register(MimeTypeNames.ApplicationXml, LoadXmlAsync);
                documentFactory.Register(MimeTypeNames.Svg, LoadSvgAsync);
            }

            return configuration.WithOnly<IXmlParser>(ctx => new XmlParser(ctx));
        }

        private static Task<IDocument> LoadXmlAsync(IBrowsingContext context, CreateDocumentOptions options, CancellationToken cancellationToken)
        {
            var parser = context.GetService<IXmlParser>() ?? throw new InvalidOperationException("The IXmlParser service has been removed. Cannot continue.");
            var document = new XmlDocument(context, options.Source);
            document.Setup(options.Response, options.ContentType, options.ImportAncestor);
            context.NavigateTo(document);
            return parser.ParseDocumentAsync(document, cancellationToken);
        }

        private static Task<IDocument> LoadSvgAsync(IBrowsingContext context, CreateDocumentOptions options, CancellationToken cancellationToken)
        {
            var parser = context.GetService<IXmlParser>() ?? throw new InvalidOperationException("The IXmlParser service has been removed. Cannot continue.");
            var document = new SvgDocument(context, options.Source);
            document.Setup(options.Response, options.ContentType, options.ImportAncestor);
            context.NavigateTo(document);
            return parser.ParseDocumentAsync(document, cancellationToken);
        }
    }
}
