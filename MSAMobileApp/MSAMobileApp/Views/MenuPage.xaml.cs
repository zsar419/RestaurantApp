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
        public static List<Page> pages = new List<Page>(); // Caching

        Color textColor = Color.White, backgroundColor = Color.Red, selectedColor = Color.Green;

        public MenuPage() {
            InitializeComponent();
            InitializePageLinks();

            HomeBtn.BackgroundColor = selectedColor;
            HomeBtn.Clicked += (sender, e) => {
                Detail = pages[0];
                ChangeSelection(sender as Button);
            };

            FoodBtn.Clicked += (sender, e) => {
                Detail = pages[1];
                ChangeSelection(sender as Button);
            };

            OrderBtn.IsEnabled = false;
            OrderBtn.Clicked += (sender, e) => {
                Detail = pages[2];
                ChangeSelection(sender as Button);
            };

            AuthBtn.Clicked += (sender, e) => {
                Detail = pages[3];
                ChangeSelection(sender as Button);
            };

            ContactBtn.Clicked += (sender, e) => {
                Detail = pages[4];
                ChangeSelection(sender as Button);
            };
            SettingsBtn.Clicked += (sender, e) => {
                Detail = pages[5];
                ChangeSelection(sender as Button);
            };
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

        private void InitializePageLinks() {
            pages.Add(new NavigationPage(new HomePage()));
            pages.Add(new NavigationPage(new TabbedPage { Children = { new ReccomendedPage(), new DealsPage(), new FoodsPage() } }));
            pages.Add(new NavigationPage(new OrderPage()));
            pages.Add(new NavigationPage(new TabbedPage { Children = { new LoginPage(), new FacebookLoginPage(), new RegistrationPage() } }));
            pages.Add(new NavigationPage(new ContactPage()));
            pages.Add(new NavigationPage(new SettingsPage()));
        }

        // Navigation logging in
        public static void GoHomeAfterLogin(User user) {
            User.CurrentUserInstance.Login(user);
            MenuPageInstance.currentLbl.Text = $"Logged in as {user.Name}";
            MenuPageInstance.currentLbl.TextColor = Color.Green;


            MenuPageInstance.Detail = pages[0];
            MenuPageInstance.HomeBtn.BackgroundColor = Color.Green;
            MenuPageInstance.AuthBtn.BackgroundColor = Color.Red;

            MenuPageInstance.OrderBtn.IsEnabled = true;

            MenuPageInstance.AuthBtn.IsEnabled = false;
            MenuPageInstance.AuthBtn.IsVisible = false;
        }

        public static void Logout() {
            MenuPageInstance.currentLbl.Text = "Welcome Guest";
            MenuPageInstance.currentLbl.TextColor = Color.Red;
        }

        public static void ChangePage(Page page, int number = 0) {
            MenuPageInstance.Detail = page;
            // For going from orders page (1 call) to main menu page
            if(number >0 ) {
                foreach (Button btn in MenuPageInstance.Layout.Children.Where(s => s is Button)) {
                    btn.TextColor = Color.White;
                    btn.BackgroundColor = Color.Red;
                }
                if (number == 1) MenuPageInstance.FoodBtn.BackgroundColor = Color.Green;
                if (number == 2) MenuPageInstance.OrderBtn.BackgroundColor = Color.Green;
                if (number == 3) MenuPageInstance.AuthBtn.BackgroundColor = Color.Green;
            }
        }

    }
}
