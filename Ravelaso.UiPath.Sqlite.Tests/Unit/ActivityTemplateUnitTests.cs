using System.Data.SQLite;
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
    }
}