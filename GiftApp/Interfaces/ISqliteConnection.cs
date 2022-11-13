using GiftApp.Models;

namespace GiftApp.Interfaces
{
    public interface ISqliteConnection
    {
        public bool AddPerson(Person person);
        public bool AddGift(Gift gift);
        public ObservableCollection<Person> GetAllPeople();
        public void DropAllTables();
    }
}