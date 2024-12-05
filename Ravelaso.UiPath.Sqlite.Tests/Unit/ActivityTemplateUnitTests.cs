using System.Data.SQLite;
using Ravelaso.UiPath.Sqlite.Tests.Workflow;
using Xunit;

namespace Ravelaso.UiPath.Sqlite.Tests.Unit
{
    public class ActivityTemplateUnitTests
    {
        [Fact]
        public void TestExecuteNonQueryMethod()
        {
            // Arrange
            var databasePath = $@"C:\Data\Dev\testdb.db";
            var dbSource = $"Data Source={databasePath}";
            using var conn = new SQLiteConnection(dbSource);
            var sql = @"DELETE FROM demo_table";
            try
            {
                SqliteHelper.ExecuteNonQuery(sql, conn);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
            }
        }

        [Fact]
        public void InsertDataTableToDb()
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
}