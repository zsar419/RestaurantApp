using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace MSAMobileApp.Views {
    public partial class LoginPage : ContentPage {
        public LoginPage() {
            InitializeComponent();
        }
        private void GoLogin(object sender, EventArgs e) {
            LoadIndicator.IsRunning = true;
            // Perform await function call to id
            // LoadIndicator.IsRunning = false;
        }
    }
}
