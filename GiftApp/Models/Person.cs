using SQLite;
using SQLiteNetExtensions.Attributes;

namespace GiftApp.Models
{
    public class Person
	{
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public int? Age { get; set; }
		public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? MiddleName { get; set; }
        public DateTime Birthday { get; set; }
        public string? BirthdateText { get; set; }
        public bool IsDeleted { get; set; }

        [OneToMany]
        public ObservableCollection<Gift>? ListOfGifts { get; set; } = new();
    }
}

