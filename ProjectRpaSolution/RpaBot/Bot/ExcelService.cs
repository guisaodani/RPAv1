using OfficeOpenXml;
using RpaBot.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RpaBot.Bot
{
    public class ExcelService
    {
        public List<string> GetId()
        {
            ExcelPackage.License.SetNonCommercialPersonal("RpaBot");
            var ids = new List<string>();

            using var package = new ExcelPackage(new FileInfo(Settings.PathExcel));
            var sheet = package.Workbook.Worksheets[0];
            int file = 2;

            while (sheet.Cells[file, 1].Value != null)
            {
                var value = sheet.Cells[file, 1].Value.ToString()!.Trim();
                if (!string.IsNullOrEmpty(value))
                {
                    ids.Add(value);
                    file++;
                }
            }
            Console.WriteLine($"{ids.Count} Cedulas encontradas en excel");
            return ids;
        }

        public void SaveResult(int row, string result)
        {
            ExcelPackage.License.SetNonCommercialPersonal("RpaBot");

            using var package = new ExcelPackage(new FileInfo(Settings.PathExcel));
            var sheet = package.Workbook.Worksheets[0];

            sheet.Cells[row, 2].Value = result;
            package.Save();
        }
    }
}