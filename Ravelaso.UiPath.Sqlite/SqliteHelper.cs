using System.Data;
using System.Data.SQLite;

namespace Ravelaso.UiPath.Sqlite;

public static class SqliteHelper
{
    public static SQLiteConnection CreateConnection(string databasePath)
    {
        var conn = new SQLiteConnection($"Data Source={databasePath}");
        conn.ParseViaFramework = true;
        conn.Open();
        return conn;
    }

    public static void CloseConnection(SQLiteConnection conn)
    {
        if(conn.State != ConnectionState.Closed) conn?.Close();
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

    public static void InsertData(SQLiteConnection conn, DataTable dt)
    {
        if (conn == null)
            throw new Exception("You need to to fill all parameters");
        if (conn!.State == ConnectionState.Closed)
        {
            conn.Open();
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
}


public static class Validate
{
    public static void ValidateConnection(string fullPath)
    {
        if (!File.Exists(fullPath) || fullPath.IndexOf('.') <= 0 ||
            (fullPath.Split('.').LastOrDefault()?.ToLower() != "db" &&
             fullPath.Split('.').LastOrDefault()?.ToLower() != "sqlite" &&
             fullPath.Split('.').LastOrDefault()?.ToLower() != "sqlite3" &&
             fullPath.Split('.').LastOrDefault()?.ToLower() != "db3"))
            throw new Exception("The path to the database is invalid. Inform the fully qualified path. The supported databases are: .db, .db3, .sqlite, .sqlite3");

    }

    public static void ValidateDatabaseName(string fullPath)
    {
        var directory = fullPath.Substring(0, fullPath.LastIndexOf('\\'));
        if (!Directory.Exists(directory))
            throw new Exception("The specified folder is invalid");

        if (fullPath.IndexOf('.') <= 0 ||
            (fullPath.Split('.').LastOrDefault()?.ToLower() != "db" &&
             fullPath.Split('.').LastOrDefault()?.ToLower() != "sqlite" &&
             fullPath.Split('.').LastOrDefault()?.ToLower() != "sqlite3" &&
             fullPath.Split('.').LastOrDefault()?.ToLower() != "db3"))
            throw new Exception("The supported database extensions are: .db, .db3, .sqlite, .sqlite3");

    }
}