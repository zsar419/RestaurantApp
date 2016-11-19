using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace MSAMobileApp.Views {
    public partial class MenuPage : MasterDetailPage {
        public MenuPage() {
            InitializeComponent();
        }

        private void GoHomePage(object sender, EventArgs e) {
            Detail = new HomePage();
            IsPresented = false;
        }

        private void GoSecondPage(object sender, EventArgs e) {
            Detail = new SecondPage();
            IsPresented = false;
        }

        private void GoAuthPage(object sender, EventArgs e) {
            Detail = new TabbedPage {
                Children = {
                    new LoginPage(), new RegistrationPage()
                }
            };
            IsPresented = false;
        }
    }
}
