﻿using System;
using System.Collections.Generic;
using System.Text;
using static MarketMapTeam6.Views.ShoppingListPage;

namespace MarketMapTeam6
{
    public class Frozen : IShoppingItem
    {
        public string Item_Description { get; set; }
        public string Item_Category { get; set; }
        public bool IsSelected { get; set; }
    }
}
