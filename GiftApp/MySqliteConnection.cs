using GiftApp.Models;
using SQLite;
using System.Linq;

namespace GiftApp;

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

    public ObservableCollection<Person> GetAllPeople(bool isComplete, bool isDeleted = false)
    {
        var testPeople = conn.Table<Person>().Where(p => p.IsDeleted == isDeleted).ToList();
        var people = testPeople.Where(p => p.IsComplete == isComplete).ToList();
        var gifts = conn.Table<Gift>().Where(g => g.IsDeleted == false).ToList();

        foreach (var person in people)
        {
            foreach (var gift in gifts)
            {
                if (gift.PersonId == person.ID)
                {
                    person.ListOfGifts?.Add(gift);
                    FigureAmountSpent(person.ID);
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
        var added = this.conn.Insert(gift) == 0
            ? false
            : true;
        FigureAmountSpent(gift.PersonId);
        return added;
    }

    public bool UpdateGift(Gift gift)
    {
        var updated = this.conn.Update(gift) == 0
            ? false
            : true;
        if (updated)
        {
            var completed = this.conn.Table<Gift>().Where(g => g.PersonId == gift.PersonId && g.Purchased != true).Count() >= 1
                ? false
                : true;
            
            var personToUpdate = GetPersonById(gift.PersonId);
            personToUpdate.IsComplete = completed;
            updated = UpdatePerson(personToUpdate);
            
        }
        return updated;
    }

    public bool UpdatePerson(Person person)
    {
        var updated = this.conn.Update(person) >= 1
            ? true
            : false;

        return updated;
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

        return GetAllPeople(true);
    }

    private void FigureAmountSpent(int id)
    {
        var person = this.conn.Get<Person>(id);
        var gifts = this.conn.Table<Gift>().Where(g => g.PersonId == id);
        //foreach (var _ in from gift in gifts
        //                  where gift.Purchased
        //                  select new { })
        //{
        //    person.AmountSpent += person.AmountSpent;
        //}
        person.AmountSpent = (decimal)0.0;
        foreach (var gift in gifts)
        {
            if (gift.Purchased)
            {
                person.AmountSpent += person.AmountSpent + gift.Price;
            }
        }
    }
}