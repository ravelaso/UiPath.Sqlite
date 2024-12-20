using System.Data;
using System.Data.SQLite;
using System.Text;

namespace Ravelaso.UiPath.Sqlite;

public static class SqliteHelper
{
    static SqliteHelper()
    {
        DependencyLoader.LoadInterop();
    }
    public static SQLiteConnection CreateConnection(string databasePath)
    {
        var conn = new SQLiteConnection($"Data Source={databasePath}");
        conn.ParseViaFramework = true;
        conn.Open();
        return conn;
    }
    public static void CloseConnection(SQLiteConnection conn)
    {
        if(conn.State != ConnectionState.Closed) conn.Close();
    }
    public static DataTable ExecuteQuery(SQLiteConnection conn, string sql)
    {
        if (conn == null && string.IsNullOrWhiteSpace(sql))
            throw new Exception("You need to to fill all parameters");
        if (conn!.State == ConnectionState.Closed)
        {
            conn.Open();
        }
        var cmd = new SQLiteCommand(sql, conn);
        using var dr = cmd.ExecuteReader();
        var dt = new DataTable();
        dt.BeginLoadData();
        dt.Load(dr);
        dt.EndLoadData();
        return dt;
    }
    public static void InsertDataTable(SQLiteConnection conn, DataTable dt, string tableName)
    {
        if (conn == null)
        {
            throw new Exception("You need to provide a db connection.");
        }

        if (dt.Rows.Count.Equals(0))
        {
            throw new Exception("DataTable rows cannot be 0");
        }

        if (conn.State == ConnectionState.Closed)
        {
            conn.Open();
        }
        using var transaction = conn.BeginTransaction();
        try
        {
            
            using (var command = conn.CreateCommand())
            {
                var columnNames = new StringBuilder();
                var parameterNames = new StringBuilder();
                
                foreach (DataColumn column in dt.Columns)
                {
                    if (columnNames.Length > 0)
                    {
                        columnNames.Append(", ");
                        parameterNames.Append(", ");
                    }
                    columnNames.Append($"\"{column.ColumnName}\"");
                    parameterNames.Append($"@{SanitizeParameterName(column.ColumnName)}");
                }
                
                command.CommandText = $"INSERT INTO {tableName} ({columnNames}) VALUES ({parameterNames})";
                
                foreach (DataRow row in dt.Rows)
                {
                    command.Parameters.Clear();
                    
                    foreach (DataColumn column in dt.Columns)
                    {
                        command.Parameters.AddWithValue($"@{SanitizeParameterName(column.ColumnName)}", row[column]);
                    }
                    command.ExecuteNonQuery();
                }
            }
            
            transaction.Commit();
        }
        catch 
        {
            transaction.Rollback();
            throw;
        }
    }
    public static void ExecuteNonQuery(string command, SQLiteConnection conn)
    {
        if (conn == null && string.IsNullOrWhiteSpace(command))
            throw new Exception("You need to to fill all parameters");
        if (conn!.State == ConnectionState.Closed)
        {
            conn.Open();
        }
        var cmd = new SQLiteCommand(command, conn);
        cmd.ExecuteNonQuery();
    }
    
    private static string SanitizeParameterName(string columnName)
    {
        // Define a list of characters to be removed or replaced
        var invalidChars = new[] { '[', ']', ' ', '-', '.', ',', ';', ':', '!', '@', '#', '$', '%', '^', '&', '*', '(', ')', '=', '+', '{', '}', '|', '\\', '/', '<', '>', '?', '~', '`' };

        // Replace invalid characters with an underscore
        columnName = invalidChars.Aggregate(columnName, (current, ch) => current.Replace(ch, '_'));

        // Ensure the parameter name starts with a valid character
        if (char.IsDigit(columnName[0]))
        {
            columnName = "_" + columnName;
        }

        return columnName;
    }
}