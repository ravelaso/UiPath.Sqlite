using System.Data.SQLite;
using Ravelaso.UiPath.Sqlite;
using Ravelaso.UiPath.Sqlite.Tests.Workflow;

public static class Program
{
    public static void Execute()
    {
        var databasePath = @"C:\Data\RPA\TestMatchedFined.db";
        var excelFilePath = @"C:\Data\RPA\Boetes_2024-10-11T16_30_20.3953613Z.csv";
        var tableName = "Fines";
            
        using var conn = new SQLiteConnection($@"Data Source={databasePath}");
        conn.Open();
        var dt_ToDB = Utils.ReadCsvAsDataTable(excelFilePath);
        SqliteHelper.InsertDataTable(conn, dt_ToDB, tableName);
    }
}