using System;
using SQLite;

namespace GiftApp.Models
{
	public class Gift
	{
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public decimal? Price { get; set; }
        public string? GiftName { get; set; }
        public bool Purchased { get; set; }
    }
}

