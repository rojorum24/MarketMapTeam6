using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace MarketMapTeam6
{
    public class Items
    {
        //[PrimaryKey, AutoIncrement]
        //public int Item_ID { get; set; }
        [Unique]
        public string Item_Description { get; set; }
        public string Item_Category { get; set; }
        public bool IsSelected { get; set; }
    }
}
