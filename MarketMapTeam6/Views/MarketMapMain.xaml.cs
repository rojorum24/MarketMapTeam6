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
    public partial class MarketMapMain : ContentPage
    {
        public MarketMapMain()
        {
            InitializeComponent();
        }
        
        private async void Button_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new StorePage());
        }

        private async void Button_Clicked_DBentry(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new DBentry());
        }
    }
}