using GiftApp.Models;

namespace GiftApp.Interfaces
{
    public interface ISqliteConnection
    {
        public bool AddPerson(Person person);
        public bool AddGiftForUser(Gift gift);
        public bool DeleteGiftFromUser(Gift giftId);
        public ObservableCollection<Person> GetAllPeople();
        public Person GetPersonById(int id);
        public ObservableCollection<Person> DropAllTables();
    }
}