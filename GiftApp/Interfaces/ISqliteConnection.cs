using GiftApp.Models;

namespace GiftApp.Interfaces
{
    public interface ISqliteConnection
    {
        public bool AddPerson(Person person);
        public bool AddGiftForUser(Gift gift);
        public ObservableCollection<Person> GetAllPeople();
        public Person GetPersonById(int id);
        public void DropAllTables();
    }
}