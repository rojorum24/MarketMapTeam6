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

            //performs a search for items based on the category passed
        public async Task<IEnumerable<Items>> CategoryAsync(string cat)
        {
            Task<List<Items>> task = GetItemsByCatAsync(cat);
            return await task;
        }
        public Task<List<Items>> GetItemsByCatAsync(string cat)
        {
            //returns current full list of items where category matches passed value when called
            return _database.Table<Items>().Where(i => i.Item_Category == cat).ToListAsync();
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
    
        public Task<int> DeleteItemsAsync<T>()
        {
            //deletes item from db table of Items
            return _database.DeleteAllAsync<Items>();
        }

        //Runs upon navigation to StorePage: clears all tables and repopulates them with default data
        public async void StartDatabase()
        {
            List<string> msgList = new List<string>();
            string msg = "";

            List<Categories> Categories = new List<Categories>()
            {
                new Categories(){Category_Name = "Dairy"},
                new Categories(){Category_Name = "Produce"},
                new Categories(){Category_Name = "Frozen"},
                new Categories(){Category_Name = "Baked"},
                new Categories(){Category_Name = "Pantry"},
                new Categories(){Category_Name = "Nonfood"},
                new Categories(){Category_Name = "Meat"}
            };

            foreach (var category in Categories)
            {
                try
                {
                    await App.Database.SaveCategoryAsync(new Categories
                    {
                        Category_Name = category.Category_Name
                    });
                }
                catch (SQLiteException ex)
                {
                    msgList.Add(category.Category_Name);
                }
            }

            foreach (var category in msgList)
            {
                msg += category + "\n";
            }

            List<Items> Items = new List<Items>()
            {
                //Dairy Items
                new Items(){Item_Description = "Butter", Item_Category = "Dairy"},
                new Items(){Item_Description = "Cheddar Cheese", Item_Category = "Dairy"},
                new Items(){Item_Description = "Heavy Cream", Item_Category = "Dairy"},
                new Items(){Item_Description = "Milk", Item_Category = "Dairy"},
                new Items(){Item_Description = "Yogurt", Item_Category = "Dairy"},
                
                //Produce Items
                new Items(){Item_Description = "Carrots", Item_Category = "Produce"},
                new Items(){Item_Description = "Peas", Item_Category = "Produce"},
                new Items(){Item_Description = "Broccoli", Item_Category = "Produce"},
                new Items(){Item_Description = "Celery", Item_Category = "Produce"},
                new Items(){Item_Description = "Lettuce", Item_Category = "Produce"},
                new Items(){Item_Description = "Kale", Item_Category = "Produce"},
                new Items(){Item_Description = "Apples", Item_Category = "Produce"},
                new Items(){Item_Description = "Oranges", Item_Category = "Produce"},
                new Items(){Item_Description = "Bananas", Item_Category = "Produce"},
                new Items(){Item_Description = "Grapes", Item_Category = "Produce"},
                new Items(){Item_Description = "Strawberries", Item_Category = "Produce"},
                new Items(){Item_Description = "Pears", Item_Category = "Produce"},
                new Items(){Item_Description = "Peaches", Item_Category = "Produce"},
                new Items(){Item_Description = "Lemons", Item_Category = "Produce"},
                new Items(){Item_Description = "Limes", Item_Category = "Produce"},
                
                //Frozen Items
                new Items(){Item_Description = "Pizza", Item_Category = "Frozen"},
                new Items(){Item_Description = "Fries", Item_Category = "Frozen"},
                new Items(){Item_Description = "Ice Cream", Item_Category = "Frozen"},
                new Items(){Item_Description = "Chicken Fingers", Item_Category = "Frozen"},
                new Items(){Item_Description = "Breakfast Sandwiches", Item_Category = "Frozen"},
                new Items(){Item_Description = "Frozen Peas", Item_Category = "Frozen"},
                new Items(){Item_Description = "Frozen Corn", Item_Category = "Frozen"},
                new Items(){Item_Description = "Frozen Broccoli", Item_Category = "Frozen"},
                new Items(){Item_Description = "Frozen Mixed Vegetables", Item_Category = "Frozen"},
                
                //Baked Items
                new Items(){Item_Description = "White Bread", Item_Category = "Baked"},
                new Items(){Item_Description = "Whole Wheat Bread", Item_Category = "Baked"},
                new Items(){Item_Description = "Bagels", Item_Category = "Baked"},
                new Items(){Item_Description = "Croissants", Item_Category = "Baked"},
                new Items(){Item_Description = "Donuts", Item_Category = "Baked"},
                new Items(){Item_Description = "Muffins", Item_Category = "Baked"},
                new Items(){Item_Description = "Cookies", Item_Category = "Baked"},
                new Items(){Item_Description = "Bagettes", Item_Category = "Baked"},
                new Items(){Item_Description = "Rolls", Item_Category = "Baked"},
                new Items(){Item_Description = "Cakes", Item_Category = "Baked"},
                new Items(){Item_Description = "Pies", Item_Category = "Baked"},
                
                //Pantry Items
                new Items(){Item_Description = "Ketchup", Item_Category = "Pantry"},
                new Items(){Item_Description = "Mustard", Item_Category = "Pantry"},
                new Items(){Item_Description = "Mayo", Item_Category = "Pantry"},
                new Items(){Item_Description = "BBQ Sauce", Item_Category = "Pantry"},
                new Items(){Item_Description = "Hot Sauce", Item_Category = "Pantry"},
                new Items(){Item_Description = "Relish", Item_Category = "Pantry"},
                new Items(){Item_Description = "Soy Sauce", Item_Category = "Pantry"},
                new Items(){Item_Description = "Peanut Butter", Item_Category = "Pantry"},
                new Items(){Item_Description = "Cereal", Item_Category = "Pantry"},
                new Items(){Item_Description = "Rice", Item_Category = "Pantry"},
                new Items(){Item_Description = "Pasta", Item_Category = "Pantry"},
                new Items(){Item_Description = "Oats", Item_Category = "Pantry"},
                new Items(){Item_Description = "Jelly", Item_Category = "Pantry"},
                new Items(){Item_Description = "Cereal", Item_Category = "Pantry"},
                
                //Nonfood Items
                new Items(){Item_Description = "Toilet Paper", Item_Category = "Nonfood"},
                new Items(){Item_Description = "Paper Towels", Item_Category = "Nonfood"},
                new Items(){Item_Description = "Tissues", Item_Category = "Nonfood"},
                new Items(){Item_Description = "Trash Bags", Item_Category = "Nonfood"},
                new Items(){Item_Description = "Dish Soap", Item_Category = "Nonfood"},
                new Items(){Item_Description = "Laundry Detergent", Item_Category = "Nonfood"},

                //Meat Items
                new Items(){Item_Description = "Beef", Item_Category = "Meat"},
                new Items(){Item_Description = "Pork", Item_Category = "Meat"},
                new Items(){Item_Description = "Fish", Item_Category = "Meat"},
                new Items(){Item_Description = "Chicken", Item_Category = "Meat"},
                new Items(){Item_Description = "Sausage", Item_Category = "Meat"},
                new Items(){Item_Description = "Bacon", Item_Category = "Meat"},
                new Items(){Item_Description = "Ham", Item_Category = "Meat"},
                new Items(){Item_Description = "Turkey", Item_Category = "Meat"},
                new Items(){Item_Description = "Lamb", Item_Category = "Meat"}

            };

            //Enter all items at once, ignoring existing items
            // ****** adds previously entered items to the list to print into the alert
            foreach (var item in Items)
            {
                try
                {
                    await App.Database.SaveItemsAsync(new Items
                    {
                        Item_Description = item.Item_Description,
                        Item_Category = item.Item_Category
                    });
                }
                catch (SQLiteException ex)
                {
                    msgList.Add(item.Item_Description);
                }
            }

            // Determine if msgList contains more than a handlful of items already included in the database
            // If so, I dont know, maybe just say "hey, new stuff was added" or something
            // Possibly just change this to show list of new items instead of existing items

            foreach (var item in msgList)
            {
                msg += item + "\n";
            }
            //await DisplayAlert("Notice", "Database Loaded", "OK");
        }
    }
}
