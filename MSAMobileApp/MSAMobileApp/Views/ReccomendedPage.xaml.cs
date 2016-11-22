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
                var pos = await locator.GetPositionAsync(timeoutMilliseconds: 10000);
                data.Text = $"Latitude: {pos.Latitude}\nLongitude: {pos.Longitude}";
                // Feed data into API to load reccomendations
                // WeatherObject.RootObject localData = await ApiManager.ApiManagerInstance.GetJsonWeatherObjectByCity("auckland");
                WeatherObject.RootObject localData = await ApiManager.ApiManagerInstance.GetJsonWeatherObjectByCoords(pos.Latitude, pos.Longitude);
                data.Text += $"\nCurrent weather for {localData.name} is {localData.main.temp} degrees";
                LoadItems(localData.weather[0].icon, localData.main.temp);

            } catch (Exception ex) {
                await DisplayAlert("Failure:", $"Unable to retrieve location location, please turn on your GPS", "OK");
            }
            LoadIndicator.IsRunning = false;
        }

        public async void LoadItems(string icon, double temp) {
            LoadIndicator.IsRunning = true;

            List<Food> foodItems = await AzureManager.AzureManagerInstance.GetFoodItems();
            // If raining or temp<15 --> icon conditions: https://openweathermap.org/weather-conditions
            int type = (Int32.Parse(icon.Substring(0, 2)) > 4) || temp < 15 ? 1 : 2; // 1 = hot, 2=cold
            CreateLinkableCells(foodItems.Where(s => s.Type == type).ToList());
            // CreateLinkableCells(foodItems.Where(s => s.Type==type).ToList());

            LoadIndicator.IsRunning = false;
        }

        public void CreateLinkableCells(List<Food> FoodDB) {
            // Creating page header
            Label header = new Label {
                Text = "Reccomended based on location",
                TextColor = Color.Purple,
                FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label)),
                HorizontalOptions = LayoutOptions.Center
            };

            // Adding all food items to imagecells
            List<ImageCell> foodDBLinks = new List<ImageCell>();
            foreach (Food item in FoodDB) {
                ImageCell cellItem = new ImageCell {
                    // Some differences with loading images in initial release.
                    ImageSource = item.Photo,
                    Text = $"{item.Name} - ${item.Price}",
                    Detail = $"{item.Description}",
                };
                cellItem.Tapped += (sender, args) => MenuPage.ChangePage(
                    // Generate new page for item
                    GenerateItemPage(item)
                );
                foodDBLinks.Add(cellItem);
            }

            TableView tableView = new TableView {
                Intent = TableIntent.Form,
                Root = new TableRoot { new TableSection { foodDBLinks } }
            };
            // Build the page.
            Content = new StackLayout { Children = { header, tableView } };
        }

        private Page GenerateItemPage(Food item) {
            Label category = new Label {
                Text = $"{item.Category.ToUpper()}",
                TextColor = Color.Blue,
                FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
                HorizontalTextAlignment = TextAlignment.Center
            };
            Image image = new Image {
                Source = item.Photo,
                Aspect = Aspect.AspectFit
            };
            Label name = new Label {
                Text = $"{item.Name}",
                TextColor = Color.Blue,
                FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
                HorizontalTextAlignment = TextAlignment.Center
            };
            Label description = new Label {
                Text = $"{item.Description}",
                FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
                HorizontalTextAlignment = TextAlignment.Center
            };

            Label price = new Label {
                Text = $"Cost: ${ item.Price}",
                TextColor = Color.Red,
                FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label)),
                Margin = 20,
                HorizontalTextAlignment = TextAlignment.End
            };
            Button addToCart = new Button {
                Text = "ADD TO CART",
                BackgroundColor = Color.Green,
                Font = Font.SystemFontOfSize(NamedSize.Large),
                BorderWidth = 1,
                HorizontalOptions = LayoutOptions.Fill,
            };

            addToCart.Clicked += async (sender, e) => {
                if (User.CurrentUserInstance.Name != null) {
                    Food.CartInstance.Add(item);
                    await DisplayAlert("Success", $"Successfully added {item.Name} to cart", "OK");
                } else {
                    var res = await DisplayAlert("Failed", "Please login to place an order", "Login", "Cancel");
                    if (res) {
                        MenuPage.ChangePage(MenuPage.pages[3], 3);
                    }
                }
            };
            Button toCartBtn = new Button {
                Text = "View Cart",
                TextColor = Color.White,
                BackgroundColor = Color.Blue,
                Font = Font.SystemFontOfSize(NamedSize.Small),
                BorderWidth = 1,
                HorizontalOptions = LayoutOptions.Fill,
            };
            toCartBtn.Clicked += (sender, e) => {
                MenuPage.ChangePage(MenuPage.pages[2], 2);
            };
            Button backToMenu = new Button {
                Text = "BACK",
                BackgroundColor = Color.Red,
                Font = Font.SystemFontOfSize(NamedSize.Small),
                BorderWidth = 1,
                HorizontalOptions = LayoutOptions.End,
            };
            backToMenu.Clicked += (sender, e) => {
                MenuPage.ChangePage(MenuPage.pages[1]);
            };

            return new NavigationPage(new ContentPage {
                Content = new StackLayout {
                    Children = { category, image, name, description, price, addToCart, toCartBtn, backToMenu }
                }
            });
        }

    }
}
