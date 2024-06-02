using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MarketMapTeam6.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ShoppingListPage : ContentPage
    {
        //Set Item Source
        ObservableCollection<Dairy> Dairy;
        ObservableCollection<Produce> Produce;
        ObservableCollection<Frozen> Frozen;
        ObservableCollection<Baked> Baked;
        ObservableCollection<Pantry> Pantry;
        ObservableCollection<Nonfood> Nonfood;
        ObservableCollection<Meat> Meat;

        public ShoppingListPage()
        {
            InitializeComponent();

            //Dairy list items
            Dairy = new ObservableCollection<Dairy>
            {
                new Dairy { Name = "Butter", Selected = 0 },
                new Dairy { Name = "Cheese", Selected = 0 },
                new Dairy { Name = "Cream", Selected = 0 },
                new Dairy { Name = "Milk", Selected = 0 },
                new Dairy { Name = "Yogurt", Selected = 0 },
            };
            collectionDairy.ItemsSource = Dairy;

            //Produce list items
            Produce = new ObservableCollection<Produce>
            {
                new Produce { Name = "Carrots", Selected = 0 },
                new Produce { Name = "Peas", Selected = 0 },
                new Produce { Name = "Broccoli", Selected = 0 },
                new Produce { Name = "Celery", Selected = 0 },
                new Produce { Name = "Peppers", Selected = 0 },
                new Produce { Name = "Lettuce", Selected = 0 },
                new Produce { Name = "Kale", Selected = 0 },
                new Produce { Name = "Apples", Selected = 0 },
                new Produce { Name = "Oranges", Selected = 0 },
                new Produce { Name = "Bananas", Selected = 0 },
                new Produce { Name = "Pears", Selected = 0 },
                new Produce { Name = "Lemons", Selected = 0 },
                new Produce { Name = "Limes", Selected = 0 },
            };
            collectionProduce.ItemsSource = Produce;

            //Frozen list items
            Frozen = new ObservableCollection<Frozen>
            {
                new Frozen { Name = "Pizza", Selected = 0 },
                new Frozen { Name = "Fries", Selected = 0 },
                new Frozen { Name = "Chicken Fingers", Selected = 0 },
                new Frozen { Name = "Ice Cream", Selected = 0 },
                new Frozen { Name = "Breakfast Sandwhich", Selected = 0 },
                new Frozen { Name = "Frozen Vegetables", Selected = 0 },


            };
            collectionFrozen.ItemsSource = Frozen;

            //Baked list items
            Baked = new ObservableCollection<Baked>
            {
                new Baked { Name = "White Bread", Selected = 0 },
                new Baked { Name = "Multigrain Bread", Selected = 0 },
                new Baked { Name = "Bagettes", Selected = 0 },
                new Baked { Name = "Muffins", Selected = 0 },
                new Baked { Name = "Cake", Selected = 0 },
                new Baked { Name = "Pie", Selected = 0 },

            };
            collectionBaked.ItemsSource = Baked;

            //Pantry list items
            Pantry = new ObservableCollection<Pantry>
            {
                new Pantry { Name = "Ketchup", Selected = 0 },
                new Pantry { Name = "Mustard", Selected = 0 },
                new Pantry { Name = "Relish", Selected = 0 },
                new Pantry { Name = "Mayonnaise", Selected = 0 },
                new Pantry { Name = "BBQ Sauce", Selected = 0 },
                new Pantry { Name = "Soy Sauce", Selected = 0 },
                new Pantry { Name = "Cereal", Selected = 0 },
                new Pantry { Name = "Rice", Selected = 0 },
                new Pantry { Name = "Oats", Selected = 0 },

            };
            collectionPantry.ItemsSource = Pantry;

            //Nonfood list items
            Nonfood = new ObservableCollection<Nonfood>
            {
                new Nonfood { Name = "Plates", Selected = 0 },
                new Nonfood { Name = "Silverware", Selected = 0 },
                new Nonfood { Name = "Cups", Selected = 0 },
                new Nonfood { Name = "Napkins", Selected = 0 },
                new Nonfood { Name = "Paper Plates", Selected = 0 },
                new Nonfood { Name = "TableCloths", Selected = 0 },

            };
            collectionNonfood.ItemsSource = Nonfood;

            //Meat list items
            Meat = new ObservableCollection<Meat>
            {
                new Meat { Name = "Beef", Selected = 0 },
                new Meat { Name = "Steak", Selected = 0 },
                new Meat { Name = "Chicken", Selected = 0 },
                new Meat { Name = "Pork", Selected = 0 },
                new Meat { Name = "Bacon", Selected = 0 },
                new Meat { Name = "Turkey", Selected = 0 },
                new Meat { Name = "Lamb", Selected = 0 },
                new Meat { Name = "Eggs", Selected = 0 },

            };
            collectionMeat.ItemsSource = Meat;
        }
        private async void Button_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new MapPage());
        }
    }

}