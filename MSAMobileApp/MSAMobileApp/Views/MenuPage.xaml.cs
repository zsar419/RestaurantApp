using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace MSAMobileApp.Views {
    public partial class MenuPage : MasterDetailPage {

        Color textColor = Color.White;
        Color backgroundColor = Color.Red;
        Color selectedColor = Color.Green;

        public MenuPage() {
            InitializeComponent();
            
            HomeBtn.BackgroundColor = selectedColor;
            HomeBtn.Clicked += (sender, e) => {
                Detail = new HomePage();
                ChangeSelection(sender as Button);
            };

            FoodBtn.Clicked += (sender, e) => {
                Detail = new TabbedPage {
                    Children = {
                        new DealsPage(), new FoodsPage()
                    }
                };
                ChangeSelection(sender as Button);
            };

            BookBtn.Clicked += (sender, e) => { };

            AuthBtn.Clicked += (sender, e) => {
                Detail = new TabbedPage {
                    Children = {
                        new LoginPage(), new RegistrationPage()
                    }
                };
                ChangeSelection(sender as Button);
            };

            ContactBtn.Clicked += (sender, e) => { };
            SettingsBtn.Clicked += (sender, e) => { };
        }

        private void ChangeSelection(Button selected) {
            IsPresented = false;
            foreach (Button btn in Layout.Children.Where(s => s is Button)) {
                btn.TextColor = textColor;
                btn.BackgroundColor = backgroundColor;
            }
            selected.BackgroundColor = selectedColor;
        }
    }
}
