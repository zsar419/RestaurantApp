using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using MSAMobileApp.Models;

namespace MSAMobileApp.Views {
    public partial class MenuPage : MasterDetailPage {
        private static MenuPage instance;

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

        public static MenuPage MenuPageInstance{
            get {
                if (instance == null) instance = new MenuPage();
                return instance;
            }
        }

        private void ChangeSelection(Button selected) {
            IsPresented = false;
            foreach (Button btn in Layout.Children.Where(s => s is Button)) {
                btn.TextColor = textColor;
                btn.BackgroundColor = backgroundColor;
            }
            selected.BackgroundColor = selectedColor;
        }

        public static void GoHomeAfterLogin(User user) {
            MenuPageInstance.currentLbl.Text = $"Logged in as {user.Name}";
            MenuPageInstance.currentLbl.TextColor = Color.Green;

           MenuPageInstance.Detail = new HomePage();
            MenuPageInstance.HomeBtn.BackgroundColor = Color.Green;
            MenuPageInstance.AuthBtn.BackgroundColor = Color.Red;
            MenuPageInstance.AuthBtn.IsEnabled = false;
            MenuPageInstance.AuthBtn.IsVisible = false;
        }

        public static void Logout() {
            MenuPageInstance.currentLbl.Text = "Welcome Guest";
            MenuPageInstance.currentLbl.TextColor = Color.Red;

        }

    }
}
