using System.Collections.Generic;
using System.IO;
using Factory.Common;
using Factory.ExcelReports.Contracts;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;

namespace Factory.ExcelReports
{
    public class ExcelWriter : IExcelWriter
    {
        public void WriteRepors(IList<string> headers, IList<string> models, IList<decimal> expensePerModel, IList<decimal> incomePerModel)
        {
            var workbook = new XSSFWorkbook();
            var sheet = workbook.CreateSheet("Financial Result");

            // Cell styles and fonts.
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
            var firstRow = sheet.CreateRow(rowIndex);
            rowIndex++;

            for (int i = 0; i < headers.Count; i++)
            {
                var currentCell = firstRow.CreateCell(i);
                currentCell.SetCellValue(headers[i]);
                currentCell.CellStyle = horizonatalAlignmentStyle;
            }

            // write cells values.
            for (int col = 0; col < models.Count; col++)
            {
                var newRow = sheet.CreateRow(rowIndex);
                newRow.CreateCell(0).SetCellValue(models[col]);
                newRow.CreateCell(1).SetCellValue((double)incomePerModel[col]);
                newRow.CreateCell(2).SetCellValue((double)expensePerModel[col]);

                var cell = newRow.CreateCell(3);
                cell.SetCellType(CellType.Formula);
                cell.SetCellFormula(string.Format("PRODUCT(B{0} - C{0})", cell.RowIndex + 1));
                
                rowIndex++;
            }

            sheet.AutoSizeColumn(0);
            sheet.AutoSizeColumn(1);
            sheet.AutoSizeColumn(2);
            sheet.AutoSizeColumn(3);

            // write total sum.
            var lastRow = sheet.CreateRow(rowIndex);
            NPOI.SS.Util.CellRangeAddress merge = new NPOI.SS.Util.CellRangeAddress(rowIndex, rowIndex, 0, 2);
            sheet.AddMergedRegion(merge);

            var totalSumTextCell = lastRow.CreateCell(0);
            totalSumTextCell.SetCellValue("Total sum");
            totalSumTextCell.CellStyle = horizonatalAlignmentStyle;
            totalSumTextCell.CellStyle.SetFont(font);

            var totalSumCell = lastRow.CreateCell(3);
            totalSumCell.SetCellType(CellType.Formula);
            totalSumCell.SetCellFormula(string.Format("SUM(D1:D{0})", totalSumCell.RowIndex));
            totalSumCell.CellStyle = horizonatalAlignmentStyle;
            totalSumCell.CellStyle = thinBorderStyle;
            totalSumCell.CellStyle.SetFont(font);

            // save file.
            System.IO.Directory.CreateDirectory(Constants.FinancialReportPath);

            using (var fileData = new FileStream(Constants.FinancialReportPath + Constants.FinanicalReportFileName, FileMode.Create))
            {
                workbook.Write(fileData);
            }
        }
    }
}
