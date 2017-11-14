using SQLite.Net.Interop;

namespace Exodus3.Core
{
    public class App
    {
        public const string DB_FILE_NAME = "E3dbSQLite.db3";
        public const string BACKEND_URL = "http://localhost:5000";

        static Exodus3Database _db;
        static string _dbFilePath;
        static ISQLitePlatform _sqlitePlatform;

        public static bool UseMockDataStore = true;


        public static Exodus3Database Database
        {
            get 
            {
                if (_db == null)
                    _db = new Exodus3Database(_dbFilePath, _sqlitePlatform);
                return _db;
            }
        }

        public static void Init(string dbFilePath, ISQLitePlatform platform)
        {
            _dbFilePath = dbFilePath;
            _sqlitePlatform = platform;
        }

    }
}
