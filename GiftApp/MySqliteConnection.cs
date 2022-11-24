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
        return this.conn.Update(gift) == 0
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