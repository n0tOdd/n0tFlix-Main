using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AngleSharp.Css;
using HtmlAgilityPack;

namespace n0tFlix.Addons.VideoExtractor.Nettwork
{
    public class n0tAgilityClient
    {
        public HtmlWeb web;

        public HtmlDocument Document;

        public n0tAgilityClient()
        {
            web = new HtmlWeb();
        }

        public void OpenDocument(string URL)
        {
            Document = web.Load("http://www.c-sharpcorner.com");
        }
    }
}