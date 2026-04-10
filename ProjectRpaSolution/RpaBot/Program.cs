using RpaBot.Bot;

var bot = new RpaServices();
await bot.StarAsync();
await bot.GoDianAsync();

Console.WriteLine("Presiona Enter para cerrar...");
Console.ReadLine();