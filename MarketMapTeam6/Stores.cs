using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace MarketMapTeam6
{
    public class Stores
    {
        [PrimaryKey, AutoIncrement]
        public int Store_ID { get; set; }
        public string Store_Name { get; set; }
        public int Zip_Code { get; set; }
    }
}
