using System.Activities;
using System.Data.SQLite;
using Xunit;

namespace Ravelaso.UiPath.Sqlite.Tests.Workflow
{
    public class ActivityTemplateWorkflowTests
    {
        [Fact]
        public void TestCreateConnection()
        {
            // Arrange
            var databasePath = @"C:\Data\Dev\database.db";
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
                Assert.Equal(System.Data.ConnectionState.Open, connection.State);

                // Cleanup
                connection.Close();
            });

            Assert.Null(exception);
        }
    }
}