using MSAMobileApp.Models;
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
                    MenuPage.ChangePage(MenuPage.pages[2], 3);
                };
                Content = new StackLayout {
                    Children = { loginLbl, loginButton }
                };
            } else Content = initialContent;
        }

        private void LogoutClicked(object sender, EventArgs e) {

        }
    }
}
