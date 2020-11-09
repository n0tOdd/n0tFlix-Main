using AngleSharp.Dom;
using AngleSharp.Html.Dom;
using AngleSharp.Js.Dom;
using n0tFlix.Addons.VideoExtractor.Interfaces;
using n0tFlix.Addons.VideoExtractor.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace n0tFlix.Addons.VideoExtractor.Extractors
{
    public class dplay : IExtractor
    {
        public string Name => "dPlay";
        public string Description => "A " + Name + " extractor for use on dplay.no|se|dk";

        private Regex URLmatcher = new Regex("dplay.(no|se|dk)", RegexOptions.Compiled);
        private string baseurl = string.Empty;
        private n0tFlix.Addons.VideoExtractor.Nettwork.n0tWebClient client;
        private string InfoURL = "https://disco-api.dplay.no/content/shows/{0}";//Her skal show navnet inn for å hente extra data
        private string EpisodeInfoURl = "https://disco-api.dplay.no/content/videos?include=primaryChannel,show&filter[videoType]=EPISODE&filter[show.id]={0}&page[size]=100&sort=seasonNumber,episodeNumber,-earliestPlayableStart";//her skal id fra url over brukes for episode info
        private string PlayBackURL = "https://disco-api.dplay.no/playback/v2/videoPlaybackInfo/{0}?usePreAuth=true";//Her skal episode info fra over brukes for stream info

        //Variabler som skal lagres etter login responsen
        private string realm = string.Empty;//bruk result fra login for å sette denne

        private string id = string.Empty;//bruk result fra login for å sette denne
        private string token = string.Empty;//bruk result fra login for å sette denne

        public bool CheckURL(string url)
        {
            Match match = URLmatcher.Match(url);
            if (match.Success)
            {
                Uri uri = new Uri(url);
                baseurl = "https://" + uri.Host.Replace("www.", "");
                client = new Nettwork.n0tWebClient();
                return true;
            }
            return false;
        }

        public Task<List<DownloadInfo>> Extract(string url)
        {
            return null;
        }

        public async Task<bool> Login(string id, string pw)
        {
            if (string.IsNullOrEmpty(id) || string.IsNullOrEmpty(pw))
            {
                Console.WriteLine("No id/pw detected, working without account logged in");
                return true;
            }
            //url = https://disco-api.dplay.no/login
            await client.OpenDocument(baseurl + "/myaccount/login");
            var username = client.Document.GetElementById("username") as IHtmlInputElement;

            username.DoClick();
            username.DoFocus();
            username.Value = id;
            var pass = client.Document.GetElementById("password") as IHtmlInputElement;
            pass.DoClick();
            pass.DoFocus();
            pass.Value = pw;
            var login = client.Document.GetElementsByClassName("button login__submit-button").First() as IHtmlInputElement;
            login.DoFocus();
            login.DoClick();
            await client.Document.WaitUntilAvailable();
            await client.Document.WaitForReadyAsync();

            var me = await client.httpClient.GetStringAsync("https://disco-api.dplay.no/users/me");
            Thread.Sleep(10);
            /*login data = {"credentials":{"username":"{0}","password":"{1}"}}
            var postdata = new System.Net.Http.StringContent(string.Format("{{\"credentials\":{{\"username\":\"{0}\",\"password\":\"{1}\"}}}}", id, pw));
            postdata.Headers.Add("Referer", baseurl + "/myaccount/login");
            var result = await client.httpClient.PostAsync("https://disco-api.dplay.no/login", postdata);
            if (result.IsSuccessStatusCode)
            {
                string json = await result.Content.ReadAsStringAsync();
                dynamic OB = JObject.Parse(json);

                //Hent realm fra json svaret, kan være uvikttig sjekk siden
                //client.httpClient.DefaultRequestHeaders.Add("x-disco-params","realm=" + OB["realm"]);
                return true;
            }
            else
            {
                Console.WriteLine("Failed to login here, wrong login info?");
                return false;
            }*/
            return false;
        }
    }
}