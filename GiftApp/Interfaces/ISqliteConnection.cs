using GiftApp.Models;

namespace GiftApp.Interfaces
{
    public interface ISqliteConnection
    {
        public bool AddPerson(Person person);
        public bool AddGiftForUser(Gift gift);
        public bool DeleteGiftFromUser(Gift giftId);
        public bool UpdateGift(Gift gift);
        public ObservableCollection<Person> GetAllPeople();
        public ObservableCollection<Person> GetAllCompletedPeople();
        public Person GetPersonById(int id);
        public ObservableCollection<Person> DropAllTables();
    }
}