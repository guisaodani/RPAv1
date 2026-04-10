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
    await Task.Delay(5000);
    //await bot.WaitCaptchaAsync();
    await bot.ClickSearchAsync();
    await Task.Delay(5000);
}

Console.WriteLine("Presiona Enter para cerrar...");
Console.ReadLine();