using MSAMobileApp.Models;
using Plugin.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace MSAMobileApp.Views {
    public partial class SettingsPage : ContentPage {

        private View initialContent;
        public SettingsPage() {
            InitializeComponent();
            initialContent = Content;
        }

        protected override async void OnAppearing() {
            // Create page if user doesnt exist or has logged out
            if (User.CurrentUserInstance.Name == null) {
                Label loginLbl = new Label {
                    Text = "Login to access your settings",
                    FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
                    Margin = 20,
                    HorizontalTextAlignment = TextAlignment.Center,
                    VerticalTextAlignment = TextAlignment.Center
                };
                Button loginButton = new Button {
                    Text = "Login",
                    TextColor = Color.White,
                    BackgroundColor = Color.Green,
                    Font = Font.SystemFontOfSize(NamedSize.Large),
                    BorderWidth = 1,
                    HorizontalOptions = LayoutOptions.Fill,
                };
                loginButton.Clicked += (sender, e) => {
                    MenuPage.ChangePage(MenuPage.pages[3], 3);
                };
                Content = new StackLayout {
                    Children = { loginLbl, loginButton }
                };
            } else Content = initialContent;
        }

        private void Change(object sender, EventArgs e) {
            Label passLbl = new Label {
                Text = "Enter curent password:",
                FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
                Margin = 20,
                HorizontalTextAlignment = TextAlignment.Center,
                VerticalTextAlignment = TextAlignment.Center
            };
            Entry pass1 = new Entry {
                Placeholder = "Password*",
                IsPassword = true
            };
            Entry pass2 = new Entry {
                Placeholder = "Current Password*",
                IsPassword = true
            };
            Button confirm = new Button {
                Text = "Confirm",
                TextColor = Color.White,
                BackgroundColor = Color.Green,
                Font = Font.SystemFontOfSize(NamedSize.Large),
                BorderWidth = 1,
                HorizontalOptions = LayoutOptions.StartAndExpand,
            };
            confirm.Clicked += async (sender2, e2) => {
                if (pass1.Text == null || pass2.Text == null || pass1.Text.Length < 1 || pass2.Text.Length < 1) {
                    await DisplayAlert("Incomplete Information", "Please complete the password fields", "OK");
                } else if (pass1.Text != pass2.Text) {
                    await DisplayAlert("Password mismatch error", "Please enter identical passwords in the displayed fields", "OK");
                } else if (pass1.Text != User.CurrentUserInstance.Password) {
                    await DisplayAlert("Password Incorrect", "Please enter correct current password", "OK");
                } else {
                    // CHECK IF USER WITH SAME EMAIL EXISTS TO PREVENT CLASHES/DUPLICATES
                    User changedUser = new User() {
                        ID = User.CurrentUserInstance.ID,
                        Name = usernameEntry.Text ?? User.CurrentUserInstance.Name,
                        Email = (emailEntry.Text == null || emailEntry.Text.Length < 1) ? User.CurrentUserInstance.Email : emailEntry.Text,
                        Password = (passwordEntry.Text == null || passwordEntry.Text.Length < 1) ? User.CurrentUserInstance.Password : passwordEntry.Text,
                        Photo = User.CurrentUserInstance.Photo,
                        Phone = phoneEntry.Text ?? User.CurrentUserInstance.Phone,
                        Address = addressEntry.Text ?? User.CurrentUserInstance.Address,
                        Date = DateTime.Now
                    };
                    await AzureManager.AzureManagerInstance.UpdateUser(changedUser);
                    await DisplayAlert("SUCCESS", "Successfuly changed your details", "OK");
                    MenuPage.GoHomeAfterLogin(changedUser);
                }
            };
            Button goBack = new Button {
                Text = "Back",
                TextColor = Color.White,
                BackgroundColor = Color.Red,
                Font = Font.SystemFontOfSize(NamedSize.Large),
                BorderWidth = 1,
                HorizontalOptions = LayoutOptions.EndAndExpand,
            };
            goBack.Clicked += (sender2, e2) => {
                MenuPage.ChangePage(MenuPage.pages[5]);
            };
            MenuPage.ChangePage(new NavigationPage(new ContentPage {
                Content = new StackLayout {
                    Children = { passLbl, pass1, pass2, new Grid { Children = { confirm, goBack } } }
                }
            }));
        }

        private void LogoutClicked(object sender, EventArgs e) {
            MenuPage.Logout();
            OnAppearing();
        }

        /*
        <Button Text="Take Picture" TextColor="Black" BackgroundColor="Lime" Clicked="TakePic"/>
        <Button Text="Change Picture" TextColor="Black" BackgroundColor="Green" Clicked="SelectPicFromGallary"/>
        <Image x:Name="image" Aspect="AspectFit"/>
        private async void TakePic(object sender, EventArgs e) {
            string path = "Android/data/com.xamarin.msarestaurantapp/files/Pictures";
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

            await DisplayAlert("File Location", file.Path, "OK");

            image.Source = ImageSource.FromStream(() => {
                var stream = file.GetStream();
                file.Dispose();
                return stream;
            });
        }

        private async void SelectPicFromGallary(object sender, EventArgs e) {
            if (!CrossMedia.Current.IsPickPhotoSupported) {
                await DisplayAlert("Photos Not Supported", ":( Permission not granted to photos.", "OK");
                return;
            }
            var file = await CrossMedia.Current.PickPhotoAsync();

            if (file == null) return;

            image.Source = ImageSource.FromStream(() => {
                var stream = file.GetStream();
                file.Dispose();
                return stream;
            });
        }
        */
    }
}
