using System;
using System.Collections.Generic;
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
        public ShoppingListPage()
        {
            InitializeComponent();
            List<String> testList = new List<String>
            {
                "Item1",
                "Item2",
                "Item3"
            };
            //ShoppingListPage.ItemsSource
        }
    }
}