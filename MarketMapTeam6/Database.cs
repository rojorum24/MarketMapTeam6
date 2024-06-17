using MarketMapTeam6.Views;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MarketMapTeam6
{
    public class Database
    {
        //create private connection to db
        private readonly SQLiteAsyncConnection _database;

        public Database(string dbPath)
        {
            //establishes a known path to the local db
            //new db path
            _database = new SQLiteAsyncConnection(dbPath);

            //create tables if one does not already exist
            _database.CreateTableAsync<Categories>();
            _database.CreateTableAsync<ShoppingLists>();
            _database.CreateTableAsync<Items>();
            _database.CreateTableAsync<Stores>();
        }

        //Categories
        public Task<List<Categories>> GetCategoriesAsync()
        {
            //returns current full list of categories when called
            return _database.Table<Categories>().ToListAsync();
        }
        public Task<int> SaveCategoryAsync(Categories category)
        {
            //adds new category to db table of categories
            return _database.InsertAsync(category);
        }

        //Items
        public Task<List<Items>> GetItemsAsync()
        {
            //returns current full list of items when called
            return _database.Table<Items>().ToListAsync();
        }
        public Task<List<Items>> GetItemsByCatAsync(string cat)
        {
            var itemsInCat = _database.Table<Items>().Where(i => i.Item_Category == cat).ToListAsync();
            //returns current full list of items when called
            return itemsInCat;//_database.Table<Items>().ToListAsync();
        }
        public Task<int> SaveItemsAsync(Items items)
        {
            //adds new item to db table of Items
            return _database.InsertAsync(items);
        }

        //Shopping Lists
        public Task<List<ShoppingLists>> GetShoppingListsAsync()
        {
            //returns current full list of shopping lists when called
            return _database.Table<ShoppingLists>().ToListAsync();
        }
        public Task<int> SaveShoppingListsAsync(ShoppingLists shoppingList)
        {
            //adds new shopping list to db table of ShoppingLists
            return _database.InsertAsync(shoppingList);
        }

        //Stores
        public Task<List<Stores>> GetStoresAsync()
        {
            //returns current full list of Stores when called
            return _database.Table<Stores>().ToListAsync();
        }
        public Task<int> SaveStoresAsync(Stores store)
        {
            //adds new Store to db table of Stores
            return _database.InsertAsync(store);
        }
    }
}
