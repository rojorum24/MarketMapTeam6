using System;
using System.Collections.Generic;
using System.Text;
using static MarketMapTeam6.Views.ShoppingListPage;

namespace MarketMapTeam6
{
    public class Frozen : IShoppingItem
    {
        public string Name { get; set; }
        public string Category { get; set; }
        public bool IsSelected { get; set; }
    }
}
