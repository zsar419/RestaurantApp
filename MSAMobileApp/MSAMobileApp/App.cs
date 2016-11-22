using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MSAMobileApp.Views;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace MSAMobileApp {
    public interface IAuthenticate {
        Task<bool> Authenticate();
    }

    public class App : Application {
        public static IAuthenticate Authenticator { get; private set; }

        public static void Init(IAuthenticate authenticator) {
            Authenticator = authenticator;
        }

        public App() {
            // The root page of your application
            MainPage = MenuPage.MenuPageInstance;
        }

        protected override void OnStart() {
            MenuPage.MenuPageInstance.Detail = new NavigationPage(new HomePage());
        }

        protected override void OnSleep() {
            // Handle when your app sleeps
        }

        protected override void OnResume() {
            // Handle when your app resumes
        }



    }
}
