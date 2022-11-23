using GiftApp.Models;
using SQLite;

namespace GiftApp
{
    public class MySqliteConnection : SQLiteAsyncConnection, ISqliteConnection
    {
        private readonly SQLiteConnection conn;

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
            var tempPeople = conn.Table<Person>().Where(p => p.IsDeleted != true);
            var tempGifts = conn.Table<Gift>().Where(g => g.IsDeleted != true);

            var people = tempPeople.ToList();
            var gifts = tempGifts.ToList();

            foreach (var person in people)
            {
                foreach (var gift in gifts)
                {
                    if (gift.PersonId == person.ID)
                    {
                        person.ListOfGifts?.Add(gift);
                    }
                }
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

        public bool DeleteGiftFromUser(Gift gift)
        {
            gift.IsDeleted = true;
            return  this.conn.InsertOrReplace(gift) == 0
                ? false
                : true;
        }

        public ObservableCollection<Person> DropAllTables()
        {
            this.conn.DropTable<Gift>();
            this.conn.DropTable<Person>();

            conn.CreateTable<Person>();
            conn.CreateTable<Gift>();

            return GetAllPeople();
        }
    }
}