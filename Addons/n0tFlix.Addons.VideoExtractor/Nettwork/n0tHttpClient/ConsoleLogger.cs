using AngleSharp;
using AngleSharp.Js;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace n0tFlix.Addons.VideoExtractor.Nettwork
{
    /// <summary>
    /// This class is used by anglesharp to collect logs writen to the console
    /// </summary>
    public class ConsoleLogger : IConsoleLogger
    {
        private readonly IBrowsingContext ctx;

        public ConsoleLogger(IBrowsingContext ctx)
        {
            this.ctx = ctx;
        }

        public void Log(object[] values)
        {
            values.ToList().ForEach(x => Console.WriteLine(x.ToString()));
            Console.WriteLine("");
            Console.WriteLine("");
        }
    }
}