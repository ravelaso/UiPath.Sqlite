using System.Activities;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using Microsoft.Data.Sqlite;

namespace Ravelaso.UiPath.Sqlite
{
    public class ActivityTemplate : CodeActivity 
    {
       
        protected override void Execute(CodeActivityContext context)
        {
            ExecuteInternal();
        }

        public void ExecuteInternal()
        {
            throw new NotImplementedException();
        }
    }

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
        public OutArgument<SqliteConnection> Connection { get; set; }
        
        protected override void Execute(CodeActivityContext context)
        {
            Connection.Set(context, SqliteHelper.CreateConnection(context.GetValue(DatabasePath)));
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
        public InArgument<SqliteConnection> Connection { get; set; }
        
        [RequiredArgument]
        [Category("Database")]
        [Description("The result DataTable")]
        public OutArgument<DataTable> Output { get; set; }
        protected override void Execute(CodeActivityContext context)
        {
            Output.Set(context, SqliteHelper.ExecuteQuery(context.GetValue(Connection), context.GetValue(Query)));
        }
    }
    
}