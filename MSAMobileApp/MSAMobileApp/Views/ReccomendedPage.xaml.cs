using MSAMobileApp.Models;
using Plugin.Geolocator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace MSAMobileApp.Views {
    public partial class ReccomendedPage : ContentPage {
        public ReccomendedPage() {
            InitializeComponent();
        }

        private async void GetLocationData(Object sender, EventArgs e) {
            LoadIndicator.IsRunning = true;

            try {
                var locator = CrossGeolocator.Current;
                locator.DesiredAccuracy = 50;
                var position = await locator.GetPositionAsync(timeoutMilliseconds: 10000);
                data.Text = $"{position.Latitude}\n {position.Longitude}";
                // Feed data into API to load reccomendations
            } catch (Exception ex) {
                await DisplayAlert("Failure:", $"Unable to retrieve location location, please turn on your GPS", "OK");
            }

            LoadIndicator.IsRunning = false;
        }

    }
}
