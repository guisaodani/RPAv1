using Microsoft.Playwright;
using RpaBot.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RpaBot.Bot
{
    public class RpaServices
    {
        private IPage? _page;
        private IBrowser? _browser;

        public async Task StarAsync()
        {
            var playwright = await Playwright.CreateAsync();
            _browser = await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
            {
                Headless = !Settings.ShowBrowser
            });
            _page = await _browser.NewPageAsync();
        }

        public async Task GoDianAsync()
        {
            await _page!.GotoAsync(Settings.UrlDian);
            await _page.WaitForLoadStateAsync(LoadState.NetworkIdle);
            Console.WriteLine("Pagina cargada");
        }

        public async Task CloseAsyns()
        {
            await _browser!.CloseAsync();
        }
    }
}