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
        private readonly string[] storeItems = new[] { "Carrots", "Peas", "Onions", "Ginger", "Peppers",
        "Brocolli"};

        public ObservableCollection<string> StoreItems { get; set; }
        public ShoppingListPage()
        {
            InitializeComponent();

            StoreItems = new ObservableCollection<string>(storeItems);

            BindingContext = this;
        }

    }
}