using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MSAMobileApp.Views {
    public partial class HomePage : ContentPage {

        public HomePage() {
            InitializeComponent();
            BackgroundImage = "m1.jpg";

            MenuBtn.Clicked += (sender, e) => {
                MenuPage.ChangePage(MenuPage.pages[1], 1);
            };
        }

    }
}
