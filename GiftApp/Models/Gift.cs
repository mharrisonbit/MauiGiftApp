using System;
using SQLite;
using SQLiteNetExtensions.Attributes;

namespace GiftApp.Models
{
    public class Gift
	{
        [PrimaryKey, AutoIncrement]
        public int ID { get; private set; }
        public decimal? Price { get; set; }
        public string? FoundAt { get; set; }
        public string? GiftName { get; set; }
        public bool Purchased { get; set; }
        public bool IsDeleted { get; set; }

        [ForeignKey(typeof(Person))]
        public int PersonId { get; set; }
    }
}

