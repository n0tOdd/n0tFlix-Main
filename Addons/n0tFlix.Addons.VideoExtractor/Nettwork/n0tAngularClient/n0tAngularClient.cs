using AngleSharp;
using AngleSharp.Browser;
using AngleSharp.Dom;
using AngleSharp.Io.Network;
using Esprima.Ast;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace n0tFlix.Addons.VideoExtractor.Nettwork.n0tAngularClient
{
    public class n0tAngularClient
    {
        #region Variables for playing with the html code

        public IBrowsingContext browser;
        public IDocument Document;
        public IConfiguration configuration;
        public INavigationHandler navigationHandler;
        public string BaseURL = "https://youtube.com";

        private int TimeOutInSec = 60; //Denne setter timeout på requestene våres så vi slipper vente 60 sek mellom hvert forsøk
        private System.Net.CookieContainer cookieContainer;
        private HttpClientHandler httpClientHandler;
        private HttpMessageHandler HttpMessageHandler;
        public HttpClient client;
        public HttpClientRequester httpClientRequester;
        private HttpRequestHeaders HeaderCollection;

        #endregion Variables for playing with the html code

        internal async Task Setupn0tAngularClient()
        {
            cookieContainer = new System.Net.CookieContainer();
            httpClientHandler = new HttpClientHandler()
            {
                AutomaticDecompression = System.Net.DecompressionMethods.GZip & System.Net.DecompressionMethods.Deflate,
                AllowAutoRedirect = true,
                CheckCertificateRevocationList = false,
                ClientCertificateOptions = ClientCertificateOption.Automatic,
                MaxAutomaticRedirections = 50,
                UseCookies = true,
                ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator,
                CookieContainer = cookieContainer,
                SslProtocols = System.Security.Authentication.SslProtocols.None,
            };
        }
    }
}