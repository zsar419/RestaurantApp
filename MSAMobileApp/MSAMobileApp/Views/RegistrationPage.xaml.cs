using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using MSAMobileApp.Models;

namespace MSAMobileApp.Views {
    public partial class RegistrationPage : ContentPage {

        
        public RegistrationPage() {
            InitializeComponent();
        }

        private async void GoRegister(object sender, EventArgs e) {
            LoadIndicator.IsRunning = true;
            
            // Perform await function call to id
            User newUser = new User() {
                Name = usernameEntry.Text,
                Email = emailEntry.Text.ToLower(),
                Password = passwordEntry.Text,
                Photo = phoneEntry.Text,
                Phone = phoneEntry.Text,
                Address = addressEntry.Text,
                Date = DateTime.Now
            };

            bool res = true;

            List<User> users = await AzureManager.AzureManagerInstance.GetUsers();
            if (users.Where(s => s.Email == newUser.Email).Count() > 0) {   // User exists
                await DisplayAlert("User exists", "User with specified email exists", "OK");
                LoadIndicator.IsRunning = false;
                return;
            }

            if (newUser.Password == null || newUser.Name == null || newUser.Email == null) {
                await DisplayAlert("Incomplete Information", "Please complete the required* fields", "OK");
                res = false;
            } else if(newUser.Password.Length < 1 || newUser.Name.Length < 1 || newUser.Email.Length < 1) {
                await DisplayAlert("Incomplete Information", "Please complete the required* fields", "OK");
                res = false;
            }

            if (passwordEntry.Text != passwordEntry2.Text) {
                await DisplayAlert("Password Mismatch", "Please make sure password fields match", "OK");
                res = false;
            }

            if (res) {
                await AzureManager.AzureManagerInstance.AddUser(newUser);
                await DisplayAlert("Success", "You have been registered to Fabrikam", "OK");
                MenuPage.GoHomeAfterLogin(newUser);
            }

            LoadIndicator.IsRunning = false;
        }
    }
}
