namespace AngleSharp.Xml
{
    using System;
    using System.IO;

    /// <summary>
    /// Extensions for the markup formattable elements.
    /// </summary>
    public static class MarkupFormatterExtensions
    {
        /// <summary>
        /// Returns the serialization of the object model guided by the
        /// XML markup formatter.
        /// </summary>
        /// <param name="markup">The markup to serialize.</param>
        /// <returns>The source code snippet.</returns>
        public static String ToXml(this IMarkupFormattable markup) =>
            markup.ToHtml(XmlMarkupFormatter.Instance);

        /// <summary>
        /// Serializes the object model guided by the XML markup formatter.
        /// </summary>
        /// <param name="markup">The markup to serialize.</param>
        /// <param name="writer">The output target of the serialization.</param>
        public static void ToXml(this IMarkupFormattable markup, TextWriter writer) =>
            markup.ToHtml(writer, XmlMarkupFormatter.Instance);

        /// <summary>
        /// Returns the serialization of the object model guided by the
        /// auto selected (XML, XHTML, HTML) markup formatter.
        /// </summary>
        /// <param name="markup">The markup to serialize.</param>
        /// <returns>The source code snippet.</returns>
        public static String ToMarkup(this IMarkupFormattable markup) =>
            markup.ToHtml(new AutoSelectedMarkupFormatter());

        /// <summary>
        /// Serializes the object model guided by the auto selected (XML,
        /// XHTML, HTML) markup formatter.
        /// </summary>
        /// <param name="markup">The markup to serialize.</param>
        /// <param name="writer">The output target of the serialization.</param>
        public static void ToMarkup(this IMarkupFormattable markup, TextWriter writer) =>
            markup.ToHtml(writer, new AutoSelectedMarkupFormatter());
    }
}
