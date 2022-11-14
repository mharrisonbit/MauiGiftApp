using System;
using GiftApp.Interfaces;
using GiftApp.Models;
using SQLite;
using System.Linq;

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

        public bool AddPerson(Person person)
        {
            return this.conn.Insert(person) == 0
                ? false
                : true;
        }

        public ObservableCollection<Person> GetAllPeople()
        {
            var people = conn.Table<Person>();
            var gifts = conn.Table<Gift>();
            foreach (var (person, gift) in from person in people
                                           from gift in gifts
                                           where gift.PersonId == person.ID
                                           select (person, gift))
            {
                person.GiftIds?.Add(gift);
            }

            return new ObservableCollection<Person>(people);
        }

        public Person GetPersonById(int id)
        {
            return this.conn.Get<Person>(id);
        }

        public bool AddGiftForUser(Gift gift)
        {
            return this.conn.Insert(gift) == 0
                ? false
                : true;
        }

        public void DropAllTables()
        {
            this.conn.DropTable<Gift>();
            this.conn.DropTable<Person>();

            conn.CreateTable<Person>();
            conn.CreateTable<Gift>();
        }
    }
}