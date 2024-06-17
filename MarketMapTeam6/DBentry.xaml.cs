using SQLite;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MarketMapTeam6
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DBentry : ContentPage
    {
        public DBentry()
        {
            InitializeComponent();
        }
        protected override async void OnAppearing()
        {
            base.OnAppearing();
        }
        
        async void DeleteDatabaseItems(object sender, EventArgs e)
        {
            await App.Database.DeleteItemsAsync<Items>();
            CollectionView.ItemsSource = await App.Database.GetItemsAsync();
        }
        async void LoadCategories(object sender, EventArgs e)
        {
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

            List<string> msgListCat = new List<string>();
            string msgCat = "";

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
                    msgListCat.Add(category.Category_Name);
                }
            }

            foreach (var category in msgListCat)
            {
                msgCat += category + "\n";
            }
            await DisplayAlert("Oopse!", "Some categories already exists \n\n" + msgCat, "OK");

            CollectionView.ItemsSource = await App.Database.GetCategoriesAsync();
        }
        async void LoadDatabase(object sender, EventArgs e)
        {
            // ****** Enable this block to enable individual item entry form ******
            //if (!string.IsNullOrEmpty(Entry.Text))
            //{
            //await App.Database.SaveItemsAsync(new Items
            //{
            //    Item_Description = Entry.Text,
            //    Item_Category = CategoryPicker.SelectedItem.ToString()
            //});
            //Entry.Text = string.Empty;
            //CategoryPicker.SelectedItem = -1;
            //CollectionView.ItemsSource = await App.Database.GetItemsAsync();
            //}

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
                new Items(){Item_Description = "Buter", Item_Category = "Dairy"},
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
            await DisplayAlert("Notice", "Database Loaded", "OK");
            
            CollectionView.ItemsSource = await App.Database.GetItemsAsync();

        }
        async void ViewDBItems(object sender, EventArgs e)
        {
            CollectionView.ItemsSource = await App.Database.GetItemsAsync();
        }
        async void ViewCategoryContents(object sender, EventArgs e)
        {
            CollectionView.ItemsSource = await App.Database.GetCategoriesAsync();
        }
        public static string GetFilePath(string filename)
        {
            return System.Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        }
    }
}