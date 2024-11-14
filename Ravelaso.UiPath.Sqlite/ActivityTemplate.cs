using System.Activities;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Diagnostics;
using Ravelaso.UiPath.Sqlite.Helpers;
using UiPath.Robot.Activities.Api;

namespace Ravelaso.UiPath.Sqlite;

[DisplayName("CreateConnection")]
[Description("Creates a connection object for the SQLite database")]
public class CreateConnection : CodeActivity
{
    [RequiredArgument]
    [Category("Database")]
    [Description("The name of the SQLite database")]
    public InArgument<string> DatabasePath { get; set; }
    
    [RequiredArgument]
    [Category("Database")]
    [Description("The database connection object")]
    public OutArgument<SQLiteConnection> Connection { get; set; }

    protected override void Execute(CodeActivityContext context)
    {
        Connection.Set(context, SqliteHelper.CreateConnection(context.GetValue(DatabasePath)));
    }
}

public class CloseConnection : CodeActivity
{
    [RequiredArgument]
    [Category("Database")]
    [Description("The database connection object")]
    public InArgument<SQLiteConnection> Connection { get; set; }
    protected override void Execute(CodeActivityContext context)
    {
        try
        {
            SqliteHelper.CloseConnection(context.GetValue(Connection));
        }
        catch (Exception ex)
        {
            var message = new LogMessage
            {
                EventType = TraceEventType.Error,
                Message = ex.Message
            };
            context.GetExecutorRuntime().LogMessage(message);
        }
    }
}

public class InsertData : CodeActivity
{
    
    protected override void Execute(CodeActivityContext context)
    {
        throw new NotImplementedException();
    }
}
[DisplayName("ExecuteQuery")]
[Description("Returns a DataTable from a query to the database connection")]
public class ExecuteQuery : CodeActivity
{
    [RequiredArgument]
    [Category("Database")]
    [Description("The SQL Statement")]
    public InArgument<string> Query { get; set; }

    [RequiredArgument]
    [Category("Database")]
    [Description("The database connection object")]
    public InArgument<SQLiteConnection> Connection { get; set; }
    
    [RequiredArgument]
    [Category("Database")]
    [Description("The result DataTable")]
    public OutArgument<DataTable> Output { get; set; }

    protected override void Execute(CodeActivityContext context)
    {
        Output.Set(context, SqliteHelper.ExecuteQuery(context.GetValue(Connection), context.GetValue(Query)));
    }
}

[DisplayName("ExecuteNonQuery")]
[Description("Executes a non query command using a database connection")]
public class ExecuteNonQuery : CodeActivity
{
    [RequiredArgument]
    [Category("Database")]
    [Description("The sql non query command")]
    public InArgument<string> Command { get; set; }

    [RequiredArgument]
    [Category("Database")]
    [Description("The database connection object")]
    public InArgument<SQLiteConnection> Connection { get; set; }
    
    protected override void Execute(CodeActivityContext context)
    {
        SqliteHelper.ExecuteNonQuery(context.GetValue(Command), context.GetValue(Connection));
    }
}