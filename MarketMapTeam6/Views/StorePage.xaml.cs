using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.AndroidSpecific;
using Xamarin.Forms.Xaml;

namespace MarketMapTeam6.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class StorePage : ContentPage
    {

        ObservableCollection<StoreName> StoreName;
        public StorePage()
        {
            InitializeComponent();

            //set store name and info
            StoreName = new ObservableCollection<StoreName>
            {
                new StoreName{ Name = "My Store", Address = "123 MyStreet Anytown, USA"}
            };
            collectionStoreName.ItemsSource = StoreName; 
        }

        // Establish Fresh DB build
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            App.Database.StartDatabase();
            await DisplayAlert("Notice", "Database Loaded", "OK");
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ShoppingListPage());
        }

        private void collectionStoreName_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CollectionView collectionView = sender as CollectionView;
            if (collectionView != null) { ListButton.IsEnabled = true; }
            else { ListButton.IsEnabled = false; }
        }

        
        
            

        

        private void ZipBox_Completed(object sender, EventArgs e)
        {
        Xamarin.Forms.Entry entry = sender as Xamarin.Forms.Entry;
        
        string newstring = entry.Text.ToString();
        int newint = int.Parse(newstring);
            
        if ( newint < 10000 || newint > 99999)
        {
            collectionStoreName.IsVisible = false;
        }
        else { collectionStoreName.IsVisible = true; }
    }







        //private async void Button_Clicked_CatDB(object sender, EventArgs e)
        //{
        //    await Navigation.PushAsync(new DBAddItem());
        //}
    }
}