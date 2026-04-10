using RpaBot.Bot;

var excel = new ExcelService();
var ccIds = excel.GetId();

var bot = new RpaServices();
await bot.StarAsync();
await bot.GoDianAsync();

foreach (var ids in ccIds)
{
    Console.WriteLine($"Procesando cedula : {ids}");
    await bot.GoDianAsync();
    await bot.PutIdAsync(ids);
    Console.WriteLine("debes resolver el captcha, ya estamos trajando para solucionarlo");
    Console.ReadLine();
}

Console.WriteLine("Presiona Enter para cerrar...");
Console.ReadLine();