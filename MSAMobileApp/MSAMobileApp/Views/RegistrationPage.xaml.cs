using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using MSAMobileApp.Models;
using Plugin.Media;

namespace MSAMobileApp.Views {
    public partial class RegistrationPage : ContentPage {

        private bool img;
        private string userUri;
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
                Photo = img==true?userUri:"",
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

        private async void TakePic(object sender, EventArgs e) {
            RegBtn.IsEnabled = false;
            LoadIndicator.IsRunning = true;

            string directory = "MSARestaurantApp";
            string imageName = "FabrikamUser.jpg";
            if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported) {
                await DisplayAlert("No Camera", ":( No camera avaialble.", "OK");
                return;
            }

            var file = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions {
                Directory = directory,
                Name = imageName
            });
            if (file == null) return;

            string responseID = await ApiManager.ApiManagerInstance.GetPostedImageUri(file.GetStream());
            file.Dispose();

            // Show user
            img = true;
            userUri = "http://i.imgur.com/" + responseID + ".jpg";
            image.Source = new Uri(userUri);

            LoadIndicator.IsRunning = false;
            RegBtn.IsEnabled = true;
        }

        private async void SelectPicFromGallary(object sender, EventArgs e) {
            RegBtn.IsEnabled = false;
            LoadIndicator.IsRunning = true;

            if (!CrossMedia.Current.IsPickPhotoSupported) {
                await DisplayAlert("Photos Not Supported", ":( Permission not granted to photos.", "OK");
                return;
            }
            var file = await CrossMedia.Current.PickPhotoAsync();
            if (file == null) return;

            string responseID = await ApiManager.ApiManagerInstance.GetPostedImageUri(file.GetStream());
            file.Dispose();
            // Show user
            img = true;
            userUri = "http://i.imgur.com/" + responseID + ".jpg";
            image.Source = new Uri(userUri);

            LoadIndicator.IsRunning = false;
            RegBtn.IsEnabled = true;
        }

    }
}
