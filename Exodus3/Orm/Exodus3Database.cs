using System;
using System.IO;
using SQLite;

namespace Exodus3
{
    public class Exodus3Database
    {
        private readonly SQLiteAsyncConnection _db;

        public Exodus3Database(string dbPath)
        {
            _db = new SQLiteAsyncConnection(dbPath);
           // _db.
        }
    }
}
