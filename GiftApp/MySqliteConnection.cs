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
            var gifts = conn.Table<Gift>();
            foreach (var person in people)
            {
                foreach (var gift in gifts)
                {
                    if (gift.PersonId == person.ID)
                    {
                        person.GiftIds?.Add(gift);
                    }
                }
            }

            return new ObservableCollection<Person>(people);
        }

        public void DropAllTables()
        {
            this.conn.DropTable<Gift>();
            this.conn.DropTable<Person>();

            conn.CreateTable<Person>();
            conn.CreateTable<Gift>();
        }

        // public AsyncTableQuery<YourModel> Logs => this.Table<YourModel>();
    }
}