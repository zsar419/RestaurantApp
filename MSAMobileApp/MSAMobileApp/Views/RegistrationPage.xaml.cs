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

            // Tesing obtaining other users
            List<User> users = await AzureManager.AzureManagerInstance.GetUsers();
            UserList.ItemsSource = users;
            // Perform await function call to id
            // Register Users
            LoadIndicator.IsRunning = false;
        }
    }
}
