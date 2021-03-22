using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;

namespace SQLiteORM
{
    class SQLiteConnector
    {
        static private string _pathTofile;
        static private string _connectionString;
        static private SQLiteConnection _qLiteConnection;
        public static SQLiteConnection Connection
        {
            get
            {
                return _qLiteConnection;
            }
        }
        public static string ConnString
        {
            get
            {
                return _connectionString;
            }
        }
        public static string PathToDataBase
        {
            get
            {
                return _pathTofile;
            }
            set
            {
                if (File.Exists(value))
                {
                    _pathTofile = value;
                    _connectionString = $"DataSource ={_pathTofile}; Version = 3";
                    _qLiteConnection = new SQLiteConnection(_connectionString);

                } else
                {
                    throw new SQLiteException("Database file  is not exists");
                }
            }
        }
        public static void CreateDatabaseSource(string pathToDatabase)
        {
            try
            {
                SQLiteConnection.CreateFile(pathToDatabase);
                PathToDataBase = pathToDatabase;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
