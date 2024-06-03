using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace MarketMapTeam6
{
    public class Items
    {
        [PrimaryKey, AutoIncrement]
        public int Item_ID { get; set; }
        public string Item_Description { get; set; }
        //Need to learn to integrate foreign key. foreign key declaration not functioning
        public string Item_Category { get; set; }
    }
}
