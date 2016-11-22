                                                                                                                        using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace MSAMobileApp.Views {
    public partial class FacebookLoginPage : ContentPage {
        // Track whether the user has authenticated.
        bool authenticated = false;

        public FacebookLoginPage() {
            InitializeComponent();
        }

        protected override async void OnAppearing() {
            base.OnAppearing();

            // Refresh items only when authenticated.
            if (authenticated == true) {
                // Set syncItems to true in order to synchronize the data on startup when running in offline mode.
                //await RefreshItems(true, syncItems: false);

                // Hide the Sign-in button.
                this.loginButton.IsVisible = false;
            }
        }

        async void loginButton_Clicked(object sender, EventArgs e) {
            if (App.Authenticator != null)
                authenticated = await App.Authenticator.Authenticate();

            if (authenticated == true) {
                this.loginButton.IsVisible = false;
                //await RefreshItems(true, syncItems: false);
            }
        }
    }
}
