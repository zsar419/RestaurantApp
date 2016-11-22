using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MSAMobileApp.Models;
using Xamarin.Forms;

namespace MSAMobileApp.Views {
    public partial class LoginPage : ContentPage {
        public LoginPage() {
            InitializeComponent();
        }

        private async void GoLogin(object sender, EventArgs e) {
            LoadIndicator.IsRunning = true;

            List<User> users = await AzureManager.AzureManagerInstance.GetUsers();
            if (users.Where(s => s.Email == emailEntry.Text.ToLower() && s.Password == passwordEntry.Text).Count() > 0) {
                await DisplayAlert("Login Succesfull", "Successfilly logged in!", "OK");
                MenuPage.GoHomeAfterLogin(users.Where(s => s.Email == emailEntry.Text.ToLower() && s.Password == passwordEntry.Text).ToArray()[0]);
            } else {
                await DisplayAlert("Login Failed", "Please login with valid credentials", "OK");
            }
            LoadIndicator.IsRunning = false;
        }

    }
}
