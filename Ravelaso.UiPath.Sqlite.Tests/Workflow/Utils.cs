using System.Data;
using System.Globalization;
using CsvHelper;
using OfficeOpenXml;

namespace Ravelaso.UiPath.Sqlite.Tests.Workflow;

public static class Utils
{
    public static DataTable ReadExcelAsDataTable(string filePath)
    {
        var dataTable = new DataTable();
        using var package = new ExcelPackage(new FileInfo(filePath));
        var worksheet = package.Workbook.Worksheets[0];

        // Add columns
        foreach (var firstRowCell in worksheet.Cells[1, 1, 1, worksheet.Dimension.End.Column])
        {
            dataTable.Columns.Add(firstRowCell.Text);
        }

        // Add rows
        for (var rowNum = 2; rowNum <= worksheet.Dimension.End.Row; rowNum++)
        {
            var row = worksheet.Cells[rowNum, 1, rowNum, worksheet.Dimension.End.Column];
            var newRow = dataTable.NewRow();
            foreach (var cell in row)
            {
                newRow[cell.Start.Column - 1] = cell.Text;
            }
            dataTable.Rows.Add(newRow);
        }

        return dataTable;
    }
    public static DataTable ReadCsvAsDataTable(string filePath)
    {
        var dataTable = new DataTable();
        using var reader = new StreamReader(filePath);
        using var csv = new CsvReader(reader, CultureInfo.InvariantCulture);
        using var dr = new CsvDataReader(csv);
        dataTable.Load(dr);

        return dataTable;
    }
    
}