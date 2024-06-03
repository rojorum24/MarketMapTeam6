using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace MarketMapTeam6
{
    public class Categories
    {
        [PrimaryKey, AutoIncrement]
        public int Category_ID { get; set; }
        public string Category_Description { get; set; }
    }
}
