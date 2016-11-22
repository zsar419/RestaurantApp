using MSAMobileApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace MSAMobileApp.Views {
    public partial class FoodsPage : ContentPage {
        public FoodsPage() {
            InitializeComponent();
            var onload = Task.Run(() => LoadItems());
        }

        private async void LoadItems() {
            LoadIndicator.IsRunning = true;

            List<Food> foodItems = await AzureManager.AzureManagerInstance.GetFoodItems();
            Page newPage = new FoodPageGenerator(foodItems);

            CreateLinkableCells(foodItems);
            //CreateLinkableCells(foodItems);
            LoadIndicator.IsRunning = false;
        }

        private void CreateLinkableCells(List<Food> FoodDB) {
            // Creating page header
            Label header = new Label {
                Text = "Fabrikam's Menu",
                TextColor = Color.Red,
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
                } else await DisplayAlert("Failed", "Please login to place an order", "OK");
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
                MenuPage.ChangePage(new NavigationPage(new OrderPage()), 2);
            };
            Button backToMenu = new Button {
                Text = "BACK",
                BackgroundColor = Color.Red,
                Font = Font.SystemFontOfSize(NamedSize.Small),
                BorderWidth = 1,
                HorizontalOptions = LayoutOptions.End,
            };
            backToMenu.Clicked += (sender, e) => {
                MenuPage.MenuPageInstance.Detail = MenuPage.pages[1];
            };

            return new NavigationPage(new ContentPage {
                Content = new StackLayout {
                    Children = { category, image, name, description, price, addToCart, toCartBtn, backToMenu }
                }
            });
        }

    }
}
