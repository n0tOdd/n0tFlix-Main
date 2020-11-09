using System;
using System.Text;

namespace SharpWebview.Content
{
    public sealed class HtmlContent : IWebviewContent
    {
        private readonly string _html;
        private readonly string _url;

        public HtmlContent(string html, string url)
        {
            _html = html;
            _url = url;
        }

        public string ToWebviewUrl()
        {
            return _url.ToString();
        }
    }
}