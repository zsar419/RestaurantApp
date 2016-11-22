using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Microsoft.WindowsAzure.MobileServices;
using System.Threading.Tasks;

namespace MSAMobileApp.Droid {
    [Activity(Label = "MSAMobileApp", Icon = "@drawable/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity, IAuthenticate {
        protected override void OnCreate(Bundle bundle) {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(bundle);

            global::Xamarin.FormsMaps.Init(this, bundle);
            global::Xamarin.Forms.Forms.Init(this, bundle);
            // Initialize the authenticator before loading the app.
            App.Init((IAuthenticate)this);
            LoadApplication(new App());
        }


        public async Task<bool> Authenticate() {
            MobileServiceUser user; // Define a authenticated user - can use this for further 

            var success = false;
            var message = string.Empty;
            try {
                // Sign in with Facebook login using a server-managed flow.
                user = await AzureManager.AzureManagerInstance.AzureClient.LoginAsync(this, MobileServiceAuthenticationProvider.Facebook);
                //user = await AzureManager.AzureManagerInstance.AzureClient.LoginAsync(this, MobileServiceAuthenticationProvider.Google);
                // user = await TodoItemManager.DefaultManager.CurrentClient.LoginAsync(this, MobileServiceAuthenticationProvider.Facebook);
                if (user != null) {
                    message = string.Format("you are now signed-in as {0}.", user.UserId);
                    success = true;
                }
            } catch (Exception ex) {
                message = ex.Message;
            }

            // Display the success or failure message.
            AlertDialog.Builder builder = new AlertDialog.Builder(this);
            builder.SetMessage(message);
            builder.SetTitle("Sign-in result");
            builder.Create().Show();

            return success;
        }
    }
}

