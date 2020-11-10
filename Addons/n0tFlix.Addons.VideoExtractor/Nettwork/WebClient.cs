using AngleSharp;
using AngleSharp.Css;
using AngleSharp.Dom;
using AngleSharp.Io;
using AngleSharp.Js;
using AngleSharp.Io.Network;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using AngleSharp.Css.RenderTree;
using AngleSharp.Browser;
using System.Threading;
using System.Net.Cache;
using System.Net.Security;

namespace n0tFlix.Addons.VideoExtractor.Nettwork
{
    public class n0tWebClient
    {
        public IBrowsingContext browser;
        public IDocument Document;
        public IConfiguration configuration;
        public INavigationHandler navigationHandler;
        public string BaseURL = "https://youtube.com";

        private int TimeOutInSec = 60; //Denne setter timeout på requestene våres så vi slipper vente 60 sek mellom hvert forsøk
        public System.Net.CookieContainer cookieContainer;
        public HttpClientHandler httpClientHandler;
        public HttpClient httpClient;
        public HttpClientRequester httpClientRequester;

        public n0tWebClient()
        {
            //
            RunSetup().ConfigureAwait(true).GetAwaiter();
        }

        private async Task RunSetup()
        {
            cookieContainer = new System.Net.CookieContainer();

            httpClientHandler = new HttpClientHandler()
            {
                AutomaticDecompression = System.Net.DecompressionMethods.GZip & DecompressionMethods.Deflate,
                AllowAutoRedirect = false,
                CheckCertificateRevocationList = false,
                ClientCertificateOptions = ClientCertificateOption.Automatic,
                MaxAutomaticRedirections = 50,
                //   PreAuthenticate = true,
                UseCookies = true,
                // Credentials = credentials,
                //UseDefaultCredentials = true,
                ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator,
                CookieContainer = cookieContainer,
                SslProtocols = System.Security.Authentication.SslProtocols.None,
            };

            httpClient = new HttpClient(httpClientHandler, false)
            {
                Timeout = TimeSpan.FromSeconds(TimeOutInSec),
            };
            httpClient.DefaultRequestHeaders.Add("Accept", "application/json,text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8");
            httpClient.DefaultRequestHeaders.Add("Accept-Encoding", "gzip, deflate, br");
            httpClient.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:82.0) Gecko/20100101 Firefox/82.0");// Mozilla/5.0 (Windows NT 6.2; WOW64; rv:19.0) Gecko/20100101 Firefox/19.0");
            httpClient.DefaultRequestHeaders.Add("Accept-Charset", "ISO-8859-1");
            httpClient.DefaultRequestHeaders.Add("TE", "trailers");
            httpClient.DefaultRequestHeaders.Add("DNT", "1");
            httpClient.DefaultRequestHeaders.Add("Connection", "keep-alive");
            SetGeoByPassIP();
            httpClientRequester = new HttpClientRequester(httpClient);
            // httpClientRequester.Requested += HttpClientRequester_Requested;
            // FileRequester fileRequester = new FileRequester();

            // DataRequester dataRequester = new DataRequester();
            //            DefaultHttpRequester defaultHttpRequester = new DefaultHttpRequester("Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:82.0) Gecko/20100101 Firefox/82.0", new Action<HttpWebRequest>(async (ss) =>
            //          {
            //        }));

            configuration = AngleSharp.Configuration.Default
                  .WithTemporaryCookies().WithCulture(CultureInfo.CurrentCulture).WithMetaRefresh()
                  .WithJs().WithEventLoop()
                  .WithRequester(httpClientRequester)//.WithRequester(dataRequester)//.WithRequester(fileRequester)
                  .WithRequesters(httpClientHandler)
                  .WithDefaultLoader(new LoaderOptions()
                  {
                      IsResourceLoadingEnabled = true,
                      IsNavigationDisabled = false,
                      Filter = (rr) =>
                      {
                          if (rr.Address.Href.Contains(".mp4", StringComparison.OrdinalIgnoreCase) || rr.Address.Href.Contains("m3u8", StringComparison.OrdinalIgnoreCase) || rr.Address.Href.Contains("manifest", StringComparison.OrdinalIgnoreCase) || rr.Address.Href.Contains("mp3", StringComparison.OrdinalIgnoreCase))
                          {
                              Console.WriteLine(rr.Address.Href);
                          }
                          return true;
                      }
                  })
                  .WithConsoleLogger(ctx => new ConsoleLogger(ctx))//We should probably make something better here
                  .WithCss().WithRenderDevice(new DefaultRenderDevice
                  {
                      DeviceHeight = 1080,
                      DeviceWidth = 1920,
                      ViewPortWidth = 1920,
                      ViewPortHeight = 1080,
                      Category = DeviceCategory.Screen,
                      IsScripting = true,
                  });
            browser = AngleSharp.BrowsingContext.New(configuration);
            //  IResponse response = await httpClientRequester.RequestAsync(new AngleSharp.Io.Request() { Address = new Url(BaseURL) }, CancellationToken.None);
            // Document = await browser.OpenAsync();
        }

        public string GetSource(string link) //constructor
        {
            WebRequest Request = WebRequest.Create(link);
            WebResponse Response = Request.GetResponse();
            System.IO.StreamReader sr = new System.IO.StreamReader(Response.GetResponseStream());
            string pagesource = sr.ReadToEnd();
            return pagesource;
        }

        private IResponse MakeRequest(string url)
        {
            return null;
        }

        private void HttpClientRequester_Requested(object sender, AngleSharp.Dom.Events.Event ev)
        {
            ev.Init(ev.Type, ev.IsBubbling, ev.IsCancelable);
            Document.Dispatch(ev);
        }

        /// <summary>
        /// Sets the x-forwarded-for header to a random ip if iPAddress= null, else it uses the ip use selected
        /// function added just because this is what youtube-dl uses for geo-bypass base
        /// </summary>
        /// <param name="iPAddress"></param>
        /// <returns></returns>
        public void SetGeoByPassIP(IPAddress iPAddress = null)
        {
            if (httpClient != null)
            {
                if (httpClient.DefaultRequestHeaders.Contains("X-Forwarded-For"))
                    httpClient.DefaultRequestHeaders.Remove("X-Forwarded-For");
                if (iPAddress == null)
                {
                    Random r = new Random(DateTime.Now.Millisecond);
                    httpClient.DefaultRequestHeaders.Add("X-Forwarded-For", r.Next(0, 255) + "." + r.Next(0, 255) + "." + r.Next(0, 255) + "." + r.Next(0, 255));
                }
                else
                {
                    httpClient.DefaultRequestHeaders.Add("X-Forwarded-For", iPAddress.ToString());
                }
            }
        }

        /// <summary>
        /// Used to open a new webpage and set the public Document to this page
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public async Task OpenDocument(string url)
        {
            string source = httpClient.GetStringAsync(url).Result;
            Document = browser.OpenAsync(x => x.Content(source).Address(url)).Result;
        }

        public string GetSourceString()
        {
            if (Document == null)
                return string.Empty;
            if (string.IsNullOrEmpty(Document.Source.Text))
                return string.Empty;

            return Document.Source.Text;
        }
    }
}