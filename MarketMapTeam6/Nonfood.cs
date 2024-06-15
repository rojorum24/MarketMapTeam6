using System;
using System.Collections.Generic;
using System.Text;
using static MarketMapTeam6.Views.ShoppingListPage;

namespace MarketMapTeam6
{
    public class Nonfood : IShoppingItem
    {
        public string Name { get; set; }
        public int Selected { get; set; }
    }
}
