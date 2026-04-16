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
        private IBrowserContext? _context;

        public async Task StarAsync()
        {
            var playwright = await Playwright.CreateAsync();
            _browser = await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
            {
                Headless = !Settings.ShowBrowser,
                Args = new[]
                {
                    "--disable-blink-features=AutomationControlled",
                    "--no-sandbox",
                    "--disable-infobars",
                    "--disable-dev-shm-usage"
                }
            });

            var context = await _browser.NewContextAsync(new BrowserNewContextOptions
            {
                UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/122.0.0.0 Safari/537.36",
                ViewportSize = new ViewportSize { Width = 1280, Height = 720 }
            });
            _page = await _browser.NewPageAsync();

            await _page.AddInitScriptAsync("Object.defineProperty(navigator, 'webdriver', {get: () => undefined})");
        }

        public async Task GoDianAsync()
        {
            await _page!.GotoAsync(Settings.UrlDian);
            await _page.WaitForLoadStateAsync(LoadState.DOMContentLoaded);
            Console.WriteLine("Pagina cargada");
        }

        public async Task PutIdAsync(string ids)
        {
            await _page!.WaitForSelectorAsync("input[name='vistaConsultaEstadoRUT:formConsultaEstadoRUT:numNit']");
            await _page!.FillAsync("input[name='vistaConsultaEstadoRUT:formConsultaEstadoRUT:numNit']", ids);
            Console.WriteLine($"Cedula : {ids} ingresada");
        }

        //public async Task WaitCaptchaAsync()
        //{
        //    Console.WriteLine("Esperando que Cloudflare se resuelva...");

        //    // Espera hasta que aparezca el check verde de Cloudflare
        //    await _page!.WaitForSelectorAsync(
        //        "div.cf-turnstile-response, [class*='success']",
        //        new PageWaitForSelectorOptions { Timeout = 30000 }
        //    );

        //    Console.WriteLine("Captcha resuelto");
        //}

        public async Task ClickSearchAsync()
        {
            await _page!.ClickAsync("input[name='vistaConsultaEstadoRUT:formConsultaEstadoRUT:btnBuscar']");
        }

        public async Task CloseAsyns()
        {
            await _context!.CloseAsync();
            await _browser!.CloseAsync();
        }

        public async Task<string> GetResultAsync()
        {
            await _page!.WaitForLoadStateAsync(LoadState.DOMContentLoaded);
            var content = await _page.ContentAsync();

            if (content.Contains("No está inscrito en el RUT"))
                return "No está inscrito en el RUT";

            if (content.Contains("REGISTRO ACTIVO"))
                return "registro activo";

            if (content.Contains("REGISTRO CANCELADO"))
                return "registro cancelado";

            if (content.Contains("REGISTRO INACTIVO") || content.Contains(" REGISTRO SUSPENDIDO"))
                return "registro inactivo o suspendido";

            return "resultado desconocido";
        }

        public async Task ClickLimpiarAsync()
        {
            await _page!.ClickAsync("input[src='imagenes/es/botones/botextolimpiar.gif']");
            await _page.WaitForLoadStateAsync(LoadState.DOMContentLoaded);
            Console.WriteLine("Formulario limpiado");
        }
    }
}