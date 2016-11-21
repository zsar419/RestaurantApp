using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MSAMobileApp.Views;

using Xamarin.Forms;

namespace MSAMobileApp {
    public class App : Application {
        public static MenuPage RootPage;

        public App() {
            // The root page of your application
            MainPage = MenuPage.MenuPageInstance;
        }

        protected override void OnStart() {
            // Handle when your app starts
        }

        protected override void OnSleep() {
            // Handle when your app sleeps
        }

        protected override void OnResume() {
            // Handle when your app resumes
        }
    }
}
