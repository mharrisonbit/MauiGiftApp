using System;
using GiftApp.Interfaces;
using GiftApp.Models;
using SQLite;

namespace GiftApp
{
    public class MySqliteConnection : SQLiteAsyncConnection, ISqliteConnection
    {
        public SQLiteConnectionWithLock conn { get; private set; }

        public MySqliteConnection(IPlatform platform) : base(Path.Combine(platform.AppData.FullName, "app.db"))
        {
            conn = this.GetConnection();
            conn.CreateTable<Person>();
            conn.CreateTable<Gift>();
        }


        public bool AddGift(Gift gift)
        {
            return true;
        }

        public bool AddPerson(Person person)
        {
            try
            {
                this.conn.Insert(person);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public ObservableCollection<Person> GetAllPeople()
        {
            var people = conn.Table<Person>().ToList();
            return new ObservableCollection<Person>(people);
        }

        // public AsyncTableQuery<YourModel> Logs => this.Table<YourModel>();
    }
}