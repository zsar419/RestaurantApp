using MSAMobileApp.Models;
using Plugin.Geolocator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace MSAMobileApp.Views {
    public partial class ContactPage : ContentPage {


        public ContactPage() {
            InitializeComponent();

            //GenerateMapData();
        }

        private async void GetLocationData(object obj, EventArgs e) {
            LoadIndicator.IsRunning = true;
            getLocation.IsEnabled = false;

            try {
                var locator = CrossGeolocator.Current;
                locator.DesiredAccuracy = 50;
                var localPos = await locator.GetPositionAsync(timeoutMilliseconds: 10000);
                // WeatherObject.RootObject localData = await ApiManager.ApiManagerInstance.GetJsonWeatherObjectByCity("auckland");
                WeatherObject.RootObject localData = await ApiManager.ApiManagerInstance.GetJsonWeatherObjectByCoords(localPos.Latitude, localPos.Longitude);
                data.Text = $"You are currently at {localData.name}";
                // Use googles service to determine time taken from fabrikam foods
                Position fabrikamPos = new Position(-36.831928, 174.795976);
                Position localGoogle = new Position(localPos.Latitude, localPos.Longitude);
                ConstructRoute(localGoogle, fabrikamPos, localData.name);

            } catch (Exception ex) {
                await DisplayAlert("Failure:", $"Unable to retrieve location location, please turn on your GPS", "OK");
            }

            getLocation.IsEnabled = true;
            LoadIndicator.IsRunning = false;
        }

        private double GetDistanceBetweenCoords(Position p1, Position p2) {
            Func<double, double> deg2rad = x => x * (Math.PI / 180);
            double R = 6371; // Radius of the earth in km
            double dLat = deg2rad(p2.Latitude - p1.Latitude);  // deg2rad below
            double dLon = deg2rad(p2.Longitude- p1.Longitude);
            double a =
              Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
              Math.Cos(deg2rad(p1.Latitude)) * Math.Cos(deg2rad(p2.Latitude)) *
              Math.Sin(dLon / 2) * Math.Sin(dLon / 2)
              ;
            double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
            double d = R * c; // Distance in km
            return d;
        }

        private void ConstructRoute(Position local, Position destination, string addrLocal) {
            var pin = new Pin {
                Type = PinType.Place,
                Position = new Position(local.Latitude, local.Longitude), // Latitude, Longitude
                Label = "Your location",
                Address = addrLocal
            };
            var pin2 = new Pin {
                Type = PinType.Place,
                Position = new Position(destination.Latitude, destination.Longitude), // Latitude, Longitude
                Label = "Fabrikam Foods",
                Address = "2 Queens Parade, Auckland"
            };
            var slider = new Slider(1, 18, 1);
            slider.ValueChanged += (sender, e) => {
                var zoomLevel = e.NewValue; // between 1 and 18
                var latlongdegrees = 360 / (Math.Pow(2, zoomLevel));
                MyMap.MoveToRegion(new MapSpan(MyMap.VisibleRegion.Center, latlongdegrees, latlongdegrees));
            };
            MyMap.Pins.Add(pin);
            MyMap.Pins.Add(pin2);
            // find distance between pin1 pos and pin2 pos
            MyMap.MoveToRegion( MapSpan.FromCenterAndRadius( pin.Position, Distance.FromKilometers(GetDistanceBetweenCoords(local, destination))));
            //Content = new StackLayout { Padding = 30, Children = { MyMap, slider } };
        }

        private void GenerateMapData() {
            var map = new Map(
            MapSpan.FromCenterAndRadius(new Position(-37.05, 174.9), Distance.FromMiles(0.3))) {
                IsShowingUser = true,
                HeightRequest = 200,
                WidthRequest = 320,
                MapType = MapType.Street,
                VerticalOptions = LayoutOptions.Center
            };
            var slider = new Slider(1, 18, 1);
            slider.ValueChanged += (sender, e) => {
                var zoomLevel = e.NewValue; // between 1 and 18
                var latlongdegrees = 360 / (Math.Pow(2, zoomLevel));
                map.MoveToRegion(new MapSpan(map.VisibleRegion.Center, latlongdegrees, latlongdegrees));
            };
            var pin = new Pin {
                Type = PinType.Place,
                Position = new Position(-37.05, 174.9), // Latitude, Longitude
                Label = "Your location",
                Address = "custom detail info"
            };
            map.Pins.Add(pin);

            Content = new StackLayout { Padding = 30, Children = { map, slider } };
        }
    }
}
