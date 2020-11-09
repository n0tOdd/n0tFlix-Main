using PlaywrightSharp;
using PlaywrightSharp.Chromium;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace n0tFlix.Addons.VideoExtractor.Nettwork.n0tPlayWrightClient
{
    /// <summary>
    /// Thhis class uses playwright with webkit to open the webpage and let the js run before we return the resulting page source
    /// </summary>
    public class n0tPlayWrightClient
    {
        public IBrowser browser;
        public IPage page;

        public n0tPlayWrightClient()
        {
            Setup().Wait();
        }

        private async Task Setup()
        {
        }

        public async Task<string> OpenPage(string url)
        {
            await Playwright.InstallAsync();
            using var playwright = await Playwright.CreateAsync();
            browser = await playwright.Webkit.LaunchAsync();
            page = await browser.NewPageAsync(default, true, true);
            await page.GoToAsync(url);
            return await page.GetInnerHtmlAsync("*");
        }
    }
}