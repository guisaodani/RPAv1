using RpaBot.Bot;

var excel = new ExcelService();
var ccIds = excel.GetId();

var bot = new RpaServices();
await bot.StarAsync();
await bot.GoDianAsync();

int row = 2; // empieza en fila 2 (la primera CC)
var random = new Random();

foreach (var ids in ccIds)
{
    Console.WriteLine($"Procesando cedula: {ids}");
    await bot.GoDianAsync();
    await bot.PutIdAsync(ids);
    await Task.Delay(5000);
    await bot.ClickSearchAsync();
    await Task.Delay(5000);

    var result = await bot.GetResultAsync();
    Console.WriteLine($"Resultado: {result}");

    excel.SaveResult(row, result);
    await bot.ClickLimpiarAsync();
    row++;

    int delay = random.Next(3000, 7000);
    await Task.Delay(delay);
}

Console.WriteLine("Proceso terminado.");
Console.WriteLine("Presiona Enter para cerrar...");
Console.ReadLine();