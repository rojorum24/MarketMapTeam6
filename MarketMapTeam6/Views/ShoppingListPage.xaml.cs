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
        //Set Item Source
        public ObservableCollection<Dairy> Dairy;
        public ObservableCollection<Produce> Produce;
        public ObservableCollection<Frozen> Frozen;
        public ObservableCollection<Baked> Baked;
        public ObservableCollection<Pantry> Pantry;
        public ObservableCollection<Nonfood> Nonfood;
        public ObservableCollection<Meat> Meat;

        //single collection for all selected items
        public ObservableCollection<IShoppingItem> SelectedItems { get; set; }


        public interface IShoppingItem
        {
            string Name { get; set; }
        }
        public ShoppingListPage()
        {
            InitializeComponent();

            SelectedItems = new ObservableCollection<IShoppingItem>();
            //Dairy list items
            Dairy = new ObservableCollection<Dairy>()
            {
                new Dairy { Name = "Butter"},
                new Dairy { Name = "Cheese"},
                new Dairy { Name = "Cream"},
                new Dairy { Name = "Milk"},
                new Dairy { Name = "Yogurt"},
            };
            collectionDairy.ItemsSource = Dairy;

            

            //Produce list items
            Produce = new ObservableCollection<Produce>()
            {
                new Produce { Name = "Carrots"},
                new Produce { Name = "Peas"},
                new Produce { Name = "Broccoli"},
                new Produce { Name = "Celery"},
                new Produce { Name = "Peppers"},
                new Produce { Name = "Lettuce"},
                new Produce { Name = "Kale"},
                new Produce { Name = "Apples"},
                new Produce { Name = "Oranges"},
                new Produce { Name = "Bananas"},
                new Produce { Name = "Pears"},
                new Produce { Name = "Lemons"},
                new Produce { Name = "Limes"},
            };
            collectionProduce.ItemsSource = Produce;

            //Frozen list items
            Frozen = new ObservableCollection<Frozen>()
            {
                new Frozen { Name = "Pizza"},
                new Frozen { Name = "Fries"},
                new Frozen { Name = "Chicken Fingers"},
                new Frozen { Name = "Ice Cream"},
                new Frozen { Name = "Breakfast Sandwhich"},
                new Frozen { Name = "Frozen Vegetables"},


            };
            collectionFrozen.ItemsSource = Frozen;

            //Baked list items
            Baked = new ObservableCollection<Baked>()
            {
                new Baked { Name = "White Bread"},
                new Baked { Name = "Multigrain Bread"},
                new Baked { Name = "Bagettes"},
                new Baked { Name = "Muffins"},
                new Baked { Name = "Cake"},
                new Baked { Name = "Pie"},

            };
            collectionBaked.ItemsSource = Baked;

            //Pantry list items
            Pantry = new ObservableCollection<Pantry>()
            {
                new Pantry { Name = "Ketchup"},
                new Pantry { Name = "Mustard"},
                new Pantry { Name = "Relish"},
                new Pantry { Name = "Mayonnaise"},
                new Pantry { Name = "BBQ Sauce"},
                new Pantry { Name = "Soy Sauce"},
                new Pantry { Name = "Cereal"},
                new Pantry { Name = "Rice"},
                new Pantry { Name = "Oats"},

            };
            collectionPantry.ItemsSource = Pantry;

            //Nonfood list items
            Nonfood = new ObservableCollection<Nonfood>()
            {
                new Nonfood { Name = "Plates"},
                new Nonfood { Name = "Silverware"},
                new Nonfood { Name = "Cups"},
                new Nonfood { Name = "Napkins"},
                new Nonfood { Name = "Paper Plates"},
                new Nonfood { Name = "TableCloths"},

            };
            collectionNonfood.ItemsSource = Nonfood;

            //Meat list items
            Meat = new ObservableCollection<Meat>()
            {
                new Meat { Name = "Beef"},
                new Meat { Name = "Steak"},
                new Meat { Name = "Chicken"},
                new Meat { Name = "Pork"},
                new Meat { Name = "Bacon"},
                new Meat { Name = "Turkey"},
                new Meat { Name = "Lamb"},
                new Meat { Name = "Eggs"},

            };
            collectionMeat.ItemsSource = Meat;

            

        }

        //dairy
        public void OnDairySelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.CurrentSelection != null)
            {
                var selectedDairy = e.CurrentSelection.Cast<Dairy>().ToList();
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
                var deselectedDairy = e.PreviousSelection.Cast<Dairy>().ToList();
                foreach (var item in deselectedDairy)
                {
                    SelectedItems.Remove(item);
                }
            }
        }

        //Produce
        public void OnProduceSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.CurrentSelection != null)
            {
                var selectedProduce = e.CurrentSelection.Cast<Produce>().ToList();
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
                var deselectedProduce = e.PreviousSelection.Cast<Produce>().ToList();
                foreach (var item in deselectedProduce)
                {
                    SelectedItems.Remove(item);
                }
            }
        }

        //Frozen
        public void OnFrozenSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.CurrentSelection != null)
            {
                var selectedFrozen = e.CurrentSelection.Cast<Frozen>().ToList();
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
                var deselectedFrozen = e.PreviousSelection.Cast<Frozen>().ToList();
                foreach (var item in deselectedFrozen)
                {
                    SelectedItems.Remove(item);
                }
            }
        }
        //Baked
        public void OnBakedSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.CurrentSelection != null)
            {
                var selectedBaked = e.CurrentSelection.Cast<Baked>().ToList();
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
                var deselectedBaked = e.PreviousSelection.Cast<Baked>().ToList();
                foreach (var item in deselectedBaked)
                {
                    SelectedItems.Remove(item);
                }
            }
        }

        //Pantry
        public void OnPantrySelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.CurrentSelection != null)
            {
                var selectedPantry = e.CurrentSelection.Cast<Pantry>().ToList();
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
                var deselectedPantry = e.PreviousSelection.Cast<Pantry>().ToList();
                foreach (var item in deselectedPantry)
                {
                    SelectedItems.Remove(item);
                }
            }
        }

        //Nonfood
        public void OnNonfoodSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.CurrentSelection != null)
            {
                var selectedNonfood = e.CurrentSelection.Cast<Nonfood>().ToList();
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
                var deselectedNonfood = e.PreviousSelection.Cast<Nonfood>().ToList();
                foreach (var item in deselectedNonfood)
                {
                    SelectedItems.Remove(item);
                }
            }
        }

        //Meat
        public void OnMeatSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.CurrentSelection != null)
            {
                var selectedMeat = e.CurrentSelection.Cast<Meat>().ToList();
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
                var deselectedMeat = e.PreviousSelection.Cast<Meat>().ToList();
                foreach (var item in deselectedMeat)
                {
                    SelectedItems.Remove(item);
                }
            }
        }
        private async void ShowSelectedItemsButton_Clicked(object sender, EventArgs e)
        {
            // Create a string to display the selected items
            string selectedItemsString = string.Join(", ", SelectedItems.Select(i => i.Name));

            // Show an Alert with the selected items
            await DisplayAlert("Selected Items", selectedItemsString, "OK");
        }
        private async void Button_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new MapPage());
        }
    }

}