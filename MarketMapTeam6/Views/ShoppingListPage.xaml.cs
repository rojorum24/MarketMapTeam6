using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Essentials;

namespace MarketMapTeam6.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]

    public partial class ShoppingListPage : ContentPage
    {
        protected override async void OnAppearing()
        {
            try
            {
                base.OnAppearing();
                App.Database.StartDatabase();
                
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
            }
        }

        List<Items> dairyList = new List<Items>();
        List<Items> produceList = new List<Items>();
        List<Items> frozenList = new List<Items>();
        List<Items> bakedList = new List<Items>();
        List<Items> pantryList = new List<Items>();
        List<Items> nonfoodList = new List<Items>();
        List<Items> meatList = new List<Items>();

        //Tester collections for lists established above
        public ObservableCollection<Items> DairyCollection = new ObservableCollection<Items>();
        public ObservableCollection<Items> ProduceCollection = new ObservableCollection<Items>();
        public ObservableCollection<Items> FrozenCollection = new ObservableCollection<Items>();
        public ObservableCollection<Items> BakedCollection = new ObservableCollection<Items>();
        public ObservableCollection<Items> PantryCollection = new ObservableCollection<Items>();
        public ObservableCollection<Items> NonfoodCollection = new ObservableCollection<Items>();
        public ObservableCollection<Items> MeatCollection = new ObservableCollection<Items>();

        //****IShoppingItem not implemented from here forward under DB interface****
        //leaving in place for testing purposes
        public interface IShoppingItem
        {
            string Item_Description { get; set; }
            string Item_Category { get; set; }
            bool IsSelected { get; set; }
        }

        //single collection for all selected items
        public ObservableCollection<Items> SelectedItems { get; set; }

        public async void PopulateCollections()
        {
            try
            {
                //populate lists of type Items from database to be assigned as item source for each collection
                dairyList = await App.Database.GetItemsByCatAsync("Dairy");
                produceList = await App.Database.GetItemsByCatAsync("Produce");
                frozenList = await App.Database.GetItemsByCatAsync("Frozen");
                bakedList = await App.Database.GetItemsByCatAsync("Baked");
                pantryList = await App.Database.GetItemsByCatAsync("Pantry");
                nonfoodList = await App.Database.GetItemsByCatAsync("Nonfood");
                meatList = await App.Database.GetItemsByCatAsync("Meat");

                //populate collections with items from lists of database items
                foreach (Items item in dairyList)
                {
                    DairyCollection.Add(item);
                }
                foreach (Items item in produceList)
                {
                    ProduceCollection.Add(item);
                }
                foreach (Items item in frozenList)
                {
                    FrozenCollection.Add(item);
                }
                foreach (Items item in bakedList)
                {
                    BakedCollection.Add(item);
                }
                foreach (Items item in pantryList)
                {
                    PantryCollection.Add(item);
                }
                foreach (Items item in nonfoodList)
                {
                    NonfoodCollection.Add(item);
                }
                foreach (Items item in meatList)
                {
                    MeatCollection.Add(item);
                }

                collectionDairy.ItemsSource = DairyCollection;
                collectionProduce.ItemsSource = ProduceCollection;
                collectionFrozen.ItemsSource = FrozenCollection;
                collectionBaked.ItemsSource = BakedCollection;
                collectionPantry.ItemsSource = PantryCollection;
                collectionNonfood.ItemsSource = NonfoodCollection;
                collectionMeat.ItemsSource = MeatCollection;
            }
            catch (Exception e)
            {
                Debug.WriteLine(" - Failed at: PopulateCollections()" + e);
            }
            

        }

        public ShoppingListPage()
        {
            try 
            { 
                InitializeComponent();
                PopulateCollections();
            }
            catch (Exception e) 
            {
                Debug.WriteLine(" - Failed at: InitializeComponent()" + e); 
            }
            
            SelectedItems = new ObservableCollection<Items>();
        }

        //dairy
        public void OnDairySelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.CurrentSelection != null)
            {
                var selectedDairy = e.CurrentSelection.Cast<Items>().ToList();
                foreach (var item in selectedDairy)
                {
                    if (!SelectedItems.Contains(item))
                    {
                        SelectedItems.Add(item);
                    }
                }
            }
            if (e.PreviousSelection != null)
            {
                // Remove the deselected items from the SelectedItems collection
                var deselectedDairy = e.PreviousSelection.Cast<Items>().ToList();
                foreach (var item in deselectedDairy)
                {
                    if (SelectedItems.Contains(item))
                    {
                        
                    }
                    else
                    {
                        SelectedItems.Remove(item);
                    }
                }
            }
        }

        //Produce
        public void OnProduceSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.CurrentSelection != null)
            {
                var selectedProduce = e.CurrentSelection.Cast<Items>().ToList();
                foreach (var item in selectedProduce)
                {
                    if (!SelectedItems.Contains(item))
                    {
                        SelectedItems.Add(item);
                    }
                }
            }
            if (e.PreviousSelection != null)
            {
                // Remove the deselected items from the SelectedItems collection
                var deselectedProduce = e.PreviousSelection.Cast<Items>().ToList();
                foreach (var item in deselectedProduce)
                {
                    if (SelectedItems.Contains(item))
                    {

                    }
                    else
                    {
                        SelectedItems.Remove(item);
                    }
                }
            }
        }

        //Frozen
        public void OnFrozenSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.CurrentSelection != null)
            {
                var selectedFrozen = e.CurrentSelection.Cast<Items>().ToList();
                foreach (var item in selectedFrozen)
                {
                    if (!SelectedItems.Contains(item))
                    {
                        SelectedItems.Add(item);
                    }
                }
            }
            if (e.PreviousSelection != null)
            {
                // Remove the deselected items from the SelectedItems collection
                var deselectedFrozen = e.PreviousSelection.Cast<Items>().ToList();
                foreach (var item in deselectedFrozen)
                {
                    if (SelectedItems.Contains(item))
                    {

                    }
                    else
                    {
                        SelectedItems.Remove(item);
                    }
                }
            }
        }
        
        //Baked
        public void OnBakedSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.CurrentSelection != null)
            {
                var selectedBaked = e.CurrentSelection.Cast<Items>().ToList();
                foreach (var item in selectedBaked)
                {
                    if (!SelectedItems.Contains(item))
                    {
                        SelectedItems.Add(item);
                    }
                }
            }
            if (e.PreviousSelection != null)
            {
                // Remove the deselected items from the SelectedItems collection
                var deselectedBaked = e.PreviousSelection.Cast<Items>().ToList();
                foreach (var item in deselectedBaked)
                {
                    if (SelectedItems.Contains(item))
                    {

                    }
                    else
                    {
                        SelectedItems.Remove(item);
                    }
                }
            }
        }

        //Pantry
        public void OnPantrySelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.CurrentSelection != null)
            {
                var selectedPantry = e.CurrentSelection.Cast<Items>().ToList();
                foreach (var item in selectedPantry)
                {
                    if (!SelectedItems.Contains(item))
                    {
                        SelectedItems.Add(item);
                    }
                }
            }
            if (e.PreviousSelection != null)
            {
                // Remove the deselected items from the SelectedItems collection
                var deselectedPantry = e.PreviousSelection.Cast<Items>().ToList();
                foreach (var item in deselectedPantry)
                {
                    if (SelectedItems.Contains(item))
                    {

                    }
                    else
                    {
                        SelectedItems.Remove(item);
                    }
                }
            }
        }

        //Nonfood
        public void OnNonfoodSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.CurrentSelection != null)
            {
                var selectedNonfood = e.CurrentSelection.Cast<Items>().ToList();
                foreach (var item in selectedNonfood)
                {
                    if (!SelectedItems.Contains(item))
                    {
                        SelectedItems.Add(item);
                    }
                }
            }
            if (e.PreviousSelection != null)
            {
                // Remove the deselected items from the SelectedItems collection
                var deselectedNonfood = e.PreviousSelection.Cast<Items>().ToList();
                foreach (var item in deselectedNonfood)
                {
                    if (SelectedItems.Contains(item))
                    {

                    }
                    else
                    {
                        SelectedItems.Remove(item);
                    }
                }
            }
        }

        //Meat
        public void OnMeatSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.CurrentSelection != null)
            {
                var selectedMeat = e.CurrentSelection.Cast<Items>().ToList();
                foreach (var item in selectedMeat)
                {
                    if (!SelectedItems.Contains(item))
                    {
                        SelectedItems.Add(item);
                    }
                }
            }
            if (e.PreviousSelection != null)
            {
                // Remove the deselected items from the SelectedItems collection
                var deselectedMeat = e.PreviousSelection.Cast<Items>().ToList();
                foreach (var item in deselectedMeat)
                {
                    if (SelectedItems.Contains(item))
                    {

                    }
                    else
                    {
                        SelectedItems.Remove(item);
                    }
                }
            }
        }
        
        private async void ShowSelectedItemsButton_Clicked(object sender, EventArgs e)
        {
            // Create a string to display the selected items
            string selectedItemsString = string.Join("\n", SelectedItems.Select(i => i.Item_Description));

            // Show an Alert with the selected items
            await DisplayAlert("Selected Items", selectedItemsString, "OK");
        }
        
        private async void Button_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new MapPage(SelectedItems));
        }
    }

}