using System.Linq;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using System.Collections.Generic;
using Factory.ExcelReports.Contracts;

namespace Factory.ExcelReports
{
    public class ExcelWriter : IExcelWriter
    {
        public void WriteRepors(IList<string> headers, IDictionary<string, IList<int>> modelFinancialData)
        {
            var workbook = new HSSFWorkbook();
            var sheet = workbook.CreateSheet("Financial Result");

            // Cell styles and fonts
            var horizonatalAlignmentStyle = workbook.CreateCellStyle();
            horizonatalAlignmentStyle.Alignment = HorizontalAlignment.Center;
            var thinBorderStyle = workbook.CreateCellStyle();
            thinBorderStyle.BorderTop = BorderStyle.Thin;
            thinBorderStyle.BorderBottom = BorderStyle.Thin;
            thinBorderStyle.BorderLeft = BorderStyle.Thin;
            thinBorderStyle.BorderRight = BorderStyle.Thin;
            thinBorderStyle.Alignment = HorizontalAlignment.Center;
            var font = workbook.CreateFont();
            font.Boldweight = (short)NPOI.SS.UserModel.FontBoldWeight.Bold;

            var rowIndex = 0;
            var row = sheet.CreateRow(rowIndex);

            var productName = row.CreateCell(0);
            productName.SetCellValue("Product model");
            productName.CellStyle = horizonatalAlignmentStyle;

            var quantity = row.CreateCell(1);
            quantity.SetCellValue("Incomes");
            quantity.CellStyle = horizonatalAlignmentStyle;

            var unitPrice = row.CreateCell(2);
            unitPrice.SetCellValue("Expenses");
            unitPrice.CellStyle = horizonatalAlignmentStyle;

            var sum = row.CreateCell(3);
            sum.SetCellValue("Financial result");
            sum.CellStyle = horizonatalAlignmentStyle;
            rowIndex++;

            // write cells values


        }
    }
}
