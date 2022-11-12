using System;
using GiftApp.Interfaces;
using SQLite;

namespace GiftApp
{
    public class MySqliteConnection : SQLiteAsyncConnection, ISqliteConnection
    {
        public MySqliteConnection(IPlatform platform) : base(Path.Combine(platform.AppData.FullName, "app.db"))
        {
            var conn = this.GetConnection();
            // conn.CreateTable<YourModel>();
        }

        public void DoSomething()
        {
            Console.WriteLine("this is a thing");
        }


        // public AsyncTableQuery<YourModel> Logs => this.Table<YourModel>();
    }
}