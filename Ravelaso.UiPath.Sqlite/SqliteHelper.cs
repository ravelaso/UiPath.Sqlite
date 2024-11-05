using System.Data;
using System.Data.Common;
using Microsoft.CodeAnalysis.Emit;
using Microsoft.Data.Sqlite;
using static System.Data.DataTable;
namespace Ravelaso.UiPath.Sqlite;

public class SqliteHelper
{
    public static SqliteConnection CreateConnection(string databasePath)
    {
        Validate.ValidateConnection(databasePath);
        var conn = new SqliteConnection("Data Source=" + databasePath + ";Version=3;");
        conn.Open();
        return conn;
    }

    public static DataTable ExecuteQuery(SqliteConnection conn, string sql)
    {
        if (conn == null && string.IsNullOrWhiteSpace(sql))
            throw new Exception("You need to to fill all parameters");
        if (conn!.State == ConnectionState.Closed)
        {
            conn.Open();
        }
        SqliteCommand cmd = new SqliteCommand(sql, conn);
        using SqliteDataReader dr = cmd.ExecuteReader();
        var dt = new DataTable();
        dt.BeginLoadData();
        dt.Load(dr);
        dt.EndLoadData();
        return dt;
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