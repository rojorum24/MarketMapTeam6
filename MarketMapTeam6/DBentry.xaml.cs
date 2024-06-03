using System;
using System.Collections.Generic;
using System.Linq;
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
            CollectionView.ItemsSource = await App.Database.GetItemsAsync();
            CategoryPicker.ItemsSource = await App.Database.GetCategoriesAsync();
        }

        async void submitItem(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Entry.Text))
            {
                await App.Database.SaveItemsAsync(new Items
                {
                    Item_Description = Entry.Text,
                    Item_Category = CategoryPicker.SelectedItem.ToString()
                });

                Entry.Text = string.Empty;
                CategoryPicker.SelectedItem = -1;
                CollectionView.ItemsSource = await App.Database.GetItemsAsync();
            }
        }

        public static string GetFilePath(string filename)
        {
            return System.Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        }
    }
}