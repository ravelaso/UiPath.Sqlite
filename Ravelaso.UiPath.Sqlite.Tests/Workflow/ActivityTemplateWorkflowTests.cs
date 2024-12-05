using System.Activities;
using System.Data;
using System.Data.SQLite;
using Xunit;
using Xunit.Abstractions;

namespace Ravelaso.UiPath.Sqlite.Tests.Workflow
{
    public class ActivityTemplateWorkflowTests
    {
        private readonly ITestOutputHelper _testOutputHelper;

        public ActivityTemplateWorkflowTests(ITestOutputHelper testOutputHelper)
        {
            _testOutputHelper = testOutputHelper;
        }

        [Fact]
        public void TestCreateConnection()
        {
            // Arrange
            var databasePath = @"C:\Data\Dev\testdb.db";
            var createConnectionActivity = new CreateConnection
            {
                DatabasePath = new InArgument<string>(databasePath)
            };

            var workflowInvoker = new WorkflowInvoker(createConnectionActivity);

            // Act & Assert
            var exception = Record.Exception(() =>
            {
                var outputs = workflowInvoker.Invoke();
                Assert.NotNull(outputs);
                Assert.True(outputs.ContainsKey("Connection"));

                var connection = outputs["Connection"] as SQLiteConnection;
                Assert.NotNull(connection);
                Assert.Equal(ConnectionState.Open, connection.State);

                // Cleanup
                connection.Close();
            });

            Assert.Null(exception);
        }

        [Fact]
        public void TestExecuteNonQuery()
        {
            // Arrange
            var databasePath = $@"C:\Data\Dev\testdb.db";
            var dbSource = $"Data Source={databasePath}";
            using var conn = new SQLiteConnection(dbSource);
            conn.Open();
            var sql = @"DELETE FROM demo_table";
            var executeNonQuery = new ExecuteNonQuery()
            {
                Connection = new InArgument<SQLiteConnection>(conn),
                Command = new InArgument<string>(sql)
            };
            var workflowInvoker = new WorkflowInvoker(executeNonQuery);
                
            // Act & Assert
            var exception = Record.Exception(() =>
            {
                var outputs = workflowInvoker.Invoke();
                Assert.NotNull(outputs);
                    
            });
            if (exception != null) _testOutputHelper.WriteLine(exception.Message);
            conn.Close();
        }

        [Fact]
        public void TestInsertDataTableCsv()
        {
            // Arrange
            var databasePath = @"C:\Data\RPA\TestMatchedFined.db";
            var excelFilePath = @"C:\Data\RPA\Boetes_2024-10-11T16_30_20.3953613Z.csv";
            var tableName = "Fines";

            var dt = Utils.ReadCsvAsDataTable(excelFilePath);

            var dbSource = $"Data Source={databasePath}";
            using var dbConn = new SQLiteConnection(dbSource);
            dbConn.Open();
            var insertDataTableActivity = new InsertDataTable()
            {
                Connection = new InArgument<SQLiteConnection>(dbConn),
                TableName = new InArgument<string>(tableName),
                DataTable = new InArgument<DataTable>(dt)
            };
            var workflowInvoker = new WorkflowInvoker(insertDataTableActivity);

            var exception = Record.Exception(() =>
            {
                var outputs = workflowInvoker.Invoke();
                Assert.NotNull(outputs);
            });
        }

        [Fact]
        public void TestInsertDataTableExcel()
        {
            // Arrange
            var databasePath = @"C:\Data\Dev\testdb.db";
            var excelFilePath = @"C:\Data\Dev\demo_table_1k.xlsx";
            var tableName = "demo_table";

            var dt = Utils.ReadExcelAsDataTable(excelFilePath);

            var dbSource = $"Data Source={databasePath}";
            using var dbConn = new SQLiteConnection(dbSource);
            dbConn.Open();
            var insertDataTableActivity = new InsertDataTable()
            {
                Connection = new InArgument<SQLiteConnection>(dbConn),
                TableName = new InArgument<string>(tableName),
                DataTable = new InArgument<DataTable>(dt)
            };
            var workflowInvoker = new WorkflowInvoker(insertDataTableActivity);

            var exception = Record.Exception(() =>
            {
                var outputs = workflowInvoker.Invoke();
                Assert.NotNull(outputs);
            });
        }
    }
}