using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace MSAMobileApp.Views {
    public partial class FoodBotSearchPage : ContentPage {
        public FoodBotSearchPage() {
            InitializeComponent();
        }

        private void InitiateBot(Object sender, EventArgs e) {
            var browser = new WebView();
            var htmlSource = new HtmlWebViewSource();
            htmlSource.Html = @"
                <html>
                    <body>
                        <p> TESTING </p>
                        <iframe src='https://directline.botframework.com/embed/Siri_Custom?s=SIek1YHuatc.cwA.pto.7cXgzHVdmcFHMDW01ybUx8oWfEa0MwGH8bkNhVB1BrI' style='height: 80%; width: 100%;'></iframe>
                    </body>
                </html>";
            browser.Source = htmlSource;
            Content = browser;
        }
    }
}
